using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Containers;
using Robust.Shared.Random;
using Robust.Shared.IoC;

namespace Content.Shared.VXS.Forts
{
    // Component for fort
    [RegisterComponent][AutoGenerateComponentState]
    public partial class SharedVXSFortStructComponent : Component
    {
        [ViewVariables(VVAccess.ReadWrite), DataField("team"), AutoNetworkedField]
        public string Team = "fort-debug-state";

    }
}
