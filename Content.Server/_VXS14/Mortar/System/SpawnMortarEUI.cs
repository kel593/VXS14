using Content.Server.EUI;
using Content.Server.Explosion.EntitySystems;
using Content.Shared.Administration;
using Content.Shared.Eui;
using JetBrains.Annotations;
using Content.Server._VXS14.Mortar;
using Content.Shared._VXS14.Mortar;
using Robust.Shared.Map;
using Robust.Shared.GameObjects;
using Content.Shared.Weapons.Ranged.Components;
using Content.Shared.Containers.ItemSlots;
using Robust.Shared.Containers;

namespace Content.Server._VXS14.Mortar;

/// <summary>
///     Mortar Eui
/// </summary>
///


[UsedImplicitly]
public sealed class MortarEui : BaseEui
{
    private int Count = 0;
    private readonly EntityUid Mortar;

    public MortarEui(EntityUid uid)
    {
        Mortar = uid;
    }

    public override void HandleMessage(EuiMessageBase msg)
    {
        base.HandleMessage(msg);

        if (msg is not MortarSpawnExplosionEuiMsg.MortarCords request)
        {
            Close();
            return;
        }
        // Dumb code
        var sysMan = IoCManager.Resolve<IEntitySystemManager>();
        var entMan = IoCManager.Resolve<IEntityManager>();
        var itemSlots = sysMan.GetEntitySystem<ItemSlotsSystem>();

        var rocket = itemSlots.GetItemOrNull(Mortar, "mortar_chamber");
        if (rocket == null)
        {
            Close();
            return;
        }
        entMan.TryGetComponent<SharedMortarShellComponent>(rocket, out var comp);
        entMan.DeleteEntity(rocket);

        // TODO: Visual and audio
        if(comp != null)
            sysMan.GetEntitySystem<ExplosionSystem>().QueueExplosion(request.Epicenter, comp.Type, comp.TotalIntensity, comp.Slope, comp.MaxTileIntensity, null);

        Close();
    }
}
