using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Keeps the camera positioned relative to a target (the player) every
    /// frame. Purely visual: it has no gameplay effect and makes no
    /// interaction or movement decisions. The default offset matches a
    /// top-down view over the X/Z ground plane the player moves on; the
    /// camera's own downward rotation is set once in the scene, not here.
    /// </summary>
    public sealed class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 10f, 0f);

        private void LateUpdate()
        {
            if (target == null)
            {
                return;
            }

            transform.position = target.position + offset;
        }
    }
}
