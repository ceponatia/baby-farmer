using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Dev-only on-screen label showing what <see cref="PlayerController"/>
    /// currently has targeted, or "none". Purely a reflection of the Domain
    /// resolver's output (via <see cref="PlayerController.CurrentTarget"/>);
    /// it never decides interaction validity itself.
    /// </summary>
    public sealed class InteractionDebugDisplay : MonoBehaviour
    {
        [SerializeField] private PlayerController player;

        private void OnGUI()
        {
            if (player == null)
            {
                return;
            }

            var label = player.CurrentTarget != null
                ? $"Target: {player.CurrentTarget.DisplayName}"
                : "Target: none";

            GUI.Label(new Rect(10, 10, 300, 30), label);
        }
    }
}
