using Content.Shared.Eui;
using Robust.Shared.Serialization;
using Robust.Shared.Map;
using Content.Shared.Eui;
using Robust.Shared.Serialization;
using Robust.Shared.Map;
using Content.Shared.Explosion;
using Content.Shared.Explosion.Components;

namespace Content.Shared._VXS14.Mortar;


    public static class MortarSpawnExplosionEuiMsg
    {
        [Serializable, NetSerializable]
        public sealed class MortarCords : EuiMessageBase
        {
            public MortarCords(MapCoordinates epicenter)
            {
                Epicenter = epicenter;
            }
            public MapCoordinates Epicenter;
        }

    }
