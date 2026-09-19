using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BabyFarmer.Presentation.Tests
{
    /// <summary>
    /// Drives <see cref="PlayerController"/> directly through its testable
    /// intent API (not simulated key presses) inside the real Sandbox scene,
    /// to confirm collision blocks movement into a configured obstacle and
    /// that the dummy interaction succeeds/fails at the expected positions.
    /// </summary>
    public sealed class PlayerControllerPlayModeTests
    {
        [UnityTest]
        public IEnumerator PlayerController_CollisionBlocksMovementIntoObstacle()
        {
            yield return SandboxSceneLoader.Load();
            yield return null;

            var player = Object.FindFirstObjectByType<PlayerController>();
            var obstacle = GameObject.Find("Obstacle");
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");
            Assert.IsNotNull(obstacle, "Expected an Obstacle in the Sandbox scene.");

            // Remove the real input reader so the headless-batch-mode "no key
            // is ever pressed" intent (Vector2.zero) doesn't overwrite the
            // intent this test drives directly every Update().
            RemoveInputReader(player);

            var obstacleSouthEdgeZ = obstacle.GetComponent<Collider>().bounds.min.z;
            var startZ = player.transform.position.z;

            player.SetMovementIntent(new Vector2(0f, 1f));

            for (var i = 0; i < 120; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            player.SetMovementIntent(Vector2.zero);

            var finalZ = player.transform.position.z;

            Assert.Greater(
                finalZ,
                startZ + 0.5f,
                $"Player should have actually moved north from its spawn (start z={startZ}, final z={finalZ}).");
            Assert.Less(
                finalZ,
                obstacleSouthEdgeZ,
                $"Player should be blocked by the obstacle's collider before reaching it (obstacle south edge z={obstacleSouthEdgeZ}, final z={finalZ}).");

            LogAssert.NoUnexpectedReceived();
        }

        [UnityTest]
        public IEnumerator PlayerController_DummyInteraction_SucceedsInFrontAndFailsWhenInvalid()
        {
            yield return SandboxSceneLoader.Load();
            yield return null;

            var player = Object.FindFirstObjectByType<PlayerController>();
            var dummy = Object.FindFirstObjectByType<DummyInteractable>();
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");
            Assert.IsNotNull(dummy, "Expected a DummyInteractable in the Sandbox scene.");

            var dummyPosition = dummy.transform.position;
            var playerY = player.transform.position.y;

            // Out of range: far from the dummy entirely (also behind the player,
            // covering the "nowhere near" case).
            player.transform.position = new Vector3(50f, playerY, 50f);
            player.SetMovementIntent(new Vector2(0f, 1f));
            player.SetMovementIntent(Vector2.zero);
            player.RequestInteract();

            Assert.IsFalse(dummy.WasInteracted, "Dummy should not be interactable from far away.");

            // In front (same lane, facing it) but beyond interactionRange (1.25).
            player.transform.position = new Vector3(dummyPosition.x, playerY, dummyPosition.z - 5f);
            player.SetMovementIntent(new Vector2(0f, 1f));
            player.SetMovementIntent(Vector2.zero);
            player.RequestInteract();

            Assert.IsFalse(dummy.WasInteracted, "Dummy should not be interactable when in front but beyond range.");

            // In range, directly south of the dummy, but facing the wrong way (south instead of north).
            player.transform.position = new Vector3(dummyPosition.x, playerY, dummyPosition.z - 1f);
            player.SetMovementIntent(new Vector2(0f, -1f));
            player.SetMovementIntent(Vector2.zero);
            player.RequestInteract();

            Assert.IsFalse(dummy.WasInteracted, "Dummy should not be interactable when the player is not facing it.");

            // In range and facing it: stand one unit south of the dummy, facing north.
            player.transform.position = new Vector3(dummyPosition.x, playerY, dummyPosition.z - 1f);
            player.SetMovementIntent(new Vector2(0f, 1f));
            player.SetMovementIntent(Vector2.zero);
            player.RequestInteract();

            Assert.IsTrue(dummy.WasInteracted, "Dummy should be interactable from directly in front of the player.");
            Assert.AreEqual(1, dummy.InteractionCount, "Exactly one interaction should be produced per interaction request.");

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
