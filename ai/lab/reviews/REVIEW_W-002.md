<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# REVIEW: W-002 — Codex skill sync
> **Date:** 2026-07-14 · **Spec:** specs/SPEC_codex-skill-sync.md · **Ledger row:** W-002
> **Reviewer:** Codex agent, fresh session — not the implementing session
> **Verdict:** request-changes

All review judgements in this document are `[inferred]` until human audit.

## Scope reviewed

The pinned change is the uncommitted W-002 working-tree addition authorized by
`ai/lab/specs/SPEC_codex-skill-sync.md`: the 15 files under `.codex/skills/`
across 11 packages, the authorizing spec, and the two strict-verifier outputs
under `ai/analysis/audit-reports/`. This review document and the W-002 ledger
row are the remaining process records required by the spec.

The existing dirty changes in `AGENTS.md`, `CLAUDE.md`, `ai/START-HERE.html`,
`ai/guide/`, the W-001 ledger row, and `ai/analysis/diagrams/` were explicitly
excluded from W-002 review. No commit or branch diff exists yet; scope was
pinned by the spec touch list and the corresponding working-tree paths.

## Checks — evidence, not assertions
| Check | Result | Evidence |
|---|---|---|
| Spec conformance — every acceptance criterion met | ❌ | T1–T3 passed, but mandatory T4 exited 1. The target contains exactly 11 union names and 15 files; all selected-source hashes and frontmatter checks passed. Acceptance criterion 2 still requires all four tests to pass. |
| Surgical diff — every hunk traces to the spec | ✅ | W-002 additions are confined to `.codex/skills/`, `ai/lab/specs/SPEC_codex-skill-sync.md`, the verifier outputs, this review, and the W-002 ledger row, all named in the touch list. Existing unrelated dirty paths were excluded. |
| Stability respected — no `frozen`/`?` files touched without recorded approval | ✅ | `ai/guide/MODULE_MAP.md:64` classifies `.codex/`, `.agents/`, `.claude/`, and `ai/` as `ours`; `git status --short -- .agents/skills .claude/skills` showed no source-package modifications. |
| Tests — new behavior covered; suites green | ❌ | T1 PowerShell union comparison: exit 0, 11 expected and 11 actual names. T2 selected-source file-set/SHA-256/reparse-point check: exit 0, 15 expected and actual files, zero missing, mismatched, extra, or reparse entries. T3 frontmatter validation: exit 0, 11 packages and zero failures. Final T4 rerun after adding the review and ledger row, `node ../ai-fication-kit/install.mjs verify . --strict`: exit 1, 181 confirmed, 3 moved, and 14 missing claims (17 unconfirmed total). |
| Conventions & license headers match neighbors | ✅ | Every target file is hash-identical to its selected source, preserving source headers exactly. The spec, review, and ledger follow neighboring process-record header and path conventions; scoped `git diff --check` reported no whitespace errors. |
| Knowledge updated — maps/catalog amended, tagged `[inferred]` | ✅ | The spec correctly records that existing `.codex/` coverage in `ai/guide/MODULE_MAP.md:64` remains accurate and no feature-map change is needed. The strict verifier regenerated `ai/analysis/audit-reports/VERIFICATION_MANIFEST.json` and `VERIFICATION_REPORT.md`; W-002 is linked from the ledger. |
| Provenance clean — no `[verified]` written by an agent | ✅ | `rg` over the W-002 spec and generated verifier artifacts found only `[inferred]` tags in the spec and no agent-authored `[verified]`; this review and ledger row are explicitly `[inferred]`. |

## Findings
| # | Severity | File | Finding | Resolution |
|---|---|---|---|---|
| 1 | major | `ai/analysis/audit-reports/VERIFICATION_REPORT.md` | Required T4 fails with 17 unconfirmed knowledge-path claims (14 missing, 3 moved), so spec acceptance criterion 2 is not met. The failures are in pre-existing `ai/guide/` content outside W-002's touch list, but the approved spec still requires a clean strict-verifier exit. | Open — repair the stale knowledge claims under separately authorized work, rerun T4 to exit 0, and re-review W-002. No out-of-scope guide edits were made in this review. |

## What the human should double-check

- Decide whether W-002 must wait for separately authorized `ai/guide/`
  corrections; under the current approved spec, the failed strict verifier
  prevents approval even though the copied skill packages themselves are exact.
- Confirm the source-priority judgement: the four overlapping packages
  (`add-feature`, `cold-start`, `fix-bug`, `review-change`) come from `.agents/`,
  while the seven remaining packages come from `.claude/`.
- Confirm that the generated failing verifier report should remain part of the
  review evidence until a clean rerun replaces it.
