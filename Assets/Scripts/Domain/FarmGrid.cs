namespace BabyFarmer.Domain
{
    /// <summary>
    /// The world-scale constant locked by issue #21: one farm cell is one
    /// world unit, which at the locked 32 pixels-per-unit import setting is
    /// exactly one 32x32 terrain tile.
    ///
    /// <para>This is deliberately the single place the cell size is written
    /// down. Presentation does not get to hold its own copy, and no engine
    /// asset derives a different scale: sprite import settings realize this
    /// constant (32 px / 32 PPU = 1 unit), they do not redefine it.</para>
    /// </summary>
    public static class FarmGrid
    {
        /// <summary>Edge length of one farm cell, in world units.</summary>
        public const float CellSize = 1f;
    }
}
