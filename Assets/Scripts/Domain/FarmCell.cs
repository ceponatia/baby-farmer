using System;

namespace BabyFarmer.Domain
{
    /// <summary>
    /// A discrete farm-grid cell (issue #21). Cells are identified by their
    /// <b>centres</b>, which sit on integer world coordinates: cell
    /// <c>(i, j)</c> covers the half-open world area
    /// <c>[i - 0.5, i + 0.5) x [j - 0.5, j + 0.5)</c> scaled by
    /// <see cref="FarmGrid.CellSize"/>.
    ///
    /// <para><b>Axis convention.</b> As in <see cref="WorldPosition2D"/>,
    /// X and Y here are the two ground-plane axes: <see cref="X"/> is the
    /// engine's world X (east/west) and <see cref="Y"/> is the engine's world
    /// <b>Z</b> (north/south). Y is not height. This assembly must never
    /// reference UnityEngine types directly (enforced by "noEngineReferences"
    /// in BabyFarmer.Domain.asmdef).</para>
    ///
    /// <para>This is the cell-coordinate contract only. It is deliberately
    /// separate from the continuous rectangle
    /// <see cref="InteractionTargetResolver"/> uses for today's interaction
    /// targeting; the two mechanisms are not unified here.</para>
    /// </summary>
    public readonly struct FarmCell : IEquatable<FarmCell>
    {
        public FarmCell(int x, int y)
        {
            X = x;
            Y = y;
        }

        /// <summary>Cell index along the east/west axis (the engine's world X).</summary>
        public int X { get; }

        /// <summary>Cell index along the north/south axis (the engine's world Z).</summary>
        public int Y { get; }

        /// <summary>
        /// The cell containing a continuous world position. Exact cell
        /// boundaries (a coordinate ending in .5) belong to the higher cell,
        /// consistently in both the positive and negative direction, because
        /// the rule is <c>floor(v / CellSize + 0.5)</c> — a true floor, not
        /// <c>Math.Round</c>, whose banker's rounding would send 0.5 and 1.5
        /// to different sides and make the grid non-uniform.
        /// </summary>
        public static FarmCell FromWorld(WorldPosition2D position)
        {
            return new FarmCell(ToCellAxis(position.X), ToCellAxis(position.Y));
        }

        /// <summary>The world position of a cell's centre.</summary>
        public static WorldPosition2D CenterOf(FarmCell cell)
        {
            return new WorldPosition2D(cell.X * FarmGrid.CellSize, cell.Y * FarmGrid.CellSize);
        }

        /// <summary>
        /// The cell one step in front of an actor standing at
        /// <paramref name="origin"/> and facing <paramref name="facing"/>.
        ///
        /// <para>The step is taken in <b>cell space</b> (this cell's index
        /// plus/minus one), not by walking the world position forward by one
        /// cell and re-quantizing it. Those two are not equivalent: near a
        /// cell boundary, <c>FromWorld(origin + forward * CellSize)</c> can
        /// land two cells away or back in the origin cell depending on
        /// floating-point noise, whereas stepping in cell space always yields
        /// exactly the adjacent cell.</para>
        /// </summary>
        public static FarmCell InFrontOf(WorldPosition2D origin, FacingDirection facing)
        {
            var (stepX, stepY) = FacingResolver.ToCellStep(facing);
            var here = FromWorld(origin);
            return new FarmCell(here.X + stepX, here.Y + stepY);
        }

        private static int ToCellAxis(float value)
        {
            return (int)Math.Floor((value / FarmGrid.CellSize) + 0.5f);
        }

        public bool Equals(FarmCell other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is FarmCell other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(FarmCell left, FarmCell right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FarmCell left, FarmCell right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return $"FarmCell({X}, {Y})";
        }
    }
}
