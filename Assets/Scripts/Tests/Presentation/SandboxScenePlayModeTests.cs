using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BabyFarmer.Presentation.Tests
{
    /// <summary>
    /// Headless, command-line-verifiable proof that the movement/interaction
    /// development scene (issue #6) enters Play Mode without console errors,
    /// analogous to <see cref="BootstrapScenePlayModeTests"/>.
    /// </summary>
    public sealed class SandboxScenePlayModeTests
    {
        [UnityTest]
        public IEnumerator SandboxScene_LoadsAndEntersPlayModeWithoutErrors()
        {
            yield return SandboxSceneLoader.Load();

            // Allow Awake/Start/first Update to run.
            yield return null;

            Assert.AreEqual("Sandbox", SceneManager.GetActiveScene().name);

            var player = Object.FindFirstObjectByType<PlayerController>();
            Assert.IsNotNull(player, "Expected a PlayerController in the Sandbox scene.");

            var dummy = Object.FindFirstObjectByType<DummyInteractable>();
            Assert.IsNotNull(dummy, "Expected a DummyInteractable in the Sandbox scene.");

            LogAssert.NoUnexpectedReceived();
        }
    }
}
