/*
Copyright (c) 2026 Kunal Suri
Author: Kunal Suri

This source code is licensed under the Apache License 2.0 (see LICENSE.txt).

This source code may use other Open Source software components (see LICENSE.txt).
*/

using AdminShellNS;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Nodes;
using Aas = AasCore.Aas3_1;

namespace AasxGoldenMasterHarness
{
    /// <summary>
    /// Headless golden-master harness: loads each sample .aasx file with
    /// AasxCsharpLibrary/AasCore.Aas3_1, runs parse/validate/serialize, and
    /// writes one JSON snapshot per input file. The snapshots are meant to be
    /// committed as golden output that other implementations (e.g. a
    /// TypeScript AASX parser) can diff their own output against.
    /// </summary>
    public static class Program
    {
        // Based on JsonSerializerOptions.Default rather than a bare `new()` so it carries
        // a TypeInfoResolver: JsonArray.Add(string) resolves to the generic Add<T>
        // overload, which wraps the value in a JsonValueCustomized<T> that resolves its
        // type info lazily from the options passed to ToJsonString - and throws
        // InvalidOperationException at write time if that options has no resolver.
        private static readonly JsonSerializerOptions OutputJsonOptions = new JsonSerializerOptions(JsonSerializerOptions.Default)
        {
            WriteIndented = true
        };

        public static int Main(string[] args)
        {
            string inputPath = null;
            string outputDir = null;

            for (var i = 0; i < args.Length; i++)
            {
                switch (args[i])
                {
                    case "--input":
                    case "-i":
                        if (!TryGetOptionValue(args, ref i, out inputPath))
                        {
                            return 1;
                        }

                        break;
                    case "--output":
                    case "-o":
                        if (!TryGetOptionValue(args, ref i, out outputDir))
                        {
                            return 1;
                        }

                        break;
                    case "--help":
                    case "-h":
                        PrintUsage();
                        return 0;
                    default:
                        Console.Error.WriteLine($"Unknown argument: {args[i]}");
                        PrintUsage();
                        return 1;
                }
            }

            if (string.IsNullOrWhiteSpace(inputPath))
            {
                Console.Error.WriteLine("Missing required --input <file-or-directory>.");
                PrintUsage();
                return 1;
            }

            outputDir = string.IsNullOrWhiteSpace(outputDir) ? "golden-master-output" : outputDir;
            try
            {
                Directory.CreateDirectory(outputDir);
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Could not create output directory '{outputDir}': {ex.Message}");
                return 1;
            }

            var files = ResolveInputFiles(inputPath);
            if (files.Count == 0)
            {
                Console.Error.WriteLine($"No .aasx files found at '{inputPath}'.");
                return 1;
            }

            var failures = 0;
            foreach (var file in files)
            {
                Console.WriteLine($"Processing {file} ...");

                JsonObject result;
                try
                {
                    result = ProcessFile(file);
                }
                catch (Exception ex)
                {
                    // Defense in depth: no single sample should be able to abort the whole
                    // batch, even if a step throws somewhere we did not anticipate.
                    Console.Error.WriteLine($"  UNEXPECTED ERROR processing {file}: {ex.Message}");
                    result = new JsonObject
                    {
                        ["sourceFile"] = Path.GetFileName(file),
                        ["parse"] = new JsonObject
                        {
                            ["success"] = false,
                            ["error"] = DescribeException(ex),
                            ["diagnostics"] = null
                        },
                        ["validation"] = null,
                        ["serialization"] = null,
                        ["summary"] = null,
                        ["environment"] = null
                    };
                }

                var outFile = Path.Combine(
                    outputDir, Path.GetFileNameWithoutExtension(file) + ".json");
                try
                {
                    File.WriteAllText(outFile, result.ToJsonString(OutputJsonOptions));
                }
                catch (Exception ex)
                {
                    failures++;
                    Console.Error.WriteLine($"  Could not write '{outFile}': {ex.Message}");
                    continue;
                }

                var parseOk = (bool)((JsonObject)result["parse"])["success"];
                var validationOk = result["validation"] == null
                    || (bool)((JsonObject)result["validation"])["success"];
                var serializationOk = result["serialization"] == null
                    || (bool)((JsonObject)result["serialization"])["success"];

                if (!parseOk || !validationOk || !serializationOk)
                {
                    failures++;
                    Console.Error.WriteLine($"  FAILED (parse={parseOk}, validate={validationOk}, serialize={serializationOk}): {file}");
                }
            }

            Console.WriteLine(
                $"Processed {files.Count} file(s), {failures} failure(s). " +
                $"Output written to '{outputDir}'.");

            return failures == 0 ? 0 : 2;
        }

        private static bool TryGetOptionValue(string[] args, ref int index, out string value)
        {
            if (index + 1 < args.Length)
            {
                value = args[++index];
                return true;
            }

            Console.Error.WriteLine($"Missing value for {args[index]}.");
            PrintUsage();
            value = null;
            return false;
        }

        private static List<string> ResolveInputFiles(string inputPath)
        {
            if (File.Exists(inputPath))
            {
                return new List<string> { inputPath };
            }

            if (Directory.Exists(inputPath))
            {
                return Directory.GetFiles(inputPath, "*.*")
                    .Where(f => f.EndsWith(".aasx", StringComparison.OrdinalIgnoreCase))
                    .OrderBy(f => f, StringComparer.OrdinalIgnoreCase)
                    .ToList();
            }

            return new List<string>();
        }

        private static JsonObject ProcessFile(string file)
        {
            var result = new JsonObject
            {
                ["sourceFile"] = Path.GetFileName(file),
                ["sourceFileSizeBytes"] = new FileInfo(file).Length,
                ["sourceFileSha256"] = ComputeSha256(file)
            };

            AdminShellPackageFileBasedEnv pkg = null;
            var originalConsoleOutput = Console.Out;
            using var parserDiagnostics = new StringWriter();
            try
            {
                // indirectLoadSave: true copies the .aasx to a temp file before opening it.
                // Package.Open() opens with ReadWrite access and can rewrite the zip
                // structure on Close()/Dispose() even without an explicit save, so loading
                // the source file directly would silently mutate committed fixtures on disk.
                Console.SetOut(parserDiagnostics);
                pkg = new AdminShellPackageFileBasedEnv(file, indirectLoadSave: true);
            }
            catch (Exception ex)
            {
                result["parse"] = new JsonObject
                {
                    ["success"] = false,
                    ["error"] = DescribeException(ex),
                    ["diagnostics"] = ToDiagnosticArray(parserDiagnostics.ToString())
                };
                result["validation"] = null;
                result["serialization"] = null;
                result["summary"] = null;
                result["environment"] = null;
                return result;
            }
            finally
            {
                Console.SetOut(originalConsoleOutput);
            }

            try
            {
                result["parse"] = new JsonObject
                {
                    ["success"] = true,
                    ["error"] = null,
                    ["diagnostics"] = ToDiagnosticArray(parserDiagnostics.ToString())
                };

                var env = pkg.AasEnv;

                // Each step is independently fallible: a bug triggered by one sample's data
                // (e.g. a NullReferenceException deep in the generated verifier) must not
                // prevent us from recording the other steps' results for this sample, nor
                // abort the rest of the batch.
                result["validation"] = RunValidation(env);
                result["summary"] = new JsonObject
                {
                    ["assetAdministrationShellCount"] = env.AssetAdministrationShells?.Count ?? 0,
                    ["submodelCount"] = env.Submodels?.Count ?? 0,
                    ["conceptDescriptionCount"] = env.ConceptDescriptions?.Count ?? 0
                };
                result["serialization"] = RunSerialization(env, out var environmentJson);
                result["environment"] = environmentJson;
            }
            finally
            {
                pkg.Dispose();
            }

            return result;
        }

        private static JsonObject RunValidation(Aas.IEnvironment env)
        {
            try
            {
                var errors = Aas.Verification.Verify(env).ToList();
                var errorsArray = new JsonArray();
                foreach (var error in errors)
                {
                    errorsArray.Add(new JsonObject
                    {
                        ["cause"] = error.Cause,
                        ["path"] = Aas.Reporting.GenerateJsonPath(error.PathSegments)
                    });
                }

                return new JsonObject
                {
                    ["success"] = true,
                    ["error"] = null,
                    ["isValid"] = errors.Count == 0,
                    ["errorCount"] = errors.Count,
                    ["errors"] = errorsArray
                };
            }
            catch (Exception ex)
            {
                return new JsonObject
                {
                    ["success"] = false,
                    ["error"] = DescribeException(ex),
                    ["isValid"] = null,
                    ["errorCount"] = null,
                    ["errors"] = null
                };
            }
        }

        private static JsonObject RunSerialization(Aas.IEnvironment env, out JsonObject environmentJson)
        {
            try
            {
                environmentJson = Aas.Jsonization.Serialize.ToJsonObject(env);
                return new JsonObject { ["success"] = true, ["error"] = null };
            }
            catch (Exception ex)
            {
                environmentJson = null;
                return new JsonObject { ["success"] = false, ["error"] = DescribeException(ex) };
            }
        }

        private static JsonObject DescribeException(Exception exception)
        {
            return new JsonObject
            {
                ["type"] = exception.GetType().FullName,
                ["message"] = exception.Message
            };
        }

        private static JsonArray ToDiagnosticArray(string diagnostics)
        {
            var lines = diagnostics.Split(
                new[] { "\r\n", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            var result = new JsonArray();
            foreach (var line in lines)
            {
                result.Add(line);
            }

            return result;
        }

        private static string ComputeSha256(string file)
        {
            using var sha = SHA256.Create();
            using var stream = File.OpenRead(file);
            var hash = sha.ComputeHash(stream);
            return Convert.ToHexString(hash).ToLowerInvariant();
        }

        private static void PrintUsage()
        {
            Console.WriteLine(
                "Usage: AasxGoldenMasterHarness --input <file-or-directory> [--output <directory>]\n\n" +
                "Loads each .aasx file, runs parse/validate/serialize via AasxCsharpLibrary and\n" +
                "AasCore.Aas3_1, and writes one golden-master JSON snapshot per input file to the\n" +
                "output directory (default: ./golden-master-output).");
        }
    }
}
