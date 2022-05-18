// Inspiration and code taken from Vek17's TabletopTweaks
// TabletopTweaks-Core: https://github.com/Vek17/TabletopTweaks-Core

using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using CompanionAscension.NewContent.Components;

namespace Kingmaker.Designers.Mechanics.Facts
{
    [TypeId("853449d83ed8468abbdaaed294bdef09")]
    public class AddCustomMechanicsFeature : UnitFactComponentDelegate
    {
        public override void OnTurnOn()
        {
            Owner.CustomMechanicsFeature(Feature).Retain();
        }

        public override void OnTurnOff()
        {
            Owner.CustomMechanicsFeature(Feature).Release();
        }

        public CustomMechanicsFeatures.CustomMechanicsFeature Feature;
    }
}
