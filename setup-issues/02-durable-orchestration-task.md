# Add durable supervision only for verified native-runtime gaps

Suggested classification: `kind:task`. File using the existing engineering-task template after the capability issue establishes a need.

## Outcome

An authorized issue run can resume after coordinator interruption, wait for CI/review events without language-model polling, preserve partial worker output, enforce cumulative budgets, and transfer ownership safely. Start with one provider adapter; add a mixed Codex/Claude pool only if it measurably helps the workflow.

## In scope

A small supervisor around supported native programmatic interfaces, structured task/handoff contracts, persistent operational state, write leases, event deduplication, model/effort verification, cancellation acknowledgement, finding budgets, and current-head PR state reconciliation. GitHub issues remain canonical requirements; local state is operational metadata, not planning documents.

## Out of scope

A general-purpose agent framework, custom model gateway, prompt-only security controls, unsafe sandbox bypass, automatic premium fallback, uncontrolled API spend, new game features, or a production deployment service.

## Acceptance

- [ ] Starts/resumes a worker with the requested context, ownership, effective model/effort, and permitted credential/tool surface.
- [ ] Persists and resumes a run idempotently; reconciles orphaned workers/worktrees before dispatching a new writer.
- [ ] Distinguishes useful progress, active tools, user approval, infrastructure failure, and reasoning failure; enforces cumulative retry/usage limits.
- [ ] Cancels and confirms cessation before transferring ownership, preserving a usable patch/handoff.
- [ ] Handles duplicate/out-of-order CI/review events and changed PR heads; wakes models only for decisions.
- [ ] Enforces configured current-head review/check/authorization gates through trusted GitHub integration, not an agent's prose claim.
- [ ] Verifies merged state before cleanup; preserves persistent branches and pending iterations.
- [ ] Covers restart, missing permission, stale green CI, late review, canceled worker, merge failure, and branch-advanced-after-review tests.
- [ ] Records measured total usage and limitations; does not claim subscription billing covers a separate API credential.

## Dependency and completion

Depends on the runtime-capability issue's actual findings and owner-approved permissions/budgets. Link implementation, tests, resulting runtime behavior, and remaining limits in this GitHub issue. Do not launch unattended operation until its accepted gates are met.
