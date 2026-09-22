using System.Collections.Generic;
using BabyFarmer.Domain;
using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Applies the ground-contact draw order to every registered
    /// <see cref="GroundSortedSprite"/> once per frame (issue #21). One
    /// instance belongs in the scene.
    ///
    /// <para>This class only adapts real Transform data into Domain inputs
    /// and writes the result back onto the renderers — it never itself
    /// decides what order things draw in. The ordering rule lives in
    /// <see cref="GroundSortComparer"/>, in the engine-independent Domain
    /// assembly, exactly as interaction targeting lives in
    /// <see cref="InteractionTargetResolver"/>.</para>
    ///
    /// <para>Runs in <c>LateUpdate</c> so it sees final positions for the
    /// frame, after movement and physics interpolation have been applied.
    /// The sorting layer of each renderer is authored in the scene and is not
    /// touched here; only <c>sortingOrder</c> within that layer is assigned:
    /// index 0 is the furthest north (drawn first, so behind), and higher
    /// orders draw later, in front.</para>
    /// </summary>
    public sealed class GroundSortController : MonoBehaviour
    {
        private readonly List<GroundSortKey> keys = new List<GroundSortKey>();
        private readonly Dictionary<int, SpriteRenderer> renderersById = new Dictionary<int, SpriteRenderer>();

        private void LateUpdate()
        {
            ApplySortOrder();
        }

        /// <summary>
        /// Rebuilds and applies the draw order immediately. Exposed so a test
        /// can assert the order for a position it just set, without depending
        /// on how many frames the test runner happens to pump.
        /// </summary>
        public void ApplySortOrder()
        {
            keys.Clear();
            renderersById.Clear();

            var registered = GroundSortRegistry.All;
            for (var i = 0; i < registered.Count; i++)
            {
                var sorted = registered[i];
                if (sorted == null)
                {
                    continue;
                }

                var renderer = sorted.TargetRenderer;
                if (renderer == null)
                {
                    continue;
                }

                // The anchor Transform is the ground-contact point; its world
                // Z is the Domain key's AnchorY, matching WorldPosition2D's
                // existing X/Y-means-X/Z convention.
                var anchor = sorted.transform.position;
                keys.Add(new GroundSortKey(anchor.z, anchor.x, sorted.StableId));
                renderersById[sorted.StableId] = renderer;
            }

            keys.Sort(GroundSortComparer.Instance);

            for (var i = 0; i < keys.Count; i++)
            {
                if (renderersById.TryGetValue(keys[i].StableId, out var renderer))
                {
                    renderer.sortingOrder = i;
                }
            }
        }
    }
}
