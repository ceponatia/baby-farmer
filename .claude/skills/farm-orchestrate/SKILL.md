---
name: farm-orchestrate
description: "Coordinate an authorized GitHub implementation task using scoped native roles, bounded retries, independent review, and a PR handoff. Use proactively for issue implementation; not for simple questions."
---

# Orchestrate an issue

Read the issue, current repository state, `.agents/policy.json`, and `.agents/references/routing-and-budgets.md`. Confirm authority and material unknowns. Use `.agents/references/handoff-contract.md` for compact task packets and checkpoints.

Classify scope/risk; retrieve focused context only as needed. Resolve blocking contract choices through the architect. Ask the PR manager/integrator to prepare an issue branch; create an early draft only once a meaningful commit exists. Follow `.agents/references/git-pr-lifecycle.md`.

Assign one coder by default. Use separate worktrees for independent parallel slices and serialize shared assets/integration. Require exact base and owned paths in every packet. Test specialists and documentation workers are conditional, not mandatory stages.

Track evidence of progress. For repeated failed hypotheses, stop and preserve the worker before advanced reassignment. Do not interpret an active test, queue, approval, or tool outage as deficient reasoning. Keep retries across agent replacements in one finding ledger.

Require independent review of the integrated current head and appropriate tests. PR/CI agents return fixes to you; they do not create nested teams. Route semantic fixes back to coders, then revalidate. Report completion only after actual merge/acceptance, otherwise report the exact waiting/blocker state. Files do not provide persistent monitoring; see `runtime-and-security.md`.
