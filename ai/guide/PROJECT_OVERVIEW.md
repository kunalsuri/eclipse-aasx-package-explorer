<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# Project overview — eclipse-aasx-package-explorer

> Status: drafted by /cold-start on 2026-07-14 @ commit `c8f3ea37`; every section
> `[inferred]` until audited.

## What this is
> [!WARNING] This is a FORK of **eclipse-aaspe/package-explorer** (upstream,
> confirmed via `git remote -v`: `upstream` → `https://github.com/eclipse-aaspe/package-explorer.git`).

Eclipse AASX Package Explorer™ is a C#/.NET desktop application (WPF) for viewing
and editing Asset Administration Shells (AAS), the digital-twin data format
defined by IDTA/Plattform Industrie 4.0. There is also a Blazor-based web UI
(`BlazorExplorer`, `BlazorUI`) sharing logic with the desktop app. Functionality
is extended via a plugin architecture (`AasxPlugin*` projects, 17 found), and the
app embeds a scripting engine (S#) for automation. `[inferred — from prior config]`

## Stack (from `ai/repo-profile.json` + manual verification)
- Languages: C# (.NET, WPF, Blazor); PowerShell for build/test/CI scripts.
- Build: `cd src && ./BuildForDebug.ps1` (verified: script exists at
  `src/BuildForDebug.ps1`).
- Test: `cd src && ./DownloadSamples.ps1 && ./BuildForDebug.ps1 && ./Test.ps1`
  (verified: scripts exist at `src/DownloadSamples.ps1`, `src/Test.ps1`; NUnit
  test DLLs are globbed from the Debug build output, so a build must precede
  `Test.ps1`).
- `ai/repo-profile.json`'s `buildCmd`/`testCmd` fields were left as `<fill in>` by
  the deterministic `orient` step (no build-system marker file like
  `.csproj`-adjacent `package.json` was auto-detected) — the commands above are
  the /cold-start pass's manually-verified correction, per the sanctioned
  exception in `CLAUDE.md`/`AGENTS.md`'s No-Churn rule.

## Why it exists  `[inferred]`
A viewer/editor for `.aasx` packages — the OPC/ZIP container format bundling one
or more Asset Administration Shell environments plus supplementary files. The
project is an Eclipse Foundation incubated project and the successor to the now
archived `admin-shell-io/aasx-package-explorer` (per `README.md`). Real code
contributions from external human contributors require a signed Eclipse
Contributor Agreement, but that process is orthogonal to agent-assisted
development in this fork.

## What we add vs. what we inherit  `[inferred]`
Essentially all of `src/` (~60 projects, one solution
`src/AasxPackageExplorer.sln`) is inherited from upstream
`eclipse-aaspe/package-explorer` — treat it as `frozen` per `MODULE_MAP.md` unless
a task explicitly requires touching it. This fork's own additions, confirmed via
git history and absent from the prior config's module list, are:
- `src/AasxGoldenMasterHarness/` — a headless console tool for golden-master
  regression testing of AAS parse/serialize output, added via PR #1/#2 and fixed
  via PR #3 (commits `c4258003`, `0e4b62e1`, `a73ae0c1`).
- `ai/`, `.agents/`, `.claude/`, `.codex/`, `.cursor/`, and the root
  `CLAUDE.md`/`AGENTS.md` — the multi-tool AI coding-agent knowledge layer
  (commits `5ce03ff7`, `5f91f65f`, `7ba6c61a`, `c8f3ea37`).

## Glossary  `[inferred]`
| Term | Meaning here |
|---|---|
| AAS | Asset Administration Shell — the IDTA/Plattform Industrie 4.0 digital-twin data format |
| `.aasx` | OPC/ZIP container package format bundling one or more AAS environments plus supplementary files |
| Submodel | A structured group of AAS data (e.g. Digital Nameplate, Technical Data, BOM) |
| AnyUi | The platform-agnostic "UI as data" abstraction shared by the WPF and Blazor front ends |
| Golden master | A reference/fixture output compared against actual output for regression testing (`AasxGoldenMasterHarness`) |
| Plugin | A dynamically-loaded `IAasxPluginInterface` assembly, usually implementing one IDTA submodel template's UI |
