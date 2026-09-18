# 04 · Gameplay and Persistence Contracts

These contracts are proposed defaults to make implementation tasks precise. Change them deliberately through a decision record.

## Core loop

```text
Get seeds → prepare ground → plant → water → advance days
→ harvest → sell or process → afford an improvement → repeat
```

The daily clock and crop growth clock are not the same feature. The first test can use an explicit “advance day” interaction. Add a continuously running clock only after the growth and save contracts work.

## System inventory

| System | Minimum contract | Representative failure to prevent |
|---|---|---|
| Input and interaction | Semantic actions; explicit selected tool and target; reach rules | The cursor highlights one tile but the action changes another |
| Movement | Collision footprint, speed, facing, diagonal policy, input-lock states | Faster diagonal movement or movement through closed geometry |
| Calendar | Integer day/minute; pause policy; ordered rollover | Crops updating twice after sleeping and loading |
| Farming | Soil, watering, growth, harvest/regrowth, removal | Dry crops advancing when the rule says watering is required |
| Inventory | Capacity, stack limits, item identity, atomic operations | Inputs disappearing when output cannot fit |
| Economy | Integer currency, prices, transaction rules | Money credited without consuming sold goods |
| Processing | Input reservation, completion time, output collection | Loading creates an extra finished product |
| Placement | Footprint, blocked cells, rotation policy, valid interaction face | A machine placed where it can never be reached |
| NPC | Schedule choice, destination, route progress, interaction availability | An NPC appears in a different location while visible |
| Requests/dialogue | Conditions, stable IDs, explicit one-time effects | Repeating a line grants a reward repeatedly |
| Progression | Unlock requirements and clear player feedback | An unlock references content that does not exist |
| Settings | Key bindings, audio, text size, accessibility | A game save overwrites device-specific preferences |

## Proposed farming semantics

A tile has a soil condition and an optional crop instance. The crop stores its definition ID, plant date, accumulated valid growth days, harvest state, and any regrowth timing. The watering state identifies the **game day** to which it applies.

For the first slice, a crop advances by one growth day at day close only if it met the configured watering requirement during that closing day. Rain can fulfill that requirement. Apply closing-day growth before resetting watering for the new day. Process each closing day once.

The design must explicitly decide season failure, missed watering, withering, harvesting inventory overflow, and regrowth. A conservative slice default is “unwatered crops pause growth; a full inventory prevents harvesting and leaves the crop untouched.” Season failure can wait until seasons enter scope.

Example tests: planting cannot replace an occupied crop; watering twice does not double growth; changing maps does not reset moisture; harvest rewards occur once; a failed harvest consumes nothing.

## Transactions

Plan an operation before applying it. Validate complete inventory capacity and all inputs together. Commit removal, addition, money, and world changes as one logical operation.

For processing, define when inputs are consumed. Proposed default: consume inputs when the machine accepts a job; save that job and its completion time; produce output into the machine's own output slot; transfer output only when the player can receive it. This avoids invisible item loss when the player's backpack is full.

A blocked or full machine reports the reason to the UI. It does not silently discard goods or create an unbounded queue.

## Calendar and pause policy

Choose policies for inventory menus, dialogue, focus loss, pause menu, cutscenes, sleeping, and saving. Proposed single-player default: pause world time in modal menus and on focus loss; processing uses game time, not computer-clock time.

Large time jumps must process relevant transitions in order. Sleeping across a boundary cannot skip a harvest event, repeat a reward, or advance a machine differently from ordinary time progression. Begin with a small, explicit scheduler rather than a general distributed job framework.

## NPCs and dialogue

Begin with one NPC, two destinations, a daily schedule, one alternative weather condition, and a fallback for blocked routes. Use a finite-state behavior model such as `travel`, `work`, `idle`, `interact`, and `return`.

On the visible map, move along routes; do not jump to a new schedule location. For unloaded maps, a simplified route-time model is adequate. Resuming the scene must respect the recorded progress. Cap replanning and provide an obvious blocked-route fallback rather than looping forever.

Write dialogue offline. Store text IDs, conditions, choices, and authorized effects. Let AI help author and review lines during development; do not add a runtime LLM merely because development uses AI. Relationships and romance are separate scope decisions.

## Save format contract

Proposed top-level information:

| Field group | Contents |
|---|---|
| Header | Save schema version, content revision, game build version, timestamp for display |
| Session | World seed, game day/minute, random-stream states |
| Player | Map ID, safe position, inventory, selected equipment, progression |
| World | Crop instances, soil state, placed objects, machine jobs, changed map state |
| Community | NPC progress, dialogue/request flags, rewards already claimed |

Use stable IDs and primitive data, not Unity instance IDs, scene object pointers, transient coroutine state, or array positions as durable identity. Store the state of each chosen random generator, not only the initial seed after draws have occurred.

## Save lifecycle

Take a consistent snapshot between transactions. For the slice, queue save requests until a short tool action has completed or been cancelled under a defined policy; make sure pausing cannot deadlock that queue. Do not serialize half a harvest.

Write a new temporary file, validate it, then replace the primary file using a platform-appropriate strategy while retaining the last good backup. “Atomic” is a property to verify on the actual storage target, not a promise from using a temporary filename.

At load: verify the header; reject unsupported newer schemas safely; migrate supported older versions; resolve content IDs; rebuild derived state; and restore the scene from authoritative state. An unknown item or crop ID needs a clear recovery policy—never silently delete the player's progress.

## Save acceptance tests

A round trip preserves all durable state. Saving immediately before and after harvest cannot duplicate a crop. A machine completes identically across save/load. An interrupted write leaves a recoverable prior save. Old fixtures migrate correctly. An unsupported future save displays a clear error without being overwritten. Loading into a changed map uses a documented safe-spawn fallback.

Cloud saves, encryption, anti-cheat, and a save editor are deferred. A checksum may detect accidental corruption; it is not a security boundary.
