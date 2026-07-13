# AasxGoldenMasterHarness

A small, headless console tool for generating golden-master JSON snapshots
from `.aasx` sample files. It references only platform-independent projects
(`AasxCsharpLibrary`, `AasCore.Aas3_1`) so it builds and runs with a plain
`dotnet build`/`dotnet run` on any OS — no WPF, no Windows-only projects.

For each input `.aasx` file it:

1. **Parses** the package with `AdminShellPackageFileBasedEnv`.
2. **Validates** the resulting `IEnvironment` with `Aas.Verification.Verify`.
3. **Serializes** the `IEnvironment` to JSON with `Aas.Jsonization.Serialize`.

Each step is recorded independently (`parse`/`validation`/`serialization`,
each with `success`/`error`), so a failure in one step (including an
exception thrown by the generated verifier itself) does not prevent the
others from being captured, and does not abort the rest of the batch.
Errors are recorded as a stable exception type/message pair, not a stack
trace, so snapshots remain portable across machines.

## Usage

```powershell
cd src
dotnet run --project AasxGoldenMasterHarness -- --input <file-or-directory> --output <directory>
```

- `--input` / `-i` (required): a single `.aasx` file, or a directory that is
  scanned (non-recursively) for `*.aasx` files.
- `--output` / `-o` (optional, default `./golden-master-output`): directory
  that one `<basename>.json` snapshot is written to per input file.

Example, against the sample files already checked into this repo:

```powershell
dotnet run --project AasxGoldenMasterHarness -- --input BlazorUI --output /tmp/golden-master
```

The checked-in baseline snapshots are generated from the eight sample files in
`BlazorUI/` with:

```powershell
dotnet run --project AasxGoldenMasterHarness -- --input BlazorUI --output AasxGoldenMasterHarness/GoldenMasters/BlazorUI
```

## Output format

```jsonc
{
  "sourceFile": "example.aasx",
  "sourceFileSizeBytes": 12345,
  "sourceFileSha256": "…",
  "parse": { "success": true, "error": null, "diagnostics": [] },
  "validation": { "success": true, "error": null, "isValid": true, "errorCount": 0, "errors": [] },
  "serialization": { "success": true, "error": null },
  "summary": {
    "assetAdministrationShellCount": 1,
    "submodelCount": 4,
    "conceptDescriptionCount": 42
  },
  "environment": { /* full AAS Environment, as produced by Aas.Jsonization.Serialize */ }
}
```

When a step throws, `error` is an object such as
`{ "type": "System.NullReferenceException", "message": "..." }`.
`validation.success` means that validation completed; `validation.isValid`
states whether the completed validation found zero specification errors.
`parse.diagnostics` contains non-fatal notices emitted by the package loader.

`sourceFileSha256` lets a consumer detect whether a committed `.aasx` fixture
has drifted from the snapshot that was generated for it.

The tool exits `0` if every step succeeded for every file, `2` if any step
failed for any file, and `1` for a usage/argument error.

## Checked-in baseline

`GoldenMasters/BlazorUI/` contains reproducible JSON snapshots for every
`.aasx` fixture in `BlazorUI/`. They are byte-for-byte reproducible with the
command above.

All eight fixtures parse and serialize successfully. For the current baseline,
validation completes for `Phoenix_Contact_PT_4_-_CAD.aasx` (8,553 reported
validation errors) and `TimesSeries - 6 - admin-shell-io-2.aasx` (one reported
validation error). The other six fixtures encounter the generated-verifier
exception described below, so the batch intentionally exits with status `2`.

## Known finding

Several of the `.aasx` sample files under `BlazorUI/` contain a
`ConceptDescription.EmbeddedDataSpecification` with a `null`
`DataSpecification` reference. The generated `Verification.Verify` in
`AasCore.Aas3_1/verification.cs` does not null-check before recursing into
that field and throws a `NullReferenceException` instead of returning a
validation error. This harness catches that exception per-file (see
`validation.error` in the output) rather than treating it as a fatal error;
the underlying generated code was intentionally left untouched.
