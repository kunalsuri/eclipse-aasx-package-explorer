# .agents/

Deep-dive reference material for AI coding agents, linked from the root
`AGENTS.md`. There's no cross-tool standard for a directory like this
(only for the `AGENTS.md` file itself), so treat it as repo-specific:
some tools will follow the links from `AGENTS.md` into here, others
won't traverse it automatically — `AGENTS.md` is written to stand on its
own without these files.

- `architecture.md` — the AnyUI UI-abstraction layer, the plugin
  interface, the embedded scripting engine, and the domain model.
- `plugins.md` — catalog of all `AasxPlugin*` projects and what each
  implements, plus how to add a new one.
- `ci-and-style.md` — exhaustive detail on the local CI-equivalent
  checks: what each one does, how to run/fix it, and the full
  exclude lists (generated/vendored code that's exempt).
