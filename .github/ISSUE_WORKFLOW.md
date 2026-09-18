# GitHub issue workflow

Read this when creating, refining, implementing, or closing an issue. This is the operating convention, not a second backlog or specification store. Templates live in `.github/ISSUE_TEMPLATE/`; their completed bodies become the issue records.

## Choose one classification

Select the template by the primary deliverable, not the subsystem or agent doing the work. Apply exactly one `kind:*` label. Other existing labels may describe area or priority. Do not invent new classifications during ordinary issue work.

| Classification | Template | Use when the deliverable is… |
| --- | --- | --- |
| `kind:feature` | [Feature](ISSUE_TEMPLATE/01-feature.md) | New or changed player-facing behavior, including a coherent parent outcome. |
| `kind:bug` | [Bug](ISSUE_TEMPLATE/02-bug.md) | Correction of observed behavior that violates an existing requirement, including tooling defects. |
| `kind:task` | [Engineering task](ISSUE_TEMPLATE/03-task.md) | Bounded infrastructure, tooling, refactoring, maintenance, or verification work without a new player-facing behavior contract. |
| `kind:decision` | [Architecture decision](ISSUE_TEMPLATE/04-architecture-decision.md) | A consequential choice, its rationale, and the maintainer's disposition—not its implementation. |
| `kind:asset` | [Asset brief](ISSUE_TEMPLATE/05-asset-brief.md) | An art, animation, UI, or audio asset/set produced and reviewed against a contract. |
| `kind:playtest` | [Playtest report](ISSUE_TEMPLATE/06-playtest-report.md) | A defined player session, observations, and triaged findings—not all resulting fixes. |
| `kind:capability` | [Tool capability check](ISSUE_TEMPLATE/07-tool-capability-check.md) | Evidence about one exact engine/package/provider/interface and a bounded go/no-go conclusion. |

A suspected defect is a bug even when its cause is unknown. “Can this endpoint do X?” is a capability check; “Which verified option should we adopt?” is a decision. A routine implementation choice stays in its feature/task. General non-tool research can use a task with a concrete question and stopping point.

Split work only when outputs need independent ownership, approval, scheduling, or acceptance. Do not automatically create all seven issue kinds for a feature. Do not create a sub-issue per file or coding step.

## Create or refine an issue

1. Read applicable repository instructions and search existing open and closed issues. Reuse the right issue rather than duplicating work. Before editing, fetch its current body, comments, labels, and relationships so another contributor's changes are not overwritten.
2. Load only the selected template. Preserve its relevant contract sections, replace prompts, and remove authoring comments. Keep it proportional to the work. Use `None — <reason>` for an inapplicable impact; use `Unknown — <fact needed and next action>` for a real gap. Do not present guesses as decisions or observations.
3. Provide the outcome, scope boundary, relevant evidence/links, and observable acceptance criteria. Identify blockers and approval or spending requirements. A planning-stage issue may remain unresolved; do not authorize implementation by filling its template.
4. Set the title and classification, then the actual assignee, milestone, and Project fields when known and available. Do not duplicate these as a manual status/owner header. Readiness requires sufficient approved scope and no unresolved prerequisite for the work being started. Small, reversible implementation details do not each need a decision issue.
5. Create/update the issue and verify the returned body and metadata. When access is missing, provide a clearly identified draft or proposed update and report the missing capability; never claim GitHub was updated.

### API, CLI, and MCP agents

The Markdown body and GitHub metadata are separate. Read the template, exclude its YAML frontmatter and authoring comments from the submitted body, populate its sections, and send title/labels through the tool's supported fields. Supply assignee/milestone only when resolved. Inspect the actual tool schema rather than assuming it accepts a template filename. The REST create-issue schema exposes issue fields, not a Markdown-template selection parameter. [S2]

Preserve unrelated labels when editing. Verify applied metadata: API permissions can cause labels or milestones to be omitted. Do not claim a label, assignment, dependency, or Project update succeeded without checking the result. [S2]

### Metadata and relationships

Use issue open/closed state and, when configured, the Project's Status field for progress. Reuse the Project's existing values; this kit does not require a particular board. Keep priority/iteration in configured metadata rather than competing body fields. With no Project, issue state and concise progress comments are sufficient.

Use native parent/sub-issue relationships for decomposition and native blocked-by/blocking relationships for actual prerequisites. A parent is not automatically a prerequisite. A related link is not automatically either relationship. GitHub supports both mechanisms; the agent's available tool may not. [S3][S4]

When the tool cannot write relationships, record a clearly labeled fallback such as `Blocked by: #123 — awaiting endpoint verification` and report that the native relationship remains unset. Migrate that fallback when access becomes available. Do not fabricate issue numbers or claim that writing a reference created a native relationship.

## Keep one current contract

The issue body holds current approved requirements and a concise outcome/evidence summary. Comments hold discussion, experiments, progress, and approval history; PRs hold implementation review. Put accepted clarifications back into the relevant body section and link the approving comment. Do not leave contradictory instructions with an addendum saying to ignore the earlier version.

Do not silently expand approved scope, rewrite another person's observations, or weaken acceptance to match an implementation. Record and resolve material disagreements. Keep logs, captures, fixtures, source assets, and machine-readable provenance in suitable repository/artifact storage and link them; these are evidence, not parallel planning documents. Promote only durable cross-cutting rules into root `AGENTS.md`, or domain rules into a scoped file, with the decision issue linked.

**Decisions have a distinct record.** Their proposed/accepted/rejected/superseded disposition is the meaning of the decision, not Project workflow status. Record the authority, date, and approval link. A closed issue is not automatically an accepted decision. To replace an accepted decision, create a new decision issue, link both directions, mark the former superseded, and preserve its original rationale. The GitHub issue number is the decision ID; no separate ADR sequence or file is needed.

## Complete the deliverable, not an imagined larger project

Check acceptance items only when evidence supports them. In the completion/result section, summarize what happened and link the relevant PR/commit/build and versions, checks actually run and results, missing checks, applicable save/content/visual evidence, costs, limitations, and required human approval. Link detailed logs instead of copying a full diff or transcript.

| Kind | Completion means… |
| --- | --- |
| Feature / bug / task | The agreed result is delivered and its required verification/review is satisfied. A draft or merged PR alone is not acceptance. |
| Decision | The authorized disposition and rationale are recorded; implementation is separately linked or explicitly unnecessary. |
| Asset | The required deliverable, provenance, technical checks, and named human acceptance are recorded. Source-only delivery may explicitly exclude engine integration. |
| Playtest | The actual session is documented and findings are triaged into linked follow-ups or explicit dispositions. Fixing every finding is separate work. |
| Capability | The bounded investigation and supported conclusion are recorded. An inconclusive result can finish an investigation, but leaves dependent integration unapproved/blocked with a named next action. |

Do not close work merely because it was created, researched, or coded. Use automatic PR closure only when merge will satisfy the entire issue contract; otherwise link without closing language. For abandoned or duplicate work, record the disposition and use an appropriate non-completion closure reason rather than implying delivery. Do not reopen every completed decision or report just because its follow-up remains open.

## Platform references

These support platform behavior, not the project's chosen taxonomy or approval policy. Checked 2026-09-18.

[S2]: https://docs.github.com/en/rest/issues/issues#create-an-issue "GitHub: issue API fields and metadata permissions"
[S3]: https://docs.github.com/en/issues/tracking-your-work-with-issues/using-issues/adding-sub-issues "GitHub: sub-issues"
[S4]: https://docs.github.com/en/issues/tracking-your-work-with-issues/using-issues/creating-issue-dependencies "GitHub: issue dependencies"
