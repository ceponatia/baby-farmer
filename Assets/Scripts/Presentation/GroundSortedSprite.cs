using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Marks one anchor GameObject as taking part in ground-contact draw
    /// ordering (issue #21). Attach it to the <b>anchor</b> object — the one
    /// whose Transform position is the ground-contact point — and point it at
    /// the child <see cref="UnityEngine.SpriteRenderer"/> that actually draws
    /// the art.
    ///
    /// <para>That split is the whole point: the anchor sits at world y=0 on
    /// the ground, while the sprite above it may be far taller than the
    /// object's collision footprint. Sorting reads the anchor, never the
    /// sprite bounds.</para>
    ///
    /// <para>This component makes no ordering decision itself; it only
    /// registers and exposes its renderer.
    /// <see cref="GroundSortController"/> applies the order, using the
    /// engine-independent rule in
    /// <see cref="BabyFarmer.Domain.GroundSortComparer"/>.</para>
    /// </summary>
    public sealed class GroundSortedSprite : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer targetRenderer;

        /// <summary>The renderer whose <c>sortingOrder</c> is assigned for this anchor.</summary>
        public SpriteRenderer TargetRenderer => targetRenderer;

        /// <summary>
        /// Stable identity handed out by <see cref="GroundSortRegistry"/> at
        /// registration, used as the final sort tie-breaker.
        /// </summary>
        public int StableId { get; private set; }

        private void OnEnable()
        {
            StableId = GroundSortRegistry.Register(this);
        }

        private void OnDisable()
        {
            GroundSortRegistry.Unregister(this);
        }
    }
}
