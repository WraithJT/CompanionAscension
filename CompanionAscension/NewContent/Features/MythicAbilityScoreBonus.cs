using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using CompanionAscension.NewContent.Components;
using CompanionAscension.Utilities;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;

namespace CompanionAscension.NewContent.Features
{
    class MythicAbilityScoreBonus
    {
        public static readonly string MythicAbilityScoreBonusGUID = "3adf757c5ba741438e9727550ab126d7";
        private static readonly string MythicAbilityScoreBonusName = "MythicAbilityScoreBonus";
        private static readonly string MythicAbilityScoreBonusDisplayName = "Mythic Ability Score Increase";
        private static readonly string MythicAbilityScoreBonusDisplayNameKey = "MythicAbilityScoreBonusName";
        private static readonly string MythicAbilityScoreBonusDescription =
            "Grants a mythic bonus equal to half your mythic rank plus 1 to your highest mental and physical ability scores.";
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

                //PatchMythicAbilityScoreBonus();
                try { PatchMythicAbilityScoreBonus(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchMythicAbilityScoreBonus()
            {
                ContextValue _mythicAbilityScoreBonusContextValue = new()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 1,
                    ValueShared = AbilitySharedValue.StatBonus,
                    Property = UnitProperty.None,
                    ValueRank = AbilityRankType.Default
                };
                Tools.LogMessage("Built: Context Value (Mythic Ability Score Bonus)");

                HighestPhysicalMentalScoreBonus _mythicPhysicalMentalScoreBonusHighestAbilityScoreBonus = new()
                {
                    name = "$HighestPhysicalMentalScoreBonus$35678b97eaba4aae94f4d965b2492ac7",
                    HighestStatBonus = _mythicAbilityScoreBonusContextValue,
                    Descriptor = ModifierDescriptor.Mythic
                };
                Tools.LogMessage("Built: Add Physical Mental Score Bonus (Mythic Ability Score Bonus)");

                ContextRankConfig _mythicAbilityScoreBonusContextRankConfig = new()
                {
                    name = "$ContextRankConfig$31b5cbc3daf2488387600fdc14a3365f",
                    m_BaseValueType = ContextRankBaseValueType.MythicLevel,
                    m_Type = AbilityRankType.Default,
                    m_Progression = ContextRankProgression.OnePlusDivStep,
                    m_StepLevel = 2,
                    m_Max = 10,
                    m_Stat = StatType.Unknown
                };
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
                    .SetHideInUI(true)
                    .Configure();
                _mythicAbilityScoreBonus.AddComponents(new BlueprintComponent[] {
                    _mythicPhysicalMentalScoreBonusHighestAbilityScoreBonus,
                    _mythicAbilityScoreBonusContextRankConfig
                    });
                Tools.LogMessage("Built: Mythic Ability Score Bonus -> " + _mythicAbilityScoreBonus.AssetGuidThreadSafe);
            }
        }
    }
}
