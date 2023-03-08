using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using CompanionAscension.NewContent.Components;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities;
using Kingmaker.UnitLogic.Buffs.Components;
using Kingmaker.UnitLogic.FactLogic;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.UnitLogic.Mechanics.Properties;
using System;

namespace CompanionAscension.NewContent.Features
{
    static class DemonCompanionChoice
    {
        public static readonly string Guid = "57e6f40e11ec421c9b2e3edb34e2beb2";
        private static readonly string Name = "DemonCompanionChoice";
        private static readonly string DisplayName = "Demon Companion Ascension";
        private static readonly string DisplayNameKey = "DemonCompanionChoiceName";
        private static readonly string Description = "At 8th mythic rank, the Demon's companions can gain further power.";
        private static readonly string DescriptionKey = "DemonCompanionChoiceDescription";

        private static readonly string DemonProgression = "285fe49f7df8587468f676aa49362213";
        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");

        private static readonly BlueprintFeature DemonRageFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("6a8af3f208a0fa747a465b70b7043019");
        private static readonly BlueprintFeature DemonChargeFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("9586810ab12098f4e979d5a13a4e94df");
        private static readonly BlueprintFeature BabauAspectFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("99a34a0fa0c3a154fbc5b11fe2d18009");

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
                try { PatchDemonCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchDemonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Demon Companion Choices");

                ContextRankConfig _demonCompanionContextRankConfig = new()
                {
                    name = "$ContextRankConfig$df14696f1e9e4e0d861b6d1d1e0f8f20",
                    m_BaseValueType = ContextRankBaseValueType.MythicLevel,
                    m_Type = AbilityRankType.Default,
                    m_Progression = ContextRankProgression.OnePlusDivStep,
                    m_StepLevel = 2,
                    m_Max = 10,
                    m_Stat = StatType.Unknown
                };
                Tools.LogMessage("Built: Skills Context Rank Config (Demon Companion Ascension)");

                ContextValue _demonCompanionContextValue = new()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 1,
                    ValueShared = AbilitySharedValue.StatBonus,
                    Property = UnitProperty.None,
                    ValueRank = AbilityRankType.Default
                };
                Tools.LogMessage("Built: Context Value (Demon Companion Bonus)");

                string _demonCompanionAspectOfBabauName = "CompanionAspectOfBabau";
                string _demonCompanionAspectOfBabauGUID = "b3cc3715391248f2993c24c4688d7ac6";
                string _demonCompanionAspectOfBabauDisplayName = "Aspect of Babau";
                string _demonCompanionAspectOfBabauDisplayNameKey = "CompanionAspectOfBabauNameKey";
                string _demonCompanionAspectOfBabauDescription =
                    "You adopt the aspect of Babau, gaining a bonus on all Mobility, Trickery, " +
                    "and Stealth skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfBabauDescriptionKey = "CompanionAspectOfBabauDescriptionKey";
                var _demonCompanionAspectOfBabau = FeatureConfigurator.New(_demonCompanionAspectOfBabauName, _demonCompanionAspectOfBabauGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfBabauDisplayNameKey, _demonCompanionAspectOfBabauDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfBabauDescriptionKey, _demonCompanionAspectOfBabauDescription))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillStealth, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillThievery, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfBabauGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfBrimorakName = "CompanionAspectOfBrimorak";
                string _demonCompanionAspectOfBrimorakGUID = "1135699c0d6f4450b7565bc4ad23a277";
                string _demonCompanionAspectOfBrimorakDisplayName = "Aspect of Brimorak";
                string _demonCompanionAspectOfBrimorakDisplayNameKey = "CompanionAspectOfBrimorakNameKey";
                string _demonCompanionAspectOfBrimorakDescription =
                    "You adopt the aspect of Brimorak, gaining a bonus on all Mobility, Trickery, " +
                    "and Use Magic Device skill checks equal to half of your mythic rank plus one. " +
                    "Those bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfBrimorakDescriptionKey = "CompanionAspectOfBrimorakDescriptionKey";
                var _demonCompanionAspectOfBrimorak = FeatureConfigurator.New(_demonCompanionAspectOfBrimorakName, _demonCompanionAspectOfBrimorakGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfBrimorakDisplayNameKey, _demonCompanionAspectOfBrimorakDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfBrimorakDescriptionKey, _demonCompanionAspectOfBrimorakDescription))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillThievery, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfBrimorakGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfIncubusName = "CompanionAspectOfIncubus";
                string _demonCompanionAspectOfIncubusGUID = "b40ecca2e1e34771bf6441b9c0741400";
                string _demonCompanionAspectOfIncubusDisplayName = "Aspect of Incubus";
                string _demonCompanionAspectOfIncubusDisplayNameKey = "CompanionAspectOfIncubusNameKey";
                string _demonCompanionAspectOfIncubusDescription =
                    "You adopt the aspect of Incubus, gaining a bonus on all Persuasion, Athletics, " +
                    "and Lore (nature) skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfIncubusDescriptionKey = "CompanionAspectOfIncubusDescriptionKey";
                var _demonCompanionAspectOfIncubus = FeatureConfigurator.New(_demonCompanionAspectOfIncubusName, _demonCompanionAspectOfIncubusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfIncubusDisplayNameKey, _demonCompanionAspectOfIncubusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfIncubusDescriptionKey, _demonCompanionAspectOfIncubusDescription))
                    .AddContextStatBonus(StatType.SkillPersuasion, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreNature, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfIncubusGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfKalavakusName = "CompanionAspectOfKalavakus";
                string _demonCompanionAspectOfKalavakusGUID = "a45213d9014f469db6f948a8bf2b4d81";
                string _demonCompanionAspectOfKalavakusDisplayName = "Aspect of Kalavakus";
                string _demonCompanionAspectOfKalavakusDisplayNameKey = "CompanionAspectOfKalavakusNameKey";
                string _demonCompanionAspectOfKalavakusDescription =
                    "You adopt the aspect of Kalavakus, gaining a bonus on all Perception, Athletics, " +
                    "and Use Magic Device skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfKalavakusDescriptionKey = "CompanionAspectOfKalavakusDescriptionKey";
                var _demonCompanionAspectOfKalavakus = FeatureConfigurator.New(_demonCompanionAspectOfKalavakusName, _demonCompanionAspectOfKalavakusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfKalavakusDisplayNameKey, _demonCompanionAspectOfKalavakusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfKalavakusDescriptionKey, _demonCompanionAspectOfKalavakusDescription))
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfKalavakusGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfNabasuName = "CompanionAspectOfNabasu";
                string _demonCompanionAspectOfNabasuGUID = "d8d930a7d1424ed09c8d587405bab4b7";
                string _demonCompanionAspectOfNabasuDisplayName = "Aspect of Nabasu";
                string _demonCompanionAspectOfNabasuDisplayNameKey = "CompanionAspectOfNabasuNameKey";
                string _demonCompanionAspectOfNabasuDescription =
                    "You adopt the aspect of Nabasu, gaining a bonus on all Lore (religion), Perception, " +
                    "and Use Magic Device skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfNabasuDescriptionKey = "CompanionAspectOfNabasuDescriptionKey";
                var _demonCompanionAspectOfNabasu = FeatureConfigurator.New(_demonCompanionAspectOfNabasuName, _demonCompanionAspectOfNabasuGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfNabasuDisplayNameKey, _demonCompanionAspectOfNabasuDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfNabasuDescriptionKey, _demonCompanionAspectOfNabasuDescription))
                    .AddContextStatBonus(StatType.SkillLoreReligion, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillUseMagicDevice, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfNabasuGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfSchirName = "CompanionAspectOfSchir";
                string _demonCompanionAspectOfSchirGUID = "56b48c98a2914688b7139c55417686e2";
                string _demonCompanionAspectOfSchirDisplayName = "Aspect of Schir";
                string _demonCompanionAspectOfSchirDisplayNameKey = "CompanionAspectOfSchirNameKey";
                string _demonCompanionAspectOfSchirDescription =
                    "You adopt the aspect of Schir, gaining a bonus on all Lore (nature), Mobility, " +
                    "and Athletics skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfSchirDescriptionKey = "CompanionAspectOfSchirDescriptionKey";
                var _demonCompanionAspectOfSchir = FeatureConfigurator.New(_demonCompanionAspectOfSchirName, _demonCompanionAspectOfSchirGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfSchirDisplayNameKey, _demonCompanionAspectOfSchirDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfSchirDescriptionKey, _demonCompanionAspectOfSchirDescription))
                    .AddContextStatBonus(StatType.SkillMobility, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreNature, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillAthletics, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfSchirGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfSuccubusName = "CompanionAspectOfSuccubus";
                string _demonCompanionAspectOfSuccubusGUID = "db8cc17589d54f80836771c47a364738";
                string _demonCompanionAspectOfSuccubusDisplayName = "Aspect of Succubus";
                string _demonCompanionAspectOfSuccubusDisplayNameKey = "CompanionAspectOfSuccubusNameKey";
                string _demonCompanionAspectOfSuccubusDescription =
                    "You adopt the aspect of Succubus, gaining a bonus on all Persuasion, Perception, and " +
                    "Knowledge (world) skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfSuccubusDescriptionKey = "CompanionAspectOfSuccubusDescriptionKey";
                var _demonCompanionAspectOfSuccubus = FeatureConfigurator.New(_demonCompanionAspectOfSuccubusName, _demonCompanionAspectOfSuccubusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfSuccubusDisplayNameKey, _demonCompanionAspectOfSuccubusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfSuccubusDescriptionKey, _demonCompanionAspectOfSuccubusDescription))
                    .AddContextStatBonus(StatType.SkillPersuasion, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillPerception, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillKnowledgeWorld, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfSuccubusGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfVrockName = "CompanionAspectOfVrock";
                string _demonCompanionAspectOfVrockGUID = "df86167a7957445cb26f1e6fbad955ee";
                string _demonCompanionAspectOfVrockDisplayName = "Aspect of Vrock";
                string _demonCompanionAspectOfVrockDisplayNameKey = "CompanionAspectOfVrockNameKey";
                string _demonCompanionAspectOfVrockDescription =
                    "You adopt the aspect of Vrock, gaining a bonus on all Knowledge (arcana), Knowledge (world), " +
                    "and Lore (religion) skill checks equal to half of your mythic rank plus one. Those " +
                    "bonuses do not stack with other bonuses from demonic aspects.";
                string _demonCompanionAspectOfVrockDescriptionKey = "CompanionAspectOfVrockDescriptionKey";
                var _demonCompanionAspectOfVrock = FeatureConfigurator.New(_demonCompanionAspectOfVrockName, _demonCompanionAspectOfVrockGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfVrockDisplayNameKey, _demonCompanionAspectOfVrockDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfVrockDescriptionKey, _demonCompanionAspectOfVrockDescription))
                    .AddContextStatBonus(StatType.SkillKnowledgeArcana, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillKnowledgeWorld, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextStatBonus(StatType.SkillLoreReligion, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfVrockGUID)
                    .SetIcon(BabauAspectFeature.Icon)
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonMinorAspectChoiceSelectionName = "CompanionMinorAspectChoiceSelection";
                string _demonMinorAspectChoiceSelectionGUID = "427ce8b5d8b24cfb82f21582a2ad68f6";
                string _demonMinorAspectChoiceSelectionDisplayName = "Minor Demonic Aspect";
                string _demonMinorAspectChoiceSelectionDisplayNameKey = "CompanionMinorAspectChoiceSelectionNameKey";
                string _demonMinorAspectChoiceSelectionDescription = "The Demon's companion may choose 1 Minor Demonic Aspect to gain the passive benefits from.";
                string _demonMinorAspectChoiceSelectionDescriptionKey = "CompanionMinorAspectChoiceSelectionDescriptionKey";
                var _demonMinorAspectChoiceSelectionSelection = FeatureSelectionConfigurator.New(_demonMinorAspectChoiceSelectionName, _demonMinorAspectChoiceSelectionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonMinorAspectChoiceSelectionDisplayNameKey, _demonMinorAspectChoiceSelectionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonMinorAspectChoiceSelectionDescriptionKey, _demonMinorAspectChoiceSelectionDescription))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideInUI(true)
                    .SetHideNotAvailibleInUI(true)
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] {
                        _demonCompanionAspectOfBabau.AssetGuidThreadSafe,
                        _demonCompanionAspectOfBrimorak.AssetGuidThreadSafe,
                        _demonCompanionAspectOfIncubus.AssetGuidThreadSafe,
                        _demonCompanionAspectOfKalavakus.AssetGuidThreadSafe,
                        _demonCompanionAspectOfNabasu.AssetGuidThreadSafe,
                        _demonCompanionAspectOfSchir.AssetGuidThreadSafe,
                        _demonCompanionAspectOfSuccubus.AssetGuidThreadSafe,
                        _demonCompanionAspectOfVrock.AssetGuidThreadSafe
                    })
                    .Configure();

                string _demonCompanionAspectOfBalorName = "CompanionAspectOfBalor";
                string _demonCompanionAspectOfBalorGUID = "f2a15d910b3a4b45b2e99f36a8b3cbd6";
                string _demonCompanionAspectOfBalorDisplayName = "Aspect of Balor";
                string _demonCompanionAspectOfBalorDisplayNameKey = "CompanionAspectOfBalorNameKey";
                string _demonCompanionAspectOfBalorDescription =
                    "You adopt the aspect of Balor, gaining a bonus to Constitution score equal to half of your mythic rank plus one.";
                string _demonCompanionAspectOfBalorDescriptionKey = "CompanionAspectOfBalorDescriptionKey";
                var _demonCompanionAspectOfBalor = FeatureConfigurator.New(_demonCompanionAspectOfBalorName, _demonCompanionAspectOfBalorGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfBalorDisplayNameKey, _demonCompanionAspectOfBalorDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfBalorDescriptionKey, _demonCompanionAspectOfBalorDescription))
                    .AddContextStatBonus(StatType.Constitution, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfBalorGUID)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonicMajorAspect.png"))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonMajorAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfColoxusName = "CompanionAspectOfColoxus";
                string _demonCompanionAspectOfColoxusGUID = "8b2d01c2250041779268b06abe365472";
                string _demonCompanionAspectOfColoxusDisplayName = "Aspect of Coloxus";
                string _demonCompanionAspectOfColoxusDisplayNameKey = "CompanionAspectOfColoxusNameKey";
                string _demonCompanionAspectOfColoxusDescription =
                    "You adopt the aspect of Coloxus, gaining a bonus to Intelligence score equal to half of your mythic rank plus one.";
                string _demonCompanionAspectOfColoxusDescriptionKey = "CompanionAspectOfColoxusDescriptionKey";
                var _demonCompanionAspectOfColoxus = FeatureConfigurator.New(_demonCompanionAspectOfColoxusName, _demonCompanionAspectOfColoxusGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfColoxusDisplayNameKey, _demonCompanionAspectOfColoxusDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfColoxusDescriptionKey, _demonCompanionAspectOfColoxusDescription))
                    .AddContextStatBonus(StatType.Intelligence, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfColoxusGUID)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonicMajorAspect.png"))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonMajorAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfOmoxName = "CompanionAspectOfOmox";
                string _demonCompanionAspectOfOmoxGUID = "59671d62b78246e29e6efccab4f4ab3c";
                string _demonCompanionAspectOfOmoxDisplayName = "Aspect of Omox";
                string _demonCompanionAspectOfOmoxDisplayNameKey = "CompanionAspectOfOmoxNameKey";
                string _demonCompanionAspectOfOmoxDescription =
                    "You adopt the aspect of Omox, gaining DR N/- where N is equal to half your mythic rank plus one.";
                string _demonCompanionAspectOfOmoxDescriptionKey = "CompanionAspectOfOmoxDescriptionKey";
                var _demonCompanionAspectOfOmox = FeatureConfigurator.New(_demonCompanionAspectOfOmoxName, _demonCompanionAspectOfOmoxGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfOmoxDisplayNameKey, _demonCompanionAspectOfOmoxDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfOmoxDescriptionKey, _demonCompanionAspectOfOmoxDescription))
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfOmoxGUID)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonicMajorAspect.png"))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonMajorAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();
                _demonCompanionAspectOfOmox.AddComponent(new AddDamageResistancePhysical
                {
                    name = "$AddDamageResistancePhysical$febec4d8899c412183c2efb542db736a",
                    Value = _demonCompanionContextValue,
                    BypassedByAlignment = false,
                    BypassedByEpic = false,
                    BypassedByForm = false,
                    BypassedByMagic = false,
                    BypassedByMaterial = false,
                    BypassedByMeleeWeapon = false,
                    BypassedByReality = false,
                    BypassedByWeaponType = false
                });

                string _demonCompanionAspectOfShadowDemonName = "CompanionAspectOfShadowDemon";
                string _demonCompanionAspectOfShadowDemonGUID = "534eb07f529b4cc7a2980687a3ef5cb3";
                string _demonCompanionAspectOfShadowDemonDisplayName = "Aspect of Shadow Demon";
                string _demonCompanionAspectOfShadowDemonDisplayNameKey = "CompanionAspectOfShadowDemonNameKey";
                string _demonCompanionAspectOfShadowDemonDescription =
                    "You adopt the aspect of Shadow Demon, gaining a bonus to Wisdom score equal to half of your mythic rank plus one.";
                string _demonCompanionAspectOfShadowDemonDescriptionKey = "CompanionAspectOfShadowDemonDescriptionKey";
                var _demonCompanionAspectOfShadowDemon = FeatureConfigurator.New(_demonCompanionAspectOfShadowDemonName, _demonCompanionAspectOfShadowDemonGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfShadowDemonDisplayNameKey, _demonCompanionAspectOfShadowDemonDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfShadowDemonDescriptionKey, _demonCompanionAspectOfShadowDemonDescription))
                    .AddContextStatBonus(StatType.Wisdom, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfShadowDemonGUID)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonicMajorAspect.png"))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonMajorAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfVavakiaName = "CompanionAspectOfVavakia";
                string _demonCompanionAspectOfVavakiaGUID = "e35f9516ae5245388fcc6dc138b21a63";
                string _demonCompanionAspectOfVavakiaDisplayName = "Aspect of Vavakia";
                string _demonCompanionAspectOfVavakiaDisplayNameKey = "CompanionAspectOfVavakiaNameKey";
                string _demonCompanionAspectOfVavakiaDescription =
                    "You adopt the aspect of Vavakia, gaining a bonus to Strength score equal to half of your mythic rank plus one.";
                string _demonCompanionAspectOfVavakiaDescriptionKey = "CompanionAspectOfVavakiaDescriptionKey";
                var _demonCompanionAspectOfVavakia = FeatureConfigurator.New(_demonCompanionAspectOfVavakiaName, _demonCompanionAspectOfVavakiaGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfVavakiaDisplayNameKey, _demonCompanionAspectOfVavakiaDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfVavakiaDescriptionKey, _demonCompanionAspectOfVavakiaDescription))
                    .AddContextStatBonus(StatType.Strength, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfVavakiaGUID)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonicMajorAspect.png"))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonMajorAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _demonCompanionAspectOfVrolikaiName = "CompanionAspectOfVrolikai";
                string _demonCompanionAspectOfVrolikaiGUID = "8a683fe2a37145789d521bee707817d5";
                string _demonCompanionAspectOfVrolikaiDisplayName = "Aspect of Vrolikai";
                string _demonCompanionAspectOfVrolikaiDisplayNameKey = "CompanionAspectOfVrolikaiNameKey";
                string _demonCompanionAspectOfVrolikaiDescription =
                    "You adopt the aspect of Vrolikai, gaining a bonus to Fast Healing score equal to half of your mythic rank plus one.";
                string _demonCompanionAspectOfVrolikaiDescriptionKey = "CompanionAspectOfVrolikaiDescriptionKey";
                var _demonCompanionAspectOfVrolikai = FeatureConfigurator.New(_demonCompanionAspectOfVrolikaiName, _demonCompanionAspectOfVrolikaiGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionAspectOfVrolikaiDisplayNameKey, _demonCompanionAspectOfVrolikaiDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionAspectOfVrolikaiDescriptionKey, _demonCompanionAspectOfVrolikaiDescription))
                    .AddContextStatBonus(StatType.Constitution, _demonCompanionContextValue, descriptor: ModifierDescriptor.DemonBonus)
                    .AddContextRankConfig(_demonCompanionContextRankConfig)
                    .AddPrerequisiteNoFeature(_demonCompanionAspectOfVrolikaiGUID)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonicMajorAspect.png"))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonMajorAspect })
                    .SetReapplyOnLevelUp(true)
                    .Configure();
                _demonCompanionAspectOfVrolikai.AddComponent(new AddEffectFastHealing
                {
                    name = "$AddEffectFastHealing$f2e72e75f76a45f6af7adb2ef08f44f3",
                    Bonus = _demonCompanionContextValue,
                    Heal = 0
                });

                string _demonMajorAspectChoiceSelectionName = "CompanionMajorAspectChoiceSelection";
                string _demonMajorAspectChoiceSelectionGUID = "4a665a43ef0e4bde9729f302dd72a3e6";
                string _demonMajorAspectChoiceSelectionDisplayName = "Major Demonic Aspect";
                string _demonMajorAspectChoiceSelectionDisplayNameKey = "CompanionMajorAspectChoiceSelectionNameKey";
                string _demonMajorAspectChoiceSelectionDescription = "The Demon's companion may choose 1 Major Aspect to gain the passive benefits from.";
                string _demonMajorAspectChoiceSelectionDescriptionKey = "CompanionMajorAspectChoiceSelectionDescriptionKey";
                var _demonMajorAspectChoiceSelectionSelection = FeatureSelectionConfigurator.New(_demonMajorAspectChoiceSelectionName, _demonMajorAspectChoiceSelectionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonMajorAspectChoiceSelectionDisplayNameKey, _demonMajorAspectChoiceSelectionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonMajorAspectChoiceSelectionDescriptionKey, _demonMajorAspectChoiceSelectionDescription))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect })
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideInUI(true)
                    .SetHideNotAvailibleInUI(true)
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] {
                        _demonCompanionAspectOfBalor.AssetGuidThreadSafe,
                        _demonCompanionAspectOfColoxus.AssetGuidThreadSafe,
                        _demonCompanionAspectOfOmox.AssetGuidThreadSafe,
                        _demonCompanionAspectOfShadowDemon.AssetGuidThreadSafe,
                        _demonCompanionAspectOfVavakia.AssetGuidThreadSafe,
                        _demonCompanionAspectOfVrolikai.AssetGuidThreadSafe
                    })
                    .Configure();

                string _demonCompanionRageName = "CompanionDemonRage";
                string _demonCompanionRageGUID = "0d77eb210ddb4d55b392cbbc5afe7540";
                string _demonCompanionRageDisplayName = "Demonic Rage";
                string _demonCompanionRageDisplayNameKey = "CompanionDemonRageNameKey";
                string _demonCompanionRageDescription =
                    "The power of the Abyss courses through the Demon's companion waiting to be unleashed. " +
                    "The Demon's companion can enter a demonic rage as a free action. The demonic rage lasts " +
                    "until the end of combat. While in demonic rage, the Demon's companion gains +2 bonus on " +
                    "attack rolls, damage rolls, caster level checks and Reflex saving throws. The DC for all " +
                    "saving throws against Demon companion's spells and abilities are increased by 2. These " +
                    "benefits increase by 1 at 6th and 9th mythic ranks.";
                string _demonCompanionRageDescriptionKey = "CompanionDemonRageDescriptionKey";
                var _demonCompanionRage = FeatureConfigurator.New(_demonCompanionRageName, _demonCompanionRageGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonCompanionRageDisplayNameKey, _demonCompanionRageDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonCompanionRageDescriptionKey, _demonCompanionRageDescription))
                    .SetIcon(DemonRageFeature.Icon)
                    .Configure();
                _demonCompanionRage.Components = DemonRageFeature.Components;

                string _demonAspectChoiceName = "CompanionAspectChoice";
                string _demonAspectChoiceGUID = "906077e087064ff3a44a15a96b62655c";
                string _demonAspectChoiceDisplayName = "Demonic Aspects";
                string _demonAspectChoiceDisplayNameKey = "CompanionAspectChoiceNameKey";
                string _demonAspectChoiceDescription = "The Demon's companion may choose to gain the passive benefits from 2 Minor Demonic Aspects and 1 Major Demonic Aspect.";
                string _demonAspectChoiceDescriptionKey = "CompanionAspectChoiceDescriptionKey";
                var _demonAspectChoice = FeatureConfigurator.New(_demonAspectChoiceName, _demonAspectChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_demonAspectChoiceDisplayNameKey, _demonAspectChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_demonAspectChoiceDescriptionKey, _demonAspectChoiceDescription))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.DemonicAspect, FeatureGroup.DemonMajorAspect, FeatureGroup.MythicAdditionalProgressions })
                    .SetIcon(BabauAspectFeature.Icon)
                    .Configure();
                _demonAspectChoice.AddSelectionCallback(_demonMinorAspectChoiceSelectionSelection, MythicCompanionProgression);
                _demonAspectChoice.AddSelectionCallback(_demonMinorAspectChoiceSelectionSelection, MythicCompanionProgression);
                _demonAspectChoice.AddSelectionCallback(_demonMajorAspectChoiceSelectionSelection, MythicCompanionProgression);

                var _demonCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[]
                    {
                        _demonCompanionRage.AssetGuidThreadSafe,
                        DemonChargeFeature.AssetGuidThreadSafe,
                        _demonAspectChoice.AssetGuidThreadSafe
                    })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_DemonCompanionChoice.png"))
                    .AddPrerequisitePlayerHasFeature(DemonProgression)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .SetHideInUI(true)
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
