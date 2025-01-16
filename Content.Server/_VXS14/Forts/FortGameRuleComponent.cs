using Content.Shared.FixedPoint;
using Content.Shared.Roles;
using Content.Shared.Storage;
using Robust.Shared.Network;
using Robust.Shared.Prototypes;
using Robust.Shared.Serialization.TypeSerializers.Implementations.Custom.Prototype;

namespace Content.Server.GameTicking.Rules.VXS.Forts.Components;

/// <summary>
/// </summary>
[RegisterComponent]
public sealed partial class VXSFortsGameRuleComponent : Component
{

    [DataField("restartDelay"), ViewVariables(VVAccess.ReadWrite)]
    public TimeSpan RestartDelay = TimeSpan.FromSeconds(10f);


}


