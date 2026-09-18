# 01 · Vision and Scope

**Status: proposed.** This document constrains discovery; it does not freeze the eventual game design.

## Player promise

Provisional promise: **Build a small place that becomes more productive, more personal, and more connected to its community through readable daily choices.**

The reference is a *kind of experience*: top-down exploration, farming, crafting, daily rhythm, relationships, and visible progression. Choose original characters, artwork, setting, dialogue, music, interface treatment, and a distinct reason to play. Do not use Stardew Valley assets as generation inputs without the necessary rights.

## Three proposed pillars

**Tactile daily work.** Moving, selecting tools, planting, watering, and harvesting should be pleasant even before the player accumulates upgrades. Success should be legible through animation, sound, and visible state.

**Meaningful small decisions.** The player decides what to grow, what to process, what to sell, and who to help. Avoid turning the first implementation into an optimization spreadsheet with decorative graphics.

**Visible change.** A few sessions should noticeably improve the farm, workshop, or community. More content is not a substitute for a satisfying improvement loop.

## Choose one differentiator, not ten

Candidate directions—not approved features:

| Direction | Distinctive promise | Main scope consequence |
|---|---|---|
| Preserving and food craft | Turn harvests into useful, giftable, higher-value foods. | Recipes, processing time, product identity. |
| Small-town restoration | Production supports visible repairs and shared spaces. | A limited set of world-state changes and authored events. |
| Gentle logistics | Improve the movement and processing of farm goods. | Placement, routing, storage, and more systemic testing. |

Select one direction for the first slice. Keep a one-sentence reason why it is better for this game than the alternatives. An example cucumber-to-pickle loop can demonstrate processing, but it is not a mandated setting or theme.

## Scope tiers

**Feasibility trial:** one avatar, one small test map, one farming action, a tiny inventory, a save/reload demonstration, and an art inspection scene. This is disposable where necessary.

**Vertical slice:** one farm, one small shop/interior, one player, one NPC, two crops, one processing station, one short request, day progression, weather demonstration, and a durable save. A proposed playtest target is a satisfying 15–30 minute session, not a development-time estimate.

**Possible minimum release:** expand only after the slice establishes the content production rate and fun. Decide the number of seasons, characters, areas, and progression hours from that evidence. Do not treat this pack as approval for a four-season town simulator.

## Explicitly defer

Multiplayer; runtime LLM conversations; generated-at-runtime artwork; large procedural worlds; full character customization; romance/marriage; combat and mining; breeding/genetics; mobile controls; console certification; a public mod SDK; a custom renderer; an engine-agnostic platform.

These are change-control boundaries, not claims that the features are impossible. Some can change the architecture materially. Multiplayer or a substantial character customization system must be revisited **before** broad production if they become central to the game.

## Initial success measures

The measures below are proposed trial criteria, not established genre benchmarks.

| Question | Evidence |
|---|---|
| Is the core loop understandable? | A new tester can plant, water, harvest, and sell without live coaching. |
| Does it feel good? | Observe hesitation, missed interactions, unwanted repeat actions, and desire to keep playing. |
| Can the art pipeline scale? | Record accepted assets per batch, correction time, and cost per accepted asset. |
| Can agents work safely? | A bounded task produces reviewable changes, passing tests, and an honest evidence report. |
| Does the game preserve progress? | Repeat the slice across save/load, quit/relaunch, and an interrupted-save recovery test. |

## Scope-change rule

Each proposed feature must state the player benefit, required art/animation/UI/audio, affected save data, dependencies, and what moves out of the next milestone. New ideas go to a parking lot until the current gate passes.

Keep one active implementation feature and one separate discovery task. Parallel agents may investigate independent questions, but should not independently redesign shared contracts or edit the same Unity scene.

## Unanswered design questions

What activity must be enjoyable before progression rewards exist? Is time pressure part of the pleasure or something to avoid? Are relationships a core system or supporting context? Is the project primarily for personal enjoyment, a public demo, or a commercial release? These are the most useful early conversations; the final town name is not.
