namespace BabyFarmer.Domain
{
    /// <summary>
    /// The four cardinal directions a player (or any facing-capable actor) can
    /// face. Diagonal facing is intentionally not represented; this issue's
    /// scope is cardinal-only movement and interaction.
    /// </summary>
    public enum FacingDirection
    {
        North,
        South,
        East,
        West
    }
}
