// Inspiration and code taken from Vek17's TabletopTweaks and cabarius's ToyBox
// TabletopTweaks-Core: https://github.com/Vek17/TabletopTweaks-Core
// ToyBox: https://github.com/cabarius/ToyBox
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Kingmaker.Armies.TacticalCombat.Parts;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Blueprints.Root;
using Kingmaker.ElementsSystem;
using Kingmaker.EntitySystem.Entities;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.PubSubSystem;
using Kingmaker.QA;
using Kingmaker.QA.Statistics;
using Kingmaker.UnitLogic.ActivatableAbilities;
using Kingmaker.UnitLogic.Buffs;
using Kingmaker.UnitLogic.Class.LevelUp.Actions;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Parts;
using Kingmaker.Utility;
using Kingmaker.View;
using Newtonsoft.Json;
using Owlcat.Runtime.Core.Utils;
using UnityEngine;
using System.ComponentModel;
using Kingmaker;
using Kingmaker.UnitLogic;
using HarmonyLib;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.Class;
using Kingmaker.UnitLogic.Class.LevelUp;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.Skills;
using Kingmaker.UI.MVVM._VM.CharGen.Phases.FeatureSelector;
using Kingmaker.UI.MVVM._VM.ServiceWindows.CharacterInfo.Sections.LevelClassScores.Experience;
using Kingmaker.UI.ServiceWindow;
using System.Reflection;
using System.Reflection.Emit;
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
