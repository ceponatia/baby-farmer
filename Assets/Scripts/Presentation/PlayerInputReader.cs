using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Translates physical input (the legacy Input Manager,
    /// <c>UnityEngine.Input</c>) into plain movement/interact intent every
    /// frame. This is the one place that calls <c>Input.GetAxisRaw</c> /
    /// <c>Input.GetButtonDown</c> in the whole project — swapping input
    /// backends (e.g. the Input System package, once approved) only requires
    /// changing this class. It contains no gameplay rules: no collision, no
    /// interaction targeting, no state mutation.
    ///
    /// Movement is restricted to the four cardinal directions (issue #6
    /// excludes diagonal movement). When both axes are pressed at once, the
    /// vertical axis takes priority, matching
    /// <see cref="BabyFarmer.Domain.FacingResolver.Resolve"/>.
    /// </summary>
    public sealed class PlayerInputReader : MonoBehaviour
    {
        /// <summary>Current movement intent, restricted to one cardinal axis at a time.</summary>
        public Vector2 MovementIntent { get; private set; }

        /// <summary>True for exactly the frame the interact button was pressed.</summary>
        public bool InteractPressed { get; private set; }

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            if (vertical != 0f)
            {
                MovementIntent = new Vector2(0f, Mathf.Sign(vertical));
            }
            else if (horizontal != 0f)
            {
                MovementIntent = new Vector2(Mathf.Sign(horizontal), 0f);
            }
            else
            {
                MovementIntent = Vector2.zero;
            }

            // A dedicated "Interact" virtual button would require adding an
            // entry to ProjectSettings/InputManager.asset, which is out of
            // scope for this issue; a direct key check needs no project
            // settings changes and keeps this the one class that would need
            // to change if the input backend changes later.
            InteractPressed = Input.GetKeyDown(KeyCode.E);
        }
    }
}
