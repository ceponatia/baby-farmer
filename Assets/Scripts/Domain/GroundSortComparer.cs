using System.Collections.Generic;

namespace BabyFarmer.Domain
{
    /// <summary>
    /// The one authoritative draw-order rule for ground-anchored visuals
    /// (issue #21): order by <see cref="GroundSortKey.AnchorY"/>
    /// <b>descending</b> (further north draws first, so it ends up behind),
    /// then by <see cref="GroundSortKey.AnchorX"/> ascending, then by
    /// <see cref="GroundSortKey.StableId"/> ascending.
    ///
    /// <para>The last term makes this a <b>total order</b>: because
    /// StableId is unique per registered drawable, no two distinct keys ever
    /// compare equal. That is the "stable tie-breaker" docs/03-architecture.md
    /// requires, and it is why this is a computed sort rather than the
    /// engine's automatic transparency sort, which has none.</para>
    ///
    /// <para>This type owns no state and has no UnityEngine dependency,
    /// matching the existing <see cref="InteractionTargetResolver"/> /
    /// <see cref="SlugFormatter"/> pattern in this assembly.</para>
    /// </summary>
    public sealed class GroundSortComparer : IComparer<GroundSortKey>
    {
        /// <summary>Shared instance; the comparer is stateless.</summary>
        public static readonly GroundSortComparer Instance = new GroundSortComparer();

        public int Compare(GroundSortKey first, GroundSortKey second)
        {
            // Descending: the larger AnchorY (further north) sorts earlier.
            var byAnchorY = second.AnchorY.CompareTo(first.AnchorY);
            if (byAnchorY != 0)
            {
                return byAnchorY;
            }

            var byAnchorX = first.AnchorX.CompareTo(second.AnchorX);
            if (byAnchorX != 0)
            {
                return byAnchorX;
            }

            return first.StableId.CompareTo(second.StableId);
        }
    }
}
