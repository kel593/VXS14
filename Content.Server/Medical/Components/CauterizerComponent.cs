namespace Content.Server.Medical.Components;

/// <summary>
/// Use for items that can be used as cauterizing tools for wounds, e.g. cautery, energy sword and so on.
/// </summary>
[RegisterComponent]
public sealed partial class CauterizerComponent : Component
{
    /// <summary>
    /// How long will cauterize take.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float DoAfterDuration = 9.0f;

    /// <summary>
    /// By how much will bleed amount change. You probably want that to be negative.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float BleedReduce = -4.0f;

    /// <summary>
    /// How much heat damage will be dealed.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float Damage = 4.5f;

    /// <summary>
    /// From how far away can you cauterize wounds.
    /// </summary>
    [DataField, ViewVariables(VVAccess.ReadWrite)]
    public float Distance = 1.0f;
}
