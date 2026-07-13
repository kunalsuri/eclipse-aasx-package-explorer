# CLAUDE.md

@AGENTS.md

Everything above (imported from `AGENTS.md`) is the canonical guide for
this repo — project overview, build/test commands, style and commit
conventions, and working rules. It's written to be tool-agnostic; nothing
below overrides it.

## Claude Code specifics

- **Project permissions**: `.claude/settings.json` allowlists routine,
  read-only/build/test commands (git inspection, `dotnet build`/`test`/
  `format`, the `Check*.ps1` scripts) so you aren't prompted for every
  invocation of something the repo's own CI already runs.
- **Custom subagents**: `.claude/agents/` has two subagents tailored to
  this repo's CI-equivalent scripts:
  - `build-and-test` — builds the solution and runs the NUnit suite, then
    reports pass/fail with failure details.
  - `ci-style-check` — runs the local equivalents of the `check-style`
    workflow (headers, format, bite-sized, dead code, TODOs, licenses)
    and summarizes findings.
  Use these when you want the full check suite run and reported back
  without spending your own context streaming their output.
- **Deep-dive docs**: `.agents/` has more detail than fits in `AGENTS.md`
  (architecture, full plugin catalog, exhaustive CI exclude lists) —
  linked from the relevant sections of `AGENTS.md`.
- Remember the **platform constraint** from `AGENTS.md`: the WPF app and
  several plugins are Windows-only. If this session isn't on Windows, say
  so rather than claiming a full build/run succeeded.
