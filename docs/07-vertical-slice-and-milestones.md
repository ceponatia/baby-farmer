# 07 · Vertical Slice and Milestones

## Plan by evidence gates, not an invented delivery date

The sequence below is a dependency plan. It is not a commitment to a particular number of weeks or a claim about how quickly AI will work.

## Gate 0 — Decide enough to test

Approve the player promise, primary platform, four-direction versus eight-direction presentation, initial scope cap, and a provisional art budget. Select one engine candidate. Sketch the core loop and choose a small original visual reference set.

**Exit:** a one-page decision record. Detailed lore, the full economy, and a complete asset list are not prerequisites.

## Gate 1 — Prove art and tooling together

Run the engine and art trials from Documents 02 and 05. Show the same approved character moving and using a tool in the engine. Demonstrate state inspection, reimport, one rule test, and a standalone build.

**Exit:** a tiny build, version/entitlement notes, recorded acceptance defects, and a go/no-go decision. A beautiful PixelLab preview alone does not pass.

**Stop/revise:** if custom tool animations cannot be made coherent at a sustainable correction cost, simplify the character style/action set or change the art source before building around it.

## Gate 2 — Complete the graybox farming loop

Implement one seed, one crop, one sale, a minimal inventory, day advancement, and save/load. Use placeholder objects where appropriate. Add the transaction and growth tests before extending the content.

**Exit:** a player plants, waters, advances days, harvests, sells, buys another seed, quits, reloads, and continues. No duplicated money or lost crop state.

## Gate 3 — Make the loop feel good

Add target highlighting, responsive movement, tool windup/contact/recovery, audio cues, animation polish, legible inventory actions, and basic pause/settings behavior. Test the camera at actual display sizes.

**Exit:** a tester can perform the loop without live explanations. Record where they hesitate and whether they choose to repeat it. Resolve major feel problems before increasing the number of crops.

## Gate 4 — Add one reason to progress

Add a second crop, one processing station, a small unlock, one NPC with a schedule, one short request, and one shop/interior transition. The request should create a reason to use the processing or crop system, not a separate disconnected tutorial.

**Exit:** a complete small session with a visible improvement and a useful next goal. Save/load preserves the whole chain, including machine progress and reward state.

## Gate 5 — Public-demo readiness decision

Test a clean install, an older save fixture, interrupted-save recovery, input devices, readable UI, audio controls, target performance, license records, and a second person's playthrough.

**Exit:** decide to expand, revise, or keep the game small. Only now estimate a minimum release's content volume using measured art/code/review throughput.

## Proposed slice content budget

| Category | Initial cap |
|---|---|
| Maps | One farm and one small shop/interior |
| Characters | One player, one NPC |
| Crops | Two, with all required states |
| Processing | One station and one recipe |
| Progression | One meaningful unlock or farm improvement |
| Requests | One authored multi-step request |
| Weather | Clear and rain, enough to test watering rules |
| Seasons | None initially; reserve content structure without building seasonal variants |
| Audio | One music loop or licensed track, a small coherent cue set |
| Save system | One working slot plus recovery backup; slot UI can follow |

## Example slice: harvest to preserved product

A player receives seeds, grows cucumbers, sells part of the harvest, obtains a simple preserving station, and fulfills a local request with the processed product. The reward makes the farm visibly better or enables a modest next choice.

This is a test scenario, not a finalized game concept. It exposes farming, time, inventory, recipes, processing, dialogue conditions, economy, and saves without requiring combat, fishing, marriage, or a large town.

## Dependency chain

```text
Player promise + provisional art scale
  → engine/art trial
  → movement + targeting
  → inventory + atomic transactions
  → farming + day progression + persistence
  → action feedback + UI usability
  → processing + request + NPC schedule
  → progression reward + full-session playtest
  → release-readiness decision
```

Art exploration can run alongside graybox code, but shared dimensions and action contracts must be approved first. Never make a critical code milestone depend on a large unproven batch of generated assets.

## Backlog policy

Use Now / Next / Later, with a small Now column. Every Now item has acceptance evidence, a dependency, and a known owner. A new system must identify the asset and testing cost it introduces. Keep exploratory notes separate from accepted implementation tasks.
