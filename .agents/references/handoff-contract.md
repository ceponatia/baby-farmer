# Task packets and preserved handoffs

Use this as an ephemeral message/structured runtime record, not a new repository specification. GitHub remains the work contract. Fields can be omitted only when genuinely inapplicable; unknown facts must be labeled unknown.

## Task packet

```yaml
run_id: <stable run identity>
issue: <repository and issue number>
issue_snapshot: <updated timestamp or body hash; relevant accepted decisions>
role: <catalog role>
objective: <one deliverable>
acceptance: <exact relevant criteria or stable references>
context: <small file/symbol/issue map>
risk: <routine or named protected boundary>
checkout: <runtime-assigned path>
base_sha: <exact expected commit>
dependency_commits: []
owned_paths: []
read_only_paths_or_shared_assets: []
branch: <worker or integration branch; exact owner>
branch_lifetime: <issue-ephemeral | persistent-iteration | release | unknown>
allowed_operations: <read/edit/test/commit/GitHub writes individually scoped>
authorization: <user/repository-policy basis; merge and spending separately>
checks: <verified commands or named missing capability>
budget: <failure cap; native turn cap; total usage cap when measurable>
predecessor_handoff: <reference or none>
```

Do not assume the native runtime exposes every field above. Verify actual model, effort, cwd, tool surface, and isolation at startup. Unknown effective usage/model is a limitation to report, not a value to manufacture.

## Progress event

Return a compact checkpoint when changing phase, encountering a material blocker, reaching a budget boundary, or asked by the coordinator. Include run/finding identity, phase, latest meaningful result, changed paths/commit, active tool/check, next experiment, and any failure count. Periodic “still working” prose without new evidence is not progress and need not consume an LLM turn.

## Completion/handoff

```yaml
status: <complete | partial | blocked | escalation-requested | canceled>
run_id: <same run>
role: <role>
actual_model_effort: <runtime evidence or unavailable>
base_sha: <actual>
head_sha_or_patch: <actual commit or preserved patch reference>
changed_paths: []
behavior: <what changed>
acceptance_evidence: <criterion -> evidence or unverified reason>
checks: <exact command, result, applicable SHA, artifact/log>
findings: <stable IDs, remaining hypotheses, blockers>
failed_approaches: <brief approach -> observed failure>
usage: <measured tokens/cost/quota or unknown; do not estimate as actual>
next_action: <specific handoff or owner decision>
```

An escalation also records cancellation acknowledgement, any running subprocesses, preserved uncommitted patch, and the owner of the next write lease. Do not transfer ownership while a predecessor can still write. An orphaned process or ambiguous state is a recovery blocker, not permission to force-reset the worktree.

Independent review adds reviewed base/head, stable finding IDs, dispositions, and validation limits. The same head SHA must connect the integrated patch, review evidence, CI gates, and merge authorization. Where base branch movement changes validation needs, re-run the applicable integration gate or use the configured merge queue.
