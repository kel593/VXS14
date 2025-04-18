using Robust.Shared.Serialization;

namespace Content.Shared.Weapons.Ranged.Events;

/// <summary>
/// Raised whenever a muzzle flash client-side entity needs to be spawned.
/// </summary>
[Serializable, NetSerializable]
public sealed class MuzzleFlashEvent : EntityEventArgs
{
    public NetEntity Uid;
    public string Prototype;

    public Angle Angle;
    public NetEntity? User;

    public MuzzleFlashEvent(NetEntity uid, string prototype, Angle angle, NetEntity? user)
    {
        Uid = uid;
        Prototype = prototype;
        Angle = angle;
        User = user;
    }
}
