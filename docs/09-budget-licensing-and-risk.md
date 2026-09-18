# 09 · Budget, Licensing, and Risk

## Spend on a proven bottleneck

Do not buy an engine asset bundle, several AI subscriptions, a music platform, and a large art plan before the feasibility trial. Reuse an existing coding agent first. Pay for one small asset workflow test and measure whether the output is usable.

## Cost categories

| Category | Planning treatment |
|---|---|
| Engine | Unity Personal eligibility depends on its current revenue/funding terms; check the applicable threshold and entity rules. [S05] |
| Editor-agent bridge | Unity's current FAQ describes MCP as free; the in-editor assistant has separate subscription/credit terms. [S02] |
| PixelLab | Check the actual account, selected interface/model, included generations, credit pricing, and retries. Do not equate an advertised image limit with finished animated characters. [S11, S12] |
| Art editing | Confirm Aseprite purchase/build/license arrangements; do not budget it as a bundled PixelLab entitlement. [S14, S17] |
| Audio | Use owned/licensed material first; verify the selected generator's commercial terms and plan. [S25] |
| Storage/builds | Account for source art, backups, binary versioning, build artifacts, and any CI activation costs. |
| Specialist support | Reserve optional targeted cleanup for the player master/action set, UI, or one music theme. |

GameMaker's current pricing page lists a $99.99 one-time commercial license for its non-console Professional path; console export requires a different tier. Defold's product page describes a no-fee engine model. Those are decision inputs, not reasons to select a tool that fails the workflow trial. [S21, S23]

PixelLab's public REST catalog gives model-specific estimates rather than one universal price. Account-specific MCP allowances and the effective price of the intended batch have not been verified here. Confirm them before authorizing a run. [S12]

## Cost per accepted asset

```text
effective asset cost =
  (generation charges + retry charges + paid cleanup + review-time valuation)
  / number of assets that pass final acceptance
```

For a hypothetical batch costing $12 with six accepted assets, the generation-only cost is $2 per accepted asset—not whatever the vendor quotes per image. This is arithmetic for planning, not a PixelLab price estimate.

Count animation directions, action variants, outfit variants, revision cycles, and human review separately. A “character” may be dozens of accepted clips and hundreds of frame cells.

## Agent spending policy

Set a user-approved cap per trial and per batch. Start with a small sample, display estimated cost and scope, and require approval before an expensive mode or bulk regeneration. Keep failed attempts in the ledger so poor acceptance rates are visible.

Agents must not purchase subscriptions, spend unapproved credits, upload private references to a new provider, or publish a build merely because a prompt says “finish the task.” Approval of coding work is not blanket authorization for spending or public release.

## Rights and provenance

PixelLab's FAQ permits commercial use of generated images and says not to train new models with them. Preserve the actual applicable terms and generation date. That permission does not by itself establish exclusivity or clear every possible third-party right. [S13]

Maintain provenance for art, music, sound effects, fonts, code dependencies, purchased packs, and reference inputs. Store the source, license, purchase/plan evidence where needed, allowed uses, attribution requirements, modifications, and the exact shipped files.

Use original game identities and owned or properly licensed references. Review final store materials and shipped content independently of the art tool's marketing claims. Seek qualified advice for an unclear release-rights issue rather than having an agent declare that generated material is automatically safe.

Steam requires an appropriate Content Survey response for AI-generated player-facing content, with separate treatment of live-generated content. Keep enough records to answer accurately at release. [S26]

## Risk register

| Risk | Early indicator | Response |
|---|---|---|
| Inconsistent character identity | Facings/actions drift from the master | Golden set; smaller batches; targeted repair; alternate art source |
| Tool-action failure | Tool and hand detach; contact misses target | Action contracts; reference poses; simplify motion; specialist correction |
| Scope growth | Every feature adds a new art/system pipeline | Slice caps; explicit tradeoffs; one differentiator |
| Agent architecture sprawl | Large abstractions before the farming loop | Small feature specs; review boundaries; prohibit unsolicited rewrites |
| Save corruption/duplication | Reload changes rewards or machine output | Transaction tests; versioned saves; backup/recovery fixtures |
| Vendor changes | Endpoint controls, prices, or terms shift | Pin integration assumptions; preserve outputs; recheck before new batches |
| Unity beta/agent restrictions | Workflow depends on a changing bridge | Official route; verified versions; manual/editor-tool fallback within applicable terms |
| Provider lock-in | Only temporary links or provider IDs exist | Download accepted assets and store neutral metadata |
| UI/accessibility debt | Tiny text or mouse-only interactions | Early input/resize tests and scalable UI |
| Audio inconsistency | Effects dominate music or loops click | Cue sheet, in-game mix review, licensed fallback |
| Production burnout | More systems than playable content | Gate-based development; keep a small enjoyable build |

## Security and data handling

Keep API keys outside source control and the shipped executable. Separate candidate/reference media from public build assets. Review agent tool permissions; keep local editor access local unless a secure remote configuration is deliberately established. Do not expose a desktop MCP bridge to the public internet as a convenience.

Treat downloaded packages and generated scripts as code that needs review. A successful import is not evidence that an untrusted editor extension is safe.
