# 10 · Decisions Before Production

## Make enough decisions to learn—not enough to prevent learning

A giant design document written before the first playable loop would create false precision. Use this pack to decide the expensive-to-change boundaries, then let prototypes inform detailed game design.

## Decide before the feasibility trial

| Decision | Proposed default | Why it matters |
|---|---|---|
| Primary delivery | Windows desktop first | Determines the engine trial and reference environment |
| Runtime model | Offline, single-player, no required AI services | Avoids accidental backend and generative-runtime scope |
| Player promise | Satisfying daily work and visible improvement | Defines what the prototype must prove |
| Differentiator | Pick one from discovery | Prevents a feature-for-feature clone plan |
| Engine candidate | Unity 6.3 LTS | Gives the trial a concrete target |
| Production budget | A user-approved trial cap | Stops unbounded asset retries |
| Visual production trial | Four displayed facings, fixed master character | Exposes animation quality and content cost early |

These are proposals. Accept, modify, or reject each in a short decision record. Do not wait for full character biographies to decide a target platform.

## Decide before broad content production

Lock the approved art scale and palette; camera perspective; target viewport behavior; animation and anchor conventions; map source of truth; canonical content format; save identity/version policy; and asset provenance workflow.

Resolve whether customization, multiplayer, or live AI is central before extending the architecture. Do not silently add them later under “polish.”

## Decide after the first enjoyable loop

Set the release content budget: crop types, NPC count, map count, seasons, requests, progression depth, and supported languages. Set an actual production schedule from measured throughput. Choose optional dialogue/localization packages and platform services only after the requirements justify them.

## Short design exercises

### A. The first ten minutes

Write what the player sees, does, learns, and receives during the first ten minutes. Include one decision and one visible improvement. Remove any mechanic the player cannot understand without a long explanation.

### B. The daily loop

Describe a normal day and an interesting exception. Identify what the player chooses freely, what they must do, and what can be skipped without making the session feel like failure.

### C. The improvement ladder

Draw three steps from starting tools to a meaningful improvement. Each step should change how the player plays, not only increase a number. Note all new art/UI/audio needed for each step.

### D. The art stress test

Describe the hardest common pose: for example, watering a tile above the character while standing next to a tall crop. Approve the production pipeline only after that pose works at game scale.

### E. The failure tour

List what happens with a full backpack, no money, blocked door, missed watering day, interrupted save, disconnected controller, and unloaded map. These are design choices before they are bug reports.

## Minimum document set to maintain

Keep the vision to one page; maintain short feature specs for active work; record consequential architecture decisions; retain a style guide and asset registry; keep save/content contracts current; and collect playtest observations. Retire obsolete content rather than preserving contradictory instructions indefinitely.

Do not maintain two long documents describing the same gameplay rule. Link to the authoritative contract.

## Initial decision log

| ID | Decision | Status | Revisit trigger |
|---|---|---|---|
| D001 | Evaluate Unity first | Proposed | Engine trial fails an actual requirement |
| D002 | Keep runtime offline/non-generative | Proposed | Live generation becomes central to the player promise |
| D003 | Four displayed facings | Proposed | Art/feel test shows a clear need for eight |
| D004 | Single canonical content source | Proposed | A real authoring bottleneck justifies tooling |
| D005 | Snapshot saves, not event-sourced storage | Proposed | A demonstrated product need cannot be met simply |
| D006 | No character customization in the slice | Proposed | Customization becomes the game's differentiator |
| D007 | One tiny completed loop before expansion | Proposed | Gate review, not spontaneous scope growth |

## Handoff to the first implementation agent

The first task is **not** “build the game.” It is: read the approved decisions; inspect the actual environment; propose the smallest engine/art feasibility setup; identify version/permission gaps; implement only the approved trial; and report real evidence.

The AGENTS template and feature-spec template define the reporting and change boundaries. Do not present the proposed directory tree or command names as an already implemented repository.
