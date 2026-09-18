using BabyFarmer.Domain;
using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Minimal presentation-layer component proving the Unity-facing runtime
    /// assembly can reference and call the domain assembly and run in Play
    /// Mode. Intentionally does nothing else — no gameplay behavior belongs
    /// here. Deliberately does not log, so it stays silent under the Unity
    /// Test Framework's unhandled-log-message check.
    /// </summary>
    public sealed class BootstrapSceneMarker : MonoBehaviour
    {
        /// <summary>The result of a cross-assembly (presentation -> domain) call, exposed for verification.</summary>
        public string DomainSmokeCheck { get; private set; }

        private void Start()
        {
            DomainSmokeCheck = SlugFormatter.ToSlug("Bootstrap Scene");
        }
    }
}
