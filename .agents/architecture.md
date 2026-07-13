# Architecture deep dive

Background for `AGENTS.md`'s repository-layout section. Read that first;
this fills in the "how do the pieces actually connect" detail.

## Domain model and package format

- `AasCore.Aas3_1` is the generated AAS v3.1 metamodel: `Aas.IReferable`,
  `Aas.AssetAdministrationShell`, `Aas.Submodel`, etc. (conventionally
  imported as `using Aas = AasCore.Aas3_1;`). It's generated from the
  IDTA metamodel — don't hand-edit it; regenerate from the upstream spec
  instead if it's ever wrong.
- `AasxCsharpLibrary` reads/writes `.aasx` files, which are OPC/ZIP
  container packages bundling one or more AAS environments plus
  supplementary files. `AdminShellPackageEnv` is the central type
  representing a loaded package.
- `AasxPredefinedConcepts` holds predefined submodel templates and
  concept descriptions (e.g. ZVEI Digital Nameplate) used to recognize
  and validate known submodel shapes.

## The AnyUI abstraction

`AnyUi` (namespace intentionally lowercase-`i`, written "AnyUI" in
prose) is a platform-agnostic UI description layer: application code
builds a UI as data (`AnyUiBase.cs`, `AnyUiforAas.cs` in
`AasxPackageLogic`) instead of directly against WPF or Blazor APIs. Each
front end then renders that description:

- WPF renders it via `AasxIntegrationBaseWpf`.
- Blazor renders it via `BlazorUI`'s `AnyUiHtml.cs`.

This is what lets `AasxPackageLogic` (dialog helpers, editing UI,
plugin-hosted panels) be written once and used from both
`AasxPackageExplorer` (WPF) and `BlazorExplorer`/`BlazorUI`. When adding
UI-producing logic that should work in both front ends, extend the
`AnyUi`-based code in `AasxPackageLogic` rather than writing directly
against `System.Windows` or Blazor components.

## Plugin interface

Plugins are separate assemblies discovered at runtime from a `plugins/`
directory (see `src/AasxPackageExplorer/plugins/README.md`); a `.plugin`
marker file tags a DLL as a plugin. Each plugin implements
`IAasxPluginInterface` (`AasxIntegrationBase/AasxPluginInterface.cs`):

- `GetPluginName()` — identifies the plugin.
- `InitPlugin(string[] args)` — one-time setup, typically loading a
  `<PluginName>.options.json` via `AasxPluginOptionsBase`.
- `ActivateAction(string action, params object[] args)` /
  `ActivateActionAsync(...)` — the plugin's actual entry points, invoked
  by name with loosely-typed arguments and returning an
  `AasxPluginResultBase` subtype (e.g. `AasxPluginResultCallMenuItem`
  carrying rendered content back to the host).
- `CheckForLogMessage()` — polled by the host to surface plugin log
  output.

See `.agents/plugins.md` for the full list of existing plugins and a
recipe for adding a new one.

## Scripting engine

`AasxPackageLogic/AasxScript.cs` embeds the S# (`Scripting.SSharp`) .NET
scripting engine so the application can be driven by script files —
used for automated demos and repeatable interaction sequences. The host
app implements `IAasxScriptRemoteInterface` (`Tool`, `Select`,
`SelectAll`, `Location`, `TakeScreenshot`) to let scripts drive the UI;
`AasxMenu`/menu-driven commands are the same commands available
interactively. This is a distinct concept from "AI agents" — it predates
and is unrelated to AI-assisted development of this repo; don't conflate
the two when a task mentions "agents" or "scripting" in an AAS/UI
context.

## Shared main-window logic

`AasxPackageLogic` centralizes logic used by all front ends:
`MainWindowLogic.cs`, `MainWindowTools.cs`, `MainWindowScripting.cs`, and
`MainWindowHeadless.cs` (a headless/no-UI variant used by console/server
tools). `ExplorerMenuFactory.cs` builds the menu structure consumed by
both WPF and Blazor.

## CLI / server tools

- `AasxToolkit` — command-line generation/extraction/validation of
  `.aasx` packages (`Cli.cs`, `Generate.cs`, `Extract.cs`).
- `AasxRestConsoleServer` + `AasxFileServerRestLibrary` — REST hosting
  for one or more loaded `.aasx` packages.
- `AasxSignature` — package signing utilities.
