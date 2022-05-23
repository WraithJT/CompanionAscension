// Inspiration and code taken from Vek17's TabletopTweaks and cabarius's ToyBox
// TabletopTweaks-Core: https://github.com/Vek17/TabletopTweaks-Core
// ToyBox: https://github.com/cabarius/ToyBox
using HarmonyLib;
using Kingmaker;
using Kingmaker.Blueprints.Classes;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;
using System.Collections.Generic;
using static CompanionAscension.NewContent.Components.CustomMechanicsFeatures;

namespace CompanionAscension.NewContent.Components
{
    [HarmonyPatch(typeof(UnitProgressionData))]
    public class UnitProgressionData_LegendaryCompanion
    {
        [HarmonyPatch(nameof(UnitProgressionData.ExperienceTable), MethodType.Getter)]
        private static bool Prefix(ref BlueprintStatProgression __result, UnitProgressionData __instance)
        {

            if (__instance.Owner.State.Features.LegendaryHero || __instance.Owner.CustomMechanicsFeature(CustomMechanicsFeature.LegendaryCompanion))
                __result = Game.Instance.BlueprintRoot.Progression.LegendXPTable;
            else
                return true;

            return false;
        }

        [HarmonyPatch(nameof(UnitProgressionData.MaxCharacterLevel), MethodType.Getter)]
        private static bool Prefix(ref int __result, UnitProgressionData __instance)
        {
            if (__instance.Owner.State.Features.LegendaryHero)
                __result = 40;
            else if (__instance.Owner.CustomMechanicsFeature(CustomMechanicsFeature.LegendaryCompanion))
                __result = 24;
            else
                return true;

            return false;
        }
    }

    public class CustomMechanicsFeatures : OldStyleUnitPart
    {
        public void AddMechanicsFeature(CustomMechanicsFeature type)
        {
            CountableFlag MechanicsFeature = GetMechanicsFeature(type);
            MechanicsFeature.Retain();
        }

        public void RemoveMechanicsFeature(CustomMechanicsFeature type)
        {
            CountableFlag MechanicsFeature = GetMechanicsFeature(type);
            MechanicsFeature.Release();
        }

        public void ClearMechanicsFeature(CustomMechanicsFeature type)
        {
            CountableFlag MechanicsFeature = GetMechanicsFeature(type);
            MechanicsFeature.ReleaseAll();
        }

        public CountableFlag GetMechanicsFeature(CustomMechanicsFeature type)
        {
            CountableFlag MechanicsFeature;
            MechanicsFeatures.TryGetValue(type, out MechanicsFeature);
            if (MechanicsFeature == null)
            {
                MechanicsFeature = new CountableFlag();
                MechanicsFeatures[type] = MechanicsFeature;
            }
            return MechanicsFeature;
        }

        private readonly Dictionary<CustomMechanicsFeature, CountableFlag> MechanicsFeatures = new Dictionary<CustomMechanicsFeature, CountableFlag>();

        public enum CustomMechanicsFeature : int
        {
            LegendaryCompanion = 1555
        }
    }

    public static class CustomMechanicsFeaturesExtentions
    {
        public static CountableFlag CustomMechanicsFeature(this UnitDescriptor unit, CustomMechanicsFeature type)
        {

            var mechanicsFeatures = unit.Ensure<CustomMechanicsFeatures>();
            return mechanicsFeatures.GetMechanicsFeature(type);
        }

        public static CountableFlag CustomMechanicsFeature(this UnitEntityData unit, CustomMechanicsFeature type)
        {
            return unit.Descriptor.CustomMechanicsFeature(type);
        }
    }
}
