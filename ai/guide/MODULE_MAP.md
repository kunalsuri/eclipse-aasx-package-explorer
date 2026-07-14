<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# Module map — directory → responsibility → entry point

> **Index only.** Find the area here, then open the entry file directly. Don't crawl
> the tree. The directory list can be regenerated; **Responsibility** and **Stability**
> are judgement and must be audited by a human.
> Last drafted: 2026-07-14 @ commit `c8f3ea37` (all rows `[inferred]`)

## Stability legend (the most important column)
- `frozen` — inherited / load-bearing legacy, **or generated/vendored code**: never
  hand-edit; if it has a regeneration command, name it in the Responsibility cell
  and change the module only by re-running it. **DO NOT edit** without explicit
  instruction.
- `stable` — works; change carefully and with tests.
- `ours`   — active development surface. Safe for agents to modify.
- `?`      — not yet audited. **Treat as `frozen` until a human decides.**

## Fork boundary (read this first)

This repo is a fork of `eclipse-aaspe/package-explorer` (upstream remote confirmed
via `git remote -v`). Essentially **all of `src/`** is inherited from upstream and
is stability `frozen` per `CLAUDE.md`/`AGENTS.md`'s hard rule — don't hand-edit it
unless a task explicitly requires it. The two areas that are genuinely **this
fork's own work** (`ours`) are:
- `src/AasxGoldenMasterHarness/` — a new console tool added in this fork (commits
  `c4258003`, `0e4b62e1`, PR #1–#3), not present as a described module in the prior
  `AGENTS.md`/`CLAUDE.md` backups.
- `ai/`, `.agents/`, `.claude/`, `.codex/`, `.cursor/`, and the root
  `CLAUDE.md`/`AGENTS.md` — the multi-tool AI agent knowledge layer, added by this
  fork's owner (commits `5ce03ff7`, `5f91f65f`, `7ba6c61a`, `c8f3ea37`).

`[inferred]` — this split is a first-pass read of git history and file presence,
not a line-by-line diff against upstream. A human should confirm no other files
have diverged from upstream before trusting `frozen` as "identical to upstream."

## Modules

| Directory | Responsibility (one line) | Entry point | Stability (guess) | Status |
|---|---|---|---|---|
| `src/AasCore.Aas3_1/` | Generated AAS v3.1 metamodel (`Aas.IReferable`, `Aas.AssetAdministrationShell`, `Aas.Submodel`, etc.), conventionally imported as `using Aas = AasCore.Aas3_1;` | (generated; no single entry point) | frozen | [inferred] |
| `src/AasxCsharpLibrary/` | Reads/writes `.aasx` package files (OPC/ZIP container); in-memory environment `AdminShellPackageEnv` | `AdminShellPackageEnv.cs` | frozen | [inferred] |
| `src/AasxPredefinedConcepts/` | Predefined submodel templates / concept descriptions (e.g. ZVEI Digital Nameplate) for recognition and validation | — | frozen | [inferred] |
| `src/AasxPackageLogic/` | UI-agnostic app logic shared by WPF and Blazor: dialog helpers (`DispEditHelper*.cs`), menu/command handling, scripting bridge (`AasxScript.cs`), main-window logic (`MainWindowLogic.cs`, `MainWindowTools.cs`, `MainWindowHeadless.cs`) | `MainWindowLogic.cs` | frozen | [inferred] |
| `src/AnyUi/` | Platform-agnostic "UI as data" abstraction; app code builds UI descriptions here instead of against WPF/Blazor APIs directly | `AnyUiBase.cs` | frozen | [inferred] |
| `src/AasxIntegrationBase/` | Plugin interface (`IAasxPluginInterface`) and shared integration base types | `AasxPluginInterface.cs` | frozen | [inferred] |
| `src/AasxIntegrationBaseWpf/` | WPF renderer for the `AnyUi` abstraction | — | frozen | [inferred] |
| `src/AasxIntegrationBaseGdi/` | GDI-based integration helpers (Windows-only) | — | frozen | [inferred] |
| `src/AasxPackageExplorer/` | WPF desktop app — entry point, `MainWindow.xaml`, menu bindings, plugin loading (`plugins/README.md`) | `MainWindow.xaml` | frozen | [inferred] |
| `src/BlazorExplorer/` | Blazor-based web UI application | — | frozen | [inferred] |
| `src/BlazorUI/` | Blazor UI components; renders `AnyUi` descriptions via `AnyUiHtml.cs` | `AnyUiHtml.cs` | frozen | [inferred] |
| `src/AasxPlugin*/` (17 projects) | Dynamically-loaded optional extensions, mostly one per IDTA submodel template UI (technical data, digital nameplate, document shelf, BOM structure, contact info, plotting, MTP viewer, OPC UA client/server, etc.) — full catalog in `.agents/plugins.md` | each implements `IAasxPluginInterface` | frozen | [inferred] |
| `src/AasxToolkit/` | CLI for `.aasx` generation/extraction/validation | `Cli.cs` | frozen | [inferred] |
| `src/AasxRestConsoleServer/` + `src/AasxFileServerRestLibrary/` | REST server hosting for one or more loaded `.aasx` packages | — | frozen | [inferred] |
| `src/AasxSignature/` | Package signing utilities | — | frozen | [inferred] |
| `src/AasxGoldenMasterHarness/` | Headless console tool comparing AAS parse/serialize output against golden-master JSON fixtures under `GoldenMasters/` — **added and actively maintained in this fork**, not upstream | `Program.cs` | **ours** | [inferred] |
| `src/AasxDictionaryImport/` + `src/AasxDictionaryImport.Tests/` | Dictionary import feature and its NUnit tests — the only dedicated `*.Tests` project found in `src/` | — | frozen | [inferred] |
| `src/AasxMqtt*` , `src/AasxOpcUa2Client/`, `src/AasxUaNet*`, `src/AasxOpenidClient/`, `src/SSIExtension/` | Protocol/integration client & server libraries (MQTT, OPC UA, OpenID/SSI) | — | frozen | [inferred] |
| `src/AasxFormatCst/`, `src/AasxAmlImExport/`, `src/AasxBammRdfImExport/`, `src/AasxCore.Samm2_2_0/`, `src/AasxSchemaExport/` | Format import/export and schema-generation libraries for various interchange formats | — | frozen | [inferred] |
| `src/WpfMtpControl/`, `src/WpfMtpVisuViewer/`, `src/WpfXamlTool/`, `src/MsaglWpfControl/` | WPF control libraries (Module Type Package visualization, XAML tooling, graph layout) — `MsaglWpfControl` is vendored (MSAGL) | — | frozen | [inferred] |
| `src/es6numberserializer/`, `src/jsoncanonicalizer/` | Vendored third-party libraries (JSON canonicalization support) | — | frozen | [inferred] |
| `src/AasxServer.DomainModelV3_0_RC02/`, `src/obsolete/` | Legacy/RC domain model and retired code kept for reference | — | frozen | [inferred] |
| `docdev/docfx_project/` | DocFX developer documentation source, published via `generate-doc` CI workflow | — | frozen | [inferred] |
| `src/Check*.ps1`, `src/Build*.ps1`, `src/Test*.ps1`, `src/Install*.ps1` | Build/test/style CI-equivalent PowerShell scripts, run from `src/` | `BuildForDebug.ps1`, `Test.ps1` | frozen | [inferred] |
| `ai/`, `.agents/`, `.claude/`, `.codex/`, `.cursor/`, `CLAUDE.md`, `AGENTS.md` | Multi-tool AI coding-agent configuration and knowledge layer — **this fork's own addition**, not upstream | `ai/INDEX.md`, `AGENTS.md` | **ours** | [inferred] |

Detected test locations (from orient / manual scan): `src/AasxDictionaryImport.Tests/`
(NUnit, discovered by `Test.ps1` globbing `*.Tests.dll` under the Debug build output).
No other `*.Tests`/`*.GuiTests` directories were found under `src/` at cold-start time
— `[inferred]`, re-check if new test projects are added.

## Audit protocol
1. /cold-start fills rows, Stability = its guess (or `?`), Status = `[inferred]`.
2. A human sets Stability per row and flips confirmed rows to `[verified] (date)`.
3. Agents treat `?` rows as `frozen`. Agents never flip tags.

Field guide for the human audit (how to decide, evidence bar, worked rows):
https://github.com/kunalsuri/ai-fication-kit/blob/main/docs/AUDIT-GUIDE.md
