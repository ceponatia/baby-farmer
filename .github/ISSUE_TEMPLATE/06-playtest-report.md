---
name: "Playtest session / report"
about: "Record a focused session, separate observations from interpretations, and triage follow-ups."
title: "[Playtest] "
labels: "kind:playtest"
assignees: ""
---

<!-- Use .github/ISSUE_WORKFLOW.md. A planned session may have Pending result sections.
Do not invent a session or promote tester suggestions to approved product scope. -->

## Session context

**Date:** [Date, or Pending.]

**Build/commit:** [Exact identifier, or Pending.]

**Tester and prior familiarity:** [Relevant description; avoid unnecessary personal data.]

**Device / controls / display:** [Relevant configuration.]

**Related work:** [Feature, asset, or earlier playtest issues.]

## Objective given to the tester

[Exact task without teaching its solution; state the question this session should answer.]

## Observations

[What the tester actually did: hesitations, errors, misunderstood feedback, and time markers. Link captures where available. Keep interpretations separate and explicitly labeled.]

## Tester comments

[Faithful quotes or clearly labeled paraphrases. These are feedback, not approval.]

## Defects and friction

[For each material finding: expected behavior, observed behavior, severity, reproduction information, and relevant capture/save state. Mark unconfirmed defects as unconfirmed.]

## Enjoyment and next-goal evidence

[What the tester voluntarily repeated, stopped doing, or wanted to try next. Distinguish observed actions from their stated preferences.]

## Decisions and follow-ups

[For each material finding: fix now, investigate, defer, or reject—with rationale and the decision authority. Link a bug, feature, or task only when a separate deliverable warrants it. Keep unapproved recommendations explicit.]

## Next test

[One focused question for the next session, or why no further test is needed.]

## Closure criteria

- [ ] The actual session/build and observed findings are recorded.
- [ ] Material findings have a disposition or a linked triage/follow-up issue; no unresolved finding is silently discarded.
- [ ] The next question is explicit. Fixing all follow-ups is not required to complete this report.
