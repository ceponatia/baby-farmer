using System.Collections.Generic;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Tracks the ground-anchored sprites currently present in the scene, so
    /// <see cref="GroundSortController"/> can sort all of them without a hard
    /// reference to every drawable in the scene. Sprites register themselves
    /// on enable and unregister on disable/destroy; this is intentionally a
    /// small runtime registry, not a generic rendering framework — it
    /// deliberately mirrors the existing
    /// <see cref="InteractionCandidateRegistry"/> pattern.
    /// </summary>
    public static class GroundSortRegistry
    {
        private static readonly List<GroundSortedSprite> Sprites = new List<GroundSortedSprite>();

        private static int nextStableId;

        public static IReadOnlyList<GroundSortedSprite> All => Sprites;

        /// <summary>
        /// Registers a sprite and hands out its stable sort identity.
        ///
        /// <para>Ids are monotonically increasing and never reused, which is
        /// what makes <see cref="BabyFarmer.Domain.GroundSortComparer"/> a
        /// total order: every currently registered sprite holds a distinct
        /// id, so two sprites standing on the exact same ground point still
        /// sort deterministically instead of swapping places frame to
        /// frame.</para>
        /// </summary>
        /// <returns>The stable id assigned to this registration.</returns>
        public static int Register(GroundSortedSprite sprite)
        {
            if (sprite == null)
            {
                return -1;
            }

            if (!Sprites.Contains(sprite))
            {
                Sprites.Add(sprite);
            }

            return nextStableId++;
        }

        public static void Unregister(GroundSortedSprite sprite)
        {
            Sprites.Remove(sprite);
        }
    }
}
