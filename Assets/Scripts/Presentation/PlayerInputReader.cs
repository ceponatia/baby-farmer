using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Translates physical input (the legacy Input Manager,
    /// <c>UnityEngine.Input</c>) into plain movement/interact intent every
    /// frame. This is the one place that calls <c>Input.GetAxisRaw</c> /
    /// <c>Input.GetKeyDown</c> in the whole project — swapping input
    /// backends (e.g. the Input System package, once approved) only requires
    /// changing this class. It contains no gameplay rules: no collision, no
    /// interaction targeting, no state mutation.
    ///
    /// Movement supports both cardinal and diagonal directions (issue #9):
    /// both axes are surfaced simultaneously, so holding two perpendicular
    /// inputs at once (e.g. Up+Right) produces a true diagonal intent. Facing
    /// resolution for diagonal intent (horizontal takes priority) is handled
    /// by <see cref="BabyFarmer.Domain.FacingResolver.Resolve"/>, not here;
    /// diagonal speed normalization is handled by
    /// <see cref="PlayerController"/>. Opposite keys on the same axis (e.g.
    /// Up+Down) cancel to zero via <c>Input.GetAxisRaw</c>, which already
    /// returns 0 when both the positive and negative bindings for an axis
    /// are held.
    /// </summary>
    public sealed class PlayerInputReader : MonoBehaviour
    {
        /// <summary>Current movement intent; both axes may be simultaneously non-zero for diagonal movement.</summary>
        public Vector2 MovementIntent { get; private set; }

        /// <summary>True for exactly the frame the interact button was pressed.</summary>
        public bool InteractPressed { get; private set; }

        private void Update()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");

            // Mathf.Sign(0f) returns 1f (not 0f), so it can't be used
            // directly here without turning "no input on this axis" into a
            // false-positive positive intent; Sign(x) as (x > 0) - (x < 0) is
            // the zero-safe equivalent.
            MovementIntent = new Vector2(Sign(horizontal), Sign(vertical));

            // A dedicated "Interact" virtual button would require adding an
            // entry to ProjectSettings/InputManager.asset, which is out of
            // scope for this issue; a direct key check needs no project
            // settings changes and keeps this the one class that would need
            // to change if the input backend changes later.
            InteractPressed = Input.GetKeyDown(KeyCode.E);
        }

        private static float Sign(float value)
        {
            if (value > 0f)
            {
                return 1f;
            }

            if (value < 0f)
            {
                return -1f;
            }

            return 0f;
        }
    }
}
