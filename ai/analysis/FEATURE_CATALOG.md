<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# Feature catalog — eclipse-aasx-package-explorer

> Source-mined on 2026-07-14 at commit `d1618f72`. Every entry is
> `[inferred]` until a human audits it. This catalog describes implemented
> user-facing and contributor-facing surfaces; it does not turn a visible menu
> label, dormant branch, or project shell into a claim that the feature works.
>
> The inherited product tree under `src/` remains frozen. This inventory is a
> navigation aid, not authorization to edit upstream code.

## Verification reality

`src/AasxDictionaryImport.Tests/` is the only dedicated test project found.
Moreover, `src/Test.ps1` currently has its `Main` call commented out, with a
November 2025 note that the samples are outdated. Therefore, an existing test
file is not evidence that the repository test script currently executes it.
Unless named otherwise below, verification is **UNSURE** and requires the
Windows build plus a focused manual exercise. The fork-owned golden-master tool
has its own direct command and committed output, described in F24.

## Feature index

| ID | User-named feature | Primary surface | Status |
|---|---|---|---|
| F01 | Package create/open/save/close | Desktop and shared menu | [inferred] |
| F02 | Desktop AAS tree browsing and editing | WPF desktop | [inferred] |
| F03 | Browser-based AAS exploration | BlazorExplorer | [inferred] |
| F04 | Legacy browser viewer/editor | BlazorUI | [inferred] |
| F05 | Find, replace, and navigation | WPF workspace | [inferred] |
| F06 | Local and remote repositories/registries | Desktop and Blazor | [inferred] |
| F07 | Validation, repair, finalization, and SMT reports | Desktop and shared logic | [inferred] |
| F08 | Signing, certificate validation, encryption, and decryption | Desktop and headless dispatcher | [inferred] |
| F09 | Template-, plugin-, and SAMM-based model creation | Shared workspace | [inferred] |
| F10 | IEC CDD and ECLASS dictionary import | Desktop importer | [inferred] |
| F11 | Local JSON submodel import/export | Desktop and shared logic | [inferred] |
| F12 | AutomationML round-trip | Desktop and Toolkit | [inferred] |
| F13 | BMEcat and CSV population | Desktop import | [inferred] |
| F14 | BAMM/SAMM semantic-aspect interchange | Desktop import/export | [inferred] |
| F15 | W3C Thing Description round-trip | Desktop import/export | [inferred] |
| F16 | OPC UA NodeSet conversion | Desktop import/export | [inferred] |
| F17 | JSON Schema export | Desktop export | [inferred] |
| F18 | Package automation CLI | AasxToolkit | [inferred] |
| F19 | AASX File Server REST client SDK | Public client API | [inferred] |
| F20 | Legacy REST console host | Console | [inferred, UNSURE] |
| F21 | OPC UA server and NodeSet export | Console/server library | [inferred] |
| F22 | AAS events and MQTT publication | WPF and MQTT plugin | [inferred] |
| F23 | Editable automation scripts | Desktop and shared scripting | [inferred] |
| F24 | Reproducible golden-master snapshots | Fork-owned CLI | [inferred] |
| F25 | Plugin discovery, menus, and visual extensions | Shared plugin host | [inferred] |
| F26 | Advanced text editing | Plugin/WPF | [inferred] |
| F27 | Asset Interfaces monitoring and operations | AnyUI plugin | [inferred] |
| F28 | BOM and package-relationship graphing | Plugin/WPF | [inferred] |
| F29 | Contact information management | AnyUI plugin | [inferred] |
| F30 | Digital nameplate presentation | AnyUI plugin | [inferred] |
| F31 | Document shelf and handover documentation | AnyUI plugin | [inferred] |
| F32 | Tabular submodel import/export | ExportTable plugin | [inferred] |
| F33 | UML and AsciiDoc SMT documentation export | ExportTable plugin | [inferred] |
| F34 | Time-series spreadsheet import | ExportTable plugin | [inferred] |
| F35 | Bulk semantic-ID replacement | ExportTable plugin | [inferred] |
| F36 | Schema-driven generic forms | AnyUI plugin | [inferred] |
| F37 | Interactive image maps | AnyUI plugin | [inferred] |
| F38 | Known Submodel Template guidance | AnyUI plugin | [inferred] |
| F39 | Module Type Package visualization | WPF plugin | [inferred] |
| F40 | Live and recorded data plotting | WPF plugin | [inferred] |
| F41 | Product Change Notification review/import | AnyUI plugin | [inferred] |
| F42 | Technical Data viewer | AnyUI plugin | [inferred] |
| F43 | Live OPC UA value refresh | Menu plus client plugin | [inferred] |
| F44 | Embedded web browser | WPF plugin | [inferred] |
| F45 | Simulation Model Description generation | Dormant plugin action | [inferred, UNSURE] |

## Cross-layer touch lists

### F01 — Package create/open/save/close [inferred]

- **Business goal:** Manage local AASX, AAS XML, and AAS JSON packages, including a hidden auxiliary package.
- **UI / interface:** `src/AasxPackageLogic/ExplorerMenuFactory.cs` and `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/PackageCentral/PackageCentral.cs`, `src/AasxPackageLogic/PackageCentral/PackageContainerLocalFile.cs`, and `src/AasxCsharpLibrary/PackageEnv/AdminShellPackageFileBasedEnv.cs` persist package parts and local files.
- **Verifying tests:** **UNSURE** — F24 exercises parsing/serialization, not the desktop menu flow.
- **Related:** F02, F06, F07, F24.

### F02 — Desktop AAS tree browsing and editing [inferred]

- **Business goal:** Navigate the environment tree, inspect/edit entities, copy/paste elements, and manage supplementary package content.
- **UI / interface:** `src/AasxPackageExplorer/MainWindow.xaml`, `src/AasxWpfControlLibrary/DiplayVisualAasxElements.xaml`, and `src/AasxWpfControlLibrary/DispEditAasxEntity.xaml`.
- **Logic / persistence:** `src/AasxPackageLogic/VisualAasxElements.cs`, `src/AasxPackageLogic/DispEditHelperEntities.cs`, `src/AasxPackageLogic/DispEditHelperCopyPaste.cs`, and the environment in `src/AasxCsharpLibrary/PackageEnv/AdminShellPackageEnvBase.cs`.
- **Verifying tests:** **UNSURE** — no desktop UI test project was found.
- **Related:** F01, F05, F09, F22, F25.

### F03 — Browser-based AAS exploration [inferred]

- **Business goal:** Load a package in a browser, navigate its tree, and render/edit the selected entity through the shared AnyUI model.
- **UI / interface:** `src/BlazorExplorer/Pages/Index.razor` and `src/BlazorExplorer/Shared/MainMenu.razor`.
- **Logic / persistence:** `src/BlazorExplorer/Data/BlazorSession.cs` and `src/BlazorExplorer/Data/BlazorSession.MainWindow.cs` coordinate `src/AasxPackageLogic/PackageCentral/PackageCentral.cs` and uploaded package files.
- **Verifying tests:** **UNSURE** — no Blazor test project was found.
- **Related:** F01, F02, F06, F25.

### F04 — Legacy browser viewer/editor [inferred]

- **Business goal:** Select a package from the working directory or configured repository and inspect/edit it in the older Blazor front end.
- **UI / interface:** `src/BlazorUI/Pages/Index.razor`.
- **Logic / persistence:** `src/BlazorUI/Program.cs` scans for package files and `src/BlazorUI/Data/AASService.cs` builds the tree and entity views.
- **Verifying tests:** **UNSURE** — no BlazorUI test project was found.
- **Related:** F03, F06.

### F05 — Find, replace, and navigation [inferred]

- **Business goal:** Search or replace model values and move through selection history, home, or saved editing locations.
- **UI / interface:** `src/AasxWpfControlLibrary/ToolControlFindReplace.xaml` and workspace commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxIntegrationBase/AasxSearchUtil.cs` and `src/AasxPackageLogic/VisualElementHistoryStack.cs`; state is transient.
- **Verifying tests:** **UNSURE**.
- **Related:** F02, F06, F23.

### F06 — Local and remote repositories/registries [inferred]

- **Business goal:** Maintain local package indexes, query by AAS/asset ID, connect to Registry/Repository APIs, and upload packages or identifiables.
- **UI / interface:** `src/AasxWpfControlLibrary/PackageCentral/PackageContainerListControl.xaml` and `src/BlazorExplorer/Pages/PanelPackageContainerList.razor`.
- **Logic / persistence:** `src/AasxPackageLogic/PackageCentral/PackageContainerHttpRepoSubset.cs`, `src/AasxPackageLogic/PackageCentral/PackageContainerListHttpRestRepository.cs`, `src/AasxPackageLogic/PackageCentral/PackageContainerListHttpRestRegistry.cs`, and local repository JSON via `src/AasxPackageLogic/PackageCentral/PackageContainerListLocal.cs`.
- **Verifying tests:** **UNSURE** — real endpoints and credentials are required for remote paths.
- **Related:** F01, F03, F19.

### F07 — Validation, repair, finalization, and SMT reports [inferred]

- **Business goal:** Find logical/schema issues, optionally repair them, finalize packages, and assess or compare Submodel Templates.
- **UI / interface:** `src/AasxWpfControlLibrary/Flyouts/ShowValidationResultsFlyout.xaml` and commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/MenuFunction/LogicValidation.cs`, `src/AasxPackageLogic/MenuFunction/MenuFuncValidateSmt.cs`, `src/AasxPackageLogic/MenuFunction/MenuFuncCompareSmt.cs`, and `src/AasxCsharpLibrary/Extensions/ExtendEnvironment.cs` mutate the current environment and write reports.
- **Verifying tests:** **UNSURE** — F24 captures validator outcomes but is not a UI/report test.
- **Related:** F01, F08, F09, F17, F24.

### F08 — Signing, certificate validation, encryption, and decryption [inferred]

- **Business goal:** Protect or verify selected AAS data and whole packages with certificate-based and credential-related flows.
- **UI / interface:** the Security menu in `src/AasxPackageLogic/ExplorerMenuFactory.cs` and dialogs in `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/MainWindowHeadless.cs`, `src/AasxPackageLogic/MainWindowTools.cs`, and `src/AasxSignature/AasxSignature.cs` operate on model content, packages, and certificate files/stores.
- **Verifying tests:** **UNSURE** — no security test suite was found.
- **Related:** F01, F07, F06.

### F09 — Template-, plugin-, and SAMM-based model creation [inferred]

- **Business goal:** Create standard submodels, restore missing concept descriptions, convert SMT metadata, or derive instances from SMT/SAMM concepts.
- **UI / interface:** the Create menu in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/MainWindowHeadless.cs`, `src/AasxPackageLogic/DispEditHelperSammModules.cs`, `src/AasxPredefinedConcepts/BaseTopUtil/DefinitionsPool.cs`, and `src/AasxPackageLogic/Plugins.cs` add submodels/concepts to the current environment.
- **Verifying tests:** **UNSURE**.
- **Related:** F07, F10, F14, F25, F36, F38.

### F10 — IEC CDD and ECLASS dictionary import [inferred]

- **Business goal:** Build submodels or elements from IEC CDD spreadsheet exports or ECLASS OntoML/service data.
- **UI / interface:** `src/AasxDictionaryImport/ImportDialog.xaml` and the dictionary commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxDictionaryImport/Cdd/Model.cs`, `src/AasxDictionaryImport/Cdd/Parser.cs`, `src/AasxDictionaryImport/Eclass/Model.cs`, and `src/AasxDictionaryImport/Import.cs` read external dictionary data and mutate the active environment.
- **Verifying tests:** `src/AasxDictionaryImport.Tests/Cdd/TestImport.cs` and `src/AasxDictionaryImport.Tests/Cdd/TestModel.cs` cover CDD internals; ECLASS and the WPF dialog remain **UNSURE**, and `src/Test.ps1` does not currently invoke its test runner.
- **Related:** F02, F09, F13.

### F11 — Local JSON submodel import/export [inferred]

- **Business goal:** Read a submodel from JSON into the current AAS or write a selected submodel to JSON.
- **UI / interface:** Import/Export commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs` and dialogs in `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/MainWindowHeadless.cs` and `src/AasxPackageLogic/MainWindowTools.cs` connect JSON files to the in-memory environment.
- **Verifying tests:** **UNSURE**. The neighboring REST GET/PUT implementations are compiled out and are not claimed here.
- **Related:** F01, F02, F19.

### F12 — AutomationML round-trip [inferred]

- **Business goal:** Exchange AAS environments, submodels, elements, links, and concept descriptions with CAEX/AutomationML.
- **UI / interface:** the AML commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs` and Toolkit load/save commands.
- **Logic / persistence:** `src/AasxAmlImExport/AmlImport.cs` and `src/AasxAmlImExport/AmlExport.cs` read/write AutomationML files through `src/AasxPackageLogic/MainWindowHeadless.cs` or F18.
- **Verifying tests:** **UNSURE**.
- **Related:** F11, F18.

### F13 — BMEcat and CSV population [inferred]

- **Business goal:** Populate submodels from product-catalog XML or comma-separated values.
- **UI / interface:** BMEcat and CSV import commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/BMEcatTools.cs` and `src/AasxPackageLogic/CSVTools.cs` are dispatched by `src/AasxPackageLogic/MainWindowHeadless.cs` and mutate the selected submodel.
- **Verifying tests:** **UNSURE**.
- **Related:** F10, F32.

### F14 — BAMM/SAMM semantic-aspect interchange [inferred]

- **Business goal:** Import BAMM/SAMM Turtle aspect models and export SAMM graphs represented by concept descriptions.
- **UI / interface:** semantic-aspect commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxBammRdfImExport/RDFimport.cs` and `src/AasxPackageLogic/DispEditHelperSammModules.cs` read/write Turtle data and concept descriptions through `src/AasxPackageLogic/MainWindowHeadless.cs`.
- **Verifying tests:** **UNSURE** — sample files exist, but no automated suite was found.
- **Related:** F09, F15.

### F15 — W3C Thing Description round-trip [inferred]

- **Business goal:** Convert a Thing Description JSON-LD document into a submodel and export it back.
- **UI / interface:** Thing Description commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/ThingDescription/TDJsonImport.cs` and `src/AasxPackageLogic/ThingDescription/TDJsonExport.cs` are dispatched from `src/AasxPackageLogic/MainWindowHeadless.cs`.
- **Verifying tests:** **UNSURE**.
- **Related:** F11, F14.

### F16 — OPC UA NodeSet conversion [inferred]

- **Business goal:** Import a generic NodeSet XML into a submodel and expose the legacy i4AAS mapping path.
- **UI / interface:** NodeSet commands in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/OpcUa/NodeSetImport.cs` and `src/AasxPackageLogic/Resources/i4AASCS.xml` feed `src/AasxPackageLogic/MainWindowHeadless.cs`.
- **Verifying tests:** **UNSURE** — generic import has source, but the i4AAS implementation is compiled out or absent in this checkout; do not treat i4AAS as confirmed.
- **Related:** F21, F43.

### F17 — JSON Schema export [inferred]

- **Business goal:** Generate a JSON Schema constraining a selected Submodel Template.
- **UI / interface:** the schema export command in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxSchemaExport/ISchemaExporter.cs` and `src/AasxSchemaExport/SubmodelTemplateJsonSchemaExporterV20.cs` write schema text through `src/AasxPackageLogic/MainWindowHeadless.cs`.
- **Verifying tests:** **UNSURE**.
- **Related:** F07, F18.

### F18 — Package automation CLI [inferred]

- **Business goal:** Script package generation/load/save, XML/JSON validation and repair, VDI 2770 extraction, template export, and CST artifact export.
- **UI / interface:** commands declared in `src/AasxToolkit/Program.cs`.
- **Logic / persistence:** `src/AasxToolkit/Execution.cs`, `src/AasxToolkit/Generate.cs`, `src/AasxToolkit/Extract.cs`, and `src/AasxFormatCst/AasxToCst.cs` operate on package, document, template, and CST files.
- **Verifying tests:** **UNSURE** — no Toolkit test project was found.
- **Related:** F01, F07, F12, F17.

### F19 — AASX File Server REST client SDK [inferred]

- **Business goal:** List/download/upload/update/delete packages and read or update shell metadata on a remote AASX File Server.
- **UI / interface:** public operations in `src/AasxFileServerRestLibrary/Api/AASXFileServerInterfaceApi.cs` and `src/AasxFileServerRestLibrary/Api/AssetAdministrationShellInterfaceApi.cs`.
- **Logic / persistence:** `src/AasxFileServerRestLibrary/Client/ApiClient.cs` handles HTTP serialization and downloads; the remote server owns persistence.
- **Verifying tests:** **UNSURE** — no SDK tests or local server fixture were found.
- **Related:** F06, F20.

### F20 — Legacy REST console host [inferred]

- **Business goal:** Load one package and serve it on a configurable host and port.
- **UI / interface:** `src/AasxRestConsoleServer/Program.cs`.
- **Logic / persistence:** **UNSURE** — `src/AasxRestConsoleServer/AasxRestConsoleServer.csproj` references a sibling REST server implementation that is absent from this checkout; the source package path itself remains the intended persistence.
- **Verifying tests:** **UNSURE** — the implementation dependency must first be resolved.
- **Related:** F19. This is an incomplete legacy surface, not a confirmed runnable server.

### F21 — OPC UA server and NodeSet export [inferred]

- **Business goal:** Load an AASX package, expose its entities as OPC UA nodes, or export the resulting address space as NodeSet XML.
- **UI / interface:** `src/AasxUaNetConsoleServer/Program.cs` and server options.
- **Logic / persistence:** `src/AasxUaNetServer/UaServerWrapper.cs`, `src/AasxUaNetServer/AasxServer/AasNodeManager.cs`, and `src/AasxUaNetServer/AasxServer/AasxUaServerOptions.cs` use package input, OPC UA configuration/certificates, and optional XML output.
- **Verifying tests:** **UNSURE** — no OPC UA server tests were found.
- **Related:** F16, F43.

### F22 — AAS events and MQTT publication [inferred]

- **Business goal:** Display/compress model events and publish initial models, value changes, or event envelopes to MQTT; optionally run an embedded broker plugin.
- **UI / interface:** `src/AasxWpfControlLibrary/AdminShellEvents/AasEventCollectionViewer.xaml` and `src/AasxPackageExplorer/Flyout/MqttPublisherFlyout.xaml`.
- **Logic / persistence:** `src/AasxPackageLogic/AdminShellEvents/AasEventCompressor.cs`, `src/AasxMqttClient/MqttClient.cs`, `src/AasxMqtt/MqttServer.cs`, and `src/AasxMqtt/Plugin.cs` use transient buffers and broker topics.
- **Verifying tests:** **UNSURE** — requires a broker and live event flow.
- **Related:** F02, F23, F27, F40.

### F23 — Editable automation scripts [inferred]

- **Business goal:** Automate menu commands, selection, file reads, screenshots, delays, and optionally operating-system commands.
- **UI / interface:** script menu/hotkeys in `src/AasxPackageLogic/ExplorerMenuFactory.cs` and editor/launch dialogs in `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/AasxScript.cs` and `src/AasxPackageLogic/MainWindowScripting.cs` execute script text and presets; `src/AasxPackageExplorer/debug.MIHO.script` is an example.
- **Verifying tests:** **UNSURE**.
- **Related:** F05 and every menu-driven feature.

### F24 — Reproducible golden-master snapshots [inferred]

- **Business goal:** Batch parse, validate, and serialize AASX fixtures into portable JSON baselines for downstream compatibility checks.
- **UI / interface:** CLI arguments in `src/AasxGoldenMasterHarness/Program.cs` and usage in `src/AasxGoldenMasterHarness/README.md`.
- **Logic / persistence:** inputs are under `src/BlazorUI/`; committed outputs are under `src/AasxGoldenMasterHarness/GoldenMasters/BlazorUI/`.
- **Verifying tests:** regenerate with the documented `dotnet run` command and compare output byte-for-byte. The documented current batch intentionally exits 2 because several fixtures expose a generated-validator exception.
- **Related:** F01, F07. This is the only fork-owned source feature.

### F25 — Plugin discovery, menus, and visual extensions [inferred]

- **Business goal:** Discover optional assemblies and expose their menus, conversions, submodel generators, events, and hosted AnyUI/WPF panels.
- **UI / interface:** dynamic attachment in `src/AasxPackageLogic/ExplorerMenuFactory.cs` and synthetic visual nodes in `src/AasxPackageLogic/VisualAasxElements.cs`.
- **Logic / persistence:** `src/AasxPackageLogic/Plugins.cs` and `src/AasxIntegrationBase/AasxPluginInterface.cs` load marker-described assemblies and adjacent option JSON.
- **Verifying tests:** **UNSURE** — no plugin-host test suite was found.
- **Related:** F09 and F26–F45.

### F26 — Advanced text editing [inferred]

- **Business goal:** Edit text payloads with syntax highlighting, search, folding, word wrap, and local open/save support.
- **UI / interface:** `src/AasxPluginAdvancedTextEditor/UserControlAdvancedTextEditor.xaml.cs` hosted by `src/AasxWpfControlLibrary/Flyouts/TextEditorFlyout.xaml.cs`.
- **Logic / persistence:** `src/AasxPluginAdvancedTextEditor/Plugin.cs` exchanges text with the host; editor-selected local files are the only confirmed persistence.
- **Verifying tests:** **UNSURE**; Windows-only runtime check required.
- **Related:** F25, F31, F44.

### F27 — Asset Interfaces monitoring and operations [inferred]

- **Business goal:** Visualize Asset Interfaces Description submodels and run mapped HTTP, Modbus, MQTT, OPC UA, and BACnet connections.
- **UI / interface:** `src/AasxPluginAssetInterfaceDesc/AssetInterfaceAnyUiControl.cs` and plugin start/stop menu actions.
- **Logic / persistence:** `src/AasxPluginAssetInterfaceDesc/AidInterfaceStatus.cs`, `src/AasxPluginAssetInterfaceDesc/AidInterfaceService.cs`, and `src/AasxPluginAssetInterfaceDesc/AasxPluginAssetInterfaceDesc.options.json` map runtime protocol data into AAS properties.
- **Verifying tests:** **UNSURE** — validate each protocol against representative endpoints.
- **Related:** F22, F40, F43.

### F28 — BOM and package-relationship graphing [inferred]

- **Business goal:** Visualize/edit BOM structures, inspect package-wide relationships, navigate nodes, and export SVG.
- **UI / interface:** `src/AasxPluginBomStructure/GenericBomControl.cs`.
- **Logic / persistence:** `src/AasxPluginBomStructure/Plugin.cs` and `src/AasxPluginBomStructure/GenericBomCreator.cs` create/mutate AAS entities and relationship elements; options control recognition/layout.
- **Verifying tests:** **UNSURE** — WPF graph editing and generated submodel require manual exercise.
- **Related:** F37, F45.

### F29 — Contact information management [inferred]

- **Business goal:** Browse, add, edit, delete, and repair standardized contact records and images.
- **UI / interface:** `src/AasxPluginContactInformation/ContactListAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginContactInformation/Plugin.cs`, `src/AasxPluginContactInformation/ContactEntity.cs`, and `src/AasxPluginContactInformation/AasxPluginContactInformation.options.json` materialize records into the active package.
- **Verifying tests:** **UNSURE**.
- **Related:** F36, F38.

### F30 — Digital nameplate presentation [inferred]

- **Business goal:** Render standardized nameplate data, markings, identifiers, QR codes, and packaged images as a recognizable plate.
- **UI / interface:** `src/AasxPluginDigitalNameplate/NameplateAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginDigitalNameplate/Plugin.cs`, `src/AasxPluginDigitalNameplate/NameplateData.cs`, and `src/AasxPluginDigitalNameplate/AasxPluginDigitalNameplate.options.json` read V1/V2 structures and supplementary files.
- **Verifying tests:** **UNSURE**.
- **Related:** F38, F42.

### F31 — Document shelf and handover documentation [inferred]

- **Business goal:** Browse, preview, add, edit, save, and remove packaged engineering documents.
- **UI / interface:** `src/AasxPluginDocumentShelf/ShelfAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginDocumentShelf/Plugin.cs`, `src/AasxPluginDocumentShelf/DocumentEntity.cs`, and `src/AasxPluginDocumentShelf/ShelfPreviewService.cs` manage metadata, digital files, and previews in the package.
- **Verifying tests:** **UNSURE**.
- **Related:** F26, F33, F36.

### F32 — Tabular submodel import/export [inferred]

- **Business goal:** Exchange submodel-element sets through tab-separated, LaTeX, Word, Excel, and Markdown tables.
- **UI / interface:** `src/AasxPluginExportTable/Table/AnyUiDialogueTable.cs`.
- **Logic / persistence:** `src/AasxPluginExportTable/Plugin.cs`, `src/AasxPluginExportTable/Table/ExportTableProcessor.cs`, and `src/AasxPluginExportTable/Table/ImportPopulateByTable.cs` read/write external tables and mutate the selected submodel on import.
- **Verifying tests:** **UNSURE**.
- **Related:** F13, F34, F35.

### F33 — UML and AsciiDoc SMT documentation export [inferred]

- **Business goal:** Publish a submodel as XMI/PlantUML or as an AsciiDoc/ZIP Submodel Template specification.
- **UI / interface:** `src/AasxPluginExportTable/Uml/AnyUiDialogueUmlExport.cs` and `src/AasxPluginExportTable/Smt/AnyUiDialogueSmtExport.cs`.
- **Logic / persistence:** `src/AasxPluginExportTable/Uml/ExportUml.cs` and `src/AasxPluginExportTable/Smt/ExportSmt.cs` write external documentation artifacts and referenced files.
- **Verifying tests:** **UNSURE**.
- **Related:** F31, F32.

### F34 — Time-series spreadsheet import [inferred]

- **Business goal:** Load recorded time-series values from Excel into AAS time-series structures.
- **UI / interface:** `src/AasxPluginExportTable/TimeSeries/AnyUiDialogueTimeSeries.cs`.
- **Logic / persistence:** `src/AasxPluginExportTable/TimeSeries/ImportTimeSeries.cs`, `src/AasxPluginExportTable/Plugin.cs`, and `src/AasxPluginExportTable/AasxPluginExportTable.options.json` map spreadsheets into the active submodel.
- **Verifying tests:** **UNSURE**.
- **Related:** F32, F40.

### F35 — Bulk semantic-ID replacement [inferred]

- **Business goal:** Apply a controlled spreadsheet list of semantic-ID changes throughout the active package.
- **UI / interface:** `src/AasxPluginExportTable/BulkChange/AnyUiDialogueBulkChangeSemanticId.cs`.
- **Logic / persistence:** `src/AasxPluginExportTable/BulkChange/BulkChangeCore.cs` and `src/AasxPluginExportTable/BulkChange/BulkChangeSemanticIdRecord.cs` read Excel input and mutate model references.
- **Verifying tests:** **UNSURE**.
- **Related:** F10, F32, F36.

### F36 — Schema-driven generic forms [inferred]

- **Business goal:** Generate, display, and edit submodels from declarative form descriptions.
- **UI / interface:** `src/AasxPluginGenericForms/GenericFormsAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginGenericForms/Plugin.cs`, `src/AasxPluginGenericForms/GenericFormsOptions.cs`, and `src/AasxPluginGenericForms/AasxPluginGenericForms.options.json` turn form data into AAS elements and load additional form definitions.
- **Verifying tests:** **UNSURE**.
- **Related:** F09, F29, F31.

### F37 — Interactive image maps [inferred]

- **Business goal:** Navigate and inspect an AAS through clickable regions over a packaged image.
- **UI / interface:** `src/AasxPluginImageMap/ImageMapAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginImageMap/Plugin.cs` and `src/AasxPluginImageMap/ImageMapOptions.cs` resolve region geometry and navigation references stored in the submodel/package.
- **Verifying tests:** **UNSURE**.
- **Related:** F28, F39.

### F38 — Known Submodel Template guidance [inferred]

- **Business goal:** Explain recognized standardized templates with cards, images, descriptions, and external links.
- **UI / interface:** `src/AasxPluginKnownSubmodels/KnownSubmodelAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginKnownSubmodels/Plugin.cs` and `src/AasxPluginKnownSubmodels/KnownSubmodelsOptions.cs` match semantic IDs to built-in records and media.
- **Verifying tests:** **UNSURE**.
- **Related:** F09, F29, F30, F41, F42.

### F39 — Module Type Package visualization [inferred]

- **Business goal:** Display an MTP process visualization, connect it to AAS entities/live OPC UA data, and navigate from process objects back to model references.
- **UI / interface:** `src/AasxPluginMtpViewer/WpfMtpControlWrapper.xaml.cs`.
- **Logic / persistence:** `src/AasxPluginMtpViewer/Plugin.cs`, `src/AasxPluginMtpViewer/MtpViewerOptions.cs`, and `src/WpfMtpControl/MtpVisuViewer.xaml.cs` use packaged MTP files, mappings, and runtime OPC UA state.
- **Verifying tests:** **UNSURE**; Windows, representative MTP data, and an OPC UA endpoint may be required.
- **Related:** F21, F37, F43.

### F40 — Live and recorded data plotting [inferred]

- **Business goal:** Chart measurement and time-series data with annotations and horizontal, vertical, or cumulative views.
- **UI / interface:** `src/AasxPluginPlotting/PlottingViewControl.xaml.cs`.
- **Logic / persistence:** `src/AasxPluginPlotting/Plugin.cs`, `src/AasxPluginPlotting/PlottingOptions.cs`, and `src/AasxPluginPlotting/PlotItem.cs` consume AAS values/events; plot buffers are transient.
- **Verifying tests:** **UNSURE**; WPF rendering and event updates require manual exercise.
- **Related:** F22, F27, F34.

### F41 — Product Change Notification review/import [inferred]

- **Business goal:** Review standardized PCN records, attachments, technical-data changes, and product impact, including SmartPCN XML import.
- **UI / interface:** `src/AasxPluginProductChangeNotifications/PcnAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginProductChangeNotifications/Plugin.cs`, `src/AasxPluginProductChangeNotifications/PcnOptions.cs`, and package file references add/review PCN records in the active submodel.
- **Verifying tests:** **UNSURE**.
- **Related:** F38, F42.

### F42 — Technical Data viewer [inferred]

- **Business goal:** Present standardized technical properties in a localized and recursively grouped product-data view.
- **UI / interface:** `src/AasxPluginTechnicalData/TechnicalDataAnyUiControl.cs`.
- **Logic / persistence:** `src/AasxPluginTechnicalData/Plugin.cs`, `src/AasxPluginTechnicalData/TechnicalDataOptions.cs`, and the package's Technical Data structures drive a read-only presentation.
- **Verifying tests:** **UNSURE**.
- **Related:** F30, F38, F41.

### F43 — Live OPC UA value refresh [inferred]

- **Business goal:** Read qualified OPC UA nodes and overwrite corresponding AAS property values.
- **UI / interface:** the OPC read command in `src/AasxPackageLogic/ExplorerMenuFactory.cs`.
- **Logic / persistence:** `src/AasxPluginUaNetClient/Plugin.cs`, `src/AasxPackageLogic/MainWindowTools.cs`, and `src/AasxOpcUa2Client/AasOpcUaClient2.cs` use qualifiers for connection/node settings and update the active environment.
- **Verifying tests:** **UNSURE** — requires representative qualifiers and an OPC UA endpoint.
- **Related:** F16, F21, F27, F39.

### F44 — Embedded web browser [inferred]

- **Business goal:** Display linked or external web content inside the Windows desktop application.
- **UI / interface:** `src/AasxPackageExplorer/BrowserContainer.cs`.
- **Logic / persistence:** `src/AasxPluginWebBrowser/Plugin.cs` and `src/AasxPluginWebBrowser/WebBrowserOptions.cs` provide navigation/zoom and user-selected downloads; browser state is transient.
- **Verifying tests:** **UNSURE**; Windows Chromium integration requires manual exercise.
- **Related:** F26, F38.

### F45 — Simulation Model Description generation [inferred]

- **Business goal:** Derive an SMD submodel from BOM relationships, simulation models, ports, mappings, and physical/signal connections.
- **UI / interface:** `src/AasxPluginSmdExporter/Plugin.cs` exposes the action, but the host call in `src/AasxPackageExplorer/MainWindow.CommandBindings.cs` is dormant.
- **Logic / persistence:** `src/AasxPluginSmdExporter/Model/Exporter.cs` reads/writes through a REST prerequisite and creates SMD structures.
- **Verifying tests:** **UNSURE** — no active host menu or targeted tests were confirmed.
- **Related:** F20, F28. Treat this as dormant implementation, not a confirmed accessible feature.

## Where new code lives

The frozen-upstream rule is the first branch in every decision:

```text
Is this change explicitly authorized to touch inherited product code?
├── No
│   ├── Golden-master/regression behavior -> src/AasxGoldenMasterHarness/
│   └── Repository intelligence/process -> ai/ (or tool config named by the task)
└── Yes
    ├── Package bytes, OPC parts, JSON/XML serialization -> src/AasxCsharpLibrary/
    ├── Shared commands, editing, import/export orchestration -> src/AasxPackageLogic/
    ├── Cross-platform UI description -> src/AnyUi/ + src/AasxPackageLogic/
    ├── Windows desktop shell/rendering -> src/AasxPackageExplorer/ or src/AasxWpfControlLibrary/
    ├── Browser front end -> src/BlazorExplorer/ (legacy: src/BlazorUI/)
    ├── Plugin-specific capability -> the matching src/AasxPlugin<Name>/ project
    ├── Plugin protocol/result/event -> src/AasxIntegrationBase/ + host handling
    ├── Package repository/registry behavior -> src/AasxPackageLogic/PackageCentral/
    ├── Dictionary import -> src/AasxDictionaryImport/ + src/AasxDictionaryImport.Tests/
    ├── Protocol server/client -> matching AasxMqtt, AasxOpcUa, or AasxUaNet project
    └── Format conversion -> matching AasxAmlImExport, AasxFormatCst,
        AasxSchemaExport, or focused AasxPackageLogic helper
```

For a new plugin, follow the neighboring plugin's marker/options/header practice;
do not create or reorganize directories merely to match this tree. A new test
location outside the dictionary importer is **UNSURE** because the repository has
no second established test-project pattern.

## The 3-file rule

Read these three files first before changing the named feature. Then expand only
along its touch list above.

| ID | First | Second | Third |
|---|---|---|---|
| F01 | `src/AasxPackageLogic/ExplorerMenuFactory.cs` | `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs` | `src/AasxCsharpLibrary/PackageEnv/AdminShellPackageFileBasedEnv.cs` |
| F02 | `src/AasxPackageExplorer/MainWindow.xaml.cs` | `src/AasxPackageLogic/DispEditHelperEntities.cs` | `src/AasxCsharpLibrary/PackageEnv/AdminShellPackageFileBasedEnv.cs` |
| F03 | `src/BlazorExplorer/Pages/Index.razor` | `src/BlazorExplorer/Data/BlazorSession.cs` | `src/BlazorExplorer/Data/BlazorSession.MainWindow.cs` |
| F04 | `src/BlazorUI/Pages/Index.razor` | `src/BlazorUI/Program.cs` | `src/BlazorUI/Data/AASService.cs` |
| F05 | `src/AasxPackageExplorer/MainWindow.CommandBindings.cs` | `src/AasxWpfControlLibrary/ToolControlFindReplace.xaml.cs` | `src/AasxIntegrationBase/AasxSearchUtil.cs` |
| F06 | `src/AasxPackageLogic/ExplorerMenuFactory.cs` | `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs` | `src/AasxPackageLogic/PackageCentral/PackageContainerHttpRepoSubset.cs` |
| F07 | `src/AasxPackageExplorer/MainWindow.CommandBindings.cs` | `src/AasxPackageLogic/MenuFunction/LogicValidation.cs` | `src/AasxCsharpLibrary/Extensions/ExtendEnvironment.cs` |
| F08 | `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs` | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxSignature/AasxSignature.cs` |
| F09 | `src/AasxPackageLogic/ExplorerMenuFactory.cs` | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxPredefinedConcepts/BaseTopUtil/DefinitionsPool.cs` |
| F10 | `src/AasxDictionaryImport/Import.cs` | `src/AasxDictionaryImport/Cdd/Model.cs` | `src/AasxDictionaryImport.Tests/Cdd/TestImport.cs` |
| F11 | `src/AasxPackageLogic/ExplorerMenuFactory.cs` | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxPackageLogic/MainWindowTools.cs` |
| F12 | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxAmlImExport/AmlImport.cs` | `src/AasxAmlImExport/AmlExport.cs` |
| F13 | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxPackageLogic/BMEcatTools.cs` | `src/AasxPackageLogic/CSVTools.cs` |
| F14 | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxBammRdfImExport/RDFimport.cs` | `src/AasxPackageLogic/DispEditHelperSammModules.cs` |
| F15 | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxPackageLogic/ThingDescription/TDJsonImport.cs` | `src/AasxPackageLogic/ThingDescription/TDJsonExport.cs` |
| F16 | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxPackageLogic/OpcUa/NodeSetImport.cs` | `src/AasxPackageLogic/Resources/i4AASCS.xml` |
| F17 | `src/AasxPackageLogic/MainWindowHeadless.cs` | `src/AasxSchemaExport/SubmodelTemplateJsonSchemaExporterV20.cs` | `src/AasxSchemaExport/ISchemaExporter.cs` |
| F18 | `src/AasxToolkit/Program.cs` | `src/AasxToolkit/Execution.cs` | `src/AasxToolkit/Instruction.cs` |
| F19 | `src/AasxFileServerRestLibrary/Api/AASXFileServerInterfaceApi.cs` | `src/AasxFileServerRestLibrary/Api/AssetAdministrationShellInterfaceApi.cs` | `src/AasxFileServerRestLibrary/Client/ApiClient.cs` |
| F20 | `src/AasxRestConsoleServer/Program.cs` | `src/AasxRestConsoleServer/AasxRestConsoleServer.csproj` | `src/AasxMqttClient/GrapevineLoggerConsumers.cs` |
| F21 | `src/AasxUaNetConsoleServer/Program.cs` | `src/AasxUaNetServer/AasxServer/AasNodeManager.cs` | `src/AasxUaNetServer/AasxServer/AasxUaServerOptions.cs` |
| F22 | `src/AasxPackageExplorer/MainWindow.xaml.cs` | `src/AasxPackageLogic/AdminShellEvents/AasEventCompressor.cs` | `src/AasxMqttClient/MqttClient.cs` |
| F23 | `src/AasxPackageLogic/MainWindowAnyUiDialogs.cs` | `src/AasxPackageLogic/AasxScript.cs` | `src/AasxPackageLogic/MainWindowScripting.cs` |
| F24 | `src/AasxGoldenMasterHarness/Program.cs` | `src/AasxGoldenMasterHarness/README.md` | `src/AasxGoldenMasterHarness/AasxGoldenMasterHarness.csproj` |
| F25 | `src/AasxPackageLogic/Plugins.cs` | `src/AasxIntegrationBase/AasxPluginInterface.cs` | `src/AasxPackageLogic/VisualAasxElements.cs` |
| F26 | `src/AasxPluginAdvancedTextEditor/Plugin.cs` | `src/AasxPluginAdvancedTextEditor/UserControlAdvancedTextEditor.xaml.cs` | `src/AasxWpfControlLibrary/Flyouts/TextEditorFlyout.xaml.cs` |
| F27 | `src/AasxPluginAssetInterfaceDesc/Plugin.cs` | `src/AasxPluginAssetInterfaceDesc/AssetInterfaceAnyUiControl.cs` | `src/AasxPluginAssetInterfaceDesc/AidInterfaceStatus.cs` |
| F28 | `src/AasxPluginBomStructure/Plugin.cs` | `src/AasxPluginBomStructure/GenericBomControl.cs` | `src/AasxPluginBomStructure/GenericBomCreator.cs` |
| F29 | `src/AasxPluginContactInformation/Plugin.cs` | `src/AasxPluginContactInformation/ContactListAnyUiControl.cs` | `src/AasxPluginContactInformation/ContactEntity.cs` |
| F30 | `src/AasxPluginDigitalNameplate/Plugin.cs` | `src/AasxPluginDigitalNameplate/NameplateAnyUiControl.cs` | `src/AasxPluginDigitalNameplate/NameplateData.cs` |
| F31 | `src/AasxPluginDocumentShelf/Plugin.cs` | `src/AasxPluginDocumentShelf/ShelfAnyUiControl.cs` | `src/AasxPluginDocumentShelf/DocumentEntity.cs` |
| F32 | `src/AasxPluginExportTable/Plugin.cs` | `src/AasxPluginExportTable/Table/AnyUiDialogueTable.cs` | `src/AasxPluginExportTable/Table/ExportTableProcessor.cs` |
| F33 | `src/AasxPluginExportTable/Plugin.cs` | `src/AasxPluginExportTable/Uml/ExportUml.cs` | `src/AasxPluginExportTable/Smt/ExportSmt.cs` |
| F34 | `src/AasxPluginExportTable/Plugin.cs` | `src/AasxPluginExportTable/TimeSeries/AnyUiDialogueTimeSeries.cs` | `src/AasxPluginExportTable/TimeSeries/ImportTimeSeries.cs` |
| F35 | `src/AasxPluginExportTable/Plugin.cs` | `src/AasxPluginExportTable/BulkChange/AnyUiDialogueBulkChangeSemanticId.cs` | `src/AasxPluginExportTable/BulkChange/BulkChangeCore.cs` |
| F36 | `src/AasxPluginGenericForms/Plugin.cs` | `src/AasxPluginGenericForms/GenericFormsAnyUiControl.cs` | `src/AasxPluginGenericForms/GenericFormsOptions.cs` |
| F37 | `src/AasxPluginImageMap/Plugin.cs` | `src/AasxPluginImageMap/ImageMapAnyUiControl.cs` | `src/AasxPluginImageMap/ImageMapOptions.cs` |
| F38 | `src/AasxPluginKnownSubmodels/Plugin.cs` | `src/AasxPluginKnownSubmodels/KnownSubmodelAnyUiControl.cs` | `src/AasxPluginKnownSubmodels/KnownSubmodelsOptions.cs` |
| F39 | `src/AasxPluginMtpViewer/Plugin.cs` | `src/AasxPluginMtpViewer/WpfMtpControlWrapper.xaml.cs` | `src/AasxPluginMtpViewer/MtpViewerOptions.cs` |
| F40 | `src/AasxPluginPlotting/Plugin.cs` | `src/AasxPluginPlotting/PlottingViewControl.xaml.cs` | `src/AasxPluginPlotting/PlottingOptions.cs` |
| F41 | `src/AasxPluginProductChangeNotifications/Plugin.cs` | `src/AasxPluginProductChangeNotifications/PcnAnyUiControl.cs` | `src/AasxPluginProductChangeNotifications/PcnOptions.cs` |
| F42 | `src/AasxPluginTechnicalData/Plugin.cs` | `src/AasxPluginTechnicalData/TechnicalDataAnyUiControl.cs` | `src/AasxPluginTechnicalData/TechnicalDataOptions.cs` |
| F43 | `src/AasxPluginUaNetClient/Plugin.cs` | `src/AasxPackageLogic/MainWindowTools.cs` | `src/AasxOpcUa2Client/AasOpcUaClient2.cs` |
| F44 | `src/AasxPluginWebBrowser/Plugin.cs` | `src/AasxPackageExplorer/BrowserContainer.cs` | `src/AasxPluginWebBrowser/WebBrowserOptions.cs` |
| F45 | `src/AasxPluginSmdExporter/Plugin.cs` | `src/AasxPackageExplorer/MainWindow.CommandBindings.cs` | `src/AasxPluginSmdExporter/Model/Exporter.cs` |

### Human sampling guide

Spot-check these five least-certain entries first:

1. **F20 Legacy REST console host** — confirm whether the absent server
   implementation is intentionally external or stale.
2. **F45 SMD generation** — confirm whether the dormant host call is an
   intentional disabled feature and whether its REST prerequisite still exists.
3. **F27 Asset Interfaces** — validate all five protocol implementations and
   start/stop behavior against real endpoints.
4. **F39 MTP visualization** — validate packaged-file resolution, OPC UA preload,
   and Windows-only rendering.
5. **F16 OPC UA NodeSet conversion** — separate the implemented generic import
   from the disabled or missing i4AAS path during audit.
