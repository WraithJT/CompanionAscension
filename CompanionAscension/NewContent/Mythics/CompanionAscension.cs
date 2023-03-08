using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using CompanionAscension.NewContent.Features;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.Items.Weapons;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums.Damage;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics.Components;
using Kingmaker.Utility;
using System;
using System.Linq;

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
        private static readonly BlueprintFeatureSelection FirstAscensionSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1421e0034a3afac458de08648d06faf0");

        private static readonly string CompanionSecondAscensionName = "CompanionSecondAscension";
        private static readonly string CompanionSecondAscensionGUID = "b6982c2cd5804966bc69c3f2d69493b4";
        private static readonly string CompanionSecondAscensionDisplayName = "Ascension";
        private static readonly string CompanionSecondAscensionDisplayNameKey = "CompanionSecondAscensionName";
        private static readonly string CompanionSecondAscensionDescription = "As the Commander's power grows, so too does the power of {mf|his|her} companions.";
        private static readonly string CompanionSecondAscensionDescriptionKey = "CompanionSecondAscensionDescription";

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

        public static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");

        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
        private static readonly BlueprintFeature ExtraMythicAbilityMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("8a6a511c55e67d04db328cc49aaad2b8");
        private static readonly BlueprintFeature ExtraMythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("e10c4f18a6c8b4342afe6954bde0587b");
        private static readonly BlueprintFeature ExtraMythicFeatMythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("c916448f690d4f4e9d824d6f376e621d");
        private static readonly BlueprintFeatureSelection MythicAbilitySelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("ba0e5a900b775be4a99702f1ed08914d");
        private static readonly BlueprintFeatureSelection MythicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("9ee0f6745f555484299b0a1563b99d81");
        private static readonly BlueprintFeatureSelection BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");

        private static readonly BlueprintFeature MythicIgnoreAlignmentRestrictions = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("24e78475f0a243e1a810452d14d0a1bd");
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

                //RemoveDefaultProgressions();
                //PatchCompanionAscension();
                //ReaddDefaultProgressions();
                try
                {
                    RemoveDefaultProgressions();
                    PatchCompanionAscension();
                    ReaddDefaultProgressions();
                }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }
            public static void PatchCompanionAscension()
            {
                Tools.LogMessage("New Content: Patching Companion Ascension");

                var _companionFirstAscension = FeatureSelectionConfigurator.New(CompanionFirstAscensionName, CompanionFirstAscensionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionFirstAscensionDisplayNameKey, CompanionFirstAscensionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionFirstAscensionDescriptionKey, CompanionFirstAscensionDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_Ascension.png"))
                    .Configure();
                _companionFirstAscension.m_AllFeatures = FirstAscensionSelection.m_AllFeatures;
                Tools.LogMessage("Built: Companion First Ascension -> " + _companionFirstAscension.AssetGuidThreadSafe);

                var _companionAscensionMythicAbility = FeatureSelectionConfigurator.New(CompanionAscensionMythicAbilityName, CompanionAscensionMythicAbilityGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionMythicAbilityDisplayNameKey, CompanionAscensionMythicAbilityDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionMythicAbilityDescriptionKey, CompanionAscensionMythicAbilityDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AscensionMythicAbility.png"))
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

                var _companionAscensionChoice4 = FeatureSelectionConfigurator.New(CompanionAscensionChoice4Name, CompanionAscensionChoice4GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice4DisplayNameKey, CompanionAscensionChoice4DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice4DescriptionKey, CompanionAscensionChoice4Description))
                    .AddToGroups(new FeatureGroup[] {
                        FeatureGroup.MythicAbility,
                        FeatureGroup.MythicFeat,
                        FeatureGroup.Feat,
                        FeatureGroup.MythicAdditionalProgressions })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_CompanionAscensionChoice.png"))
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] {
                        _companionAscensionMythicFeat,
                        _companionAscensionMythicAbility,
                        BasicFeatSelection })
                    .Configure();
                Tools.LogMessage("Built: Companion First Ascension bonus choice -> " + _companionAscensionChoice4.AssetGuidThreadSafe);

                var _companionSecondAscension = FeatureSelectionConfigurator.New(CompanionSecondAscensionName, CompanionSecondAscensionGUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionSecondAscensionDisplayNameKey, CompanionSecondAscensionDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionSecondAscensionDescriptionKey, CompanionSecondAscensionDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_Ascension.png"))
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] {
                        AeonCompanionChoice.Guid,
                        AngelCompanionChoice.Guid,
                        AzataCompanionChoice.Guid,
                        DemonCompanionChoice.Guid,
                        LichCompanionChoice.Guid,
                        TricksterCompanionChoice.Guid,
                        GoldDragonCompanionChoice.Guid,
                        LegendCompanionChoice.Guid,
                        DevilCompanionChoice.Guid,
                        MythicMindAndBody.Guid})
                    .Configure();
                Tools.LogMessage("Built: Companion Second Ascension -> " + _companionSecondAscension.AssetGuidThreadSafe);

                var _companionAscensionChoice8 = FeatureSelectionConfigurator.New(CompanionAscensionChoice8Name, CompanionAscensionChoice8GUID)
                    .SetDisplayName(LocalizationTool.CreateString(CompanionAscensionChoice8DisplayNameKey, CompanionAscensionChoice8DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(CompanionAscensionChoice8DescriptionKey, CompanionAscensionChoice8Description))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.MythicAbility, FeatureGroup.MythicFeat, FeatureGroup.Feat, FeatureGroup.MythicAdditionalProgressions })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_CompanionAscensionChoice.png"))
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] {
                        _companionAscensionMythicFeat,
                        _companionAscensionMythicAbility,
                        BasicFeatSelection })
                    .Configure();
                Tools.LogMessage("Built: Companion Second Ascension bonus choice -> " + _companionAscensionChoice8.AssetGuidThreadSafe);

                // TESTING AREA

                // END TESTING AREA

                var _aeonCompanionNinthLevelImmunities = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(AeonCompanionChoice.AeonCompanionNinthLevelImmunitiesGUID);
                var _goldDragonCompanionFeat = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(GoldDragonCompanionChoice.GoldDragonCompanionFeatGUID);
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
                            });
                        le.Where(e => e.Level == 9)
                            .ForEach(e =>
                            {
                                e.m_Features.Add(LifeBondingFriendshipSelection1.ToReference<BlueprintFeatureBaseReference>());
                                e.m_Features.Add(_aeonCompanionNinthLevelImmunities.ToReference<BlueprintFeatureBaseReference>());
                                e.m_Features.Add(_goldDragonCompanionFeat.ToReference<BlueprintFeatureBaseReference>());
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
                Tools.LogMessage("Patched: Temporarily removed default Mythic Feat selections at Mythic Rank 4 and 8");
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
                    if (!_feature.m_Classes.Contains(_classWithLevel))
                    {
                        _feature.m_Classes = _feature.m_Classes.AppendToArray(_classWithLevel);
                        _feature.GiveFeaturesForPreviousLevels = true;
                    }
                }
                Tools.LogMessage("Patched: Prerequisites for First Ascension Progressions");

                var _aeonFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("4db463bcf37d6014eaa23d3219703a9b");
                if (!_aeonFirstAscentionResource.m_MaxAmount.m_Class.Contains(_mythicCompanionClassReference))
                {
                    _aeonFirstAscentionResource.m_MaxAmount.m_Class = _aeonFirstAscentionResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                var _angelFirstAscentionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("6da18ecb21a24814eb79ab075a0b6d5e");
                if (!_angelFirstAscentionResource.m_MaxAmount.m_ClassDiv.Contains(_mythicCompanionClassReference))
                {
                    _angelFirstAscentionResource.m_MaxAmount.m_ClassDiv = _angelFirstAscentionResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
                }
                var _angelFirstAscentionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("f86857af1c584e248b9284654f31d39c");
                BlueprintComponent[] contextRankConfig = _angelFirstAscentionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    if (!c.m_Class.Contains(_mythicCompanionClassReference))
                    {
                        c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                    }
                }

                var _azataFirstAscensionResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("8419c285e922c1044893472bcbd3d3bf");
                if (!_azataFirstAscensionResource.m_MaxAmount.m_ClassDiv.Contains(_mythicCompanionClassReference))
                {
                    _azataFirstAscensionResource.m_MaxAmount.m_ClassDiv = _azataFirstAscensionResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
                }
                var _azataFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("5d5f0c9b274bab44eb01272c8fcf251d");
                contextRankConfig = _azataFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    if (!c.m_Class.Contains(_mythicCompanionClassReference))
                    {
                        c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                    }
                }

                var _lichChannelNegativeResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("e5ef1aae31818f041bccbc9fd37662bf");
                if (!_lichChannelNegativeResource.m_MaxAmount.m_Class.Contains(_mythicCompanionClassReference))
                {
                    _lichChannelNegativeResource.m_MaxAmount.m_Class = _lichChannelNegativeResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                var _ttricksterFirstAscensionAbility = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("918e6e7085d81094790e806a49922694");
                contextRankConfig = _ttricksterFirstAscensionAbility.ComponentsArray.Where(c => (c is ContextRankConfig)).ToArray();
                foreach (ContextRankConfig c in contextRankConfig)
                {
                    if (!c.m_Class.Contains(_mythicCompanionClassReference))
                    {
                        c.m_Class = c.m_Class.AppendToArray(_mythicCompanionClassReference);
                    }
                }

                Tools.LogMessage("Patched: Added companion class to first ascension abilities and resources");
            }

            static void CorrectPrerequisites()
            {
                var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                //BlueprintProgression.ClassWithLevel _classWithLevel = new()
                //{
                //    m_Class = _mythicCompanionClassReference,
                //    AdditionalLevel = 0
                //};

                var _deathOfElementsConsumingElementsResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("7a558d186755620439e35817f174f749");
                if (!_deathOfElementsConsumingElementsResource.m_MaxAmount.m_Class.Contains(_mythicCompanionClassReference))
                {
                    _deathOfElementsConsumingElementsResource.m_MaxAmount.m_Class = _deathOfElementsConsumingElementsResource.m_MaxAmount.m_Class.AppendToArray(_mythicCompanionClassReference);
                }

                var _angelSwordResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("5578b13626344e6409c56bb024ec9529");
                if (!_angelSwordResource.m_MaxAmount.m_ClassDiv.Contains(_mythicCompanionClassReference))
                {
                    _angelSwordResource.m_MaxAmount.m_ClassDiv = _angelSwordResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
                }

                Tools.LogMessage("Patched: Added companion class to mythic path abilities and resources");
            }
        }
    }
}