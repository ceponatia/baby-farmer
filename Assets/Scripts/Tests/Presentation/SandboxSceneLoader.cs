using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor.SceneManagement;
#endif

namespace BabyFarmer.Presentation.Tests
{
    /// <summary>
    /// Loads the Sandbox development scene for PlayMode tests.
    ///
    /// The scene is deliberately not registered in
    /// ProjectSettings/EditorBuildSettings.asset (that file is outside this
    /// task's scope), so runtime <c>SceneManager.LoadSceneAsync(name)</c>
    /// cannot resolve it by name. In the Editor (where these PlayMode tests
    /// actually run via "-testPlatform PlayMode"), <c>EditorSceneManager
    /// .LoadSceneInPlayMode</c> loads any scene under Assets by path while
    /// already in Play Mode, without needing a build list entry — the same
    /// mechanism Unity itself uses when you press Play on a scene that was
    /// never added to Build Settings.
    /// </summary>
    internal static class SandboxSceneLoader
    {
        private const string ScenePath = "Assets/Scenes/Sandbox.unity";

        public static IEnumerator Load()
        {
#if UNITY_EDITOR
            var scene = EditorSceneManager.LoadSceneInPlayMode(
                ScenePath, new LoadSceneParameters(LoadSceneMode.Single));
            while (!scene.isLoaded)
            {
                yield return null;
            }
#else
            yield return SceneManager.LoadSceneAsync("Sandbox", LoadSceneMode.Single);
#endif
        }
    }
}
