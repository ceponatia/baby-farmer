using BabyFarmer.Domain;
using NUnit.Framework;

namespace BabyFarmer.Domain.Tests
{
    /// <summary>
    /// EditMode tests for the pure facing-direction rule used by player
    /// movement: facing tracks the last non-zero movement intent and
    /// persists once the intent returns to zero (issue #6). For diagonal
    /// intent (both axes non-zero), the horizontal axis takes priority for
    /// facing (issue #9).
    /// </summary>
    public sealed class FacingResolverTests
    {
        [TestCase(0f, 1f, FacingDirection.North)]
        [TestCase(0f, -1f, FacingDirection.South)]
        [TestCase(1f, 0f, FacingDirection.East)]
        [TestCase(-1f, 0f, FacingDirection.West)]
        public void Resolve_FollowsNonZeroIntent(float intentX, float intentY, FacingDirection expected)
        {
            var result = FacingResolver.Resolve(FacingDirection.South, intentX, intentY);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Resolve_PersistsCurrentFacingWhenIntentStops()
        {
            var result = FacingResolver.Resolve(FacingDirection.East, 0f, 0f);

            Assert.AreEqual(FacingDirection.East, result);
        }

        [TestCase(1f, 1f, FacingDirection.East, TestName = "Resolve_PrioritizesHorizontalIntentOverVertical(NorthEast)")]
        [TestCase(-1f, 1f, FacingDirection.West, TestName = "Resolve_PrioritizesHorizontalIntentOverVertical(NorthWest)")]
        [TestCase(1f, -1f, FacingDirection.East, TestName = "Resolve_PrioritizesHorizontalIntentOverVertical(SouthEast)")]
        [TestCase(-1f, -1f, FacingDirection.West, TestName = "Resolve_PrioritizesHorizontalIntentOverVertical(SouthWest)")]
        public void Resolve_PrioritizesHorizontalIntentOverVertical(float intentX, float intentY, FacingDirection expected)
        {
            var result = FacingResolver.Resolve(FacingDirection.South, intentX, intentY);

            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Resolve_DiagonalIntentStopping_PersistsLastResolvedFacing()
        {
            var facingWhileMoving = FacingResolver.Resolve(FacingDirection.South, 1f, 1f);
            var facingAfterStopping = FacingResolver.Resolve(facingWhileMoving, 0f, 0f);

            Assert.AreEqual(FacingDirection.East, facingWhileMoving);
            Assert.AreEqual(FacingDirection.East, facingAfterStopping);
        }

        [TestCase(FacingDirection.North)]
        [TestCase(FacingDirection.East)]
        public void Resolve_ZeroIntent_PersistsFacingRegardlessOfStartingFacing(FacingDirection current)
        {
            // Confirms the zero-intent persistence rule holds from multiple
            // starting facings, not just one (Resolve_PersistsCurrentFacingWhenIntentStops
            // covers a single case). Note this only exercises FacingResolver
            // itself with a (0, 0) intent; it does not exercise
            // PlayerInputReader's Input.GetAxisRaw opposite-key cancellation
            // (e.g. Left+Right both held), which happens upstream of this
            // resolver and isn't reachable from a Domain-level test.
            var result = FacingResolver.Resolve(current, 0f, 0f);

            Assert.AreEqual(current, result);
        }

        [Test]
        public void ToUnitVector_ReturnsCardinalUnitVectors()
        {
            Assert.AreEqual((0f, 1f), FacingResolver.ToUnitVector(FacingDirection.North));
            Assert.AreEqual((0f, -1f), FacingResolver.ToUnitVector(FacingDirection.South));
            Assert.AreEqual((1f, 0f), FacingResolver.ToUnitVector(FacingDirection.East));
            Assert.AreEqual((-1f, 0f), FacingResolver.ToUnitVector(FacingDirection.West));
        }

        [Test]
        public void ToCellStep_ReturnsIntegerCardinalSteps()
        {
            // The discrete counterpart of ToUnitVector (issue #21): the same
            // Y-means-world-Z convention, but integer cell steps.
            Assert.AreEqual((0, 1), FacingResolver.ToCellStep(FacingDirection.North));
            Assert.AreEqual((0, -1), FacingResolver.ToCellStep(FacingDirection.South));
            Assert.AreEqual((1, 0), FacingResolver.ToCellStep(FacingDirection.East));
            Assert.AreEqual((-1, 0), FacingResolver.ToCellStep(FacingDirection.West));
        }

        [TestCase(FacingDirection.North)]
        [TestCase(FacingDirection.South)]
        [TestCase(FacingDirection.East)]
        [TestCase(FacingDirection.West)]
        public void ToCellStep_AgreesWithToUnitVectorOnEveryFacing(FacingDirection facing)
        {
            // The two representations must never drift apart: a facing that
            // moves east continuously must also step east in cell space.
            var (unitX, unitY) = FacingResolver.ToUnitVector(facing);
            var (stepX, stepY) = FacingResolver.ToCellStep(facing);

            Assert.AreEqual(unitX, (float)stepX, $"Cell step X disagrees with unit vector X for {facing}.");
            Assert.AreEqual(unitY, (float)stepY, $"Cell step Y disagrees with unit vector Y for {facing}.");
        }
    }
}
