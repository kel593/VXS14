using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Containers;
using Robust.Shared.Random;
using Robust.Shared.IoC;
using Robust.Shared.Player;
using Robust.Server.Player;
using Robust.Shared.Network;

namespace Content.Server.VXS.Team
{
    [RegisterComponent]
    public partial class VXSTeamRuleComponent : Component
    {
        public Dictionary<ICommonSession, string> playerJobs = new();


    }

}
