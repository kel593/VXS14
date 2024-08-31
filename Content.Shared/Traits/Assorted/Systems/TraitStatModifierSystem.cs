using Content.Shared.Mobs.Components;
using Content.Shared.Mobs.Systems;
using Content.Shared.Traits.Assorted.Components;
using Content.Shared.Weapons.Melee.Events;
using Content.Shared.Damage.Components;

namespace Content.Shared.Traits.Assorted.Systems;

public sealed partial class TraitStatModifierSystem : EntitySystem
{
    [Dependency] private readonly MobThresholdSystem _threshold = default!;
    public override void Initialize()
    {
        base.Initialize();
        SubscribeLocalEvent<CritModifierComponent, ComponentStartup>(OnCritStartup);
        SubscribeLocalEvent<DeadModifierComponent, ComponentStartup>(OnDeadStartup);
        SubscribeLocalEvent<StaminaCritModifierComponent, ComponentStartup>(OnStaminaCritStartup);
    }

    private void OnCritStartup(EntityUid uid, CritModifierComponent component, ComponentStartup args)
    {
        if (!TryComp<MobThresholdsComponent>(uid, out var threshold))
            return;

        var critThreshold = _threshold.GetThresholdForState(uid, Mobs.MobState.Critical, threshold);
        if (critThreshold != 0)
            _threshold.SetMobStateThreshold(uid, critThreshold + component.CritThresholdModifier, Mobs.MobState.Critical);
    }

    private void OnDeadStartup(EntityUid uid, DeadModifierComponent component, ComponentStartup args)
    {
        if (!TryComp<MobThresholdsComponent>(uid, out var threshold))
            return;

        var deadThreshold = _threshold.GetThresholdForState(uid, Mobs.MobState.Dead, threshold);
        if (deadThreshold != 0)
            _threshold.SetMobStateThreshold(uid, deadThreshold + component.DeadThresholdModifier, Mobs.MobState.Dead);
    }

    private void OnStaminaCritStartup(EntityUid uid, StaminaCritModifierComponent component, ComponentStartup args)
    {
        if (!TryComp<StaminaComponent>(uid, out var stamina))
            return;

        stamina.CritThreshold += component.CritThresholdModifier;
    }
}
