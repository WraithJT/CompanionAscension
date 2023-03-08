using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.Classes.Prerequisites;
using Kingmaker.Blueprints.JsonSystem;
using System;

namespace CompanionAscension.NewContent.Features
{
    class AeonCompanionChoice
    {
        public static readonly string Guid = "c1dd81e75695467cb3bac2381d3cec91";
        private static readonly string ShortName = "AeonCompanionChoice";
        private static readonly string DisplayName = "Aeon Companion Ascension";
        private static readonly string DisplayNameKey = "AeonCompanionChoiceName";
        private static readonly string Description =
            "At 8th mythic rank, Aeon's companions can gain further power.";
        private static readonly string DescriptionKey = "AeonCompanionChoiceDescription";

        private static readonly string AeonCompanionEighthLevelImmunitiesName = "AeonCompanionImmunitiesEightLevel";
        private static readonly string AeonCompanionEighthLevelImmunitiesGUID = "e54bd0e9361c407fb8e26f64de8e4e4a";
        private static readonly string AeonCompanionEighthLevelImmunitiesDisplayName = "Aeon Companion Immunities";
        private static readonly string AeonCompanionEighthLevelImmunitiesDisplayNameKey = "AeonCompanionImmunitiesEightLevelNameKey";
        private static readonly string AeonCompanionEighthLevelImmunitiesDescription =
            "Aeon's companion gains immunity to bleed, mind-affecting effects, and ability damage. " +
            "\nAt the next mythic level, they also gain immunity to curse and death effects, as well as energy drain.";
        private static readonly string AeonCompanionEighthLevelImmunitiesDescriptionKey = "AeonCompanionImmunitiesEightLevelDescriptionKey";
        private static readonly BlueprintFeature ImmunityToBleed = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("52f8ef060a751a247964adae7fcb7e64");
        private static readonly BlueprintFeature ImmunityToMindAffecting = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("3eb606c0564d0814ea01a824dbe42fb0");
        private static readonly BlueprintFeature ImmunityToAbilityDamage = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("fda40b9ba7644754f97cb51f04759a3e");

        public static readonly string AeonCompanionNinthLevelImmunitiesGUID = "76a5af87f6594d5e90568b706f0809ed";
        private static readonly string AeonCompanionNinthLevelImmunitiesName = "AeonCompanionImmunitiesNinthLevel";
        private static readonly string AeonCompanionNinthLevelImmunitiesDisplayName = "Aeon Companion Immunities Ninth Level";
        private static readonly string AeonCompanionNinthLevelImmunitiesDisplayNameKey = "AeonCompanionImmunitiesNinthLevelNameKey";
        private static readonly string AeonCompanionNinthLevelImmunitiesDescription =
            "Aeon's companion gains immunity to curse and death effects, as well as energy drain.";
        private static readonly string AeonCompanionNinthLevelImmunitiesDescriptionKey = "AeonCompanionImmunitiesNinthLevelDescriptionKey";
        private static readonly BlueprintFeature ImmunityToCurseEffects = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("d64da5fbf9783b946ac7a0e94c9bccc1");
        private static readonly BlueprintFeature ImmunityToDeathEffects = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("41d5e076fcea3fa4a9158ffded9185f7");
        private static readonly BlueprintFeature ImmunityToEnergyDrain = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("efe0344bca1290244a277ed5c45d9ff2");

        private static readonly string AeonBaneFeatureGUID = "0b25e8d8b0488c84c9b5714e9ca0a204";
        private static readonly string AeonProgression = "34b9484b0d5ce9340ae51d2bf9518bbe";
        public static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchAeonCompanionChoice();
                try { PatchAeonCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchAeonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Aeon Companion Choices");

                string _immunityToCurseEffectsName = "ImmunityToCurseEffects";
                string _immunityToCurseEffectsGUID = "6491a3c3a5444f73bd4e93b734223634";
                string _immunityToCurseEffectsDisplayName = "Immunity to Curse Effects";
                var _immunityToCurseEffects = FeatureConfigurator.New(_immunityToCurseEffectsName, _immunityToCurseEffectsGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_immunityToCurseEffectsName + "Key", _immunityToCurseEffectsDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString("immunityToCurseEffectsDescKey", ""))
                    .Configure();
                _immunityToCurseEffects.AddComponents(ImmunityToCurseEffects.Components);

                string _immunityToDeathEffectsName = "ImmunityToDeathEffects";
                string _immunityToDeathEffectsGUID = "75539df05aac4fbba482c3e8c1ba64df";
                string _immunityToDeathEffectsDisplayName = "Immunity to Death Effects";
                var _immunityToDeathEffects = FeatureConfigurator.New(_immunityToDeathEffectsName, _immunityToDeathEffectsGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_immunityToDeathEffectsName + "Key", _immunityToDeathEffectsDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString("_immunityToDeathEffectsDescKey", ""))
                    .Configure();
                _immunityToDeathEffects.AddComponents(ImmunityToDeathEffects.Components);

                string _immunityToEnergyDrainName = "ImmunityToEnergyDrain";
                string _immunityToEnergyDrainGUID = "e6537e18e65d4481a9c8e9e1b7dbbb65";
                string _immunityToEnergyDrainDisplayName = "Immunity to Energy Drain";
                var _immunityToEnergyDrain = FeatureConfigurator.New(_immunityToEnergyDrainName, _immunityToEnergyDrainGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_immunityToEnergyDrainName + "Key", _immunityToEnergyDrainDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString("_immunityToEnergyDrainDescKey", ""))
                    .Configure();
                _immunityToEnergyDrain.AddComponents(ImmunityToEnergyDrain.Components);

                var _aeonCompanionEighthLevelImmunities = FeatureConfigurator.New(AeonCompanionEighthLevelImmunitiesName, AeonCompanionEighthLevelImmunitiesGUID)
                    .SetDisplayName(LocalizationTool.CreateString(AeonCompanionEighthLevelImmunitiesDisplayNameKey, AeonCompanionEighthLevelImmunitiesDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(AeonCompanionEighthLevelImmunitiesDescriptionKey, AeonCompanionEighthLevelImmunitiesDescription))
                    .AddFacts(new() { ImmunityToBleed, ImmunityToMindAffecting, ImmunityToAbilityDamage })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AeonCompanionImmunities.png"))
                    .Configure();

                PrerequisiteFeature _aeonCompanionNinthLevelImmunitiesPrereq = new();
                _aeonCompanionNinthLevelImmunitiesPrereq.m_Feature = _aeonCompanionEighthLevelImmunities.ToReference<BlueprintFeatureReference>();
                _aeonCompanionNinthLevelImmunitiesPrereq.CheckInProgression = true;

                var _aeonCompanionNinthLevelImmunities = FeatureConfigurator.New(AeonCompanionNinthLevelImmunitiesName, AeonCompanionNinthLevelImmunitiesGUID)
                    .SetDisplayName(LocalizationTool.CreateString(AeonCompanionNinthLevelImmunitiesDisplayNameKey, AeonCompanionNinthLevelImmunitiesDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(AeonCompanionNinthLevelImmunitiesDescriptionKey, AeonCompanionNinthLevelImmunitiesDescription))
                    .AddFacts(new()
                    {
                        _immunityToCurseEffects.AssetGuidThreadSafe,
                        _immunityToDeathEffects.AssetGuidThreadSafe,
                        _immunityToEnergyDrain.AssetGuidThreadSafe
                    })
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AeonCompanionImmunities.png"))
                    .Configure();
                _aeonCompanionNinthLevelImmunities.AddComponents(_aeonCompanionNinthLevelImmunitiesPrereq);


                //BlueprintFeature PowerAttackFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("9972f33f977fc724c838e59641b2fca5");
                //BlueprintProgression.ClassWithLevel _classWithLevel = new()
                //{
                //    m_Class = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>(),
                //    AdditionalLevel = 0
                //};
                //string _aeonCompanionImmunityName = "AeonCompanionImmunityProgression";
                //string _aeonCompanionImmunityProgressionGUID = "a24bb471a444485fa586e8796095b5d4";
                //var _aeonCompanionImmunityProgression = ProgressionConfigurator.New(_aeonCompanionImmunityName, _aeonCompanionImmunityProgressionGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(_aeonCompanionImmunityName + "NameKey", "Aeon Companion Immunities Progression", false))
                //    .SetDescription(LocalizationTool.CreateString(_aeonCompanionImmunityName + "DescriptionKey", "description here"))
                //    //.AddToGroups(new FeatureGroup[] { FeatureGroup.MythicAdditionalProgressions, FeatureGroup.MythicAbility })
                //    //.SetIcon(AngelWardFromWeakness.Icon)
                //    .Configure();
                ////BlueprintProgression _aeonCompanionImmunityProgression = new()
                ////{
                ////    name = _aeonCompanionImmunityName,
                ////    AssetGuid = BlueprintGuid.Parse(_aeonCompanionImmunityProgressionGUID),
                ////    m_DisplayName = LocalizationTool.CreateString(_aeonCompanionImmunityName + "NameKey", "Aeon Companion Immunities Progression", false),
                ////    m_Description = LocalizationTool.CreateString(_aeonCompanionImmunityName + "DescriptionKey", "description here"),
                ////    m_DescriptionShort = LocalizationTool.CreateString(_aeonCompanionImmunityName + "DescriptionKey", "description here"),
                //_aeonCompanionImmunityProgression.GiveFeaturesForPreviousLevels = true;
                //_aeonCompanionImmunityProgression.Ranks = 1;
                //_aeonCompanionImmunityProgression.IsClassFeature = true;
                //_aeonCompanionImmunityProgression.m_Classes = new[] { _classWithLevel };
                ////};
                //_aeonCompanionImmunityProgression.LevelEntries.TemporaryContext(le =>
                //        {
                //            le.Where(e => e.Level == 8)
                //                .ForEach(e =>
                //                {
                //                    e.m_Features.Add(_aeonCompanionEighthLevelImmunities.ToReference<BlueprintFeatureBaseReference>());
                //                    e.m_Features.Add(PowerAttackFeature.ToReference<BlueprintFeatureBaseReference>());
                //                });
                //            le.Where(e => e.Level == 9)
                //                .ForEach(e =>
                //                {
                //                    e.m_Features.Add(_aeonCompanionNinthLevelImmunities.ToReference<BlueprintFeatureBaseReference>());
                //                });
                //        });

                //Tools.LogMessage("Built: Aeon Companion Progression -> " + _aeonCompanionImmunityProgression.AssetGuidThreadSafe);

                var _aeonCompanionChoice = FeatureSelectionConfigurator.New(ShortName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddToGroups(FeatureGroup.MythicAdditionalProgressions)
                    .AddToAllFeatures(AeonBaneFeatureGUID)
                    .AddToAllFeatures(_aeonCompanionEighthLevelImmunities.AssetGuidThreadSafe)
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AeonCompanionChoice.png"))
                    .AddPrerequisitePlayerHasFeature(AeonProgression)
                    .SetHideInUI(true)
                    .SetHideNotAvailibleInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .Configure();
                Tools.LogMessage("Built: Aeon Companion Choices -> " + _aeonCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
