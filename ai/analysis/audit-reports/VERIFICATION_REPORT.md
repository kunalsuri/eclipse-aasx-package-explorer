# Claim verification report

> Generated mechanically by ai-fication-kit `verify` on 2026-07-14.
> A claim is a backtick-quoted path in the knowledge docs; verification is a
> file-existence check against the repository tree. No model involved — treat
> the statuses as facts, the fix as your judgement.

| Status | Count | Meaning |
|---|---|---|
| confirmed | 181 | claim found on disk |
| moved | 3 | path is stale; a file with that name exists elsewhere |
| missing | 14 | nothing on disk matches the claim |

## Stale or missing claims (fix the docs, or the docs lie)

| Claim | Status | Found at | Source | Line |
|---|---|---|---|---|
| `GoldenMasters/` | missing | — | ai/guide/ARCHITECTURE.md | 23 |
| `plugins/` | missing | — | ai/guide/ARCHITECTURE.md | 34 |
| `AasxPackageLogic/AasxScript.cs` | moved | `src/AasxPackageLogic/AasxScript.cs` | ai/guide/ARCHITECTURE.md | 40 |
| `AasxGoldenMasterHarness/Program.cs` | moved | `obsolete/2020-07-20/AasxGenerate/Program.cs` | ai/guide/ARCHITECTURE.md | 44 |
| `mristin/opinionated-commit-message` | missing | — | ai/guide/CONVENTIONS.md | 51 |
| `data.json` | missing | — | ai/guide/FEATURE_MAP.md | 27 |
| `GoldenMasters/` | missing | — | ai/guide/FEATURE_MAP.md | 45 |
| `install.mjs` | missing | — | ai/guide/FEATURE_MAP.md | 62 |
| `package.json` | missing | — | ai/guide/FEATURE_MAP.md | 62 |
| `eclipse-aaspe/package-explorer` | missing | — | ai/guide/MODULE_MAP.md | 20 |
| `AdminShellPackageEnv.cs` | missing | — | ai/guide/MODULE_MAP.md | 41 |
| `plugins/README.md` | moved | `.agents/README.md` | ai/guide/MODULE_MAP.md | 48 |
| `GoldenMasters/` | missing | — | ai/guide/MODULE_MAP.md | 55 |
| `src/obsolete/` | missing | — | ai/guide/MODULE_MAP.md | 61 |
| `package.json` | missing | — | ai/guide/PROJECT_OVERVIEW.md | 28 |
| `admin-shell-io/aasx-package-explorer` | missing | — | ai/guide/PROJECT_OVERVIEW.md | 36 |
| `eclipse-aaspe/package-explorer` | missing | — | ai/guide/PROJECT_OVERVIEW.md | 44 |

181 confirmed claim(s) — full list in VERIFICATION_MANIFEST.json.
