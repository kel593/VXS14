namespace Content.Server._VXS.TriggerOnLand
{
    [RegisterComponent]
    public sealed partial class TriggerOnLandComponent : Component
    {
        [DataField("Chance")] public float Prob = 1f;
    }
}
