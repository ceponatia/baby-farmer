using System.Collections.Generic;
using BabyFarmer.Domain;
using NUnit.Framework;

namespace BabyFarmer.Domain.Tests
{
    /// <summary>
    /// EditMode tests for the one authoritative interaction-targeting rule
    /// (issue #6): a candidate is only ever resolved when it is within range
    /// and in front of the player's facing direction, at most one target is
    /// ever produced, and interacting against no target never mutates state.
    /// </summary>
    public sealed class InteractionTargetResolverTests
    {
        private const float Range = 1.5f;
        private const float HalfWidth = 0.5f;

        private sealed class FakeCandidate : IInteractionCandidate
        {
            public FakeCandidate(string name, WorldPosition2D position)
            {
                DisplayName = name;
                Position = position;
            }

            public WorldPosition2D Position { get; }

            public string DisplayName { get; }

            public int InteractCount { get; private set; }

            public bool TryInteract()
            {
                InteractCount++;
                return true;
            }
        }

        [Test]
        public void Resolve_SelectsCandidateInRangeAndFacing()
        {
            var origin = new WorldPosition2D(0f, 0f);
            var candidate = new FakeCandidate("Dummy", new WorldPosition2D(0f, 1f));

            var resolution = InteractionTargetResolver.Resolve(
                origin, FacingDirection.North, Range, HalfWidth, new IInteractionCandidate[] { candidate });

            Assert.IsTrue(resolution.IsValid);
            Assert.AreSame(candidate, resolution.Target);
        }

        [Test]
        public void Resolve_ReturnsNoneWhenCandidateOutOfRange()
        {
            var origin = new WorldPosition2D(0f, 0f);
            var candidate = new FakeCandidate("Dummy", new WorldPosition2D(0f, Range + 1f));

            var resolution = InteractionTargetResolver.Resolve(
                origin, FacingDirection.North, Range, HalfWidth, new IInteractionCandidate[] { candidate });

            Assert.IsFalse(resolution.IsValid);
            Assert.IsNull(resolution.Target);
        }

        [Test]
        public void Resolve_ReturnsNoneWhenCandidateIsNotInFacingDirection()
        {
            var origin = new WorldPosition2D(0f, 0f);
            // Directly south of the player, who is facing north.
            var candidate = new FakeCandidate("Dummy", new WorldPosition2D(0f, -1f));

            var resolution = InteractionTargetResolver.Resolve(
                origin, FacingDirection.North, Range, HalfWidth, new IInteractionCandidate[] { candidate });

            Assert.IsFalse(resolution.IsValid);
        }

        [Test]
        public void Resolve_ReturnsNoneWhenCandidateIsOutsideLateralWidth()
        {
            var origin = new WorldPosition2D(0f, 0f);
            // In front (north), but well off to the side of the bounded lane.
            var candidate = new FakeCandidate("Dummy", new WorldPosition2D(HalfWidth + 1f, 1f));

            var resolution = InteractionTargetResolver.Resolve(
                origin, FacingDirection.North, Range, HalfWidth, new IInteractionCandidate[] { candidate });

            Assert.IsFalse(resolution.IsValid);
        }

        [Test]
        public void Resolve_SelectsExactlyOneTarget_TheNearest_WhenMultipleQualify()
        {
            var origin = new WorldPosition2D(0f, 0f);
            var near = new FakeCandidate("Near", new WorldPosition2D(0f, 0.5f));
            var far = new FakeCandidate("Far", new WorldPosition2D(0f, 1.2f));

            var resolution = InteractionTargetResolver.Resolve(
                origin, FacingDirection.North, Range, HalfWidth, new IInteractionCandidate[] { far, near });

            Assert.IsTrue(resolution.IsValid);
            Assert.AreSame(near, resolution.Target);
        }

        [Test]
        public void AttemptInteract_OnNoTarget_PerformsNoMutation()
        {
            var performed = InteractionTargetResolver.AttemptInteract(InteractionTargetResolution.None);

            Assert.IsFalse(performed);
        }

        [Test]
        public void AttemptInteract_OnResolvedTarget_InteractsExactlyOnce()
        {
            var candidate = new FakeCandidate("Dummy", new WorldPosition2D(0f, 1f));
            var resolution = InteractionTargetResolution.For(candidate);

            var performed = InteractionTargetResolver.AttemptInteract(resolution);

            Assert.IsTrue(performed);
            Assert.AreEqual(1, candidate.InteractCount);
        }

        [Test]
        public void Resolve_ReturnsNoneWhenNoCandidates()
        {
            var origin = new WorldPosition2D(0f, 0f);

            var resolution = InteractionTargetResolver.Resolve(
                origin, FacingDirection.North, Range, HalfWidth, new List<IInteractionCandidate>());

            Assert.IsFalse(resolution.IsValid);
        }
    }
}
