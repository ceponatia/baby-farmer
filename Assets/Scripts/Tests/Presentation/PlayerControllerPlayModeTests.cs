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

            var obstacleSouthEdgeZ = obstacle.GetComponent<Collider>().bounds.min.z;

            player.SetMovementIntent(new Vector2(0f, 1f));

            for (var i = 0; i < 120; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            player.SetMovementIntent(Vector2.zero);

            Assert.Less(
                player.transform.position.z,
                obstacleSouthEdgeZ,
                "Player should be blocked by the obstacle's collider before reaching it.");

            LogAssert.NoUnexpectedReceived();
        }

        [UnityTest]
        public IEnumerator PlayerController_DummyInteraction_SucceedsInFrontAndFailsOutOfRange()
        {
            yield return SandboxSceneLoader.Load();
            yield return null;

            var player = Object.FindFirstObjectByType<PlayerController>();
            var dummy = Object.FindFirstObjectByType<DummyInteractable>();
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");
            Assert.IsNotNull(dummy, "Expected a DummyInteractable in the Sandbox scene.");

            // Out of range: teleport far away from the dummy, face north, and interact.
            player.transform.position = new Vector3(50f, player.transform.position.y, 50f);
            player.SetMovementIntent(new Vector2(0f, 1f));
            player.SetMovementIntent(Vector2.zero);
            player.RequestInteract();

            Assert.IsFalse(dummy.WasInteracted, "Dummy should not be interactable from out of range.");

            // In range and facing it: stand one unit south of the dummy, facing north.
            var dummyPosition = dummy.transform.position;
            player.transform.position = new Vector3(dummyPosition.x, player.transform.position.y, dummyPosition.z - 1f);
            player.SetMovementIntent(new Vector2(0f, 1f));
            player.SetMovementIntent(Vector2.zero);
            player.RequestInteract();

            Assert.IsTrue(dummy.WasInteracted, "Dummy should be interactable from directly in front of the player.");
            Assert.AreEqual(1, dummy.InteractionCount, "Exactly one interaction should be produced per interaction request.");

            LogAssert.NoUnexpectedReceived();
        }
    }
}
