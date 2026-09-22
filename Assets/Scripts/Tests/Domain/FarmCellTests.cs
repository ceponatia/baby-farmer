using BabyFarmer.Domain;
using NUnit.Framework;

namespace BabyFarmer.Domain.Tests
{
    /// <summary>
    /// EditMode tests for the world-position to farm-cell mapping locked by
    /// issue #21: cells are identified by their centres on integer world
    /// coordinates, and quantization uses <c>floor(v / CellSize + 0.5)</c>.
    ///
    /// These are contract tests for the coordinate decision itself. They are
    /// deliberately independent of the continuous interaction-targeting
    /// rectangle in <see cref="InteractionTargetResolver"/>; nothing in
    /// gameplay consumes cells yet (that is issue #23).
    /// </summary>
    public sealed class FarmCellTests
    {
        [Test]
        public void CellSize_IsOneWorldUnit()
        {
            // 32px terrain tile at 32 pixels-per-unit == 1 world unit.
            Assert.AreEqual(1f, FarmGrid.CellSize);
        }

        // The worked table from the accepted decision. Boundaries land in the
        // higher cell in BOTH directions, which is what distinguishes
        // floor(v + 0.5) from Math.Round's banker's rounding.
        [TestCase(0f, 0f, 0, 0, TestName = "FromWorld(origin)")]
        [TestCase(0.5f, 0f, 1, 0, TestName = "FromWorld(positive boundary 0.5 -> higher cell)")]
        [TestCase(-0.5f, 0f, 0, 0, TestName = "FromWorld(negative boundary -0.5 -> higher cell)")]
        [TestCase(-0.51f, 0f, -1, 0, TestName = "FromWorld(clearly inside the negative cell)")]
        [TestCase(0.49f, 0f, 0, 0, TestName = "FromWorld(just inside cell 0)")]
        [TestCase(1.5f, 0f, 2, 0, TestName = "FromWorld(1.5 -> cell 2, not banker's-rounded to 2 by luck)")]
        [TestCase(2.5f, 0f, 3, 0, TestName = "FromWorld(2.5 -> cell 3, where Math.Round would give 2)")]
        [TestCase(0f, 1f, 0, 1, TestName = "FromWorld(DummyInteractable's actual scene position)")]
        [TestCase(0f, 2.5f, 0, 3, TestName = "FromWorld(Obstacle's actual scene position, a cell boundary)")]
        [TestCase(-3.2f, -7.8f, -3, -8, TestName = "FromWorld(both axes negative)")]
        public void FromWorld_QuantizesToTheContainingCell(
            float worldX, float worldY, int expectedX, int expectedY)
        {
            var cell = FarmCell.FromWorld(new WorldPosition2D(worldX, worldY));

            Assert.AreEqual(new FarmCell(expectedX, expectedY), cell);
        }

        [Test]
        public void FromWorld_IsUniformAcrossConsecutiveBoundaries()
        {
            // Math.Round would break this: it sends 0.5 -> 0 and 1.5 -> 2,
            // making cell 0 twice as wide as cell 1. Every cell must be
            // exactly CellSize wide, everywhere, including across zero.
            for (var cell = -4; cell <= 4; cell++)
            {
                var lowerEdge = cell - 0.5f;
                var upperEdgeInside = cell + 0.49f;

                Assert.AreEqual(
                    new FarmCell(cell, 0),
                    FarmCell.FromWorld(new WorldPosition2D(lowerEdge, 0f)),
                    $"The lower edge of cell {cell} should belong to cell {cell}.");
                Assert.AreEqual(
                    new FarmCell(cell, 0),
                    FarmCell.FromWorld(new WorldPosition2D(upperEdgeInside, 0f)),
                    $"A point just inside the upper edge of cell {cell} should belong to cell {cell}.");
            }
        }

        [TestCase(0, 0, 0f, 0f)]
        [TestCase(0, 1, 0f, 1f)]
        [TestCase(3, -2, 3f, -2f)]
        public void CenterOf_ReturnsTheIntegerWorldCentre(
            int cellX, int cellY, float expectedX, float expectedY)
        {
            var centre = FarmCell.CenterOf(new FarmCell(cellX, cellY));

            Assert.AreEqual(expectedX, centre.X);
            Assert.AreEqual(expectedY, centre.Y);
        }

        [Test]
        public void CenterOf_RoundTripsThroughFromWorld()
        {
            for (var x = -3; x <= 3; x++)
            {
                for (var y = -3; y <= 3; y++)
                {
                    var cell = new FarmCell(x, y);

                    Assert.AreEqual(cell, FarmCell.FromWorld(FarmCell.CenterOf(cell)));
                }
            }
        }

        [Test]
        public void InFrontOf_FacingNorthFromOrigin_IsTheDummyInteractablesCell()
        {
            // Anchors the contract to today's actual Sandbox scene: the
            // player spawns at world (0, 0) and DummyInteractable sits at
            // world (x=0, z=1), one cell north.
            var front = FarmCell.InFrontOf(new WorldPosition2D(0f, 0f), FacingDirection.North);

            Assert.AreEqual(new FarmCell(0, 1), front);

            var frontCentre = FarmCell.CenterOf(front);
            Assert.AreEqual(0f, frontCentre.X);
            Assert.AreEqual(1f, frontCentre.Y);
        }

        [TestCase(FacingDirection.North, 0, 1)]
        [TestCase(FacingDirection.South, 0, -1)]
        [TestCase(FacingDirection.East, 1, 0)]
        [TestCase(FacingDirection.West, -1, 0)]
        public void InFrontOf_StepsOneCellInEveryFacing(
            FacingDirection facing, int expectedX, int expectedY)
        {
            var front = FarmCell.InFrontOf(new WorldPosition2D(0f, 0f), facing);

            Assert.AreEqual(new FarmCell(expectedX, expectedY), front);
        }

        [Test]
        public void InFrontOf_StepsInCellSpace_NotByQuantizingAWorldStep()
        {
            // This is the case that separates the two implementations. The
            // origin sits a hair south of the cell-0 lower boundary, so it is
            // in cell -1; the cell in front of it (north) must be cell 0.
            // Walking the WORLD position forward by one CellSize first would
            // give -0.5001 + 1 = 0.4999 -> cell 0 here, but the same trick
            // lands two cells away for an origin a hair north of a boundary.
            var justSouthOfBoundary = new WorldPosition2D(0f, -0.5001f);
            Assert.AreEqual(
                new FarmCell(0, -1),
                FarmCell.FromWorld(justSouthOfBoundary),
                "Precondition: the origin should be in cell -1.");

            Assert.AreEqual(
                new FarmCell(0, 0),
                FarmCell.InFrontOf(justSouthOfBoundary, FacingDirection.North));

            // And symmetrically, a hair north of the boundary is in cell 0,
            // so the cell in front is cell 1 — exactly one step, never two.
            var justNorthOfBoundary = new WorldPosition2D(0f, -0.4999f);
            Assert.AreEqual(
                new FarmCell(0, 0),
                FarmCell.FromWorld(justNorthOfBoundary),
                "Precondition: the origin should be in cell 0.");

            Assert.AreEqual(
                new FarmCell(0, 1),
                FarmCell.InFrontOf(justNorthOfBoundary, FacingDirection.North));
        }

        [Test]
        public void InFrontOf_IsAlwaysExactlyOneCellAway()
        {
            var facings = new[]
            {
                FacingDirection.North,
                FacingDirection.South,
                FacingDirection.East,
                FacingDirection.West
            };

            // Sweep across a cell boundary in small steps: the front cell
            // must always be a direct neighbour of the origin's own cell, at
            // every sub-cell offset, never a two-cell jump.
            for (var offset = -1.2f; offset <= 1.2f; offset += 0.1f)
            {
                var origin = new WorldPosition2D(offset, offset);
                var here = FarmCell.FromWorld(origin);

                foreach (var facing in facings)
                {
                    var front = FarmCell.InFrontOf(origin, facing);
                    var manhattanDistance = System.Math.Abs(front.X - here.X)
                        + System.Math.Abs(front.Y - here.Y);

                    Assert.AreEqual(
                        1,
                        manhattanDistance,
                        $"Front cell {front} should be adjacent to {here} facing {facing} at offset {offset}.");
                }
            }
        }

        [Test]
        public void Equality_IsValueBased()
        {
            Assert.AreEqual(new FarmCell(2, -3), new FarmCell(2, -3));
            Assert.IsTrue(new FarmCell(2, -3) == new FarmCell(2, -3));
            Assert.IsTrue(new FarmCell(2, -3) != new FarmCell(-3, 2));
            Assert.AreEqual(new FarmCell(2, -3).GetHashCode(), new FarmCell(2, -3).GetHashCode());
        }
    }
}
