# Plugin catalog

All plugins live under `src/AasxPlugin*` and implement
`IAasxPluginInterface` (see `.agents/architecture.md`). Most correspond
to one specific IDTA submodel template's editing/viewing UI. This list
is inferred from each project's name and contents — check the project
itself for authoritative behavior before relying on the one-line summary.

| Project | Purpose |
|---|---|
| `AasxPluginAdvancedTextEditor` | Rich text editing for text-valued submodel elements. |
| `AasxPluginAssetInterfaceDesc` | Asset Interfaces Description submodel template support. |
| `AasxPluginBomStructure` | Bill-of-Materials (BOM) structure visualization. |
| `AasxPluginContactInformation` | Contact Information submodel template UI. |
| `AasxPluginDigitalNameplate` | Digital Nameplate submodel template UI. |
| `AasxPluginDocumentShelf` | Handover Documentation / "document shelf" browsing and viewing. |
| `AasxPluginExportTable` | Export submodel data to tabular formats. |
| `AasxPluginGenericForms` | Generic, schema-driven form rendering for arbitrary submodels. |
| `AasxPluginImageMap` | Image-map style visualization (clickable regions over an image). |
| `AasxPluginKnownSubmodels` | Recognition/handling of a set of known/predefined submodels. |
| `AasxPluginMtpViewer` | Module Type Package (MTP) viewer (see also `WpfMtpControl`, `WpfMtpVisuViewer`). |
| `AasxPluginPlotting` | Plotting/charting of time-series or measurement data. |
| `AasxPluginProductChangeNotifications` | Product Change Notification (PCN) submodel template UI. |
| `AasxPluginSmdExporter` | Export to SMD (submodel data) format. |
| `AasxPluginTechnicalData` | Technical Data submodel template UI — good template for a new simple plugin. |
| `AasxPluginUaNetClient` | OPC UA client integration. Windows-only. |
| `AasxPluginUaNetServer` | OPC UA server integration. Windows-only. |
| `AasxPluginWebBrowser` | Embedded web browser panel for rendering external/linked content. |

## Adding a new plugin

1. Copy an existing simple plugin as a template —
   `AasxPluginTechnicalData` is small and self-contained.
2. Implement `IAasxPluginInterface` (`GetPluginName`, `InitPlugin`,
   `ActivateAction`/`ActivateActionAsync`, `CheckForLogMessage`).
3. Add a `<YourPlugin>.plugin` marker file (copy the boilerplate text
   from any existing one) so the host recognizes the built DLL as a
   plugin.
4. If the plugin needs configuration, add a
   `<YourPlugin>.options.json` and a matching options class deriving
   from `AasxPluginOptionsBase`.
5. Build UI with the `AnyUi` abstraction (see `.agents/architecture.md`)
   rather than WPF- or Blazor-specific APIs, so the plugin works in both
   front ends.
6. Add the new `.csproj` to `src/AasxPackageExplorer.sln` (and to
   `src/filtered.slnf` if it should be part of the fast build filter).
7. Give the new project's `.cs` files the standard license header (see
   `AGENTS.md`) and make sure `CheckHeaders.ps1`/`CheckFormat.ps1` pass.
