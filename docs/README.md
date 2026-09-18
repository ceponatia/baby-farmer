# Cozy Farming Game — Preproduction Pack

**Prepared for Brian · 17 September 2026 · Draft 0.1**

## Recommendation

Use **Unity 6.3 LTS, C#, PixelLab, and Aseprite** as the first stack to evaluate. Keep gameplay rules in a small, engine-independent C# module; let Unity supply rendering, input, audio, collision, UI, and authoring. Use a local coding agent with Unity's official MCP integration for editor work. Treat this as a recommendation to validate, not an engine commitment already made. [S01–S09]

**Phaser with TypeScript** is the strongest alternative when browser delivery and a web-style development loop matter more than a native game editor. **Defold** is worth considering when a lightweight, text-oriented engine and cross-platform editor are the priority. Do not build several prototypes in parallel unless the first candidate fails a specific requirement. [S19–S21]

PixelLab supplies production inputs, not finished game systems. A consistent sprite does not automatically have correct collision, tool contact, draw order, animation timing, or gameplay behavior. Those remain explicit contracts and review tasks in this plan.

## What this pack is—and is not

This is a starting architecture, production workflow, and decision framework for a new, original farming/life-simulation game. It is **not** a completed game design document, a promise of AI output quality, or an implemented project. No engine build, PixelLab generation, MCP connection, or paid integration has been tested for this project. Public documentation was checked; project-specific compatibility and quality still require the trials described here.

**Confirmed requirements:** Stardew Valley-like visual/gameplay direction; substantial AI assistance; no dependence on personal drawing ability; preference against Godot.

**Working assumptions:** solo-led development, single-player, offline play, Windows-first testing, four-direction pixel-art characters, and a small commercial-quality vertical slice before a larger game. These are proposals, not additional requirements attributed to Brian. Browser/mobile/console delivery, multiplayer, runtime generative AI, monetization, setting, and final content scale remain undecided.

## Read in this order

| Document | Purpose |
|---|---|
| [01 · Vision and scope](docs/01-vision-and-scope.md) | Player promise, assumptions, exclusions, scope control. |
| [02 · Engine decision](docs/02-engine-decision.md) | Five non-Godot choices, proposed stack, technical trial. |
| [03 · Architecture](docs/03-architecture.md) | Module boundaries, runtime flow, project layout, ownership. |
| [04 · Gameplay and persistence contracts](docs/04-gameplay-and-persistence-contracts.md) | Time, farming, inventory, NPCs, saves, migrations. |
| [05 · Art and animation pipeline](docs/05-art-and-animation-pipeline.md) | Style contract, PixelLab limits, animation semantics, validation. |
| [06 · AI-assisted production workflows](docs/06-ai-assisted-production-workflows.md) | Practical AI/human handoffs across the production lifecycle. |
| [07 · Vertical slice and milestones](docs/07-vertical-slice-and-milestones.md) | Dependency order, exit tests, production gates. |
| [08 · Quality and testing](docs/08-quality-and-testing.md) | Automated checks, playtesting, accessibility, release evidence. |
| [09 · Budget, licensing, and risk](docs/09-budget-licensing-and-risk.md) | Spending controls, source provenance, operational risks. |
| [10 · Decisions before production](docs/10-decisions-before-production.md) | Decisions needed now versus later; short design exercises. |
| [11 · Source ledger](docs/11-source-ledger.md) | Dated primary sources, limitations, and unresolved verification. |
| [AGENTS template](AGENTS.template.md) | Boundaries for coding/content agents; not active repo configuration. |

Templates: [feature specification](templates/feature-spec.md), [asset brief](templates/asset-brief.md), [architecture decision](templates/architecture-decision.md), [playtest report](templates/playtest-report.md), and [tool capability check](templates/tool-capability-check.md).

## First decision gate

Approve only a provisional player promise, a tiny slice, an art feasibility trial, and an engine feasibility trial. Do **not** commission the full cast, draw the whole town, implement multiplayer, or build a general-purpose simulation engine yet.

The first milestone should answer: **Can we repeatedly produce one coherent character, make that character do convincing farming actions, and preserve the resulting world through save/load?**

## Evidence conventions

`[Snn]` references point to the source ledger. Vendor capabilities and terms are sourced; architecture, thresholds, milestone sizes, and acceptance tests are design proposals. An API advertising a control does not establish its visual reliability. Versions and commercial terms must be rechecked when installing or purchasing. There are intentionally no production-ready credentials or assumed account entitlements in this pack.
