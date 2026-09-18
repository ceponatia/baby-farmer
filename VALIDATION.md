# Validation record

Research and artifact date: 2026-09-18.

## Executed locally

- `python scripts/sync_agents.py` — generated native roles and skill mirrors successfully.
- `python scripts/sync_agents.py --check` — generated outputs matched their canonical sources.
- `python -m unittest discover -s scripts/tests -v` — **16 tests passed**.

The tests cover native role completeness, TOML parsing, the generator's restricted JSON-value YAML representation, preserved canonical content, exact skill mirroring, model/effort fields, intentionally omitted Haiku effort, flat worker toolsets, worktree-base warnings, idempotence, drift detection, refusing conflicting manual edits, selective stale-output removal, missing/duplicate/path-escape rejection, portable manifest separators, and CRLF-safe regeneration.

The generator and tests require Python 3.11+ and no third-party dependency. Structural validation is not validation against every runtime version's complete schema.

## Not executed or verified

Codex/Claude CLI loading, native agent/skill selection, effective model access/effort, authentication/subscription usage, operating-system isolation, tool/credential permissions, cancellation of real workers, engine/editor interaction, repository test commands, GitHub writes, CI/review monitoring, branch protection, auto-merge, and branch deletion.

No live repository was modified. Vesper was inspected only as a reference. No claims of measured optimal models, production-safe unattended execution, or implemented durable supervision are made.
