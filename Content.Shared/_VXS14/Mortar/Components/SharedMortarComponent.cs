using Robust.Shared.GameObjects;
using Robust.Shared.Map;
using Robust.Shared.Containers;
using Robust.Shared.Random;
using Robust.Shared.IoC;

namespace Content.Shared._VXS14.Mortar
{
    [RegisterComponent][AutoGenerateComponentState]
    public partial class SharedMortarComponent : Component
    {

        [ViewVariables(VVAccess.ReadWrite), DataField("accuracy"), AutoNetworkedField]
        public float Accuracy = 1f;

    }
}
