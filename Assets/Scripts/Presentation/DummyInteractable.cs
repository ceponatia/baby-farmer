using BabyFarmer.Domain;
using UnityEngine;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Placeholder interactable used to prove the interaction-targeting
    /// contract end to end. It has no gameplay behavior of its own (no
    /// inventory, no items) beyond recording that a valid interaction
    /// happened, which is enough to test "interacts from the correct
    /// position" and "does nothing when out of range" without building any
    /// real interactable system.
    /// </summary>
    public sealed class DummyInteractable : MonoBehaviour, IInteractionCandidate
    {
        /// <summary>How many times this candidate has actually been interacted with.</summary>
        public int InteractionCount { get; private set; }

        public bool WasInteracted => InteractionCount > 0;

        public WorldPosition2D Position => new WorldPosition2D(transform.position.x, transform.position.z);

        public string DisplayName => name;

        private void OnEnable()
        {
            InteractionCandidateRegistry.Register(this);
        }

        private void OnDisable()
        {
            InteractionCandidateRegistry.Unregister(this);
        }

        /// <summary>
        /// Called only once this candidate has already been resolved as the
        /// player's single valid target (see
        /// <see cref="BabyFarmer.Domain.InteractionTargetResolver"/>); it does
        /// not re-check range or facing itself.
        /// </summary>
        public bool TryInteract()
        {
            InteractionCount++;
            return true;
        }
    }
}
