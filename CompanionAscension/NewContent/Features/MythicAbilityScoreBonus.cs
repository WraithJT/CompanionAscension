using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.Configurators.Abilities;
using BlueprintCore.Blueprints.Components;
using BlueprintCore.Utils;
using HarmonyLib;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Properties;
using Kingmaker.Blueprints.Classes.Prerequisites;
using System;
using CompanionAscension.Utilities;
using BlueprintCore.Blueprints.Configurators.UnitLogic;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Customization;
using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
using BlueprintCore.Blueprints.Configurators.EntitySystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.Utility;
using System.Linq;
using Kingmaker.EntitySystem;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Buffs.Blueprints;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics.Components;
using BlueprintCore.Conditions.Builder;
using Kingmaker.Designers.Mechanics.Facts;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using CompanionAscension.Utilities.TTTCore;
using System.Text.RegularExpressions;
using CompanionAscension.NewContent.Components;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Abilities;

namespace CompanionAscension.NewContent.Features
{
    class MythicAbilityScoreBonus
    {
        private static readonly string MythicAbilityScoreBonusName = "MythicAbilityScoreBonus";
        private static readonly string MythicAbilityScoreBonusGUID = "3adf757c5ba741438e9727550ab126d7";
        private static readonly string MythicAbilityScoreBonusDisplayName = "Mythic Ability Score Increase";
        private static readonly string MythicAbilityScoreBonusDisplayNameKey = "MythicAbilityScoreBonusName";
        private static readonly string MythicAbilityScoreBonusDescription = 
            "Increases your highest ability score by an amount equal to 1 plus half your mythic level.";
        private static readonly string MythicAbilityScoreBonusDescriptionKey = "MythicAbilityScoreBonusDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchMythicAbilityScoreBonus();
                //try { PatchMythicAbilityScoreBonus(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchMythicAbilityScoreBonus()
            {
                ContextValue _mythicAbilityScoreBonusContextValue = new();
                _mythicAbilityScoreBonusContextValue.ValueType = ContextValueType.Rank;
                _mythicAbilityScoreBonusContextValue.Value = 1;
                _mythicAbilityScoreBonusContextValue.ValueShared = AbilitySharedValue.StatBonus;
                _mythicAbilityScoreBonusContextValue.Property = UnitProperty.None;
                _mythicAbilityScoreBonusContextValue.ValueRank = AbilityRankType.Default;
                Tools.LogMessage("Built: Context Value (Mythic Ability Score Bonus)");

                HighestAbilityScoreBonus _mythicAbilityScoreBonusHighestAbilityScoreBonus = new();
                _mythicAbilityScoreBonusHighestAbilityScoreBonus.name = "$AddMaxAbilityScoreBonus$35678b97eaba4aae94f4d965b2492ac7";
                _mythicAbilityScoreBonusHighestAbilityScoreBonus.HighestStatBonus = _mythicAbilityScoreBonusContextValue;
                _mythicAbilityScoreBonusHighestAbilityScoreBonus.Descriptor = ModifierDescriptor.Mythic;
                Tools.LogMessage("Built: Add Highest Ability Score Bonus (Mythic Ability Score Bonus)");

                ContextRankConfig _mythicAbilityScoreBonusContextRankConfig = new();
                _mythicAbilityScoreBonusContextRankConfig.name = "$ContextRankConfig$31b5cbc3daf2488387600fdc14a3365f";
                _mythicAbilityScoreBonusContextRankConfig.m_BaseValueType = ContextRankBaseValueType.MythicLevel;
                _mythicAbilityScoreBonusContextRankConfig.m_Type = AbilityRankType.Default;
                _mythicAbilityScoreBonusContextRankConfig.m_Progression = ContextRankProgression.OnePlusDivStep;
                _mythicAbilityScoreBonusContextRankConfig.m_StepLevel = 2;
                _mythicAbilityScoreBonusContextRankConfig.m_Max = 10;
                _mythicAbilityScoreBonusContextRankConfig.m_Stat = StatType.Unknown;
                Tools.LogMessage("Built: Context Rank Config (Mythic Ability Score Bonus)");

                var _mythicAbilityScoreBonus = FeatureConfigurator.New(MythicAbilityScoreBonusName, MythicAbilityScoreBonusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(MythicAbilityScoreBonusDisplayNameKey, MythicAbilityScoreBonusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(MythicAbilityScoreBonusDescriptionKey, MythicAbilityScoreBonusDescription))
                    .AddRecalculateOnStatChange(stat: StatType.Strength)
                    .AddRecalculateOnStatChange(stat: StatType.Dexterity)
                    .AddRecalculateOnStatChange(stat: StatType.Constitution)
                    .AddRecalculateOnStatChange(stat: StatType.Wisdom)
                    .AddRecalculateOnStatChange(stat: StatType.Intelligence)
                    .AddRecalculateOnStatChange(stat: StatType.Charisma)
                    .SetReapplyOnLevelUp(true)
                    .Configure();
                _mythicAbilityScoreBonus.AddComponents(new BlueprintComponent[] {
                    _mythicAbilityScoreBonusHighestAbilityScoreBonus,
                    _mythicAbilityScoreBonusContextRankConfig });
                Tools.LogMessage("Built: Mythic Ability Score Bonus -> " + _mythicAbilityScoreBonus.AssetGuidThreadSafe);
            }
        }
    }
}
