# AGENTS.md — Proposed Operating Rules

**Template only.** Adapt this after engine/platform decisions are accepted. This file is not an active configuration for an existing repository.

## Project intent

Build a small, original farming/life-simulation game. Favor a playable, testable vertical slice over general-purpose infrastructure. The current proposed engine is Unity; Godot is excluded from the engine selection.

## Before changing anything

Read the current vision, accepted decisions, relevant feature specification, and existing implementation. Identify the actual engine/package versions and available tools. Distinguish requirements from proposals. Do not create a new system to replace one that already satisfies the task.

## Boundaries

Keep simulation rules independent of Unity presentation APIs. Use stable content IDs. Do not put irreversible rewards in animation callbacks. Preserve save compatibility or provide an explicit migration. Use the canonical authoring source; never edit generated catalogs or controllers as the long-term fix.

Do not change the engine, target platform, art dimensions, core content schema, save format, dependencies, or architecture outside the task's scope without a decision record and approval.

## Tool truthfulness

Inspect available MCP tools and their schemas before use. Do not invent endpoints, assume web-tool/API parity, or report that an editor action ran without evidence. Use the authorized Unity integration applicable to the installed version. Missing access is a blocker to report, not permission to fake success.

Do not assume an available cloud agent can reach a local desktop editor. Verify the actual execution environment.

## Art and cost controls

Read the approved art brief and selected provider mode. Verify dimensions, directions, frames, references, output format, costs, and unsupported controls. Start small. Do not silently switch to a more expensive mode, bulk regenerate, publish, or expose private assets.

Preserve generation records and accepted outputs. Never overwrite the golden set or visual baselines simply to make a test pass. Subjective art/audio acceptance belongs to the human reviewer.

## Implementation discipline

Make one bounded, reviewable change. Add regression tests for changed rules. Do not weaken tests to hide a failure. Keep engine/editor wiring inspectable. Avoid unrelated formatting, dependency upgrades, and architecture rewrites.

Use one owner for shared Unity scenes/assets at a time. Coordinate parallel work through separate tasks and files. Keep `.meta` references intact.

## Validation

Run the relevant compilation, content checks, rule tests, engine tests, and build smoke tests that are actually available. Inspect errors. Document unavailable checks. A successful code-generation step is not a successful implementation.

## Required completion report

Report: changed behavior; changed files; commands/tools actually run; test/build results; visual evidence; save/content compatibility impact; costs incurred; manual steps still required; and remaining known defects.

Use explicit statuses: written, compiled, tested, visually reviewed, accepted. Never claim a future action is completed.

## Hard exclusions without a separately approved scope change

No runtime generative AI, networking, backend, multiplayer, custom renderer, public mod SDK, large procedural world, or general-purpose simulation platform. No secrets in source or builds. No purchases or publishing without explicit authorization.
