using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace BabyFarmer.Presentation.Tests
{
    /// <summary>
    /// Headless, command-line-verifiable proof that the minimal bootstrap
    /// scene enters Play Mode without console errors. Runs via
    /// "-runTests -testPlatform PlayMode".
    /// </summary>
    public sealed class BootstrapScenePlayModeTests
    {
        [UnityTest]
        public IEnumerator BootstrapScene_LoadsAndEntersPlayModeWithoutErrors()
        {
            yield return SceneManager.LoadSceneAsync("Bootstrap", LoadSceneMode.Single);

            // Allow BootstrapSceneMarker.Start() to run.
            yield return null;

            Assert.AreEqual("Bootstrap", SceneManager.GetActiveScene().name);

            var marker = Object.FindFirstObjectByType<BootstrapSceneMarker>();
            Assert.IsNotNull(marker, "Expected BootstrapSceneMarker in the loaded scene.");
            Assert.AreEqual("bootstrap-scene", marker.DomainSmokeCheck);

            LogAssert.NoUnexpectedReceived();
        }
    }
}
