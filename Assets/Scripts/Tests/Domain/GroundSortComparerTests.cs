using System.Collections.Generic;
using BabyFarmer.Domain;
using NUnit.Framework;

namespace BabyFarmer.Domain.Tests
{
    /// <summary>
    /// EditMode tests for the ground-contact draw-order rule locked by issue
    /// #21: AnchorY (world Z) descending, then AnchorX ascending, then
    /// StableId ascending — a total order with no unresolved ties.
    ///
    /// The tie cases are the point of these tests. An ordering that merely
    /// "usually looks right" but leaves exact ties unresolved is what
    /// produces frame-to-frame draw-order flicker, which is exactly what
    /// docs/03-architecture.md's "stable tie-breaker" requirement forbids.
    /// </summary>
    public sealed class GroundSortComparerTests
    {
        [Test]
        public void Compare_OrdersFurtherNorthFirst()
        {
            // Larger AnchorY (further north) draws first, so it ends up
            // behind whatever is south of it.
            var north = new GroundSortKey(5f, 0f, 1);
            var south = new GroundSortKey(-5f, 0f, 2);

            Assert.Less(GroundSortComparer.Instance.Compare(north, south), 0);
            Assert.Greater(GroundSortComparer.Instance.Compare(south, north), 0);
        }

        [Test]
        public void Compare_ExactAnchorYTie_IsBrokenByAnchorXAscending()
        {
            var west = new GroundSortKey(2.5f, -1f, 99);
            var east = new GroundSortKey(2.5f, 1f, 1);

            Assert.AreEqual(
                west.AnchorY,
                east.AnchorY,
                "Precondition: this case only means anything if AnchorY is exactly tied.");

            Assert.Less(
                GroundSortComparer.Instance.Compare(west, east),
                0,
                "With AnchorY tied, the smaller AnchorX must sort first regardless of StableId.");
            Assert.Greater(GroundSortComparer.Instance.Compare(east, west), 0);
        }

        [Test]
        public void Compare_ExactAnchorYAndAnchorXTie_IsBrokenByStableIdAscending()
        {
            // Two things standing on the exact same ground point: the only
            // remaining discriminator is the registration identity.
            var first = new GroundSortKey(2.5f, 2.5f, 3);
            var second = new GroundSortKey(2.5f, 2.5f, 7);

            Assert.Less(GroundSortComparer.Instance.Compare(first, second), 0);
            Assert.Greater(GroundSortComparer.Instance.Compare(second, first), 0);
        }

        [Test]
        public void Compare_IsZeroOnlyForAnIdenticalKey()
        {
            var key = new GroundSortKey(1f, 2f, 3);

            Assert.AreEqual(0, GroundSortComparer.Instance.Compare(key, key));
            Assert.AreNotEqual(0, GroundSortComparer.Instance.Compare(key, new GroundSortKey(1f, 2f, 4)));
            Assert.AreNotEqual(0, GroundSortComparer.Instance.Compare(key, new GroundSortKey(1f, 2.0001f, 3)));
            Assert.AreNotEqual(0, GroundSortComparer.Instance.Compare(key, new GroundSortKey(1.0001f, 2f, 3)));
        }

        [Test]
        public void Sort_ProducesTheFullDocumentedOrder()
        {
            // Deliberately seeded out of order, and containing both a
            // Y-only tie and a (Y, X) tie, so a comparer that dropped either
            // tie-breaker could not produce this exact sequence.
            var keys = new List<GroundSortKey>
            {
                new GroundSortKey(0f, 0f, 4),      // southmost          -> index 4
                new GroundSortKey(2.5f, 3f, 2),    // (Y, X) tie, id 2   -> index 3
                new GroundSortKey(2.5f, 0f, 5),    // Y tie, smallest X  -> index 1
                new GroundSortKey(2.5f, 3f, 0),    // (Y, X) tie, id 0   -> index 2
                new GroundSortKey(9f, 0f, 3)       // northmost          -> index 0
            };

            keys.Sort(GroundSortComparer.Instance);

            var stableIdOrder = new List<int>();
            foreach (var key in keys)
            {
                stableIdOrder.Add(key.StableId);
            }

            // AnchorY desc: 9 first; then the three at 2.5 ordered by
            // AnchorX asc (0 before 3), and within AnchorX 3 by StableId asc
            // (0 before 2); then 0 last.
            CollectionAssert.AreEqual(new[] { 3, 5, 0, 2, 4 }, stableIdOrder);
        }

        [Test]
        public void Sort_IsDeterministicRegardlessOfInputOrder()
        {
            // List.Sort is not a stable sort, so a comparer with unresolved
            // ties would let the input order leak into the output. A total
            // order must give the same result from any permutation.
            var ascending = new List<GroundSortKey>
            {
                new GroundSortKey(1f, 1f, 1),
                new GroundSortKey(1f, 1f, 2),
                new GroundSortKey(1f, 1f, 3),
                new GroundSortKey(1f, 1f, 4)
            };
            var descending = new List<GroundSortKey>
            {
                new GroundSortKey(1f, 1f, 4),
                new GroundSortKey(1f, 1f, 3),
                new GroundSortKey(1f, 1f, 2),
                new GroundSortKey(1f, 1f, 1)
            };

            ascending.Sort(GroundSortComparer.Instance);
            descending.Sort(GroundSortComparer.Instance);

            for (var i = 0; i < ascending.Count; i++)
            {
                Assert.AreEqual(i + 1, ascending[i].StableId);
                Assert.AreEqual(i + 1, descending[i].StableId);
            }
        }
    }
}
