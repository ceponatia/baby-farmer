namespace BabyFarmer.Domain
{
    /// <summary>
    /// Pure, engine-independent facing-direction logic. Presentation feeds this
    /// the player's current movement intent every frame; it never needs to know
    /// about <c>UnityEngine.Input</c> or physics.
    /// </summary>
    public static class FacingResolver
    {
        /// <summary>
        /// Determines the facing direction that should follow from a movement
        /// intent, given the actor's current facing.
        ///
        /// Facing persists the last non-zero movement direction: when the
        /// intent is zero (the actor is not currently trying to move), the
        /// current facing is returned unchanged rather than reset.
        ///
        /// When both axes are non-zero (a diagonal intent), the horizontal
        /// axis takes priority for facing purposes: a positive
        /// <paramref name="intentX"/> resolves to East, a negative one
        /// resolves to West, regardless of the vertical component. This is
        /// the accepted diagonal facing rule (issue #9): diagonal movement
        /// still moves along both axes, but the displayed facing collapses
        /// to left/right since only the four cardinal facings exist. Pure
        /// single-axis (cardinal) intent is unaffected and keeps its
        /// existing behavior.
        /// </summary>
        public static FacingDirection Resolve(FacingDirection current, float intentX, float intentY)
        {
            if (intentX > 0f)
            {
                return FacingDirection.East;
            }

            if (intentX < 0f)
            {
                return FacingDirection.West;
            }

            if (intentY > 0f)
            {
                return FacingDirection.North;
            }

            if (intentY < 0f)
            {
                return FacingDirection.South;
            }

            return current;
        }

        /// <summary>Unit direction vector for a facing, as plain (x, y) components.</summary>
        public static (float X, float Y) ToUnitVector(FacingDirection facing)
        {
            switch (facing)
            {
                case FacingDirection.North:
                    return (0f, 1f);
                case FacingDirection.South:
                    return (0f, -1f);
                case FacingDirection.East:
                    return (1f, 0f);
                case FacingDirection.West:
                    return (-1f, 0f);
                default:
                    return (0f, 0f);
            }
        }
    }
}
