# 06 · AI-Assisted Production Workflows

## Operating principle

Use AI for bounded generation, implementation, analysis, and repetitive transformations. Retain human ownership of priorities, taste, feel, and acceptance. A workflow is complete only when the output has been checked in its intended context.

MCP is a connection mechanism, not a guarantee of access. Configure the actual local client, permissions, credentials, and reachable editor. Codex documentation supports local and HTTP MCP configurations; the environment still has to be configured and tested. [S27]

## 1. Product discovery and differentiation

**Input:** player promise, constraints, reference experiences, and the scope cap. **AI work:** propose three contrasting concepts, identify the smallest differentiating mechanic, and challenge contradictions. **Human decision:** choose one promise and one differentiator. **Output:** a one-page vision, assumptions register, and exclusions.

Acceptance: describe the game without saying “Stardew Valley, but more.” Reject features whose only justification is that the reference has them. Use AI to compare alternatives, not to keep expanding the feature inventory.

## 2. Gameplay loops and economy

**Input:** actions, resource flows, progression goal, and intended session rhythm. **AI work:** build a small spreadsheet-like model or script, inspect recipes and unlock dependencies, and propose several parameter sets. **Human work:** play the loop and assess effort, clarity, and motivation. **Output:** versioned tuning data plus a playtest note.

Model cash, inventory capacity, time, stamina if used, machine throughput, and the opportunity cost of processing. Simulations can find runaway profit loops and dead ends. They cannot establish that a game is fun.

## 3. Art direction and asset planning

**Input:** view, scale, palette, original references, and target animation set. **AI work:** draft asset briefs, create small candidate batches, and prepare comparison sheets. **Human work:** approve one coherent golden set and reject inconsistencies. **Output:** style guide, approved references, asset inventory, and batch budget.

Use asset dependencies: the player's scale precedes door height, furniture height, tool reach, and environment density. Do not produce a whole town before that relationship works.

## 4. Sprites, terrain, objects, and UI decoration

**Input:** approved asset brief and golden references. **AI work:** use the verified PixelLab interface, retain job/settings/output records, and run structural checks. **Human work:** choose candidates and perform limited cleanup or request targeted repair. **Output:** approved sources and manifests.

Generated UI art should provide decorative pieces such as panels and icons. Build actual labels, buttons, focus behavior, and layout with engine UI controls. Do not bake interactive text into an image.

## 5. Animation and tool interactions

**Input:** one approved character and an action contract. **AI work:** propose motion frames, normalize canvas alignment, organize tags, bind clips, and create a repeatable inspection scene. **Human work:** inspect loop quality, silhouettes, foot contact, tool contact, and feel. **Output:** animation set with timing/anchor metadata and an in-game acceptance recording.

When a generator cannot reliably make a specialized action, narrow the motion, use a licensed motion/base asset, edit the critical poses, or commission that small action set. Do not redesign the whole game around unverified generation claims.

## 6. Level and world design

**Input:** a simple adjacency sketch, traversal goals, interaction points, and the map budget. **AI work:** draft layouts as data or permitted editor operations; calculate approximate travel distances; run reachability checks. **Human work:** navigate and observe sightlines, visual clutter, route readability, and daily friction. **Output:** authored map plus collision, exit, spawn, and interaction metadata.

Use a graybox first. Art generation is not a substitute for authored navigability. Test both entering and leaving every door, and whether a placed object blocks a required route.

## 7. Gameplay code and engine wiring

**Input:** a bounded feature specification, existing architecture, exact package versions, and acceptance tests. **AI work:** inspect the repo, implement the smallest change, add tests, perform permitted editor wiring, and collect evidence. **Human work:** review changed behavior and play the build. **Output:** one reviewable change set, tests, screenshots/logs, and a concise handoff.

Require source inspection before new abstractions. A feature task should not also upgrade the engine, restructure the repository, rewrite all saves, and replace the UI library. Never accept an “implemented” status that only means files were written.

## 8. NPC schedules, dialogue, and requests

**Input:** character voice notes, schedule rules, world facts, and authorized dialogue effects. **AI work:** draft dialogue variants, validate text IDs and conditions, identify contradictions, and generate path/schedule fixtures. **Human work:** edit voice, pacing, repetition, and emotional credibility. **Output:** authored dialogue and schedule data, not an unrestricted runtime agent.

Use exact constraints for rewards and relationship changes. Dialogue text can describe a reward; only the validated game action grants it. Review repeated interactions and missing-condition fallbacks.

## 9. Interface, onboarding, and accessibility

**Input:** core actions, control devices, display sizes, and common failures. **AI work:** draft screen flows, help text, focus-order tests, controller bindings, and resize cases. **Human work:** play without developer knowledge and observe real readability. **Output:** working UI with visible focus, remappable actions, clear error states, and scalable text.

Prototype inventory management with mouse/keyboard and controller navigation before making decorative assets final. Do not rely only on color for crop status, item quality, or blocked actions.

## 10. Sound effects, ambience, and music

**Input:** a cue sheet with trigger, purpose, priority, expected duration, variation, and loop needs. **AI work:** suggest cues; optionally generate sound-effect candidates; analyze loudness/loop boundaries; draft music briefs. **Human work:** listen in the actual game, clean/edit, and confirm rights. **Output:** approved audio, event bindings, mix settings, and provenance.

ElevenLabs documents text-generated sound effects with duration and looping controls; treat it as an optional candidate source, not an automatic music-production pipeline. Verify the selected plan's rights and actual export behavior. [S24, S25]

For music, begin with a small licensed set or an original piece assembled/edited in a DAW. AI can help draft a motif, arrangement, and cue plan. Do not assume a full-song generator will yield exact stems, matching revisions, or seamless adaptive layers. Avoid voice acting in the first slice unless it is central to the player promise.

## 11. Testing and debugging

**Input:** invariants, known defects, reproducible saves, and scenario definitions. **AI work:** write targeted tests, generate boundary cases, run bounded simulations, inspect logs, and propose minimal fixes. **Human work:** confirm the failure and play the changed behavior. **Output:** a regression test and verified fix, with any untested part explicitly identified.

A screenshot validates visible state, not causality. Combine screenshots with command/state evidence. Never instruct an agent to make failing tests pass by weakening the requirement.

## 12. Performance and platform compatibility

**Input:** target hardware, representative maps, and profiler captures. **AI work:** summarize hotspots, suggest measured changes, and compare before/after results. **Human work:** reproduce on target devices and assess stutter, input feel, and battery/thermal behavior where relevant. **Output:** a performance report tied to a build.

Optimize observed bottlenecks. Do not introduce pooling, ECS, multithreading, or custom rendering merely because an agent expects games to need them. Test save paths, case sensitivity, focus loss, input devices, and controller reconnects independently of average frame rate.

## 13. Localization and narrative review

**Input:** externalized strings, contextual notes, glossary, and UI length limits. **AI work:** draft translations, flag inconsistent terminology, and generate pseudo-localized stress cases. **Human work:** native-language review for release languages and UI verification. **Output:** reviewed localized strings with stable IDs.

Preserve placeholders, plural behavior, punctuation, and player names. Do not ship unreviewed generated translations for important story or instructional content solely because they are grammatically plausible.

## 14. Production management and documentation

**Input:** approved milestone, actual evidence, and unresolved decisions. **AI work:** maintain the feature register, identify dependency blockers, and summarize completed versus unverified work. **Human work:** choose priorities and accept gates. **Output:** a short current-state document and small next-task queue.

Prefer one source of truth per decision. Replace obsolete statements instead of appending contradictory notes. At the end of each work session record the build/commit, what changed, what was tested, what remains broken, and the next concrete task.

## 15. Release, store materials, and maintenance

**Input:** a tested build, rights ledger, accessibility facts, and actual screenshots. **AI work:** draft store text, check packaging, prepare release notes, and triage reproducible reports. **Human work:** verify claims, approve publishing, complete current platform disclosures, and test the install. **Output:** release candidate, recovery plan, support process, and approved store materials.

Steam's current Content Survey addresses AI-generated content shipped to and consumed by players and distinguishes pre-generated from live-generated content. It says development-efficiency tooling is not the focus of that section. Complete the actual current survey rather than copying an old blanket “all AI code” explanation. [S26]
