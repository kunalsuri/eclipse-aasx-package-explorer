<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# Feature map — feature → files, intent, gotchas

> Humans think in features; agents should too. This file holds the SHORT version —
> per-feature pointers and non-obvious notes. The full generated catalog lives in
> `ai/analysis/FEATURE_CATALOG.md` (via /create-feature-catalog — still a
> placeholder as of this /cold-start pass).

## Template (copy per feature)

### <Feature name>  `[inferred]`
- **Business goal:** <one line>
- **Touches:** <dirs/files across layers — UI, backend, persistence, tests>
- **Verify with:** <the specific test command or suite>
- **Gotchas:** <the non-obvious thing that bites people>
- **Related:** <other features that share code paths>

## Candidate features (drafted by /cold-start, audit before trusting)

### AASX package load/save  `[inferred]`
- **Business goal:** Read/write `.aasx` package files (OPC/ZIP) as
  `AdminShellPackageEnv`, the in-memory environment every front end operates on.
- **Touches:** `src/AasxCsharpLibrary/`, `src/AasCore.Aas3_1/`.
- **Verify with:** `src/Test.ps1` (only `AasxDictionaryImport.Tests` is a dedicated
  NUnit project found under `src/`); broader coverage is manual via the WPF/Blazor
  apps.
- **Gotchas:** file loading with `data.json` had a recent fix (commit `384d8266`,
  "Fix AASX file loading with data.json") — treat load-path edge cases as
  historically fragile.
- **Related:** Golden master regression harness.

### Plugin-hosted submodel UIs  `[inferred]`
- **Business goal:** Render/edit specific IDTA submodel templates (Digital
  Nameplate, BOM, Technical Data, Document Shelf, etc.) inside the host app.
- **Touches:** `src/AasxPlugin*/` (17 projects), `src/AasxIntegrationBase/AasxPluginInterface.cs`, `src/AnyUi/`.
- **Verify with:** manual UI testing in the WPF app (`BuildForDebug.ps1`, then run
  `AasxPackageExplorer`); no automated plugin test suite was found.
- **Gotchas:** must build UI through the `AnyUi` abstraction, not WPF/Blazor APIs
  directly, or the plugin only works in one front end — see
  `ai/guide/ARCHITECTURE.md`.
- **Related:** AnyUi seam, front-end rendering.

### Golden master regression harness  `[inferred] — fork addition, not upstream`
- **Business goal:** Headless comparison of AAS parse/serialize output against
  fixture JSON (`GoldenMasters/`) to catch regressions without a GUI.
- **Touches:** `src/AasxGoldenMasterHarness/` (`Program.cs`, `GoldenMasters/`).
- **Verify with:** run the `AasxGoldenMasterHarness` console tool directly (see its
  own `src/AasxGoldenMasterHarness/README.md`); it is not wired into the
  `Test.ps1` NUnit suite.
- **Gotchas:** JSON output for files with parse diagnostics was recently fixed
  (commit `0e4b62e1`); a `TypeInfoResolver` bug was fixed via PR #3
  (`a73ae0c1`) — this tool is actively evolving, don't assume its output format is
  stable.
- **Related:** AasxCsharpLibrary, AasCore.Aas3_1.

### AI agent knowledge layer  `[inferred] — fork addition, not upstream`
- **Business goal:** Give AI coding agents (Claude Code, Codex, Cursor, Copilot)
  consistent, human-audited context about this repo instead of each re-deriving it.
- **Touches:** `ai/`, `.agents/`, `.claude/`, `.codex/`, `.cursor/`, root
  `CLAUDE.md`/`AGENTS.md`, `.github/copilot-instructions.md`.
- **Verify with:** `node install.mjs verify . --strict` per `CLAUDE.md`'s hard
  rules — **UNSURE, needs human**: no `install.mjs` or `package.json` was found
  anywhere in this repo at cold-start time, so this command likely refers to the
  `ai-fication-kit` tool run from wherever it's installed (e.g. via `npx`), not a
  script checked into this repo. Confirm the actual invocation before relying on it.
- **Gotchas:** `[inferred]` tags in `ai/` must never be flipped to `[verified]` by
  an agent — that's a human signature.
- **Related:** —
