using Content.Client.Eui;
using Content.Shared.Administration;
using Content.Shared.Eui;
using Content.Shared._VXS14.Mortar;
using JetBrains.Annotations;
using Robust.Client.Graphics;
using Robust.Shared.Map;

namespace Content.Client._VXS14.Mortar;

[UsedImplicitly]
public sealed class MortarEui : BaseEui
{
    [Dependency] private readonly EntityManager _entManager = default!;

    private readonly MortarWindow _window;
    private MapCoordinates _mapCords;

    public MortarEui()
    {
        IoCManager.InjectDependencies(this);
        _window = new MortarWindow(this);
        _window.OnClose += SendClosedMessage;
    }

    public override void Opened()
    {
        base.Opened();
        _window.OpenCentered();
    }

    public override void Closed()
    {
        base.Closed();
        _window.OnClose -= SendClosedMessage;
        _window.Close();
    }

    public void SendClosedMessage()
    {
        SendMessage(new CloseEuiMessage());
    }

    public void SendCords(MapCoordinates cords)
    {
        SendMessage(new MortarSpawnExplosionEuiMsg.MortarCords(cords));
    }



    /// <summary>
    ///     Receive explosion preview data and add a client-side explosion preview overlay
    /// </summary>
    /// <param name="msg"></param>
    public override void HandleMessage(EuiMessageBase msg)
    {

        if (msg is not MortarSpawnExplosionEuiMsg.MortarCords data)
            return;

        _mapCords = data.Epicenter;
    }
}
