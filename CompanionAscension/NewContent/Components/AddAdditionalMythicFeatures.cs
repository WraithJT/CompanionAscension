// Credit to Vek17 for pieces of this: https://github.com/Vek17/TabletopTweaks-Core

using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UI.Common;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.FeatureSelector;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using System.Linq;

namespace CompanionAscension.NewContent.Components
{
    [TypeId("433e50ea9c8441c8ad72db5f63251e78")]
    public class AddAdditionalMythicFeatures : UnitFactComponentDelegate
    {

        public override void OnActivate()
        {
            LevelUpController controller = Kingmaker.Game.Instance?.LevelUpController;
            if (controller == null) { return; }
            if (controller.State.Mode != LevelUpState.CharBuildMode.Mythic) { return; }
            
            LevelUpHelper.AddFeaturesFromProgression(controller.State, Owner, this.Features.Select(f => f.Get()).ToArray(), Source, 0);
        }

        public BlueprintFeatureBaseReference[] Features;

        public FeatureSource Source;

        [HarmonyPatch(typeof(CharGenFeatureSelectorPhaseVM), "OrderPriority", MethodType.Getter)]
        static class Background_OrderPriority_Patch
        {
            static void Postfix(ref int __result, CharGenFeatureSelectorPhaseVM __instance)
            {
                FeatureGroup featureGroup = UIUtilityUnit.GetFeatureGroup(__instance.FeatureSelectorStateVM?.Feature);
                if (featureGroup == FeatureGroup.BackgroundSelection) { __result += 500; }
            }
        }
    }
}