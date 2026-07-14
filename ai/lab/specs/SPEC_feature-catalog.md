<!-- Copyright (c) 2026 Kunal Suri (CEA LIST). All rights reserved. -->
# SPEC: Repository feature catalog
> **Status:** approved `[inferred — authorized by the user's 2026-07-14 request]`
> **Author:** AI draft · **Date:** 2026-07-14 · **Revision:** 1

All statements in this document are `[inferred]` until human audit.

## 1. Goal

Replace the placeholder feature catalog with a source-mined inventory of the
repository's implemented, user-visible capabilities and the cross-layer files
an agent must inspect before changing each capability.

## 2. Hard constraints

| # | Constraint |
|---|---|
| C1 | Do not modify files under `src/` or any other inherited upstream source. |
| C2 | Name and cluster features from a user's perspective, starting from UI, CLI, and public API surfaces. |
| C3 | Mark every catalog entry `[inferred]`; write `UNSURE` wherever a layer or verification path cannot be confirmed. |
| C4 | Update `ai/guide/FEATURE_MAP.md` only to replace its candidate-list content with a reference to the generated catalog. |
| C5 | End the catalog with a repository-specific "Where new code lives" decision tree and a three-file first-read rule for every feature. |

## 3. Scope

**In:** implemented capabilities surfaced by the WPF and Blazor applications,
CLI tools, public servers/integrations, plugins, and the fork-owned golden-master
harness; their service, persistence, and test touch lists; required process and
verification records.

**Out:** planned features, source changes, generated/vendored code changes,
directory reorganization, and claims of human verification.

## 4. Touch list

| Layer | Location | Stability | Change |
|---|---|---|---|
| Generated analysis | `ai/analysis/FEATURE_CATALOG.md` | ours | Replace placeholder with the mined catalog. |
| Guide pointer | `ai/guide/FEATURE_MAP.md` | ours | Reference the full generated catalog, nothing more. |
| Process record | `ai/lab/specs/SPEC_feature-catalog.md` | ours | Add this authorizing spec. |
| Process record | `ai/lab/reviews/REVIEW_W-003.md` | ours | Record a fresh-context review of the completed catalog. |
| Process record | `ai/lab/WORKLOG.md` | ours | Append W-003. |
| Generated verification | `ai/analysis/audit-reports/VERIFICATION_MANIFEST.json`, `ai/analysis/audit-reports/VERIFICATION_REPORT.md` | ours | Regenerate through the mandatory strict verifier if available. |

Stability check: the touch list contains no `frozen` or `?` files. `[inferred]`

## 5. Test plan

| # | Test | Assertion |
|---|---|---|
| T1 | Scan the catalog structure and provenance. | Every feature has a goal, layer touch list, verification evidence, related features, and `[inferred]`; both required ending sections exist. |
| T2 | Check every backticked repository path in the changed Markdown files. | Each claimed path exists; `UNSURE` is used for unconfirmed layers or tests. |
| T3 | Inspect the scoped diff and run `git diff --check`. | No source file changed, the feature-map edit is only a catalog reference, and no whitespace errors exist. |
| T4 | Run `node install.mjs verify . --strict` or the installed equivalent. | The knowledge-path verifier runs and its exact exit status is recorded. |

## 6. Acceptance criteria

1. The placeholder is gone and real feature entries satisfy C2, C3, and C5.
2. The source tree is unchanged and the diff stays within the touch list.
3. T1-T4 are run, with any environment or pre-existing strict-verifier blocker
   reported precisely rather than hidden.
4. W-003 links this spec, the review, and the knowledge documents changed.

## 7. Human sampling requirement

The final handoff names the five least-certain catalog entries for a human to
spot-check first, as required by `/create-feature-catalog`.
