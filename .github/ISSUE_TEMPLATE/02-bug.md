---
name: "Bug / regression"
about: "Record an observed defect, reproduction evidence, and the required correction."
title: "[Bug] "
labels: "kind:bug"
assignees: ""
---

<!-- Use .github/ISSUE_WORKFLOW.md. Report observations, not an assumed root cause.
Unknown reproduction or environment details must remain explicit. -->

## Expected versus actual behavior

**Expected:** [Behavior and the issue/contract establishing it.]

**Actual:** [Observed failure.]

**Impact:** [Who/what is affected, severity with justification, frequency, and workaround if known.]

## Reproduction and environment

**Build/commit:** [Identifier, or Unknown.]

**Environment:** [Relevant OS, engine/package version, device/input, and save/content fixture.]

1. [Starting state.]
2. [Action.]
3. [Observed failure.]

[Attach/link relevant capture, redacted logs, or fixture. State whether reproduced or only reported.]

## Boundaries and related work

[Relevant feature/decision/report, suspected area explicitly labeled as a hypothesis, compatibility/data-recovery concerns, and known blockers. Do not turn the fix into an unrelated redesign.]

## Acceptance criteria

- [ ] [Reproduction now produces the expected result — verification method.]
- [ ] [Regression coverage and relevant adjacent behavior/save compatibility are verified, or a documented reason explains unavailable coverage.]

## Completion record

Pending.

<!-- Record confirmed cause if established, fix/PR/commit, actual verification results,
remaining limitations, and any required human review. -->
