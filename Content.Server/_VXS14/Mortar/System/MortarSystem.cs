using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Server.GameObjects;
using Robust.Shared.Containers;
using Robust.Shared.Random;
using Content.Shared._VXS14.Mortar;
using Robust.Shared.IoC;
using Content.Server.Explosion.EntitySystems;
using Content.Shared.Verbs;
using Content.Server.Administration.Commands;
using Content.Server.EUI;
using Robust.Server.Player;
using System.Numerics;
using Robust.Shared.Utility;
using Content.Shared.Weapons.Ranged.Components;
using Robust.Shared.Containers;
using Content.Shared.Containers.ItemSlots;

namespace Content.Server._VXS14.Mortar
{
    public sealed class MortarSystem : EntitySystem
    {
        [Dependency] private readonly SharedTransformSystem _transform = default!;
        [Dependency] protected readonly EntityManager EntityManager = default!;
        [Dependency] private readonly UserInterfaceSystem _uiSystem = default!;
        [Dependency] private readonly IRobustRandom _random = default!;
        [Dependency] private readonly ExplosionSystem _explosionSystem = default!;

        [Dependency] private readonly IPlayerManager _playerManager = default!;

        public override void Initialize()
        {
            base.Initialize();

            // SubscribeNetworkEvent<MortarMessage>(OnMortarFire);
            SubscribeLocalEvent<SharedMortarComponent, GetVerbsEvent<ExamineVerb>>(OnMortarVerbUtility);
        }

        private void OnMortarVerbUtility(EntityUid uid, SharedMortarComponent component, GetVerbsEvent<ExamineVerb> args)
        {
            var sysMan = IoCManager.Resolve<IEntitySystemManager>();
            var ItemSlots = sysMan.GetEntitySystem<ItemSlotsSystem>();
            var rocket = ItemSlots.GetItemOrNull(uid, "mortar_chamber");

            if (rocket != null)
            {
                var verb = new ExamineVerb
                {
                    Act = () => OnUsed(uid, args.User),
                };
                verb.Text = Loc.GetString("Open Mortar UI");
                verb.Icon = new SpriteSpecifier.Texture(new ResPath("/Textures/VXS/Interface/mortarIcon.png"));
                args.Verbs.Add(verb);
            }

        }

    private void OnUsed(EntityUid uid,  EntityUid user, bool canReach = true)
    {
                if(_playerManager.TryGetSessionByEntity(user, out var session))
                {
                    var eui = IoCManager.Resolve<EuiManager>();
                    var ui = new MortarEui(uid);
                    eui.OpenEui(ui, session);
                }
}

    }
}
