using Robust.Shared.Player;
using Content.Server.GameTicking.Rules.Components;
using Content.Server.GameTicking.Rules;
using Content.Server.GameTicking;
using Content.Shared.GameTicking.Components;
using Content.Server.RoundEnd;
using Content.Server.Station.Systems;
using Content.Shared.GameTicking.Components;
using Robust.Server.GameObjects;
using Robust.Server.Player;
using Robust.Shared.Utility;
using Content.Server.Mind;
using Content.Shared.Clothing;
using Content.Shared.Preferences;
using Content.Shared.Preferences.Loadouts;
using Robust.Shared.Player;
using Robust.Shared.Map;
using Robust.Shared.Random;
using Content.Server.Spawners.Components;
using Robust.Shared.Prototypes;
using Content.Shared.Roles;
using Content.Shared.Roles.Jobs;

namespace Content.Server.VXS.Team
{
    public sealed class VXSTeamRuleSystem : GameRuleSystem<VXSTeamRuleComponent>
    {
        [Dependency] private readonly IPlayerManager _player = default!;
        [Dependency] private readonly MindSystem _mind = default!;
        [Dependency] private readonly StationSpawningSystem _stationSpawning = default!;
        [Dependency] private readonly TransformSystem _transform = default!;

        [Dependency] private readonly IPrototypeManager _prototypeManager = default!;
        public override void Initialize()
        {
            base.Initialize();
            SubscribeLocalEvent<PlayerBeforeSpawnEvent>(OnBeforeSpawn);

        }

        private void OnBeforeSpawn(PlayerBeforeSpawnEvent ev)
        {

            var query = EntityQueryEnumerator<VXSTeamRuleComponent, GameRuleComponent>();
            while (query.MoveNext(out var uid, out var team, out var rule))
            {

                if (!GameTicker.IsGameRuleActive(uid, rule))
                    continue;

                if(ev.JobId != null)
                {

                    var job = new JobComponent {Prototype = ev.JobId};

                    var newMind = _mind.CreateMind(ev.Player.UserId, ev.Profile.Name);
                    _mind.SetUserId(newMind, ev.Player.UserId);
                    EntityCoordinates spawnCoordinates = EntityCoordinates.Invalid;

                    if(ev.JobId.Contains("Solfed"))
                    {
                        foreach (var spawn in EntityQuery<TransformComponent>())
                        {
                            if (EntityManager.GetComponentOrNull<MetaDataComponent>(spawn.Owner)?.EntityPrototype?.ID == "VXSSpawnPointSolfedPeacekeeper")
                            {
                                spawnCoordinates = Transform(spawn.Owner).Coordinates;
                                break;
                            }
                        }
                    }
                    else if(ev.JobId.Contains("SyndAnarchy"))
                    {
                        foreach (var spawn in EntityQuery<TransformComponent>())
                        {
                            if (EntityManager.GetComponentOrNull<MetaDataComponent>(spawn.Owner)?.EntityPrototype?.ID == "VXSSpawnPointSyndAnarchykeeper")
                            {
                                spawnCoordinates = Transform(spawn.Owner).Coordinates;
                                break;
                            }
                        }
                    }

                    var mobMaybe = _stationSpawning.SpawnPlayerMob(spawnCoordinates, job, ev.Profile, ev.Station );
                    DebugTools.AssertNotNull(mobMaybe);
                    var mob = mobMaybe;
                    _mind.TransferTo(newMind, mob);

                    ev.Handled = true;
                    break;
                }
            }
        }


    }
}
