namespace BabyFarmer.Domain
{
    /// <summary>
    /// A plain, engine-independent 2D world position used by interaction-targeting
    /// logic. Presentation adapts real Unity <c>Transform</c> positions into this
    /// type; this assembly must never reference UnityEngine types directly
    /// (enforced by "noEngineReferences" in BabyFarmer.Domain.asmdef).
    /// </summary>
    public readonly struct WorldPosition2D
    {
        public WorldPosition2D(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; }

        public float Y { get; }

        /// <summary>Vector from this position to <paramref name="other"/>.</summary>
        public WorldPosition2D VectorTo(WorldPosition2D other)
        {
            return new WorldPosition2D(other.X - X, other.Y - Y);
        }
    }
}
