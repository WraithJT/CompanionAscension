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
using BlueprintCore.Blueprints.Components;

namespace CompanionAscension.NewContent.Features
{
    class DemonCompanionChoice
    {
        public static readonly string Guid = "57e6f40e11ec421c9b2e3edb34e2beb2";
        //private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string Name = "DemonCompanionChoice";
        private static readonly string DisplayName = "Demon Companion Ascension";
        private static readonly string DisplayNameKey = "DemonCompanionChoiceName";
        private static readonly string Description = "Select one minor demon aspect to gain the passive benefits from that aspect.";
        private static readonly string DescriptionKey = "DemonCompanionChoiceDescription";

        private static readonly string DemonProgression = "285fe49f7df8587468f676aa49362213";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]

        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchDemonCompanionChoice();
                //try { PatchDemonCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchDemonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Demon Companion Choices");

                ContextRankConfig _demonCompanionSkillsContextRankConfig = new();
                _demonCompanionSkillsContextRankConfig.name = "$ContextRankConfig$df14696f1e9e4e0d861b6d1d1e0f8f20";
                _demonCompanionSkillsContextRankConfig.m_BaseValueType = ContextRankBaseValueType.MythicLevel;
                _demonCompanionSkillsContextRankConfig.m_Type = AbilityRankType.Default;
                _demonCompanionSkillsContextRankConfig.m_Progression = ContextRankProgression.OnePlusDivStep;
                _demonCompanionSkillsContextRankConfig.m_StepLevel = 2;
                _demonCompanionSkillsContextRankConfig.m_Max = 10;
                _demonCompanionSkillsContextRankConfig.m_Stat = StatType.Unknown;
                Tools.LogMessage("Built: Skills Context Rank Config (Demon Companion Ascension)");

                ContextValue _demonCompanionSkillsContextValue = new();
                _demonCompanionSkillsContextValue.ValueType = ContextValueType.Rank;
                _demonCompanionSkillsContextValue.Value = 1;
                _demonCompanionSkillsContextValue.ValueShared = AbilitySharedValue.StatBonus;
                _demonCompanionSkillsContextValue.Property = UnitProperty.None;
                _demonCompanionSkillsContextValue.ValueRank = AbilityRankType.Default;
                Tools.LogMessage("Built: Context Value (Mythic Ability Score Bonus)");

                var _demonCompanionAspectOfBabau = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillStealth, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillThievery, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfBrimorak = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillThievery, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfIncubus = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillPersuasion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreNature, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfKalavakus = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfNabasu = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillLoreReligion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfSchir = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreNature, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfSuccubus = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillPersuasion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillKnowledgeWorld, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionAspectOfVrock = FeatureConfigurator.New("name", "guid")
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddContextStatBonus(StatType.SkillKnowledgeArcana, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillKnowledgeWorld, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreReligion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                var _demonCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    //.PrerequisitePlayerHasFeature(DemonProgression)
                    //.SetHideInUi(true)
                    .Configure();
                //_demonCompanionChoice.m_AllFeatures = DemonUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Demon Companion Choices -> " + _demonCompanionChoice.AssetGuidThreadSafe);
            }

            private static void ConfigAspects()
            {

            }

            public class Aspect
            {
                public StatType[] Skills;
            }
        }
    }
}
