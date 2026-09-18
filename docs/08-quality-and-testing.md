# 08 · Quality and Testing

## Quality has several independent dimensions

Compilation is not gameplay correctness. Gameplay correctness is not visual coherence. Visual coherence is not fun. Each needs evidence.

## Test layers

| Layer | What to verify | Suggested evidence |
|---|---|---|
| Static/content | IDs, schemas, ranges, references, missing assets | Machine-readable validation report |
| Rule/unit | Growth, transactions, unlocks, pause/time semantics | Core test results |
| Simulation | Ordered time advancement, repeated days, seeded random behavior | Scenario/state trace and reproducible seed/state |
| Engine integration | Collision, input, scene transitions, animation bindings, imports | EditMode/PlayMode results and targeted captures |
| Build smoke | Startup, independent play, save paths, quit/relaunch | Tested executable and smoke-test report |
| Visual/audio | Pixel scale, seams, contact, clipping, loop quality, mixing | Human-reviewed inspection scene and recording |
| Playtest | Comprehension, friction, motivation, accessibility | Observation report, not only opinions |

Unity's Test Framework documents command-line test execution. Pin the relevant package and use its version-specific options when implementing the test wrapper. Do not invent a working command before the project's paths and environment exist. [S09]

## Initial regression scenarios

Test no-seed planting; occupied-soil planting; repeated watering; dry-day growth; rain watering; harvest into a full inventory; repeated harvest input; selling only the requested count; processing with insufficient inputs; collecting output into a full bag; sleeping over a completion boundary; leaving/re-entering a map; saving before and after a transaction; recovering from a truncated save; and loading an older content revision.

For NPCs, test a blocked route, a schedule change during interaction, an unloaded-map transition, and a reload at an intermediate journey point. For UI, test cancel/back navigation, selected-item feedback, empty slots, max stacks, controller focus, and switching input devices.

## Invariants worth asserting

Inventory quantities and currency never become negative. Each reward is granted at most once under its documented conditions. Failed transactions preserve state. Growth does not run twice for one closing day. Crops and machines progress the same way on loaded and unloaded maps. Every durable content reference resolves or produces an explicit recovery path.

These are proposed rules, not universal requirements for every possible game. Document intentional exceptions before changing tests.

## Asset validators

Check image dimensions, alpha behavior, expected animation keys, frame counts, durations, pivots, canvas consistency, palette deviations, missing facings, clipped nontransparent pixels, and invalid tile-sheet layouts. Verify referenced audio files and loop metadata.

Automated palette or image-difference checks are triage tools. A detected difference can be intentional, and an undetected change can still look bad. Have a human approve golden references; never let the same agent silently replace baselines to make visual tests pass.

## Debugging support to build early

Provide development-only views for selected tile coordinates, crop growth/watering state, current game time, inventory transactions, active action/target, NPC destination, and save schema. Add controlled commands to advance a day, load a fixture, and spawn a test item.

Keep these tools unavailable in normal release UI and exclude secrets. A simple debug overlay is enough; avoid building a remote administration platform.

## Performance plan

Proposed early target: stable 60 FPS on the designated reference machine during the slice, with save/load and map-transition stalls measured separately. This is a project target to validate, not a statement that every target device will meet it.

Profile representative farming density and map objects, not an empty scene. Record CPU/GPU frame time, allocations, asset memory, loading, and stutter. Verify the Linux target separately if it becomes supported. Do not derive minimum system requirements from the developer's high-end desktop alone.

## CI and local feedback

Keep the fastest checks local and on each change: schema validation, compilation, and small rule tests. Run more expensive editor/build suites at merge gates or when relevant files change. Cache appropriate dependencies, but periodically prove a clean build.

Before adopting hosted Unity CI, validate its licensing/activation requirements for the chosen plan and runner. Do not assume a Personal seat automatically provides every unattended-build entitlement. Local verified builds are a valid initial approach.

## Playtest method

Give a tester one objective, then watch silently. Record what they attempt, where they pause, what feedback they miss, and what they believe will happen next. Ask them afterward what they enjoyed and what goal they would pursue next.

Do not correct them during the test unless they are completely blocked. Separate a discoverability defect from a missing feature request. Fix the highest-friction existing interaction before adding a new activity.

## Definition of done

A feature has an approved behavior contract; required tests pass; relevant scenes and assets work; save impact is handled; a standalone build demonstrates the behavior where applicable; no unexpected errors remain; documentation reflects the actual result; and a human has accepted any subjective output.

The report must distinguish **written**, **compiled**, **tested**, **visually reviewed**, and **accepted**. Those are different statuses.
