# Install and validate

## Merge the kit, do not overwrite your repository

Copy/merge `AGENTS.md`, `CLAUDE.md`, `.agents/`, `.codex/agents/`, `.claude/agents/`, `.claude/skills/`, and `scripts/` while preserving existing project facts and configuration. The earlier classified issue templates/workflow are included unchanged under `.github/`; keep your existing authoritative versions when they have evolved. Merge `.gitignore.fragment` entries rather than replacing `.gitignore`. Create/map the seven `kind:*` issue labels before relying on template label defaults.

The example settings are deliberately named `.example`: inspect and merge them into actual `.codex/config.toml` and `.claude/settings.json` only after verifying your installed versions. Do not blindly replace user/global settings, permissions, MCP servers, or organization policy.

Use **Python 3.11+** for the portable generator and tests:

```bash
python scripts/sync_agents.py --check
python -m unittest discover -s scripts/tests -v
```

On a system where the command is `python3`, substitute that executable. The generator performs no network calls and has no third-party package dependency. It validates its generated TOML with Python's standard library and refuses to overwrite hand-edited/untracked native files. Move legitimate changes into canonical sources, reconcile generated files, and rerun; do not solve drift by deleting source history.

## Canonical editing

Edit `.agents/roles/*.md` for role behavior, `.agents/catalog.json` for descriptions/model/native settings, `.agents/skills/` for shared workflows, and `.agents/common.md` for shared role boundaries. Run `python scripts/sync_agents.py`; commit canonical changes and generated outputs together. Add the check/test commands to the repository's CI using the existing workflow conventions.

`policy.json` contains owner-approved intended runtime policy; it does not enforce itself. In particular, its timers, usage caps, and merge switches do not install a scheduler or GitHub protection rule.

## Main sessions and automatic routing

Start your usual Codex or Claude Code session at the repository root after loading the validated project settings. Root instructions tell the main session to act as orchestrator for implementation requests, load the orchestration skill, and select native role descriptions. A normal request such as “Implement issue #123 within its accepted scope” should not require mentioning every worker.

This is intentionally not a wholesale replacement of the platform's built-in main-session prompt. Claude's optional custom-main-agent mode and any special launcher need separate verification. The native `farm-orchestrator` definition is supplied for that use, but the default root-based route is sufficient for a first evaluation.

Confirm effective main-session effort and worker pins in the actual runtime; main-session defaults, environments, account caps, and model substitutions can affect behavior. If an exact model/effort is unavailable, stop or record an approved remapping in the catalog. Do not silently route to a more expensive model.

## Scope, isolation, and tools

Future domain folders should get short scoped `AGENTS.md` files only where conventions actually differ, plus `CLAUDE.md` containing `@AGENTS.md` when applicable. Do not create empty domain trees just to house instructions.

Verify worker isolation from the actual requested base. Claude's automatic worktree base may differ from parent HEAD; this is handled explicitly in generated writer instructions. Codex needs verified runtime worktree assignment. Serialize writes if native isolation cannot be proven.

Reader/reviewer Claude toolsets are intentionally restricted and receive GitHub content/diffs/logs from the parent. Add verified read-only connectors only as necessary. Check inherited MCP permissions in Codex. A shell-enabled role has technical powers beyond its textual remit; use real credential/tool isolation before unattended runs.

## Activation gates

Use the ready-to-file capability issue in `setup-issues/` to record actual CLI versions, available models, loaded agents/skills, automatic routing, worktree bases, scoped instruction behavior, turn/cancel behavior, branch retention, and negative permission tests. These are issue-body drafts, not a second specification system; file them in GitHub and maintain the resulting issues there.

Keep unattended merge disabled until current-head review/check requirements, actual GitHub permissions, branch policy, and authorized risk classes are verified. Then approve a standing repository policy rather than asking for routine merge permission repeatedly. Optional deep-rescue/API expenditure remains separately gated.

A second ready-to-file task describes durable supervision; implement it only when native-session limitations justify it. The kit does not claim automatic CI monitoring continues after the session exits.
