# 02 · Engine Decision

## Recommendation and its limits

Start by evaluating **Unity 6.3 LTS on a compatible, pinned patch**. Unity lists support for that LTS through December 2027. Do not interpret “LTS” as a claim that it is the newest Unity feature release. [S01]

My recommendation is based on workflow fit, not an empirical ranking of coding models. For this project, useful criteria are: readable gameplay code; a way for agents to inspect editor state; repeatable imports and tests; competent 2D authoring; and the amount of infrastructure the developer must create.

## Non-Godot shortlist

| Option | Why it is credible | Main tradeoff for this project | Decision |
|---|---|---|---|
| **Unity + C#** | Tilemaps, pixel-perfect tooling, Aseprite import, command-line tests, and official editor-agent tooling. [S03, S06–S09] | Editor state, import settings, package compatibility, licensing, and beta-tool changes require discipline. | **First candidate for an editor-led desktop game.** |
| **Phaser + TypeScript** | A code-oriented 2D framework; Phaser 4 is available and has a new renderer. [S19, S20] | Browser-first runtime; desktop packaging, platform integration, and persistent storage need separate validation. | **Best alternative when web delivery is primary.** |
| **Defold + Lua** | Text-oriented project resources, scripting, tilemaps, cross-platform editor, and a no-fee engine model. [S21] | A different language/tool ecosystem to learn; prove the desired authoring and agent feedback loop rather than assuming parity with Unity. | **Strong lightweight alternative.** |
| **GameMaker + GML** | Focused game-making workflow with desktop/web/mobile exports; its Professional license covers commercial use. [S23] | Validate automated testing, external agent editing, and resource workflows for the actual project before committing. | **Good when its editor feels most productive to you.** |
| **MonoGame + C#** | Open-source framework focused on code and lower-level game facilities. [S22] | More responsibility for UI, content tooling, world editing, and integration. | **Choose for code-first control, not to avoid learning an editor.** |

Do not select MonoGame simply because the reference game has a related technical heritage. The important question is whether *this development workflow* benefits from owning more of the infrastructure.

## Why Unity is the proposed default

Unity's official MCP bridge can expose scene/component state, console information, script work, and build configuration to compatible agents. Its guide also describes custom tools for project-specific editor actions. That provides a way to make the editor part of a inspect–change–verify workflow rather than having an agent emit C# and leave all wiring to you. [S03]

This does not guarantee autonomous game development. You still need a local editor or another explicitly configured environment that the agent can reach. A browser chat by itself is not evidence of access to your desktop Unity process.

The current Unity AI FAQ says its MCP server is free and does not consume Unity credits; the tools remain in beta. The older May 2026 setup article mentions trial/subscription prerequisites, so do not use it as the current billing authority. Verify setup and entitlement in the installed version. [S02, S03]

Unity's June 2026 terms restrict agentic access to authorized frameworks. Use the official or explicitly designated route; do not assume an arbitrary community MCP bridge is allowed. This is a dependency-review issue, not a legal opinion about any particular third-party package. [S04]

## Proposed stack

| Layer | Initial choice | Deliberately excluded initially |
|---|---|---|
| Engine | Unity 6.3 LTS, pinned patch and package lock | Automatic upgrades during a milestone |
| Rendering | 2D project; test a URP 2D configuration before locking it | Custom rendering pipeline, heavy shader stack |
| Game rules | Plain C# with an assembly boundary excluding Unity APIs | A general-purpose world engine or ECS framework |
| Maps | Unity Tilemaps and explicit interaction/spawn markers | Two competing map-authoring sources |
| Character art | PixelLab candidates → Aseprite approval/editing → Unity import | Direct promotion of unreviewed generations |
| Animation | Frame-based clips; gameplay-owned action state | Bone deformation as the default for tiny pixel sprites |
| Content | Versioned JSON definitions, validated at import/build | Production game data scattered through scripts |
| Saves | Local versioned snapshots with backup and migration | Backend, accounts, database service |
| Agents | Existing local coding agent; official Unity MCP; PixelLab MCP where configured | Purchasing several overlapping agent subscriptions before a trial |
| Testing | Core tests, Unity EditMode/PlayMode tests, build smoke tests | A generated screenshot being treated as proof of gameplay |

Tiled can be introduced if its editing workflow is clearly preferable. Its documented JSON format supports map layers and properties. Use a verified importer or a narrowly scoped adapter, and designate either Tiled or Unity as the map source of truth—not both. [S18]

## Engine feasibility trial: observable pass/fail

Use placeholder art except for the art-import test. The trial must demonstrate the following end to end:

1. A local agent can inspect actual project state and console output, make one bounded scene change, and undo or revert it.
2. One approved sprite animation imports with correct dimensions, names, timing, and stable references after reimport.
3. The character moves, collides with one obstacle, selects one adjacent tile, and performs one action.
4. A rule test runs non-interactively and reports a deliberately inserted failure correctly.
5. A development build launches independently of the editor and saves/reloads one changed tile.
6. The project reopens from a clean checkout with documented prerequisites and without lost `.meta` references.

Record exact versions, OS, package names, commands, account prerequisites, output artifacts, and every manual step. “The agent says it worked” is not acceptance evidence.

## Rejection and fallback criteria

Move to Phaser if web delivery is the chosen goal or Unity editor automation is disproportionately obstructive. Move to Defold if lower tooling overhead and a text-oriented engine are more valuable than Unity's authoring ecosystem. Consider GameMaker after a hands-on authoring trial. Choose MonoGame only with explicit acceptance of the additional infrastructure ownership.

Do not pre-build engine adapters for all five choices. Preserve simple domain boundaries for testing and maintainability; portability is a possible benefit, not the project objective.
