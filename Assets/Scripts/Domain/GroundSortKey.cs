namespace BabyFarmer.Domain
{
    /// <summary>
    /// The ground-contact sort key for one drawable actor or object (issue
    /// #21). Sorting is decided from the ground anchor — the point where the
    /// thing actually touches the ground — never from the top of its sprite
    /// or its transparent canvas bounds, per the "Spatial model" section of
    /// docs/03-architecture.md.
    ///
    /// <para><b>Axis convention.</b> As in <see cref="WorldPosition2D"/>,
    /// X and Y here are the two ground-plane axes: <see cref="AnchorX"/> is
    /// the engine's world X (east/west) and <see cref="AnchorY"/> is the
    /// engine's world <b>Z</b> (north/south). Y is not height; nothing in
    /// this assembly has a height axis.</para>
    ///
    /// <para><see cref="StableId"/> is an identity assigned once per
    /// registered drawable by Presentation. It exists so that
    /// <see cref="GroundSortComparer"/> is a total order with no unresolved
    /// ties: two things standing on the exact same ground point still get a
    /// deterministic, frame-to-frame stable draw order instead of flickering.
    /// This assembly must never reference UnityEngine types directly
    /// (enforced by "noEngineReferences" in BabyFarmer.Domain.asmdef).</para>
    /// </summary>
    public readonly struct GroundSortKey
    {
        public GroundSortKey(float anchorY, float anchorX, int stableId)
        {
            AnchorY = anchorY;
            AnchorX = anchorX;
            StableId = stableId;
        }

        /// <summary>Ground anchor along the north/south axis (the engine's world Z).</summary>
        public float AnchorY { get; }

        /// <summary>Ground anchor along the east/west axis (the engine's world X).</summary>
        public float AnchorX { get; }

        /// <summary>Stable per-drawable identity used as the final tie-breaker.</summary>
        public int StableId { get; }

        public override string ToString()
        {
            return $"GroundSortKey(anchorY: {AnchorY}, anchorX: {AnchorX}, stableId: {StableId})";
        }
    }
}
