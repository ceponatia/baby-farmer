using BabyFarmer.Domain;
using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Owns player position, facing, and collision integration for the
    /// movement and interaction sandbox (issue #6). Consumes movement intent
    /// through <see cref="SetMovementIntent"/> rather than reading
    /// <c>UnityEngine.Input</c> directly, so it can be driven directly by a
    /// test. The world is a top-down ground plane on X/Z (Y is locked); actual
    /// collision against configured obstacle colliders is delegated entirely
    /// to Unity's physics (a dynamic, gravity-free <see cref="Rigidbody"/>)
    /// rather than hand-rolled.
    ///
    /// Interaction targeting is resolved by asking the engine-independent
    /// <see cref="InteractionTargetResolver"/> in the Domain assembly; this
    /// class only adapts real Transform data into Domain inputs and reflects
    /// the result (<see cref="CurrentTarget"/>) — it never itself decides
    /// whether an interaction is valid.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInputReader inputReader;
        [SerializeField] private Transform facingIndicator;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float interactionRange = 1.25f;
        [SerializeField] private float interactionHalfWidth = 0.5f;
        [SerializeField] private float facingIndicatorDistance = 0.6f;

        private Rigidbody body;

        /// <summary>Current movement intent, expressed on at most one cardinal axis (x = east/west, y = north/south).</summary>
        public Vector2 MovementIntent { get; private set; }

        /// <summary>The last non-zero movement direction; persists while intent is zero.</summary>
        public FacingDirection Facing { get; private set; } = FacingDirection.South;

        /// <summary>The currently resolved interaction target, or null when none. Debug/presentation only.</summary>
        public IInteractionCandidate CurrentTarget { get; private set; }

        private void Awake()
        {
            body = GetComponent<Rigidbody>();
            body.useGravity = false;
            body.constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionY;
        }

        private void Update()
        {
            if (inputReader != null)
            {
                SetMovementIntent(inputReader.MovementIntent);
            }

            CurrentTarget = ResolveTarget();
            UpdateFacingIndicator();

            if (inputReader != null && inputReader.InteractPressed)
            {
                RequestInteract();
            }
        }

        private void FixedUpdate()
        {
            body.linearVelocity = new Vector3(MovementIntent.x, 0f, MovementIntent.y) * moveSpeed;
        }

        /// <summary>
        /// Sets the current movement intent and updates facing accordingly.
        /// This is the single entry point movement is driven through, so
        /// tests can drive it directly without simulating key presses.
        /// </summary>
        public void SetMovementIntent(Vector2 intent)
        {
            MovementIntent = intent;
            Facing = FacingResolver.Resolve(Facing, intent.x, intent.y);
        }

        /// <summary>
        /// Resolves the current interaction target from this player's
        /// position and facing against every registered candidate, then
        /// attempts to interact with it. A resolution with no target is a
        /// safe no-op — no gameplay mutation occurs.
        /// </summary>
        public void RequestInteract()
        {
            var resolution = InteractionTargetResolver.Resolve(
                OriginPosition(), Facing, interactionRange, interactionHalfWidth, InteractionCandidateRegistry.All);

            CurrentTarget = resolution.Target;
            InteractionTargetResolver.AttemptInteract(resolution);
        }

        private IInteractionCandidate ResolveTarget()
        {
            var resolution = InteractionTargetResolver.Resolve(
                OriginPosition(), Facing, interactionRange, interactionHalfWidth, InteractionCandidateRegistry.All);
            return resolution.Target;
        }

        private WorldPosition2D OriginPosition()
        {
            var position = transform.position;
            return new WorldPosition2D(position.x, position.z);
        }

        private void UpdateFacingIndicator()
        {
            if (facingIndicator == null)
            {
                return;
            }

            var (x, z) = FacingResolver.ToUnitVector(Facing);
            facingIndicator.localPosition = new Vector3(x, 0f, z) * facingIndicatorDistance;
        }
    }
}
