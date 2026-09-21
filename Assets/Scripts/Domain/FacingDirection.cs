namespace BabyFarmer.Domain
{
    /// <summary>
    /// The four displayed directions a player (or any facing-capable actor)
    /// can face. There is no separate diagonal facing: when movement intent
    /// is diagonal, <see cref="FacingResolver.Resolve"/> collapses it to one
    /// of these four values (East/West takes priority; see its doc comment),
    /// matching decision D003 ("Four displayed facings") in
    /// docs/10-decisions-before-production.md.
    /// </summary>
    public enum FacingDirection
    {
        North,
        South,
        East,
        West
    }
}
