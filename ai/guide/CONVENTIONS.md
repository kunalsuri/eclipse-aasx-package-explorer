<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# Conventions — how to write code that fits eclipse-aasx-package-explorer

> Status: drafted by /cold-start on 2026-07-14 @ commit `c8f3ea37`, from the prior
> `AGENTS.md`/`CLAUDE.md` config backups, `src/.editorconfig`, and
> `.agents/ci-and-style.md`. `[inferred]` until a human confirms.

## Languages & style  `[inferred]`
- C# (.NET), WPF for the desktop front end, Blazor for the web front end.
  PowerShell (`pwsh`) for all build/test/CI-equivalent scripts, run from `src/`.
- `src/.editorconfig` (verified): 4-space indent, CRLF line endings, `var`
  preferred when the type is apparent.
- Formatter: `dotnet format` — `src/CheckFormat.ps1` verifies no diff,
  `src/FormatCode.ps1` applies it. Linter: JetBrains ReSharper via
  `src/InspectCode.ps1` (`dotnet jb inspectcode`). Local dotnet tools (`dead-csharp`,
  `bite-sized`, `doctest-csharp`, `opinionated-csharp-todos`,
  `jetbrains.resharper.globaltools`) are declared in `src/.config/dotnet-tools.json`
  — run `dotnet tool restore` or `src/InstallToolsForStyle.ps1` first.

## Patterns to follow  `[inferred]`
- **New plugin:** copy an existing simple one as a template —
  `AasxPluginTechnicalData` is small and self-contained (see
  `.agents/plugins.md` for the full recipe). Implement `IAasxPluginInterface`,
  add a `<YourPlugin>.plugin` marker file, add the `.csproj` to
  `src/AasxPackageExplorer.sln` (and `src/filtered.slnf` if it belongs in the fast
  build filter).
- **UI-producing logic that should work in both front ends:** extend the `AnyUi`
  abstraction in `AasxPackageLogic` (e.g. `DispEditHelper*.cs`) rather than writing
  directly against `System.Windows` or Blazor components — see
  `ai/guide/ARCHITECTURE.md`.
- **License header** — new `.cs`/`.xaml` files need the standard header at the top
  (match the existing author/year convention of the surrounding file instead of
  inventing new ones):
  ```csharp
  /*
  Copyright (c) 2018-2023 Festo SE & Co. KG <https://www.festo.com/net/de_de/Forms/web/contact_international>
  Author: <name>

  This source code is licensed under the Apache License 2.0 (see LICENSE.txt).

  This source code may use other Open Source software components (see LICENSE.txt).
  */
  ```
  Generated/vendored trees and a handful of named files are excluded from the
  header/format/dead-code/TODO checks — the full exclude lists are in
  `.agents/ci-and-style.md`; don't assume a file must comply without checking there.
- **TODO format:** `// TODO (author, YYYY-MM-DD): description`, e.g.
  `// TODO (MIHO, 2024-06-02): remove, if not anymore required?`
- **Commit/PR style:** imperative mood ("Add X", "Fix Y"), first line ≤ 72 chars,
  no trailing period — enforced by `check-commit-messages.yml`
  (`mristin/opinionated-commit-message`); extra accepted verbs in
  `src/AdditionalVerbsInImperativeMood.txt`. PRs are squash-merged.
- **Scope:** keep changes to one concern per PR — `CheckBiteSized.ps1` (max line
  length 240 chars) and the overall project culture favor small, reviewable diffs.

## Things that look wrong but are right  `[verified] required`
<Only humans add rows. The institutional knowledge that prevents "helpful" breakage.>

## Definition of done
- Builds: `cd src && ./BuildForDebug.ps1` (PowerShell; WPF app + several plugins
  are Windows-only, built `--runtime win-x64` — say so explicitly if verification
  couldn't run on Windows rather than claiming a full build succeeded).
- Tests pass: `cd src && ./DownloadSamples.ps1` (once, fetches sample `.aasx`
  fixtures) `&& ./BuildForDebug.ps1 && ./Test.ps1` (NUnit via
  OpenCover/`nunit3-console`; `Test.ps1 -Test "<prefix>"` to scope to matching
  tests). Only `src/AasxDictionaryImport.Tests/` was found as a dedicated NUnit
  project at cold-start time — `[inferred]`, re-check if more exist.
- Style gates mirroring `check-style.yml` should also pass for touched files:
  `CheckHeaders.ps1`, `CheckFormat.ps1`, `CheckBiteSized.ps1`, `CheckDeadCode.ps1`,
  `CheckTodos.ps1`, `CheckLicenses.ps1` (all run from `src/`).
- License headers match neighbors; diffs are surgical; `ai/` knowledge updated if
  the change moved or added modules/features.
