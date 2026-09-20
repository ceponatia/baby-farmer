using System.Collections.Generic;

namespace BabyFarmer.Domain
{
    /// <summary>
    /// The one authoritative interaction-targeting rule for the movement and
    /// interaction sandbox (issue #6): resolves at most one interaction
    /// candidate that lies within a bounded rectangular area directly in
    /// front of the player, and performs an interaction against it only when
    /// explicitly requested. This type owns no state and has no UnityEngine
    /// dependency, matching the existing SlugFormatter pattern in this
    /// assembly.
    /// </summary>
    public static class InteractionTargetResolver
    {
        /// <summary>
        /// Resolves at most one target from <paramref name="candidates"/>.
        ///
        /// A candidate is in the bounded area in front of the player when its
        /// offset from <paramref name="origin"/>, projected onto the facing
        /// direction, is strictly positive and no greater than
        /// <paramref name="range"/>, and its perpendicular (lateral) offset
        /// from the facing axis is no greater than
        /// <paramref name="halfWidth"/>. A candidate outside that area is
        /// never selected.
        ///
        /// When more than one candidate qualifies, the nearest one (by
        /// distance along the facing direction) is selected, so exactly one
        /// target is ever returned.
        /// </summary>
        public static InteractionTargetResolution Resolve(
            WorldPosition2D origin,
            FacingDirection facing,
            float range,
            float halfWidth,
            IEnumerable<IInteractionCandidate> candidates)
        {
            if (candidates == null)
            {
                return InteractionTargetResolution.None;
            }

            var (forwardX, forwardY) = FacingResolver.ToUnitVector(facing);

            IInteractionCandidate nearest = null;
            var nearestForwardDistance = float.PositiveInfinity;

            foreach (var candidate in candidates)
            {
                if (candidate == null)
                {
                    continue;
                }

                var offset = origin.VectorTo(candidate.Position);
                var forwardDistance = (offset.X * forwardX) + (offset.Y * forwardY);
                if (forwardDistance <= 0f || forwardDistance > range)
                {
                    continue;
                }

                var lateralDistance = (offset.X * forwardY) - (offset.Y * forwardX);
                if (lateralDistance < -halfWidth || lateralDistance > halfWidth)
                {
                    continue;
                }

                if (forwardDistance < nearestForwardDistance)
                {
                    nearest = candidate;
                    nearestForwardDistance = forwardDistance;
                }
            }

            return nearest == null ? InteractionTargetResolution.None : InteractionTargetResolution.For(nearest);
        }

        /// <summary>
        /// Performs the interaction against an already-resolved target. A
        /// resolution with no target (an invalid/empty target) is a safe
        /// no-op: it never mutates gameplay state.
        /// </summary>
        /// <returns>Whether an interaction was actually performed.</returns>
        public static bool AttemptInteract(InteractionTargetResolution resolution)
        {
            if (!resolution.IsValid)
            {
                return false;
            }

            return resolution.Target.TryInteract();
        }
    }
}
