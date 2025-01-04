using System.Linq;
using Content.Server.Administration.Commands;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.KillTracking;
using Content.Server.Mind;
using Content.Server.Points;
using Content.Server.RoundEnd;
using Content.Server.Station.Systems;
using Content.Shared.GameTicking.Components;
using Content.Shared.Points;
using Content.Shared.Storage;
using Robust.Server.GameObjects;
using Robust.Server.Player;
using Robust.Shared.Utility;
using Content.Shared.Clothing.Components;
using Content.Shared.Roles;
using Content.Shared.Station;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Content.Shared.Clothing;
using Content.Shared.Roles.Jobs;
using Content.Shared.Preferences;
using System.Linq;
using Content.Server.Access.Systems;
using Content.Server.DetailExaminable;
using Content.Server.Humanoid;
using Content.Server.IdentityManagement;
using Content.Server.Mind.Commands;
using Content.Server.PDA;
using Content.Server.Shuttles.Systems;
using Content.Server.Spawners.EntitySystems;
using Content.Server.Station.Components;
using Content.Shared.Access.Components;
using Content.Shared.Access.Systems;
using Content.Shared.CCVar;
using Content.Shared.Clothing;
using Content.Shared.Humanoid;
using Content.Shared.Humanoid.Prototypes;
using Content.Shared.PDA;
using Content.Shared.Preferences;
using Content.Shared.Preferences.Loadouts;
using Content.Shared.Random;
using Content.Shared.Random.Helpers;
using Content.Shared.Roles;
using Content.Shared.Roles.Jobs;
using Content.Shared.Station;
using Content.Shared.StatusIcon;
using JetBrains.Annotations;
using Robust.Shared.Configuration;
using Robust.Shared.Map;
using Robust.Shared.Player;
using Robust.Shared.Prototypes;
using Robust.Shared.Random;
using Robust.Shared.Utility;
using Content.Server.Station.Systems;
using Content.Shared.Station;
using Content.Shared.GameTicking;
using Content.Server.GameTicking.Rules.VXS.Forts.Components;
using Content.Shared.Destructible;
using Content.Server.RoundEnd;
using Content.Shared.VXS.Forts;
namespace Content.Server.GameTicking.Rules;

public sealed class VXSFortsGameRuleSystem : GameRuleSystem<VXSFortsGameRuleComponent>
{
    [Dependency] private readonly IPlayerManager _player = default!;
    [Dependency] private readonly MindSystem _mind = default!;
    [Dependency] private readonly PointSystem _point = default!;
    [Dependency] private readonly RespawnRuleSystem _respawn = default!;
    [Dependency] private readonly RoundEndSystem _roundEnd = default!;
    [Dependency] private readonly StationSpawningSystem _stationSpawning = default!;
    [Dependency] private readonly TransformSystem _transform = default!;

    [Dependency] private readonly ActorSystem _actors = default!;

    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    private string Winner = "Debug"; // Duh...

    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<SharedVXSFortStructComponent, DestructionEventArgs>(OnDestroyed);

    }

    private void OnDestroyed(EntityUid uid, SharedVXSFortStructComponent component, DestructionEventArgs args)
    {
        Winner = component.Team;
        _roundEnd.EndRound(TimeSpan.FromSeconds(10f));
    }

    protected override void AppendRoundEndText(EntityUid uid, VXSFortsGameRuleComponent component, GameRuleComponent gameRule, ref RoundEndTextAppendEvent args)
    {
        args.AddLine("");
        args.AddLine(Loc.GetString($"vxs-forts-round-end", ("team" , Winner)));
        args.AddLine("");
        Winner = "NotTiled";
    }
}


