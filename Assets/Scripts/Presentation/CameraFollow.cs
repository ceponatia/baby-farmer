using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Keeps the camera positioned relative to a target (the player) every
    /// frame. Purely visual: it has no gameplay effect and makes no
    /// interaction or movement decisions. The default offset matches a
    /// top-down view over the X/Z ground plane the player moves on; the
    /// camera's own downward rotation is set once in the scene, not here.
    ///
    /// <para>The camera's own position is snapped to the 1/32-unit texel grid
    /// implied by the locked 32 pixels-per-unit art scale (issue #21), so a
    /// continuously moving target does not drag the whole world across
    /// sub-texel offsets and shimmer. This snap is <b>presentation-only</b>:
    /// it is applied to this camera's Transform and nothing else. The
    /// player's Rigidbody, its Transform, and every
    /// <see cref="BabyFarmer.Domain.WorldPosition2D"/> handed to Domain stay
    /// continuous and unsnapped.</para>
    /// </summary>
    public sealed class CameraFollow : MonoBehaviour
    {
        /// <summary>
        /// Locked art scale from issue #21: 32 texels per world unit, so one
        /// texel is 1/32 of a unit.
        /// </summary>
        private const float PixelsPerUnit = 32f;

        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 10f, 0f);

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            var followPosition = target.position + offset;

            // Only the two ground-plane axes are snapped; height is left
            // exactly as configured, since it has no effect on which texel a
            // straight-down orthographic camera samples.
            transform.position = new Vector3(
                SnapToTexelGrid(followPosition.x),
                followPosition.y,
                SnapToTexelGrid(followPosition.z));
        }

        private static float SnapToTexelGrid(float value)
        {
            return Mathf.Round(value * PixelsPerUnit) / PixelsPerUnit;
        }
    }
}
