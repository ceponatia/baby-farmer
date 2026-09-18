# Verify native agent discovery, routing, models, and safety gates

Suggested classification: `kind:capability`. File using the existing tool-capability issue template; these paragraphs are prepared content, not a new template type.

## Requirement and scope

Verify the shared-source Codex/Claude agent kit in the actual development environment before activating unattended GitHub writes/merge. Record installed versions, operating system, account mode, actual available model/effort pairs, tool credentials, and repository settings. Use a disposable test branch/issue where writes are authorized. No production workflow bypass or unapproved API spending.

## Minimal checks

- [ ] Generator drift check and local tests pass; native runtimes actually load all intended roles/skills without warnings or silent omission.
- [ ] A plain implementation request reaches the orchestrator behavior, invokes only useful roles, and uses actual intended model/effort settings.
- [ ] Root/scoped instructions load correctly in both platforms; workers do not accidentally create nested teams.
- [ ] Reader/reviewer cannot mutate GitHub or project content through inherited tools/credentials under the configured environment.
- [ ] Parallel writers have distinct worktrees/branches, verified requested base SHAs, and disjoint ownership; dependent work is not branched from a stale default base.
- [ ] Cancellation stops workers and relevant subprocesses before a replacement can write; partial output and uncommitted work survive.
- [ ] A repeated finding preserves retry/usage accounting across resumes and escalation; a healthy slow test is not misclassified as a reasoning stall.
- [ ] An early draft links the issue; required CI runs on the intended events and does not mistake missing/stale statuses for passing evidence.
- [ ] An internal review report is distinguished from GitHub-required approval/status; changed head invalidates stale review/merge evidence.
- [ ] Merge cannot bypass protected gates; auto-merge enrollment is not reported as completion.
- [ ] Ephemeral branch cleanup succeeds only after verified merge and no remaining consumers/work; a persistent single-issue iteration branch is retained.

## Evidence and decision

Record actual command/tool outputs, effective model/effort, run/PR/commit IDs, permission failures expected and observed, and untested capabilities. Approve only the verified workflow/risk class. Missing capabilities remain blocked; do not mark the whole platform approved from documentation alone.
