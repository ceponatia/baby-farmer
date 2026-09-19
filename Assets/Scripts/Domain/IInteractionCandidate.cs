namespace BabyFarmer.Domain
{
    /// <summary>
    /// Anything that can be resolved as an interaction target by
    /// <see cref="InteractionTargetResolver"/>: a farm tile, an NPC, a door, a
    /// machine, a container, a pickup, or (for this issue) a placeholder dummy
    /// interactable. Implementations live in Presentation (or later, other
    /// gameplay assemblies) and adapt real world state into these plain values;
    /// this contract itself has no UnityEngine dependency.
    /// </summary>
    public interface IInteractionCandidate
    {
        /// <summary>World position used for range/facing checks.</summary>
        WorldPosition2D Position { get; }

        /// <summary>Display name for debug/on-screen target feedback.</summary>
        string DisplayName { get; }

        /// <summary>
        /// Performs the interaction. Only called by
        /// <see cref="InteractionTargetResolver.AttemptInteract"/> once this
        /// candidate has already been resolved as the single valid target for
        /// the current interaction request; implementations do not need to
        /// re-check range or facing.
        /// </summary>
        /// <returns>Whether the interaction was accepted.</returns>
        bool TryInteract();
    }
}
