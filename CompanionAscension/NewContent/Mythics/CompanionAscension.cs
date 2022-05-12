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
using CompanionAscension.NewContent.Features;

namespace CompanionAscension.NewContent.Mythics
{
    class CompanionAscension
    {
        private static readonly string CompanionAscensionChoice4Name = "AscensionChoice4";
        private static readonly string CompanionAscensionChoice4GUID = "b37eb4fa415a42c7bf9dd1f8ee4e8be6";
        private static readonly string CompanionAscensionChoice4DisplayName = "Companion Ascension";
        private static readonly string CompanionAscensionChoice4DisplayNameKey = "AscensionChoice4Name";
        private static readonly string CompanionAscensionChoice4Description = "Select one extra feat, mythic ability, or mythic feat.";
        private static readonly string CompanionAscensionChoice4DescriptionKey = "AscensionChoice4Description";

        private static readonly string CompanionAscensionChoice8Name = "AscensionChoice8";
        private static readonly string CompanionAscensionChoice8GUID = "1cfed023c8e34abfb14bb686d6a62a49";
        private static readonly string CompanionAscensionChoice8DisplayName = "Companion Ascension";
        private static readonly string CompanionAscensionChoice8DisplayNameKey = "AscensionChoice8Name";
        private static readonly string CompanionAscensionChoice8Description = "Select one extra feat, mythic ability, or mythic feat.";
        private static readonly string CompanionAscensionChoice8DescriptionKey = "AscensionChoice8Description";

        private static readonly string CompanionFirstAscensionName = "CompanionFirstAscension";
        private static readonly string CompanionFirstAscensionGUID = "f1f2559d70fe46f98b001e58697e324e";
        private static readonly string CompanionFirstAscensionDisplayName = "Ascension";
        private static readonly string CompanionFirstAscensionDisplayNameKey = "CompanionFirstAscensionName";
        private static readonly string CompanionFirstAscensionDescription = "As the Commander's power grows, so too does the power of {mf|his|her} companions.";
        private static readonly string CompanionFirstAscensionDescriptionKey = "CompanionFirstAscensionDescription";
        //private static readonly string FirstAscensionSelectionGUID = "1421e0034a3afac458de08648d06faf0";
        private static readonly BlueprintFeatureSelection FirstAscensionSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1421e0034a3afac458de08648d06faf0");

        private static readonly string CompanionSecondAscensionName = "CompanionSecondAscension";
        private static readonly string CompanionSecondAscensionGUID = "b6982c2cd5804966bc69c3f2d69493b4";
        private static readonly string CompanionSecondAscensionDisplayName = "Ascension";
        private static readonly string CompanionSecondAscensionDisplayNameKey = "CompanionSecondAscensionName";
        private static readonly string CompanionSecondAscensionDescription = "As the Commander's power grows, so too does the power of {mf|his|her} companions.";
        private static readonly string CompanionSecondAscensionDescriptionKey = "CompanionSecondAscensionDescription";

        //private static readonly string RemoveDiseaseGUID = "4093d5a0eb5cae94e909eb1e0e1a6b36";
        private static readonly BlueprintAbility RemoveDisease = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("4093d5a0eb5cae94e909eb1e0e1a6b36");
        private static readonly BlueprintAbility Guidance = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("c3a8f31778c3980498d8f00c980be5f5");

        private static readonly string CompanionAscensionMythicAbilityName = "CompanionAscensionMythicAbility";
        private static readonly string CompanionAscensionMythicAbilityGUID = "2a00502c5f0b45c6aaa810e9107953d7";
        private static readonly string CompanionAscensionMythicAbilityDisplayName = "Mythic Ability";
        private static readonly string CompanionAscensionMythicAbilityDisplayNameKey = "CompanionAscensionMythicAbilityName";
        private static readonly string CompanionAscensionMythicAbilityDescription = "Select one new mythic ability.";
        private static readonly string CompanionAscensionMythicAbilityDescriptionKey = "CompanionAscensionMythicAbilityDescription";

        private static readonly string CompanionAscensionMythicFeatName = "CompanionAscensionMythicFeat";
        private static readonly string CompanionAscensionMythicFeatGUID = "c29ef48b8dd74498824c35c3893dbb98";
        private static readonly string CompanionAscensionMythicFeatDisplayName = "Mythic Feat";
        private static readonly string CompanionAscensionMythicFeatDisplayNameKey = "CompanionAscensionMythicFeatName";
        private static readonly string CompanionAscensionMythicFeatDescription = "Select one new mythic feat.";
        private static readonly string CompanionAscensionMythicFeatDescriptionKey = "CompanionAscensionMythicFeatDescription";

        private static readonly string MythicSavingThrowBonusGUID = "b49f559f4daa4a87b080eccf1a0dc9a9";
        private static readonly string MythicAbilityScoreIncreaseName = "MythicAbilityScoreIncrease";
        private static readonly string MythicAbilityScoreBonusGUID = "3adf757c5ba741438e9727550ab126d7";
        private static readonly string MythicAbilityScoreIncreaseDisplayName = "Mythic Ability Score Increase";
        private static readonly string MythicAbilityScoreIncreaseDisplayNameKey = "MythicAbilityScoreIncreaseName";
        private static readonly string MythicAbilityScoreIncreaseDescription = "Increases your highest ability score by an amount equal to 1 plus half your mythic level.";
        private static readonly string MythicAbilityScoreIncreaseDescriptionKey = "MythicAbilityScoreIncreaseDescription";

        //private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string AeonCompanionChoiceName = "AeonCompanionChoice";
        private static readonly string AeonCompanionChoiceGUID = "c1dd81e75695467cb3bac2381d3cec91";
        private static readonly string AeonCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string AeonCompanionChoiceDisplayNameKey = "AeonCompanionChoiceName";
        private static readonly string AeonCompanionChoiceDescription = "";
        private static readonly string AeonCompanionChoiceDescriptionKey = "AeonCompanionChoiceDescription";

        //private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string AngelCompanionChoiceName = "AngelCompanionChoice";
        private static readonly string AngelCompanionChoiceGUID = "29ca6c2414f84577a8ad8c9c7e0742fd";
        private static readonly string AngelCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string AngelCompanionChoiceDisplayNameKey = "AngelCompanionChoiceName";
        private static readonly string AngelCompanionChoiceDescription = "";
        private static readonly string AngelCompanionChoiceDescriptionKey = "AngelCompanionChoiceDescription";

        //private static readonly string AzataSuperpowersGUID = "8a30e92cd04ff5b459ba7cb03584fda0";
        private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string AzataCompanionChoiceName = "AzataCompanionChoice";
        private static readonly string AzataCompanionChoiceGUID = "f7191b869724482b8f1d14b9b195c764";
        private static readonly string AzataCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string AzataCompanionChoiceDisplayNameKey = "AzataCompanionChoiceName";
        private static readonly string AzataCompanionChoiceDescription = "Select one Azata Superpower.";
        private static readonly string AzataCompanionChoiceDescriptionKey = "AzataCompanionChoiceDescription";

        //private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string DemonCompanionChoiceName = "DemonCompanionChoice";
        private static readonly string DemonCompanionChoiceGUID = "57e6f40e11ec421c9b2e3edb34e2beb2";
        private static readonly string DemonCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string DemonCompanionChoiceDisplayNameKey = "DemonCompanionChoiceName";
        private static readonly string DemonCompanionChoiceDescription = "";
        private static readonly string DemonCompanionChoiceDescriptionKey = "DemonCompanionChoiceDescription";

        private static readonly BlueprintFeatureSelection LichUniqueAbilitiesSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1f646b820a37d3d4a8ab116a24ee0022");
        private static readonly string LichCompanionChoiceName = "LichCompanionChoice";
        private static readonly string LichCompanionChoiceGUID = "4387b5bc3f424b2fa9575d4620d9489c";
        private static readonly string LichCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string LichCompanionChoiceDisplayNameKey = "LichCompanionChoiceName";
        private static readonly string LichCompanionChoiceDescription = "";
        private static readonly string LichCompanionChoiceDescriptionKey = "LichCompanionChoiceDescription";

        private static readonly BlueprintFeatureSelection TricksterRank1Selection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("4fbc563529717de4d92052048143e0f1");
        private static readonly string TricksterCompanionChoiceName = "TricksterCompanionChoice";
        private static readonly string TricksterCompanionChoiceGUID = "095edab4d08f4b7dab6eb7450e93cfca";
        private static readonly string TricksterCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string TricksterCompanionChoiceDisplayNameKey = "TricksterCompanionChoiceName";
        private static readonly string TricksterCompanionChoiceDescription = "";
        private static readonly string TricksterCompanionChoiceDescriptionKey = "TricksterCompanionChoiceDescription";

        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");

        private static readonly string AeonProgression = "34b9484b0d5ce9340ae51d2bf9518bbe";
        private static readonly string AngelProgression = "2f6fe889e91b6a645b055696c01e2f74";
        private static readonly string AzataProgression = "9db53de4bf21b564ca1a90ff5bd16586";
        private static readonly string DemonProgression = "285fe49f7df8587468f676aa49362213";
        private static readonly string LichProgression = "ccec4e01b85bf5d46a3c3717471ba639";
        private static readonly string TricksterProgression = "cc64789b0cc5df14b90da1ffee7bbeea";
        private static readonly string DevilProgression = "87bc9abf00b240a44bb344fea50ec9bc";
        private static readonly string GoldDragonProgression = "a6fbca43902c6194c947546e89af64bd";
        private static readonly string LegendProgression = "905383229aaf79e4b8d7e2d316b68715";
        private static readonly string SwarmThatWalksProgression = "bf5f103ccdf69254abbad84fd371d5c9";

        

        //private static readonly string ExtraMythicAbilityMythicFeatSelectionGUID = "8a6a511c55e67d04db328cc49aaad2b8";
        //private static readonly string ExtraMythicFeatSelectionGUID = "e10c4f18a6c8b4342afe6954bde0587b";
        //private static readonly string ExtraMythicFeatMythicAbilitySelectionGUID = "c916448f690d4f4e9d824d6f376e621d";
        private static readonly string BasicFeatSelectionGUID = "247a4068296e8be42890143f451b4b45";
        //private static readonly string MythicAbilitySelectionGUID = "ba0e5a900b775be4a99702f1ed08914d";
        //private static readonly string MythicFeatSelectionGUID = "9ee0f6745f555484299b0a1563b99d81";

        //private static readonly string MythicFeatSelectionGUID = "9ee0f6745f555484299b0a1563b99d81";
        //private static readonly BlueprintFeature MythicFeatSelectionSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(MythicFeatSelectionGUID);

        private static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
        private static readonly BlueprintFeature ExtraMythicAbilityMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("8a6a511c55e67d04db328cc49aaad2b8");
        private static readonly BlueprintFeature ExtraMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("e10c4f18a6c8b4342afe6954bde0587b");
        private static readonly BlueprintFeature ExtraMythicFeatMythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("c916448f690d4f4e9d824d6f376e621d");
        //private static readonly BlueprintFeature BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(BasicFeatSelectionGUID);
        private static readonly BlueprintFeatureSelection MythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
        private static readonly BlueprintFeatureSelection MythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("9ee0f6745f555484299b0a1563b99d81");

        private static readonly string[] AscensionGUIDS = new string[] {
                    "46df0532714a9454eb5fbad64ce6c14f",     //Aeon
                    "b236003be2b9400498b9dd0f07b0c93c",     //Angel
                    "132afc1a28bd9d442821385f7cbf1c05",     //Azata
                    "8afd697daf0d47a4883759a6bc1aff88",     //Demon
                    "a796af14198bf5e45b63f056c32107a2",     //Lich
                    "11ba180ac736c894e937602e54f7320c"};    //Trickster

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]

        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.Last)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchCompanionAscension();
                //try { PatchCompanionAscension(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchCompanionAscension()
            {
                Tools.LogMessage("New Content: Patching Companion Ascension");

                var _companionFirstAscension = FeatureSelectionConfigurator.New(CompanionFirstAscensionName, CompanionFirstAscensionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionFirstAscensionDisplayNameKey, CompanionFirstAscensionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionFirstAscensionDescriptionKey, CompanionFirstAscensionDescription))
                    .SetIcon(Guidance.Icon)
                    .Configure();
                _companionFirstAscension.m_AllFeatures = FirstAscensionSelection.m_AllFeatures;
                Tools.LogMessage("Built: Companion First Ascension -> " + _companionFirstAscension.AssetGuidThreadSafe);

                var _companionAscensionMythicAbility = FeatureSelectionConfigurator.New(CompanionAscensionMythicAbilityName, CompanionAscensionMythicAbilityGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionMythicAbilityDisplayNameKey, CompanionAscensionMythicAbilityDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionMythicAbilityDescriptionKey, CompanionAscensionMythicAbilityDescription))
                    .SetIcon(RemoveDisease.Icon)
                    .Configure();
                _companionAscensionMythicAbility.m_AllFeatures = MythicAbilitySelection.m_AllFeatures.Where(c => (
                    c.deserializedGuid != ExtraMythicFeatMythicAbilitySelection.ToReference<BlueprintFeatureReference>().deserializedGuid
                )).ToArray();
                Tools.LogMessage("Built: Companion Ascension Mythic Ability -> " + _companionAscensionMythicAbility.AssetGuidThreadSafe);

                var _companionAscensionMythicFeat = FeatureSelectionConfigurator.New(CompanionAscensionMythicFeatName, CompanionAscensionMythicFeatGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionMythicFeatDisplayNameKey, CompanionAscensionMythicFeatDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionMythicFeatDescriptionKey, CompanionAscensionMythicFeatDescription))
                    .SetIcon(ExtraMythicFeatSelection.Icon)
                    .Configure();
                _companionAscensionMythicFeat.m_AllFeatures = MythicFeatSelection.m_AllFeatures.Where(c => (
                    c.deserializedGuid != ExtraMythicAbilityMythicFeatSelection.ToReference<BlueprintFeatureReference>().deserializedGuid &&
                    c.deserializedGuid != ExtraMythicFeatSelection.ToReference<BlueprintFeatureReference>().deserializedGuid
                )).ToArray();
                Tools.LogMessage("Built: Companion Ascension Mythic Feat -> " + _companionAscensionMythicFeat.AssetGuidThreadSafe);

                // TESTING
                //string lichbook = "3f16e9caf7c683c40884c7c455ed26af";
                //BlueprintFeatureSelectMythicSpellbook lichbookselect = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelectMythicSpellbook>(lichbook);
                //FeatureSelectMythicSpellbookConfigurator.New("selectLichBook", "42AC672E-65A6-4845-BF1A-06F06077D6D6")
                //    .SetDisplayName(LocalizationTool.CreateString("lichbookkey", "LichBookStuff", false))
                //    .SetDescription(LocalizationTool.CreateString("descKey", "Some descirption here"))
                //    .PrerequisiteCasterType(isArcane: true)
                //    .SetRanks(1)
                //    .Configure();
                ////lichSelectBook.m_AllowedSpellbooks = lichbookselect.m_AllowedSpellbooks;
                ////lichSelectBook.m_MythicSpellList = lichbookselect.m_MythicSpellList;

                //var addLichBook = FeatureConfigurator.New("AddLichBook", "CEBC59F9-B6D2-40A8-A076-92EB57DDF30C")
                //    .SetDisplayName(LocalizationTool.CreateString("AddLichBookKey", "Add Lich Book", false))
                //    .SetDescription(LocalizationTool.CreateString("AddLichBookDKey", "Some descirption here"))
                //    .AddFeatureIfHasFact(checkedFact: "CEBC59F9-B6D2-40A8-A076-92EB57DDF30C", feature: "3f16e9caf7c683c40884c7c455ed26af")
                //    .Configure();

                //var lichSelectBook = FeatureConfigurator.New("LichBookFeature", "BC484E2D-F50B-47C7-A084-68EE0CC5A172")
                //    .SetDisplayName(LocalizationTool.CreateString("lichbookfeatkey", "LichBookStuff", false))
                //    .SetDescription(LocalizationTool.CreateString("lichbookfeatdescKey", "Some descirption here"))
                //    .Configure();
                //lichSelectBook.AddComponents()

                //var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                //BlueprintProgression.ClassWithLevel _classWithLevel = new();
                //_classWithLevel.m_Class = _mythicCompanionClassReference;
                //_classWithLevel.AdditionalLevel = 0;
                //var lichCompProgr = ProgressionConfigurator.New("LichBookFeature", "BC484E2D-F50B-47C7-A084-68EE0CC5A172")
                //    .SetDisplayName(LocalizationTool.CreateString("lichbookfeatkey", "LichBookStuff", false))
                //    .SetDescription(LocalizationTool.CreateString("lichbookfeatdescKey", "Some descirption here"))
                //    .Configure();
                //lichCompProgr.m_Classes = lichCompProgr.m_Classes.AppendToArray(_classWithLevel);
                //lichCompProgr.GiveFeaturesForPreviousLevels = true;
                //lichCompProgr.LevelEntries.TemporaryContext(le =>
                // {
                //     le.Where(e => e.Level == 4)
                //         .ForEach(e =>
                //         {
                //             e.m_Features.Add(lichbookselect.ToReference<BlueprintFeatureBaseReference>());
                //         });
                // });
                //AddNocticulaBonus anb = new();
                //anb.HighestStatBonus.ValueType = Kingmaker.UnitLogic.Mechanics.ContextValueType.Rank;
                //anb.HighestStatBonus.ValueRank = AbilityRankType.StatBonus;
                //anb.HighestStatBonus.Property = Kingmaker.UnitLogic.Mechanics.Properties.UnitProperty.None;
                //anb.HighestStatBonus.ValueShared = Kingmaker.UnitLogic.Abilities.AbilitySharedValue.StatBonus;
                //anb.Descriptor = ModifierDescriptor.Mythic;
                //BlueprintGuid bguid = BlueprintGuid.NewGuid();
                

                //CompanionSpellbookMerge csm = new();
                //csm.AssetGuid = BlueprintGuid.NewGuid();
                //csm.m_SpellKnownForSpontaneous = lichbookselect.m_SpellKnownForSpontaneous;
                //csm.m_AllowedSpellbooks = lichbookselect.m_AllowedSpellbooks;
                //csm.m_MythicSpellList = lichbookselect.m_MythicSpellList;
                //Tools.LogMessage("Built CSM: " + csm.AssetGuidThreadSafe);

                //ContextValue _mythicAbilityScoreBonusContextValue = new();
                //_mythicAbilityScoreBonusContextValue.ValueType = ContextValueType.Rank;
                //_mythicAbilityScoreBonusContextValue.Value = 1;
                //_mythicAbilityScoreBonusContextValue.ValueShared = AbilitySharedValue.StatBonus;
                //_mythicAbilityScoreBonusContextValue.Property = UnitProperty.None;
                //_mythicAbilityScoreBonusContextValue.ValueRank = AbilityRankType.Default;
                //Tools.LogMessage("Built: Context Value (Mythic Ability Score Bonus)");

                //HighestAbilityScoreBonus _mythicAbilityScoreBonusHighestAbilityScoreBonus = new();
                //_mythicAbilityScoreBonusHighestAbilityScoreBonus.name = "$AddMaxAbilityScoreBonus$35678b97eaba4aae94f4d965b2492ac7";
                //_mythicAbilityScoreBonusHighestAbilityScoreBonus.HighestStatBonus = _mythicAbilityScoreBonusContextValue;
                //_mythicAbilityScoreBonusHighestAbilityScoreBonus.Descriptor = ModifierDescriptor.Mythic;
                //Tools.LogMessage("Built: Add Highest Ability Score Bonus (Mythic Ability Score Bonus)");

                //ContextRankConfig _mythicAbilityScoreBonusContextRankConfig = new();
                //_mythicAbilityScoreBonusContextRankConfig.name = "$ContextRankConfig$31b5cbc3daf2488387600fdc14a3365f";
                //_mythicAbilityScoreBonusContextRankConfig.m_BaseValueType = ContextRankBaseValueType.MythicLevel;
                //_mythicAbilityScoreBonusContextRankConfig.m_Type = AbilityRankType.Default;
                //_mythicAbilityScoreBonusContextRankConfig.m_Progression = ContextRankProgression.OnePlusDivStep;
                //_mythicAbilityScoreBonusContextRankConfig.m_StepLevel = 2;
                //_mythicAbilityScoreBonusContextRankConfig.m_Max = 10;
                //_mythicAbilityScoreBonusContextRankConfig.m_Stat = StatType.Unknown;
                //Tools.LogMessage("Built: Context Rank Config (Mythic Ability Score Bonus)");

                //var _mythicAbilityScoreBonus = FeatureConfigurator.New(MythicAbilityScoreIncreaseName, MythicAbilityScoreIncreaseGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(MythicAbilityScoreIncreaseDisplayNameKey, MythicAbilityScoreIncreaseDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(MythicAbilityScoreIncreaseDescriptionKey, MythicAbilityScoreIncreaseDescription))
                //    .AddRecalculateOnStatChange(stat: StatType.Strength)
                //    .AddRecalculateOnStatChange(stat: StatType.Dexterity)
                //    .AddRecalculateOnStatChange(stat: StatType.Constitution)
                //    .AddRecalculateOnStatChange(stat: StatType.Wisdom)
                //    .AddRecalculateOnStatChange(stat: StatType.Intelligence)
                //    .AddRecalculateOnStatChange(stat: StatType.Charisma)
                //    .SetReapplyOnLevelUp(true)
                //    .Configure();
                //_mythicAbilityScoreBonus.AddComponents(new BlueprintComponent[] {
                //    _mythicAbilityScoreBonusHighestAbilityScoreBonus,
                //    _mythicAbilityScoreBonusContextRankConfig });
                //Tools.LogMessage("Built: Mythic Ability Score Bonus -> " + _mythicAbilityScoreBonus.AssetGuidThreadSafe);

                var _companionAscensionChoice4 = FeatureSelectionConfigurator.New(CompanionAscensionChoice4Name, CompanionAscensionChoice4GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice4DisplayNameKey, CompanionAscensionChoice4DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice4DescriptionKey, CompanionAscensionChoice4Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility, FeatureGroup.MythicFeat, FeatureGroup.Feat, FeatureGroup.MythicAdditionalProgressions)
                    .SetIcon(MythicAbilitySelection.Icon)
                    .AddToFeatures(CompanionAscensionMythicFeatGUID)
                    .AddToFeatures(CompanionAscensionMythicAbilityGUID)
                    .AddToFeatures(BasicFeatSelectionGUID)
                    .AddToFeatures(MythicSavingThrowBonusGUID)
                    .AddToFeatures(MythicAbilityScoreBonusGUID)
                    .Configure();
                Tools.LogMessage("Built: Companion First Ascension bonus choice -> " + _companionAscensionChoice4.AssetGuidThreadSafe);

                //var _aeonCompanionChoice = FeatureSelectionConfigurator.New(AeonCompanionChoiceName, AeonCompanionChoiceGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(AeonCompanionChoiceDisplayNameKey, AeonCompanionChoiceDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(AeonCompanionChoiceDescriptionKey, AeonCompanionChoiceDescription))
                //    .PrerequisitePlayerHasFeature(AeonProgression)
                //    //.SetHideInUi(true)
                //    .Configure();
                ////AzataCompanionChoice.m_AllFeatures = AzataSuperpowersSelection.m_AllFeatures;
                //Tools.LogMessage("Built: Aeon Companion Choices -> " + _aeonCompanionChoice.AssetGuidThreadSafe);

                //var _angelCompanionChoice = FeatureSelectionConfigurator.New(AngelCompanionChoiceName, AngelCompanionChoiceGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(AngelCompanionChoiceDisplayNameKey, AngelCompanionChoiceDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(AngelCompanionChoiceDescriptionKey, AngelCompanionChoiceDescription))
                //    .PrerequisitePlayerHasFeature(AngelProgression)
                //    //.SetHideInUi(true)
                //    .Configure();
                ////AzataCompanionChoice.m_AllFeatures = AzataSuperpowersSelection.m_AllFeatures;
                //Tools.LogMessage("Built: Angel Companion Choices -> " + _angelCompanionChoice.AssetGuidThreadSafe);

                //var _azataCompanionChoice = FeatureSelectionConfigurator.New(AzataCompanionChoiceName, AzataCompanionChoiceGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(AzataCompanionChoiceDisplayNameKey, AzataCompanionChoiceDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(AzataCompanionChoiceDescriptionKey, AzataCompanionChoiceDescription))
                //    //.PrerequisitePlayerHasFeature(AzataProgression)
                //    .SetIcon(AzataSuperpowersSelection.Icon)
                //    //.SetHideInUi(true)
                //    .Configure();
                //_azataCompanionChoice.m_AllFeatures = AzataSuperpowersSelection.m_AllFeatures;
                //Tools.LogMessage("Built: Azata Companion Choices -> " + _azataCompanionChoice.AssetGuidThreadSafe);

                //var _demonCompanionChoice = FeatureSelectionConfigurator.New(DemonCompanionChoiceName, DemonCompanionChoiceGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(DemonCompanionChoiceDisplayNameKey, DemonCompanionChoiceDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(DemonCompanionChoiceDescriptionKey, DemonCompanionChoiceDescription))
                //    .PrerequisitePlayerHasFeature(DemonProgression)
                //    //.SetHideInUi(true)
                //    .Configure();
                ////AzataCompanionChoice.m_AllFeatures = AzataSuperpowersSelection.m_AllFeatures;
                //Tools.LogMessage("Built: Demon Companion Choices -> " + _demonCompanionChoice.AssetGuidThreadSafe);

                //var _lichCompanionChoice = FeatureSelectionConfigurator.New(LichCompanionChoiceName, LichCompanionChoiceGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(LichCompanionChoiceDisplayNameKey, LichCompanionChoiceDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(LichCompanionChoiceDescriptionKey, LichCompanionChoiceDescription))
                //    //.PrerequisitePlayerHasFeature(LichProgression)
                //    //.SetHideInUi(true)
                //    .Configure();
                //_lichCompanionChoice.m_AllFeatures = LichUniqueAbilitiesSelection.m_AllFeatures;
                //Tools.LogMessage("Built: Lich Companion Choices -> " + _lichCompanionChoice.AssetGuidThreadSafe);

                //var _tricksterCompanionChoice = FeatureSelectionConfigurator.New(TricksterCompanionChoiceName, TricksterCompanionChoiceGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(TricksterCompanionChoiceDisplayNameKey, TricksterCompanionChoiceDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(TricksterCompanionChoiceDescriptionKey, TricksterCompanionChoiceDescription))
                //    //.PrerequisitePlayerHasFeature(TricksterProgression)
                //    //.SetHideInUi(true)
                //    .Configure();
                //_tricksterCompanionChoice.m_AllFeatures = TricksterRank1Selection.m_AllFeatures;
                //Tools.LogMessage("Built: Trickster Companion Choices -> " + _tricksterCompanionChoice.AssetGuidThreadSafe);

                // Build choices for each mythic path
                // Some generic choices for all paths
                //      Legend: boost max level somehow?
                //
                //      Lich: DeathOfElementsConsumingElementsResource
                //      add companion class
                //      book merge for arcanes?
                //      
                //      Gold Dragon: Choice of +4 to one ability? Choice of boosting saves?
                //      
                //      Angel: book merge for divines?
                var _companionSecondAscension = FeatureSelectionConfigurator.New(CompanionSecondAscensionName, CompanionSecondAscensionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionSecondAscensionDisplayNameKey, CompanionSecondAscensionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionSecondAscensionDescriptionKey, CompanionSecondAscensionDescription))
                    .SetIcon(Guidance.Icon)
                    .AddToFeatures(AeonCompanionChoiceGUID)
                    .AddToFeatures(AngelCompanionChoiceGUID)
                    .AddToFeatures(AzataCompanionChoiceGUID)
                    .AddToFeatures(DemonCompanionChoiceGUID)
                    .AddToFeatures(LichCompanionChoiceGUID)
                    .AddToFeatures(TricksterCompanionChoiceGUID)
                    .Configure();
                Tools.LogMessage("Built: Companion Second Ascension -> " + _companionSecondAscension.AssetGuidThreadSafe);

                var _companionAscensionChoice8 = FeatureSelectionConfigurator.New(CompanionAscensionChoice8Name, CompanionAscensionChoice8GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice8DisplayNameKey, CompanionAscensionChoice8DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice8DescriptionKey, CompanionAscensionChoice8Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility, FeatureGroup.MythicFeat, FeatureGroup.Feat, FeatureGroup.MythicAdditionalProgressions)
                    .SetIcon(MythicAbilitySelection.Icon)
                    .AddToFeatures(CompanionAscensionMythicFeatGUID)
                    .AddToFeatures(CompanionAscensionMythicAbilityGUID)
                    .AddToFeatures(BasicFeatSelectionGUID)
                    .Configure();
                Tools.LogMessage("Built: Companion Second Ascension bonus choice -> " + _companionAscensionChoice8.AssetGuidThreadSafe);

                var _aeonCompanionChoice = ResourcesLibrary.TryGetBlueprint < BlueprintFeatureSelection>("c1dd81e75695467cb3bac2381d3cec91");
                //76a5af87f6594d5e90568b706f0809ed
                var _aeonCompanionNinthLevelImmunities = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("76a5af87f6594d5e90568b706f0809ed");

                if (Main.Settings.useCompanionAscension == false) { return; }
                AddCompanionsToFirstAscensions();
                CorrectPrerequisites();
                MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                {
                    le.Where(e => e.Level == 4)
                        .ForEach(e =>
                        {
                            e.m_Features.Remove(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(_companionFirstAscension.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(_companionAscensionChoice4.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(MythicIgnoreAlignmentRestrictions.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(_aeonCompanionChoice.ToReference<BlueprintFeatureBaseReference>());

                        });
                    le.Where(e => e.Level == 5)
                        .ForEach(e =>
                        {
                            e.m_Features.Add(_aeonCompanionNinthLevelImmunities.ToReference<BlueprintFeatureBaseReference>());
                        });
                    le.Where(e => e.Level == 8)
                        .ForEach(e =>
                        {
                            e.m_Features.Remove(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                            //e.m_Features.Add(CompanionSecondAscension.ToReference<BlueprintFeatureBaseReference>());
                            //e.m_Features.Add(CompanionAscensionChoice8.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                        });
                });
                MythicCompanionProgression.UIGroups = MythicCompanionProgression.UIGroups.AppendToArray(
                    Helpers.CreateUIGroup(
                    MythicAbilitySelection,
                    _companionAscensionChoice4)
                );
                Tools.LogMessage("New Content: Companion Ascension patching completed");
            }

            static void AddCompanionsToFirstAscensions()
            {
                var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                BlueprintProgression.ClassWithLevel _classWithLevel = new();
                _classWithLevel.m_Class = _mythicCompanionClassReference;
                _classWithLevel.AdditionalLevel = 0;

                foreach (string s in AscensionGUIDS)
                {
                    var _feature = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>(s);
                    _feature.m_Classes = _feature.m_Classes.AppendToArray(_classWithLevel);
                    _feature.GiveFeaturesForPreviousLevels = true;
                }
                Tools.LogMessage("Patched: Prerequisites for First Ascension Progressions");

                var _ttricksterFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("918e6e7085d81094790e806a49922694");
                BlueprintComponent[] contextRankConfig = _ttricksterFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                var _aeonFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("4db463bcf37d6014eaa23d3219703a9b");
                _aeonFirstAscentionResource.m_MaxAmount.m_Class = _aeonFirstAscentionResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);

                var _angelFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("6da18ecb21a24814eb79ab075a0b6d5e");
                _angelFirstAscentionResource.m_MaxAmount.m_ClassDiv = _angelFirstAscentionResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
                var _angelFirstAscentionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("f86857af1c584e248b9284654f31d39c");
                contextRankConfig = _angelFirstAscentionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                var _azataFirstAscensionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("8419c285e922c1044893472bcbd3d3bf");
                _azataFirstAscensionResource.m_MaxAmount.m_ClassDiv = _azataFirstAscensionResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
                var _azataFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("5d5f0c9b274bab44eb01272c8fcf251d");
                contextRankConfig = _azataFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                var _lichChannelNegativeResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("e5ef1aae31818f041bccbc9fd37662bf");
                _lichChannelNegativeResource.m_MaxAmount.m_Class = _lichChannelNegativeResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);

                Tools.LogMessage("Patched: Added companion class to first ascension abilities and resources");
            }

            static void CorrectPrerequisites()
            {
                var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                BlueprintProgression.ClassWithLevel _classWithLevel = new();
                _classWithLevel.m_Class = _mythicCompanionClassReference;
                _classWithLevel.AdditionalLevel = 0;
                //DeathOfElementsConsumingElementsResource
                //7a558d186755620439e35817f174f749
                //MaxAmount :: Class
                var DeathOfElementsConsumingElementsResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("7a558d186755620439e35817f174f749");
                DeathOfElementsConsumingElementsResource.m_MaxAmount.m_Class = DeathOfElementsConsumingElementsResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);

            }

            static BlueprintFeatureSelection BuildMythicSelection(string shortName, string featureGUID, string displayName, string description)
            {
                Guid nameGUID = Guid.NewGuid();
                Guid descGUID = Guid.NewGuid();

                BlueprintFeatureSelection featureSelection = FeatureSelectionConfigurator.New(shortName, featureGUID)
                    .SetDisplayName(LocalizationTool.CreateString(nameGUID.ToString(), displayName, false))
                    .SetDescription(LocalizationTool.CreateString(descGUID.ToString(), description))
                    .Configure();

                return featureSelection;
            }
        }
    }
}


/*/
 * Azata: Choice of Song
 * Trickster: T1 Trick ---
 * Demon: Minor Aspect Passive bonus
 * Lich: Lich Power ---
 * Angel: Sword of Heaven unupgraded
 * Gold Dragon: Damage Type Conversion
 * Devil: Choice of Decree?
 * Aeon: Aeon Bane (base only non upgraded)
 * Legend: I'd ignore entierly
/*/