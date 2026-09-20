using System.Collections;
using System.Reflection;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace BabyFarmer.Presentation.Tests
{
    /// <summary>
    /// PlayMode coverage for diagonal player movement (issue #9): diagonal
    /// intent drives the real <see cref="Rigidbody"/> at the same speed as
    /// cardinal movement (no sqrt(2) diagonal advantage), collision against a
    /// configured obstacle still blocks diagonal movement, and interaction
    /// targeting still resolves correctly against the facing diagonal
    /// movement produces.
    /// </summary>
    public sealed class PlayerControllerDiagonalMovementPlayModeTests
    {
        [UnityTest]
        public IEnumerator PlayerController_DiagonalMovement_MatchesCardinalSpeed()
        {
            yield return SandboxSceneLoader.Load();
            yield return null;

            var player = Object.FindFirstObjectByType<PlayerController>();
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");
            RemoveInputReader(player);

            var body = player.GetComponent<Rigidbody>();
            Assert.IsNotNull(body, "Expected the player to have a Rigidbody.");

            // Compare the actual Rigidbody velocity magnitude the physics
            // step applies for cardinal vs. diagonal intent, directly at the
            // source of the sqrt(2) diagonal-speed bug, rather than
            // integrating travel distance over many steps (which is also
            // sensitive to nearby colliders and floating-point drift).
            player.SetMovementIntent(new Vector2(0f, 1f));
            yield return new WaitForFixedUpdate();
            var cardinalSpeed = body.linearVelocity.magnitude;

            player.SetMovementIntent(Vector2.zero);
            yield return new WaitForFixedUpdate();

            player.SetMovementIntent(new Vector2(1f, 1f));
            yield return new WaitForFixedUpdate();
            var diagonalSpeed = body.linearVelocity.magnitude;

            player.SetMovementIntent(Vector2.zero);

            // Anchor cardinalSpeed itself against the PlayerController's
            // actually configured moveSpeed, not just against diagonalSpeed:
            // otherwise this assertion would pass vacuously if a regression
            // zeroed moveSpeed or made SetMovementIntent a no-op (both
            // cardinalSpeed and diagonalSpeed would be 0 and still "equal").
            var configuredMoveSpeed = (float)typeof(PlayerController)
                .GetField("moveSpeed", BindingFlags.Instance | BindingFlags.NonPublic)
                .GetValue(player);

            Assert.AreEqual(
                configuredMoveSpeed,
                cardinalSpeed,
                0.01f,
                $"Cardinal movement speed should match the configured moveSpeed (configured={configuredMoveSpeed}, actual={cardinalSpeed}).");
            Assert.AreEqual(
                cardinalSpeed,
                diagonalSpeed,
                0.01f,
                $"Diagonal movement should travel at the same speed as cardinal movement (cardinal={cardinalSpeed}, diagonal={diagonalSpeed}).");

            LogAssert.NoUnexpectedReceived();
        }

        [UnityTest]
        public IEnumerator PlayerController_DiagonalMovement_FacesEastAndCanInteractEastward()
        {
            yield return SandboxSceneLoader.Load();
            yield return null;

            var player = Object.FindFirstObjectByType<PlayerController>();
            var dummy = Object.FindFirstObjectByType<DummyInteractable>();
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");
            Assert.IsNotNull(dummy, "Expected a DummyInteractable in the Sandbox scene.");
            RemoveInputReader(player);

            var dummyPosition = dummy.transform.position;
            var playerY = player.transform.position.y;

            // Stand one unit west of the dummy, then move with a north+east
            // diagonal intent: facing should collapse to East (per the
            // accepted facing rule) and remain resolved after intent stops,
            // so interacting east of the player succeeds. Facing is verified
            // indirectly through the interaction result (rather than
            // asserting the FacingDirection enum directly), matching the
            // acceptance criterion that interaction targeting resolves
            // correctly against the facing diagonal movement produces.
            player.transform.position = new Vector3(dummyPosition.x - 1f, playerY, dummyPosition.z);
            player.SetMovementIntent(new Vector2(1f, 1f));

            yield return new WaitForFixedUpdate();

            player.SetMovementIntent(Vector2.zero);
            player.transform.position = new Vector3(dummyPosition.x - 1f, playerY, dummyPosition.z);
            player.RequestInteract();

            Assert.IsTrue(dummy.WasInteracted, "Dummy should be interactable from the facing resolved by diagonal (north+east) movement, confirming it resolved to East.");

            LogAssert.NoUnexpectedReceived();
        }

        [UnityTest]
        public IEnumerator PlayerController_DiagonalMovement_StillBlockedByObstacleCollision()
        {
            yield return SandboxSceneLoader.Load();
            yield return null;

            var player = Object.FindFirstObjectByType<PlayerController>();
            var obstacle = GameObject.Find("Obstacle");
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");
            Assert.IsNotNull(obstacle, "Expected an Obstacle in the Sandbox scene.");
            RemoveInputReader(player);

            var obstacleBounds = obstacle.GetComponent<Collider>().bounds;
            var playerY = player.transform.position.y;

            // A north+east diagonal intent (equal-rate components) moves at
            // 45 degrees. Starting directly south of the obstacle's center
            // (as the pure-cardinal collision test does) would let the
            // player clear the obstacle's narrow width before its northward
            // motion ever reaches the obstacle's depth -- that's correct,
            // unobstructed diagonal movement around a corner, not a
            // collision-blocking scenario. Instead, start west of and below
            // the obstacle so the 45-degree diagonal path is aimed at the
            // obstacle's south face: the player's continued northward
            // progress should still be blocked by that face, exactly as in
            // the pure-cardinal case, once it arrives there.
            player.transform.position = new Vector3(obstacleBounds.center.x - 1.5f, playerY, 0f);
            player.GetComponent<Rigidbody>().position = player.transform.position;

            // Let the reposition settle through one physics step before
            // measuring/driving movement, so the Rigidbody's internal
            // position (which an interpolated, non-kinematic Rigidbody does
            // not necessarily adopt instantly from a mid-frame Transform
            // write) is fully synced before the timed movement window below.
            yield return new WaitForFixedUpdate();
            yield return null;

            var startX = player.transform.position.x;
            var startZ = player.transform.position.z;

            player.SetMovementIntent(new Vector2(1f, 1f));

            for (var i = 0; i < 50; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            player.SetMovementIntent(Vector2.zero);

            var finalZ = player.transform.position.z;
            var finalX = player.transform.position.x;

            Assert.Greater(
                finalX,
                startX + 0.5f,
                $"Player should have actually moved east toward the obstacle while moving diagonally (start x={startX}, final x={finalX}).");
            Assert.Greater(
                finalZ,
                startZ + 0.5f,
                $"Player should have actually moved north from its spawn while moving diagonally (start z={startZ}, final z={finalZ}).");
            Assert.Less(
                finalZ,
                obstacleBounds.min.z,
                $"Player should be blocked by the obstacle's south face before crossing into it while moving diagonally (obstacle south edge z={obstacleBounds.min.z}, final z={finalZ}).");

            LogAssert.NoUnexpectedReceived();
        }

        private static void RemoveInputReader(PlayerController player)
        {
            var reader = player.GetComponent<PlayerInputReader>();
            if (reader != null)
            {
                Object.DestroyImmediate(reader);
            }
        }
    }
}
