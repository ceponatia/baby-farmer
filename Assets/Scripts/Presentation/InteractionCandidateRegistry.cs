using System.Collections.Generic;
using BabyFarmer.Domain;

namespace BabyFarmer.Presentation
{
    /// <summary>
    /// Tracks the interaction candidates currently present in the scene, so
    /// <see cref="PlayerController"/> can ask the Domain resolver to consider
    /// all of them without needing a hard reference to every concrete
    /// interactable type. Candidates register themselves on enable and
    /// unregister on disable/destroy; this is intentionally a small runtime
    /// registry, not a generic interaction framework.
    /// </summary>
    public static class InteractionCandidateRegistry
    {
        private static readonly List<IInteractionCandidate> Candidates = new List<IInteractionCandidate>();

        public static IReadOnlyList<IInteractionCandidate> All => Candidates;

        public static void Register(IInteractionCandidate candidate)
        {
            if (candidate != null && !Candidates.Contains(candidate))
            {
                Candidates.Add(candidate);
            }
        }

        public static void Unregister(IInteractionCandidate candidate)
        {
            Candidates.Remove(candidate);
        }
    }
}
