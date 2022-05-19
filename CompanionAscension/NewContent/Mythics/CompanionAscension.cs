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
using static CompanionAscension.NewContent.Components.CustomMechanicsFeatures;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.RuleSystem.Rules;

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

        public static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");
        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
        private static readonly BlueprintFeature ExtraMythicAbilityMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("8a6a511c55e67d04db328cc49aaad2b8");
        private static readonly BlueprintFeature ExtraMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("e10c4f18a6c8b4342afe6954bde0587b");
        private static readonly BlueprintFeature ExtraMythicFeatMythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("c916448f690d4f4e9d824d6f376e621d");
        //private static readonly BlueprintFeature BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(BasicFeatSelectionGUID);
        private static readonly BlueprintFeatureSelection MythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
        private static readonly BlueprintFeatureSelection MythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("9ee0f6745f555484299b0a1563b99d81");

        private static readonly BlueprintFeature MythicBypassEpicDR = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("c60933cc085132b49970cbc4a4b8338f");
        private static readonly BlueprintFeatureSelection LifeBondingFriendshipSelection1 = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("69a33d6ced23446e819667149d088898");

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

                RemoveDefaultProgressions();
                PatchCompanionAscension();
                ReaddDefaultProgressions();
                //try 
                //{
                //    RemoveDefaultProgressions();
                //    PatchCompanionAscension();
                //    ReaddDefaultProgressions();
                //}
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

                //CompanionSpellbookMerge csm = new();
                //csm.AssetGuid = BlueprintGuid.NewGuid();
                //csm.m_SpellKnownForSpontaneous = lichbookselect.m_SpellKnownForSpontaneous;
                //csm.m_AllowedSpellbooks = lichbookselect.m_AllowedSpellbooks;
                //csm.m_MythicSpellList = lichbookselect.m_MythicSpellList;
                //Tools.LogMessage("Built CSM: " + csm.AssetGuidThreadSafe);
                // END TESTING



                var _companionAscensionChoice4 = FeatureSelectionConfigurator.New(CompanionAscensionChoice4Name, CompanionAscensionChoice4GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice4DisplayNameKey, CompanionAscensionChoice4DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice4DescriptionKey, CompanionAscensionChoice4Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility, FeatureGroup.MythicFeat, FeatureGroup.Feat, FeatureGroup.MythicAdditionalProgressions)
                    .SetIcon(MythicAbilitySelection.Icon)
                    .AddToFeatures(new string[] {
                        CompanionAscensionMythicFeatGUID,
                        CompanionAscensionMythicAbilityGUID,
                        BasicFeatSelectionGUID})
                    .Configure();
                Tools.LogMessage("Built: Companion First Ascension bonus choice -> " + _companionAscensionChoice4.AssetGuidThreadSafe);

                var _companionSecondAscension = FeatureSelectionConfigurator.New(CompanionSecondAscensionName, CompanionSecondAscensionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionSecondAscensionDisplayNameKey, CompanionSecondAscensionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionSecondAscensionDescriptionKey, CompanionSecondAscensionDescription))
                    .SetIcon(Guidance.Icon)
                    .AddToFeatures(new string[] {
                        AeonCompanionChoice.Guid,
                        AngelCompanionChoice.Guid,
                        AzataCompanionChoice.Guid,
                        DemonCompanionChoice.Guid,
                        LichCompanionChoice.Guid,
                        TricksterCompanionChoice.Guid,
                        MythicMindAndBody.Guid})
                    //.AddToFeatures(AngelCompanionChoice.Guid)
                    //.AddToFeatures(AzataCompanionChoice.Guid)
                    //.AddToFeatures(DemonCompanionChoice.Guid)
                    //.AddToFeatures(LichCompanionChoiceGUID)
                    //.AddToFeatures(TricksterCompanionChoiceGUID)
                    //.AddToFeatures(MythicMindAndBody.MythicMindAndBodyGUID)
                    .Configure();
                Tools.LogMessage("Built: Companion Second Ascension -> " + _companionSecondAscension.AssetGuidThreadSafe);

                var _companionAscensionChoice8 = FeatureSelectionConfigurator.New(CompanionAscensionChoice8Name, CompanionAscensionChoice8GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice8DisplayNameKey, CompanionAscensionChoice8DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice8DescriptionKey, CompanionAscensionChoice8Description))
                    .SetFeatureGroups(FeatureGroup.MythicAbility, FeatureGroup.MythicFeat, FeatureGroup.Feat, FeatureGroup.MythicAdditionalProgressions)
                    .SetIcon(MythicAbilitySelection.Icon)
                    .AddToFeatures(new string[]
                    {
                        _companionAscensionMythicFeat.AssetGuidThreadSafe,
                        _companionAscensionMythicAbility.AssetGuidThreadSafe,
                        BasicFeatSelectionGUID
                    })
                    .Configure();
                Tools.LogMessage("Built: Companion Second Ascension bonus choice -> " + _companionAscensionChoice8.AssetGuidThreadSafe);

                // TESTING AREA
                var _aeonCompanionChoice = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>(AeonCompanionChoice.Guid);
                //var _aeonCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("a24bb471a444485fa586e8796095b5d4");
                var _aeonCompanionNinthLevelImmunities = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(AeonCompanionChoice.AeonCompanionNinthLevelImmunitiesGUID);
                // END TESTING AREA

                if (Main.Settings.useCompanionAscension == false) { return; }
                if (Main.Settings.useBasicAscensionsOnly == false)
                {
                    MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                    {
                        le.Where(e => e.Level == 4)
                            .ForEach(e =>
                            {
                                e.m_Features.Add(_companionFirstAscension.ToReference<BlueprintFeatureBaseReference>());
                            });
                        le.Where(e => e.Level == 8)
                            .ForEach(e =>
                            {
                                e.m_Features.Add(_companionSecondAscension.ToReference<BlueprintFeatureBaseReference>());
                                //e.m_Features.Add(ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("57e6f40e11ec421c9b2e3edb34e2beb2").ToReference<BlueprintFeatureBaseReference>());
                            });
                        le.Where(e => e.Level == 9)
                            .ForEach(e =>
                            {
                                e.m_Features.Add(LifeBondingFriendshipSelection1.ToReference<BlueprintFeatureBaseReference>());
                                e.m_Features.Add(_aeonCompanionNinthLevelImmunities.ToReference<BlueprintFeatureBaseReference>());
                            });
                    });
                    AddCompanionsToFirstAscensions();
                    CorrectPrerequisites();
                    Tools.LogMessage("Built: Added Mythic Path companion ascensions");
                }
                MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                {
                    le.Where(e => e.Level == 4)
                        .ForEach(e =>
                        {
                            e.m_Features.Add(MythicIgnoreAlignmentRestrictions.ToReference<BlueprintFeatureBaseReference>());
                            e.m_Features.Add(_companionAscensionChoice4.ToReference<BlueprintFeatureBaseReference>());
                        });
                    le.Where(e => e.Level == 8)
                        .ForEach(e =>
                        {
                            e.m_Features.Add(_companionAscensionChoice8.ToReference<BlueprintFeatureBaseReference>());
                        });
                });
                Tools.LogMessage("Built: Added basic companion ascensions");

                MythicCompanionProgression.UIGroups = MythicCompanionProgression.UIGroups.AppendToArray(
                    Helpers.CreateUIGroup(
                        _companionFirstAscension,
                        _companionSecondAscension)
                    );
                MythicCompanionProgression.UIGroups = MythicCompanionProgression.UIGroups.AppendToArray(
                    Helpers.CreateUIGroup(
                        MythicAbilitySelection,
                        _companionAscensionChoice4,
                        _companionAscensionChoice8)
                    );
                MythicCompanionProgression.UIGroups = MythicCompanionProgression.UIGroups.AppendToArray(
                    Helpers.CreateUIGroup(
                        MythicBypassEpicDR,
                        MythicFeatSelection)
                    );
                Tools.LogMessage("New Content: Companion Ascension patching completed");
            }

            static void RemoveDefaultProgressions()
            {
                MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                {
                    le.Where(e => e.Level == 4)
                        .ForEach(e =>
                        {
                            e.m_Features.Remove(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());

                        });
                    le.Where(e => e.Level == 8)
                        .ForEach(e =>
                        {
                            e.m_Features.Remove(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                        });
                });
                Tools.LogMessage("Patched: Removed default Mythic Feat selections at Mythic Rank 4 and 8");
            }

            static void ReaddDefaultProgressions()
            {
                MythicCompanionProgression.LevelEntries.TemporaryContext(le =>
                {
                    le.Where(e => e.Level == 4)
                        .ForEach(e =>
                        {
                            e.m_Features.Add(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());

                        });
                    le.Where(e => e.Level == 8)
                        .ForEach(e =>
                        {
                            e.m_Features.Add(MythicFeatSelection.ToReference<BlueprintFeatureBaseReference>());
                        });
                });
                Tools.LogMessage("Patched: Readded default Mythic Feat selections at Mythic Rank 4 and 8");
            }

            static void AddCompanionsToFirstAscensions()
            {
                var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                BlueprintProgression.ClassWithLevel _classWithLevel = new()
                {
                    m_Class = _mythicCompanionClassReference,
                    AdditionalLevel = 0
                };

                foreach (string s in AscensionGUIDS)
                {
                    var _feature = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>(s);
                    _feature.m_Classes = _feature.m_Classes.AppendToArray(_classWithLevel);
                    _feature.GiveFeaturesForPreviousLevels = true;
                }
                Tools.LogMessage("Patched: Prerequisites for First Ascension Progressions");

                var _aeonFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("4db463bcf37d6014eaa23d3219703a9b");
                _aeonFirstAscentionResource.m_MaxAmount.m_Class = _aeonFirstAscentionResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);

                var _angelFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("6da18ecb21a24814eb79ab075a0b6d5e");
                _angelFirstAscentionResource.m_MaxAmount.m_ClassDiv = _angelFirstAscentionResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
                var _angelFirstAscentionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("f86857af1c584e248b9284654f31d39c");
                BlueprintComponent[] contextRankConfig = _angelFirstAscentionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
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

                var _ttricksterFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("918e6e7085d81094790e806a49922694");
                contextRankConfig = _ttricksterFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                Tools.LogMessage("Patched: Added companion class to first ascension abilities and resources");
            }

            static void CorrectPrerequisites()
            {
                var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                BlueprintProgression.ClassWithLevel _classWithLevel = new()
                {
                    m_Class = _mythicCompanionClassReference,
                    AdditionalLevel = 0
                };

                var _deathOfElementsConsumingElementsResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("7a558d186755620439e35817f174f749");
                _deathOfElementsConsumingElementsResource.m_MaxAmount.m_Class = _deathOfElementsConsumingElementsResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);

                var _angelSwordResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("5578b13626344e6409c56bb024ec9529");
                _angelSwordResource.m_MaxAmount.m_ClassDiv = _angelSwordResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);

                Tools.LogMessage("Patched: Added companion class to mythic path abilities and resources");
            }
        }
    }
}


// book merge notes: 
// FeatureSelection.Group ?

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