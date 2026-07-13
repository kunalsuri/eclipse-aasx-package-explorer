# CLAUDE.md — AI Agent Guide

This file orients AI coding agents (Claude Code and similar) working in the
Eclipse AASX Package Explorer repository. It covers what the project is,
how the source is organized, how to build/test/lint it, and the
conventions CI enforces — so agent-authored changes pass review the first
time.

## What this project is

Eclipse AASX Package Explorer™ is a C#/.NET desktop application (WPF) for
viewing and editing Asset Administration Shells (AAS), the digital-twin
data format defined by IDTA/Plattform Industrie 4.0. There is also a
Blazor-based web UI (`BlazorExplorer`, `BlazorUI`) sharing logic with the
desktop app. Functionality is extended via a plugin architecture
(`AasxPlugin*` projects).

The project is an Eclipse Foundation incubated project; real code
contributions from human contributors require a signed Eclipse
Contributor Agreement (see `CONTRIBUTING.md`). That process is orthogonal
to agent-assisted development in this repo — it doesn't block you from
building, testing, or drafting changes.

## Repository layout

All source lives under `src/`, as one big Visual Studio solution
(`src/AasxPackageExplorer.sln`, ~60 projects). Key projects:

- `AasCore.Aas3_1` — generated AAS v3.1 metamodel (data classes), the
  core domain model. Treat as mostly generated/vendored code.
- `AasxCsharpLibrary` — core library for reading/writing `.aasx` packages.
- `AasxPackageLogic` — shared UI-agnostic application logic used by both
  the WPF and Blazor front ends (dialog helpers, menu/command handling,
  scripting, main-window logic).
- `AasxPackageExplorer` — the WPF desktop application (entry point,
  `MainWindow.xaml`, menu bindings).
- `BlazorExplorer` / `BlazorUI` — the Blazor-based web UI.
- `AasxPlugin*` — optional plugins (technical data, document shelf, BOM
  structure, plotting, MTP viewer, UA Net client/server, etc.), each a
  self-contained project implementing a common plugin interface. Use an
  existing plugin (e.g. `AasxPluginTechnicalData`) as a template for a new
  one; see `src/AasxPackageExplorer/plugins/README.md`.
- `AasxPredefinedConcepts`, `AasxFormatCst`, `AasxDictionaryImport`,
  `AasxAmlImExport`, `AasxBammRdfImExport`, `AasxMqtt*` — format/import
  and predefined-concept support libraries.
- `*.Tests` / `*.GuiTests` projects — NUnit test suites next to the code
  they cover.
- `docdev/docfx_project` — DocFX developer documentation source
  (published via the `generate-doc` CI workflow).

`src/filtered.slnf` is a solution filter excluding some heavier/optional
projects — useful for faster local builds when you don't need everything.

## Platform constraints

The desktop app (WPF) and several plugins (OPC UA, GDI/Windows interop)
are **Windows-only** and build with `--runtime win-x64`. CI runs on
`windows-latest`. If you're working in a non-Windows agent sandbox, you
generally cannot build or run the WPF app or `win-x64` plugins end to
end — you can still read/edit code, and you can build/test
platform-independent projects (e.g. `AasxCsharpLibrary`,
`AasxPackageLogic`, format libraries) with a plain `dotnet build`/`dotnet
test` if a Windows CI/agent isn't available. Say so explicitly rather
than claiming a full build/run succeeded if you couldn't actually verify
it on this platform.

## Build

All scripts below are PowerShell (`pwsh`), run from `src/`:

```powershell
cd src
./InstallSolutionDependencies.ps1      # restore/prep solution deps
./BuildForDebug.ps1                    # dotnet publish, Debug, win-x64
./BuildForDebug.ps1 -clean             # clean instead of build
./BuildForDebug.ps1 -project <path>    # build a single project
```

`BuildForRelease.ps1` / `PackageRelease.ps1` are for release packaging —
not needed for normal development changes.

## Tests

NUnit tests run via OpenCover/`nunit3-console`, wired up in `Test.ps1`:

```powershell
cd src
./InstallToolsForBuildTestInspect.ps1   # one-time: NUnit console, OpenCover, ReportGenerator
./DownloadSamples.ps1                   # fetch sample .aasx files (needed for integration tests)
./BuildForDebug.ps1
./Test.ps1                              # run all tests
./Test.ps1 -Explore                     # list test names, don't run
./Test.ps1 -Test "AasxDictionaryImport" # run tests matching a prefix
```

Test DLLs are discovered by globbing `*.Tests.dll` under the Debug build
output, so a build must precede `Test.ps1`.

## Style, lint, and other CI gates

CI runs two workflows agents should keep green: `build-test-inspect.yml`
and `check-style.yml`. Locally, from `src/`:

```powershell
./InstallToolsForStyle.ps1
./CheckLicenses.ps1      # third-party license compliance
./CheckHeaders.ps1       # every .cs/.xaml needs the standard copyright header (see below)
./CheckFormat.ps1        # `dotnet format` must produce no diff
./CheckBiteSized.ps1     # max line length 240 chars (no hard file-size cap currently)
./CheckDeadCode.ps1      # no commented-out code (dead-csharp)
./CheckTodos.ps1         # TODOs must follow the `// TODO (name, YYYY-MM-DD): ...` format
```

`FormatCode.ps1` applies `dotnet format` in place if `CheckFormat.ps1`
fails.

**License header** — new `.cs`/`.xaml` files need this at the top (match
existing author/year conventions in the surrounding project instead of
inventing new ones):

```csharp
/*
Copyright (c) 2018-2023 Festo SE & Co. KG <https://www.festo.com/net/de_de/Forms/web/contact_international>
Author: <name>

This source code is licensed under the Apache License 2.0 (see LICENSE.txt).

This source code may use other Open Source software components (see LICENSE.txt).
*/
```

Generated code, vendored third-party sources, and a handful of named
projects are excluded from these checks — see the `--excludes` lists
inside `CheckHeaders.ps1`, `CheckBiteSized.ps1`, `CheckDeadCode.ps1`
before assuming a file must comply.

**TODO format** — `// TODO (author, YYYY-MM-DD): description`, e.g.:

```csharp
// TODO (MIHO, 2024-06-02): remove, if not anymore required?
```

**Editor conventions** — see `src/.editorconfig`: 4-space indent, CRLF
line endings, `var` preferred when the type is apparent.

## Commit message / PR conventions

CI (`check-commit-messages.yml`) enforces the
[opinionated-commit-message](https://github.com/mristin/opinionated-commit-message)
style on PR titles/commits:

- Imperative mood ("Add X", "Fix Y", not "Added"/"Fixes").
- First line ≤ 72 characters, no trailing period.
- Additional accepted domain verbs are listed in
  `src/AdditionalVerbsInImperativeMood.txt` (e.g. "export", "dispatch",
  "enhance", "unit test").

PRs are squash-merged, so per-commit message hygiene matters less than
getting the PR title/description right.

## Working conventions for agents

- Prefer editing an existing, similar file (e.g. another plugin, another
  `DispEditHelper*.cs`) as a template — the codebase has strong existing
  patterns per area; don't introduce a new pattern where one already
  exists.
- Keep changes scoped to one concern per PR — `CheckBiteSized.ps1` and the
  overall project culture favor small, reviewable diffs over sweeping
  refactors.
- Don't touch files under vendored/generated trees
  (`AasCore.Aas3_1`, `es6numberserializer`, `jsoncanonicalizer`,
  `AasxFileServerRestLibrary`, `*_bkp`) unless the task specifically
  targets them.
- When a change affects public developer docs, DocFX sources live under
  `docdev/docfx_project` (built via `GenerateDocdev.ps1` /
  `generate-doc.yml`).
