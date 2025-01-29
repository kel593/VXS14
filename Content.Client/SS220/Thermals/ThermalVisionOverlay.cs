// Original code github.com/CM-14 Licence MIT, EULA/CLA with a hosting restriction, full text: https://raw.githubusercontent.com/SerbiaStrong-220/space-station-14/master/CLA.txt
using System.Numerics;
using Content.Shared.SS220.Thermals;
using Content.Shared.Mobs.Components;
using Robust.Client.GameObjects;
using Robust.Client.Graphics;
using Robust.Client.Player;
using Robust.Shared.Enums;
using Robust.Shared.Map;
using Robust.Shared.Prototypes;
using Serilog;
using Robust.Client.ComponentTrees;

namespace Content.Client.SS220.Thermals;

public sealed class ThermalVisionOverlay : Overlay
{
    [Dependency] private readonly IEntityManager _entity = default!;
    [Dependency] private readonly IPlayerManager _playerManager = default!;
    [Dependency] private readonly IPrototypeManager _prototypeManager = default!;

    private readonly ContainerSystem _container;
    private readonly TransformSystem _transform;
    private readonly EntityLookupSystem _entityLookup;
    private readonly float _showRadius;
    private readonly float _showCloseRadius;
    private const float MIN_RANGE = 0.3f;
    public override OverlaySpace Space => OverlaySpace.WorldSpace;

    public ThermalVisionOverlay(float showRadius)
    {
        IoCManager.InjectDependencies(this);

        _container = _entity.System<ContainerSystem>();
        _transform = _entity.System<TransformSystem>();
        _entityLookup = _entity.System<EntityLookupSystem>();

        _showRadius = showRadius;
        _showCloseRadius = _showRadius / 4 < MIN_RANGE ? MIN_RANGE : _showRadius / 4;
    }

    protected override void Draw(in OverlayDrawArgs args)
    {
        if (_playerManager.LocalEntity == null)
            return;

        if (!_entity.TryGetComponent(_playerManager.LocalEntity, out ThermalVisionComponent? thermalVision) ||
            thermalVision.State == ThermalVisionState.Off)
            return;

        if (_entity.TryGetComponent<TransformComponent>(_playerManager.LocalEntity,
                                                out var playerTransform) == false)
            return; // maybe need to log it
        var handle = args.WorldHandle;
        var eye = args.Viewport.Eye;
        var eyeRot = eye?.Rotation ?? default;

        if (_showRadius < MIN_RANGE)
            return; // can cause execp also need to log it

        var entities = _entityLookup.GetEntitiesInRange<MobStateComponent>(playerTransform.Coordinates, _showRadius);
        var entitiesClose = _entityLookup.GetEntitiesInRange<MobStateComponent>(playerTransform.Coordinates, _showCloseRadius);

        foreach (var (uid, stateComp) in entities)
        {
            if (_entity.TryGetComponent<SpriteComponent>(uid, out var sprite) == false)
                continue;
            if (_entity.TryGetComponent<TransformComponent>(uid, out var xform) == false)
                continue;
            if (_container.IsEntityOrParentInContainer(uid)
                            && entitiesClose.Contains((uid, stateComp)) == false)
                continue;

            Render((uid, sprite, xform), eye?.Position.MapId, handle, eyeRot);
        }
        handle.SetTransform(Matrix3x2.Identity);
    }

    private void Render(Entity<SpriteComponent, TransformComponent> ent,
                        MapId? map, DrawingHandleWorld handle, Angle eyeRot)
    {
        var (uid, sprite, xform) = ent;
        if (xform.MapID != map)
            return;



        var position = _transform.GetWorldPosition(xform);
        var rotation = _transform.GetWorldRotation(xform);

        sprite.Render(handle, eyeRot, rotation, position: position);
    }
}
