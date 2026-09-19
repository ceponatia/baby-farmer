namespace BabyFarmer.Domain
{
    /// <summary>
    /// Outcome of resolving interaction candidates for one player position and
    /// facing. Presentation reflects this to the player (highlight/debug
    /// label) but never decides validity itself.
    /// </summary>
    public readonly struct InteractionTargetResolution
    {
        private InteractionTargetResolution(IInteractionCandidate target)
        {
            Target = target;
        }

        public static readonly InteractionTargetResolution None = new InteractionTargetResolution(null);

        public static InteractionTargetResolution For(IInteractionCandidate target)
        {
            return new InteractionTargetResolution(target);
        }

        /// <summary>The single resolved target, or null when none was found.</summary>
        public IInteractionCandidate Target { get; }

        /// <summary>
        /// Whether a target was resolved (in-range and facing). A resolution
        /// with no target is never valid, by construction — a candidate that
        /// is out of range or not in front of the player is never resolved in
        /// the first place.
        /// </summary>
        public bool IsValid => Target != null;
    }
}
