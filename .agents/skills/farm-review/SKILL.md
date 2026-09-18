---
name: farm-review
description: "Independently review an exact code revision and integrated diff against requirements, invariants, security, and tests. Use before final acceptance of behavior-changing code."
---

# Review the current head

Obtain the issue contract, exact base/head, complete relevant diff, context map, and test evidence. Use a fresh reviewer that did not author the implementation. Read surrounding code and tests; examine the integrated result, not only individual slice reports.

The coordinator's brief must state the exact checkout/worktree path, base branch, and expected head SHA — never leave the reviewer to discover its own location. If the implementation's commit lives only in a worker's isolated worktree, integrate or check it out somewhere the reviewer can actually reach before dispatching; a reviewer that cannot see the real head will waste its budget reconstructing state instead of reviewing it. The reviewer verifies the stated head against its actual checkout first (`git rev-parse HEAD` or equivalent) and treats read-only git commands as inspection tools, not as something to avoid because it "cannot modify source" — that boundary is about writes, not visibility.

Prioritize demonstrated correctness risks, unmet acceptance, security boundaries, persistence, and regression coverage. Avoid a mandatory findings quota and unrelated style cleanup. Report stable finding ID, severity, location, scenario, evidence, and the acceptance condition for resolution.

Reconcile previous findings explicitly as fixed, still open, invalid with rationale, or owner-deferred. An owner deferral must not silently waive required acceptance. Every changed head needs current-head attestation; proportionately inspect changed areas and interactions rather than blindly repeating a whole expensive review.

Return reviewed SHA and limitations. Your report is not automatically a GitHub approval or status check; the runtime must represent any enforced review gate through an authorized, head-bound mechanism.
