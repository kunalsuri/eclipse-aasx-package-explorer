# CI and style checks — full detail

Background for `AGENTS.md`'s style/lint section. All scripts here run
from `src/` and mirror `.github/workflows/check-style.yml` and
`build-test-inspect.yml`.

## check-style.yml, in order

1. **`CheckLicenses.ps1`** — verifies third-party license compliance
   across dependencies.
2. **`CheckHeaders.ps1`** — every `.cs`/`.xaml` file must start with the
   standard copyright/license header (see `AGENTS.md` for the exact
   text). Runs `dotnet run --project CheckHeadersScript` over
   `**\*.cs`/`**\*.xaml`. Excluded (do not add headers to, and don't flag
   as violations): `packages/**`, `**/obj/**`, `**/bin/**`,
   `**/AssemblyInfo.cs`, `**/Properties/*.Designer.cs`,
   `**/DocTest*.cs`, `*.Tests/**/*.cs`, `*.GuiTests/**/*.cs`,
   `AasxOpenidClient/*.cs`, several named files under
   `AasxUaNetServer`/`AasxPluginUaNetClient`, `MsaglWpfControl/*`,
   `CheckHeadersScript/*.cs`, `CheckScript/*.cs`, a handful of vendored
   XAML resources (`AasxIntegrationBaseWpf/OriginalResources/*.xaml`,
   specific files under `WpfMtpControl/Resources`,
   `WpfXamlTool/Resources`), and whole vendored/generated trees:
   `AasxFileServerRestLibrary/**`, `es6numberserializer/**`,
   `jsoncanonicalizer/**`, `AasCore.Aas3_0/**`, `AasxCsharpLib_bkp/**`,
   `AasxServer.DomainModelV3_0_RC02/**`.
3. **`CheckFormat.ps1`** — runs `dotnet format AasxPackageExplorer.sln
   --verify-no-changes` (or `--check` on older `dotnet format`), excludes
   `**/DocTest*.cs`. Any diff fails the check. Fix with `FormatCode.ps1`.
4. **`CheckBiteSized.ps1`** — runs `dotnet bite-sized` with
   `--max-line-length 240` (no effective file-size cap:
   `--max-lines-in-file 100000`). Excludes generated/Designer files and
   a few named files with long hard-wired strings
   (`AasxToolkit/Generate.cs`, `AasxPluginExportTable/ExportTableFlyout.xaml.cs`,
   `MsaglWpfControl/GraphViewer.cs`, `MsaglWpfControl/VEdge.cs`,
   `MsaglWpfControl/VNode.cs`) plus the vendored trees
   (`AasxFileServerRestLibrary/**`, `AasxCsharpLib_bkp/**`,
   `AasxServer.DomainModelV3_0_RC02/**`). URLs are ignored via
   `--ignore-lines-matching '[a-z]+://[^ \t]+$'`.
5. **`CheckDeadCode.ps1`** — runs `dotnet dead-csharp` to flag
   commented-out code. Excludes `**/obj/**`, `packages/**`,
   `**/Properties/**`, and the same vendored trees as above plus
   `AasCore.Aas3_0/**`.
6. **`CheckTodos.ps1`** — runs `dotnet opinionated-csharp-todos`,
   requiring the format `// TODO (author, YYYY-MM-DD): description`.
   Excludes `packages/**`, `**/obj/**`, `MsaglWpfControl/**`,
   `AasxCsharpLib_bkp/**`.

All the `dotnet <tool>` invocations above come from local tools declared
in `src/.config/dotnet-tools.json` (`dead-csharp`, `bite-sized`,
`doctest-csharp`, `opinionated-csharp-todos`, plus
`jetbrains.resharper.globaltools` for `InspectCode.ps1`) — run
`dotnet tool restore` (or `InstallToolsForStyle.ps1` /
`InstallToolsForBuildTestInspect.ps1`) before invoking them directly.

## build-test-inspect.yml

- **`Doctest.ps1 -check`** — verifies "doctest" code samples embedded in
  doc comments actually compile/match, via `dotnet doctest-csharp`.
- **`InspectCode.ps1`** — runs JetBrains ReSharper
  (`dotnet jb inspectcode`) over the whole solution, excluding
  `*\obj\*`, `packages\*`, `*\bin\*`, `*\*.json`, and the vendored trees
  (`AasxFileServerRestLibrary`, `es6numberserializer`,
  `jsoncanonicalizer`). Output goes to
  `artefacts/resharper-code-inspection.xml`, uploaded as a CI artifact.
- The build and test steps themselves (`BuildForDebug.ps1`, `Test.ps1`,
  Coveralls upload) are currently commented out in the workflow file —
  don't assume CI is running them even though the scripts exist and work
  locally.

## check-commit-messages.yml

Runs `mristin/opinionated-commit-message` against the PR title/commit
messages, with `src/AdditionalVerbsInImperativeMood.txt` as the extra
accepted-verbs list. See `AGENTS.md`'s commit-message section for the
practical rules this implies.
