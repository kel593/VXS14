using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Containers;
using Robust.Shared.Random;
using Robust.Shared.IoC;

namespace Content.Shared._VXS14.Mortar
{
    // Component for mortar shell type and power
    [RegisterComponent][AutoGenerateComponentState]
    public partial class SharedMortarShellComponent : Component
    {

        [ViewVariables(VVAccess.ReadWrite), DataField("explosionType"), AutoNetworkedField]
        public string Type = "Default";

        [ViewVariables(VVAccess.ReadWrite), DataField("totalIntensity"), AutoNetworkedField]
        public float TotalIntensity = 105f;

        [ViewVariables(VVAccess.ReadWrite), DataField("slope"), AutoNetworkedField]
        public float Slope = 200f;

        [ViewVariables(VVAccess.ReadWrite), DataField("maxTileIntensity"), AutoNetworkedField]
        public float MaxTileIntensity = 2f;
    }
}
