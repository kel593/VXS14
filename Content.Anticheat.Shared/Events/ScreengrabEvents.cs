﻿// ***
// This Source Code Form is subject to the terms of the Mozilla Public License, v. 2.0.
// If a copy of the MPL was not distributed with this file, You can obtain one at https://mozilla.org/MPL/2.0/.
// This Source Code Form is "Incompatible With Secondary Licenses", as defined by the Mozilla Public License, v. 2.0.
// ***

using Robust.Shared.GameObjects;
using Robust.Shared.Serialization;

namespace Content.Anticheat.Shared.Events;

[Serializable, NetSerializable]
public sealed class ScreengrabRequestEvent : ExpectedReplyEntityEventArgs
{
    [field: NonSerialized]
    public override Type ExpectedReplyType { get; } = typeof(ScreengrabResponseEvent);
}

[Serializable, NetSerializable]
public sealed class ScreengrabResponseEvent : EntityEventArgs
{
    // Limit screengrab size to 1.5mbs
    public byte[] Screengrab = new byte[1500000];
}
