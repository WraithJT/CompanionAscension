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
    static class DemonCompanionChoice
    {
        public static readonly string Guid = "57e6f40e11ec421c9b2e3edb34e2beb2";
        //private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string Name = "DemonCompanionChoice";
        private static readonly string DisplayName = "Demon Companion Ascension";
        private static readonly string DisplayNameKey = "DemonCompanionChoiceName";
        private static readonly string Description = "Select one minor demon aspect to gain the passive benefits from that aspect.";
        private static readonly string DescriptionKey = "DemonCompanionChoiceDescription";

        private static readonly string DemonProgression = "285fe49f7df8587468f676aa49362213";

        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]

        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchDemonCompanionChoice();
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

                // Option for Demon Charge?
                // Option for Demon Rage?

                string _demonCompanionAspectOfBabauName = "CompanionAspectOfBabau";
                string _demonCompanionAspectOfBabauGUID = "b3cc3715391248f2993c24c4688d7ac6";
                string _demonCompanionAspectOfBabauDisplayName = "Companion Aspect of Babau";
                string _demonCompanionAspectOfBabauDisplayNameKey = "CompanionAspectOfBabauNameKey";
                string _demonCompanionAspectOfBabauDescription = "stuff";
                string _demonCompanionAspectOfBabauDescriptionKey = "CompanionAspectOfBabauDescriptionKey";
                var _demonCompanionAspectOfBabau = FeatureConfigurator.New(_demonCompanionAspectOfBabauName, _demonCompanionAspectOfBabauGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfBabauDisplayNameKey, _demonCompanionAspectOfBabauDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfBabauDescriptionKey, _demonCompanionAspectOfBabauDescription))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillStealth, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillThievery, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfBabauGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfBrimorakName = "CompanionAspectOfBrimorak";
                string _demonCompanionAspectOfBrimorakGUID = "1135699c0d6f4450b7565bc4ad23a277";
                string _demonCompanionAspectOfBrimorakDisplayName = "Companion Aspect of Brimorak";
                string _demonCompanionAspectOfBrimorakDisplayNameKey = "CompanionAspectOfBrimorakNameKey";
                string _demonCompanionAspectOfBrimorakDescription = "things";
                string _demonCompanionAspectOfBrimorakDescriptionKey = "CompanionAspectOfBrimorakDescriptionKey";
                var _demonCompanionAspectOfBrimorak = FeatureConfigurator.New(_demonCompanionAspectOfBrimorakName, _demonCompanionAspectOfBrimorakGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfBrimorakDisplayNameKey, _demonCompanionAspectOfBrimorakDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfBrimorakDescriptionKey, _demonCompanionAspectOfBrimorakDescription))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillThievery, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfBrimorakGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfIncubusName = "CompanionAspectOfIncubus";
                string _demonCompanionAspectOfIncubusGUID = "b40ecca2e1e34771bf6441b9c0741400";
                string _demonCompanionAspectOfIncubusDisplayName = "Companion Aspect of Incubus";
                string _demonCompanionAspectOfIncubusDisplayNameKey = "CompanionAspectOfIncubusNameKey";
                string _demonCompanionAspectOfIncubusDescription = "words";
                string _demonCompanionAspectOfIncubusDescriptionKey = "CompanionAspectOfIncubusDescriptionKey";
                var _demonCompanionAspectOfIncubus = FeatureConfigurator.New(_demonCompanionAspectOfIncubusName, _demonCompanionAspectOfIncubusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfIncubusDisplayNameKey, _demonCompanionAspectOfIncubusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfIncubusDescriptionKey, _demonCompanionAspectOfIncubusDescription))
                    .AddContextStatBonus(StatType.SkillPersuasion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreNature, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfIncubusGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfKalavakusName = "CompanionAspectOfKalavakus";
                string _demonCompanionAspectOfKalavakusGUID = "a45213d9014f469db6f948a8bf2b4d81";
                string _demonCompanionAspectOfKalavakusDisplayName = "Companion Aspect of Kalavakus";
                string _demonCompanionAspectOfKalavakusDisplayNameKey = "CompanionAspectOfKalavakusNameKey";
                string _demonCompanionAspectOfKalavakusDescription = "asdf";
                string _demonCompanionAspectOfKalavakusDescriptionKey = "CompanionAspectOfKalavakusDescriptionKey";
                var _demonCompanionAspectOfKalavakus = FeatureConfigurator.New(_demonCompanionAspectOfKalavakusName, _demonCompanionAspectOfKalavakusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfKalavakusDisplayNameKey, _demonCompanionAspectOfKalavakusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfKalavakusDescriptionKey, _demonCompanionAspectOfKalavakusDescription))
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfKalavakusGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfNabasuName = "CompanionAspectOfNabasu";
                string _demonCompanionAspectOfNabasuGUID = "d8d930a7d1424ed09c8d587405bab4b7";
                string _demonCompanionAspectOfNabasuDisplayName = "Companion Aspect of Nabasu";
                string _demonCompanionAspectOfNabasuDisplayNameKey = "CompanionAspectOfNabasuNameKey";
                string _demonCompanionAspectOfNabasuDescription = "1234";
                string _demonCompanionAspectOfNabasuDescriptionKey = "CompanionAspectOfNabasuDescriptionKey";
                var _demonCompanionAspectOfNabasu = FeatureConfigurator.New(_demonCompanionAspectOfNabasuName, _demonCompanionAspectOfNabasuGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfNabasuDisplayNameKey, _demonCompanionAspectOfNabasuDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfNabasuDescriptionKey, _demonCompanionAspectOfNabasuDescription))
                    .AddContextStatBonus(StatType.SkillLoreReligion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfNabasuGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfSchirName = "CompanionAspectOfSchir";
                string _demonCompanionAspectOfSchirGUID = "56b48c98a2914688b7139c55417686e2";
                string _demonCompanionAspectOfSchirDisplayName = "Companion Aspect of Schir";
                string _demonCompanionAspectOfSchirDisplayNameKey = "CompanionAspectOfSchirNameKey";
                string _demonCompanionAspectOfSchirDescription = "werds";
                string _demonCompanionAspectOfSchirDescriptionKey = "CompanionAspectOfSchirDescriptionKey";
                var _demonCompanionAspectOfSchir = FeatureConfigurator.New(_demonCompanionAspectOfSchirName, _demonCompanionAspectOfSchirGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfSchirDisplayNameKey, _demonCompanionAspectOfSchirDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfSchirDescriptionKey, _demonCompanionAspectOfSchirDescription))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreNature, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfSchirGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfSuccubusName = "CompanionAspectOfSuccubus";
                string _demonCompanionAspectOfSuccubusGUID = "db8cc17589d54f80836771c47a364738";
                string _demonCompanionAspectOfSuccubusDisplayName = "Companion Aspect of Succubus";
                string _demonCompanionAspectOfSuccubusDisplayNameKey = "CompanionAspectOfSuccubusNameKey";
                string _demonCompanionAspectOfSuccubusDescription = "asdfagdsa";
                string _demonCompanionAspectOfSuccubusDescriptionKey = "CompanionAspectOfSuccubusDescriptionKey";
                var _demonCompanionAspectOfSuccubus = FeatureConfigurator.New(_demonCompanionAspectOfSuccubusName, _demonCompanionAspectOfSuccubusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfSuccubusDisplayNameKey, _demonCompanionAspectOfSuccubusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfSuccubusDescriptionKey, _demonCompanionAspectOfSuccubusDescription))
                    .AddContextStatBonus(StatType.SkillPersuasion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillKnowledgeWorld, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfSuccubusGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfVrockName = "CompanionAspectOfVrock";
                string _demonCompanionAspectOfVrockGUID = "df86167a7957445cb26f1e6fbad955ee";
                string _demonCompanionAspectOfVrockDisplayName = "Companion Aspect of Vrock";
                string _demonCompanionAspectOfVrockDisplayNameKey = "CompanionAspectOfVrockNameKey";
                string _demonCompanionAspectOfVrockDescription = "vrick";
                string _demonCompanionAspectOfVrockDescriptionKey = "CompanionAspectOfVrockDescriptionKey";
                var _demonCompanionAspectOfVrock = FeatureConfigurator.New(_demonCompanionAspectOfVrockName, _demonCompanionAspectOfVrockGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfVrockDisplayNameKey, _demonCompanionAspectOfVrockDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfVrockDescriptionKey, _demonCompanionAspectOfVrockDescription))
                    .AddContextStatBonus(StatType.SkillKnowledgeArcana, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillKnowledgeWorld, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreReligion, _demonCompanionSkillsContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionSkillsContextRankConfig)
                    .PrerequisiteNoFeature(_demonCompanionAspectOfVrockGUID)
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonMinorAspectChoiceSelectionName = "CompanionMinorAspectChoiceSelection";
                string _demonMinorAspectChoiceSelectionGUID = "427ce8b5d8b24cfb82f21582a2ad68f6";
                string _demonMinorAspectChoiceSelectionDisplayName = "Companion Minor Aspect Choice Selection";
                string _demonMinorAspectChoiceSelectionDisplayNameKey = "CompanionMinorAspectChoiceSelectionNameKey";
                string _demonMinorAspectChoiceSelectionDescription = "do something";
                string _demonMinorAspectChoiceSelectionDescriptionKey = "CompanionMinorAspectChoiceSelectionDescriptionKey";
                var _demonMinorAspectChoiceSelectionSelection = FeatureSelectionConfigurator.New(_demonMinorAspectChoiceSelectionName, _demonMinorAspectChoiceSelectionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonMinorAspectChoiceSelectionDisplayNameKey, _demonMinorAspectChoiceSelectionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonMinorAspectChoiceSelectionDescriptionKey, _demonMinorAspectChoiceSelectionDescription))
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideInUi(true)
                    .SetHideNotAvailableInUI(true)
                    .AddToFeatures(new string[] {
                        _demonCompanionAspectOfBabauGUID,
                        _demonCompanionAspectOfBrimorakGUID,
                        _demonCompanionAspectOfIncubusGUID,
                        _demonCompanionAspectOfKalavakusGUID,
                        _demonCompanionAspectOfNabasuGUID,
                        _demonCompanionAspectOfSchirGUID,
                        _demonCompanionAspectOfSuccubusGUID,
                        _demonCompanionAspectOfVrockGUID
                    })
                    .Configure();

                string _demonMinorAspectChoiceName = "CompanionMinorAspectChoice";
                string _demonMinorAspectChoiceGUID = "906077e087064ff3a44a15a96b62655c";
                string _demonMinorAspectChoiceDisplayName = "Demon Minor Aspect Choice";
                string _demonMinorAspectChoiceDisplayNameKey = "CompanionMinorAspectChoiceNameKey";
                string _demonMinorAspectChoiceDescription = "do something";
                string _demonMinorAspectChoiceDescriptionKey = "CompanionMinorAspectChoiceDescriptionKey";
                var _demonMinorAspectChoice = FeatureConfigurator.New(_demonMinorAspectChoiceName, _demonMinorAspectChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonMinorAspectChoiceDisplayNameKey, _demonMinorAspectChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonMinorAspectChoiceDescriptionKey, _demonMinorAspectChoiceDescription))
                    .AddToFeatureGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect, FeatureGroup.MythicAdditionalProgressions })
                    .Configure();
                _demonMinorAspectChoice.AddSelectionCallback(_demonMinorAspectChoiceSelectionSelection, MythicCompanionProgression);

                //PrerequisiteFeature _demonAspectPrerequisite = new();
                //_demonAspectPrerequisite.CheckInProgression = true;
                //_demonAspectPrerequisite.Group = Prerequisite.GroupType.All;
                //_demonAspectPrerequisite.m_Feature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(_demonMinorAspectChoiceGUID).ToReference<BlueprintFeatureReference>();
                //_demonAspectPrerequisite.name = "customprereqname";
                ////_demonMinorAspectChoiceSelectionSelection.AddComponents(_demonAspectPrerequisite);

                var _demonCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddToFeatures("247a4068296e8be42890143f451b4b45")
                    .AddToFeatures(_demonMinorAspectChoiceGUID)
                    //.PrerequisitePlayerHasFeature(DemonProgression)
                    .SetHideInUi(true)
                    .Configure();
                Tools.LogMessage("Built: Demon Companion Choices -> " + _demonCompanionChoice.AssetGuidThreadSafe);
            }

        }
        private static void AddSelectionCallback(this BlueprintFeature Feature, BlueprintFeatureSelection Selection, BlueprintProgression Progression)
        {
            Feature.AddComponent(Helpers.Create<AddAdditionalMythicFeatures>(c =>
            {
                c.Features = new BlueprintFeatureBaseReference[] { Selection.ToReference<BlueprintFeatureBaseReference>() };
                c.Source = FeatureSource.GetMythicSource(Progression);
            }));
        }
    }
}
