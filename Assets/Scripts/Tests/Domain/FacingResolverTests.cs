using BabyFarmer.Domain;
using NUnit.Framework;

namespace BabyFarmer.Domain.Tests
{
    /// <summary>
    /// EditMode tests for the pure facing-direction rule used by player
    /// movement (issue #6): facing tracks the last non-zero movement intent
    /// and persists once the intent returns to zero.
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

        [Test]
        public void Resolve_PrioritizesVerticalIntentOverHorizontal()
        {
            var result = FacingResolver.Resolve(FacingDirection.West, 1f, 1f);

            Assert.AreEqual(FacingDirection.North, result);
        }

        [Test]
        public void ToUnitVector_ReturnsCardinalUnitVectors()
        {
            Assert.AreEqual((0f, 1f), FacingResolver.ToUnitVector(FacingDirection.North));
            Assert.AreEqual((0f, -1f), FacingResolver.ToUnitVector(FacingDirection.South));
            Assert.AreEqual((1f, 0f), FacingResolver.ToUnitVector(FacingDirection.East));
            Assert.AreEqual((-1f, 0f), FacingResolver.ToUnitVector(FacingDirection.West));
        }
    }
}
