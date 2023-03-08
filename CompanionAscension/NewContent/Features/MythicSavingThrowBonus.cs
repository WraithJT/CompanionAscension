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
    class MythicSavingThrowBonus
    {
        public static readonly string MythicSavingThrowBonusGUID = "b49f559f4daa4a87b080eccf1a0dc9a9";
        private static readonly string MythicSavingThrowBonusName = "MythicSavingThrowBonus";
        private static readonly string MythicSavingThrowBonusDisplayName = "Mythic Saving Throw Bonus";
        private static readonly string MythicSavingThrowBonusDisplayNameKey = "MythicSavingThrowBonusDisplayNameKey";
        private static readonly string MythicSavingThrowBonusDescription =
            "Grants a mythic bonus equal to your mythic rank to your lowest saving throw.";
        private static readonly string MythicSavingThrowBonusDescriptionKey = "MythicSavingThrowBonusDescriptionKey";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchMythicSavingThrowBonus();
                try { PatchMythicSavingThrowBonus(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchMythicSavingThrowBonus()
            {
                ContextValue _mythicSavingThrowBonusContextValue = new()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 1,
                    ValueShared = AbilitySharedValue.StatBonus,
                    Property = UnitProperty.None,
                    ValueRank = AbilityRankType.Default
                };
                Tools.LogMessage("Built: Context Value (Mythic Saving Throw Bonus)");

                LowestSaveBonus _mythicSavingThrowBonusHighestAbilityScoreBonus = new()
                {
                    name = "$AddMaxAbilityScoreBonus$35678b97eaba4aae94f4d965b2492ac7",
                    LowestScoreBonus = _mythicSavingThrowBonusContextValue,
                    Descriptor = ModifierDescriptor.Mythic
                };
                Tools.LogMessage("Built: Add Lowest Saving Throw Bonus (Mythic Saving Throw Bonus)");

                ContextRankConfig _mythicSavingThrowBonusContextRankConfig = new()
                {
                    name = "$ContextRankConfig$31b5cbc3daf2488387600fdc14a3365f",
                    m_BaseValueType = ContextRankBaseValueType.MythicLevel,
                    m_Type = AbilityRankType.Default,
                    m_Progression = ContextRankProgression.AsIs,
                    m_Max = 10,
                    m_Stat = StatType.Unknown
                };
                Tools.LogMessage("Built: Context Rank Config (Mythic Saving Throw Bonus)");

                var _mythicSavingThrowBonus = FeatureConfigurator.New(MythicSavingThrowBonusName, MythicSavingThrowBonusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(MythicSavingThrowBonusDisplayNameKey, MythicSavingThrowBonusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(MythicSavingThrowBonusDescriptionKey, MythicSavingThrowBonusDescription))
                    .SetReapplyOnLevelUp(true)
                    .SetHideInUI(true)
                    .Configure();
                _mythicSavingThrowBonus.AddComponents(new BlueprintComponent[] {
                    _mythicSavingThrowBonusHighestAbilityScoreBonus,
                    _mythicSavingThrowBonusContextRankConfig
                    });
                Tools.LogMessage("Built: Mythic Saving Throw Bonus -> " + _mythicSavingThrowBonus.AssetGuidThreadSafe);
            }
        }
    }
}
