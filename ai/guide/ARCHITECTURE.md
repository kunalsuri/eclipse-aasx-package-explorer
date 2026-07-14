<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# Architecture — eclipse-aasx-package-explorer

> Status: drafted by /cold-start on 2026-07-14 @ commit `c8f3ea37`, from the prior
> `AGENTS.md`/`CLAUDE.md` config backups and `.agents/architecture.md`. Tag every
> claim `[inferred]` or `[verified] (date)`.

## The big pieces  `[inferred]`
- `AasCore.Aas3_1` — the generated AAS v3.1 metamodel; every other module builds on
  its types (`Aas.AssetAdministrationShell`, `Aas.Submodel`, `Aas.IReferable`, ...).
- `AasxCsharpLibrary` — reads/writes `.aasx` package files (OPC/ZIP) into
  `AdminShellPackageEnv`, the in-memory representation everything else operates on.
- `AasxPackageLogic` — UI-agnostic application logic (dialog helpers, menu/command
  handling, main-window logic, the scripting bridge) shared by both front ends.
- `AnyUi` — a platform-agnostic "UI as data" abstraction that lets
  `AasxPackageLogic` describe UI once and have each front end render it.
- `AasxPackageExplorer` (WPF) and `BlazorExplorer`/`BlazorUI` (web) — the two front
  ends, both consuming `AasxPackageLogic` + `AnyUi`.
- `AasxPlugin*` (17 projects) — dynamically-loaded extensions, mostly one per IDTA
  submodel template's editing/viewing UI, each implementing `IAasxPluginInterface`.
- `AasxGoldenMasterHarness` — **fork addition**: a headless console tool that
  parses/serializes sample `.aasx` packages and diffs the output against fixture
  JSON in `GoldenMasters/`, used as a regression check during recent bug fixes.

## How they connect  `[inferred]`
- **AnyUi seam (verified in `.agents/architecture.md`, in-process, same .NET
  solution — not a network protocol):** `AasxPackageLogic` builds UI descriptions
  as data (`AnyUiBase.cs`, `AnyUiforAas.cs`). `AasxIntegrationBaseWpf` renders that
  description for the WPF front end; `BlazorUI`'s `AnyUiHtml.cs` renders it for the
  Blazor front end. Adding UI-producing logic that should work in both front ends
  means extending the `AnyUi`-based code in `AasxPackageLogic`, not writing
  directly against `System.Windows` or Blazor components.
- **Plugin loading:** plugins are separate assemblies discovered at runtime from a
  `plugins/` directory (see `src/AasxPackageExplorer/plugins/README.md`); a
  `.plugin` marker file tags a DLL as a plugin. The host calls
  `GetPluginName()` → `InitPlugin(string[] args)` (loads
  `<PluginName>.options.json` via `AasxPluginOptionsBase`) →
  `ActivateAction`/`ActivateActionAsync(string action, params object[] args)`,
  returning an `AasxPluginResultBase` subtype rendered back into the host UI.
- **Scripting:** `AasxPackageLogic/AasxScript.cs` embeds the S# (`Scripting.SSharp`)
  engine; the host implements `IAasxScriptRemoteInterface` so script files can
  drive the UI (`Tool`, `Select`, `SelectAll`, `Location`, `TakeScreenshot`). This
  is unrelated to "AI coding agents" — don't conflate the two.
- **Golden-master harness (fork-specific):** `AasxGoldenMasterHarness/Program.cs`
  drives `AasxCsharpLibrary`/`AasCore.Aas3_1` headlessly against sample packages
  and compares JSON output to `GoldenMasters/` fixtures — it is not wired into the
  `Test.ps1` NUnit suite; it's a standalone console tool (see its own
  `src/AasxGoldenMasterHarness/README.md`).

## Diagrams
Text-based (Mermaid) diagrams live in `ai/analysis/diagrams/`: `package-deps.mmd`,
`domain-core.mmd`, `seam.mmd`. Regenerate them via /cold-start; do not hand-maintain.

## Invariants an agent must not break  `[verified] required`
<Only humans add rows here. Examples: "generated code in X is never hand-edited",
"public API schemas are backwards compatible".>
