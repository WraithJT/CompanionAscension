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
    class AeonCompanionChoice
    {
        public static readonly string AeonCompanionChoiceGUID = "c1dd81e75695467cb3bac2381d3cec91";
        private static readonly string AeonCompanionChoiceName = "AeonCompanionChoice";
        private static readonly string AeonCompanionChoiceDisplayName = "Aeon Companion Ascension";
        private static readonly string AeonCompanionChoiceDisplayNameKey = "AeonCompanionChoiceName";
        private static readonly string AeonCompanionChoiceDescription =
            "At 8th mythic rank, Aeon's companions can gain further power.";
        private static readonly string AeonCompanionChoiceDescriptionKey = "AeonCompanionChoiceDescription";

        private static readonly string AeonCompanionEighthLevelImmunitiesName = "AeonCompanionImmunities";
        private static readonly string AeonCompanionEighthLevelImmunitiesGUID = "e54bd0e9361c407fb8e26f64de8e4e4a";
        private static readonly string AeonCompanionEighthLevelImmunitiesDisplayName = "Aeon Companion Immunities";
        private static readonly string AeonCompanionEighthLevelImmunitiesDisplayNameKey = "AeonCompanionImmunitiesNameKey";
        private static readonly string AeonCompanionEighthLevelImmunitiesDescription =
            "Aeon's companion gains immunity to bleed, mind-affecting effects, and ability damage. " +
            "At the next mythic level, they also gain immunity to curse and death effects, as well as energy drain.";
        private static readonly string AeonCompanionEighthLevelImmunitiesDescriptionKey = "AeonCompanionImmunitiesDescriptionKey";
        private static readonly string[] AeonFifthLevelImmunitiesList = {
                "52f8ef060a751a247964adae7fcb7e64",             // ImmunityToBleed
                "3eb606c0564d0814ea01a824dbe42fb0",             // ImmunityToMindAffecting
                "fda40b9ba7644754f97cb51f04759a3e"              // ImmunityToAbilityDamage
        };

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
        private static readonly string[] AeonSeventhLevelImmunitiesList = {
                "d64da5fbf9783b946ac7a0e94c9bccc1",             // ImmunityToCurseEffects
                "41d5e076fcea3fa4a9158ffded9185f7",             // ImmunityToDeathEffects
                "efe0344bca1290244a277ed5c45d9ff2"              // ImmunityToEnergyDrain
        };

        private static readonly string AeonBaneFeatureGUID = "0b25e8d8b0488c84c9b5714e9ca0a204";
        //private static readonly BlueprintFeature AeonBaneFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(AeonBaneFeatureGUID);
        //private static readonly string AeonFifthLevelImmunitiesGUID = "c52b48a922161fe45a258f0214d6501a";
        //private static readonly BlueprintFeature AeonFifthLevelImmunities = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(AeonFifthLevelImmunitiesGUID);
        private static readonly string AeonSeventhLevelImmunitiesGUID = "c52b48a922161fe45a258f0214d6501a";
        private static readonly BlueprintFeature AeonSeventhLevelImmunities = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(AeonSeventhLevelImmunitiesGUID);

        private static readonly string AeonProgression = "34b9484b0d5ce9340ae51d2bf9518bbe";

        private static readonly BlueprintAbility AngelWardFromWeakness = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("b66eee357e8404d49b7d8ad58cbb7f15");
        private static readonly BlueprintAbility OathOfPeace = ResourcesLibrary.TryGetBlueprint<BlueprintAbility>("cb3d70cc98b5f1540bddfff6f9667f73");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchAeonCompanionChoice();
                //try { PatchAeonCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
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
                    //.PrerequisiteFeature(AeonCompanionEighthLevelImmunitiesGUID)
                    //.PrerequisiteNoFeature(AeonBaneFeatureGUID)
                    .Configure();
                _immunityToCurseEffects.AddComponents(ImmunityToCurseEffects.Components);

                string _immunityToDeathEffectsName = "ImmunityToDeathEffects";
                string _immunityToDeathEffectsGUID = "75539df05aac4fbba482c3e8c1ba64df";
                string _immunityToDeathEffectsDisplayName = "Immunity to Death Effects";
                var _immunityToDeathEffects = FeatureConfigurator.New(_immunityToDeathEffectsName, _immunityToDeathEffectsGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_immunityToDeathEffectsName + "Key", _immunityToDeathEffectsDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString("_immunityToDeathEffectsDescKey", ""))
                    //.PrerequisiteFeature(AeonCompanionEighthLevelImmunitiesGUID)
                    //.PrerequisiteNoFeature(AeonBaneFeatureGUID)
                    .Configure();
                _immunityToDeathEffects.AddComponents(ImmunityToDeathEffects.Components);

                string _immunityToEnergyDrainName = "ImmunityToEnergyDrain";
                string _immunityToEnergyDrainGUID = "e6537e18e65d4481a9c8e9e1b7dbbb65";
                string _immunityToEnergyDrainDisplayName = "Immunity to Energy Drain";
                var _immunityToEnergyDrain = FeatureConfigurator.New(_immunityToEnergyDrainName, _immunityToEnergyDrainGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_immunityToEnergyDrainName + "Key", _immunityToEnergyDrainDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString("_immunityToEnergyDrainDescKey", ""))
                    //.PrerequisiteFeature(AeonCompanionEighthLevelImmunitiesGUID)
                    //.PrerequisiteNoFeature(AeonBaneFeatureGUID)
                    .Configure();
                _immunityToEnergyDrain.AddComponents(ImmunityToEnergyDrain.Components);



                var _aeonCompanionEighthLevelImmunities = FeatureConfigurator.New(AeonCompanionEighthLevelImmunitiesName, AeonCompanionEighthLevelImmunitiesGUID)
                    .SetDisplayName(LocalizationTool.CreateString(AeonCompanionEighthLevelImmunitiesDisplayNameKey, AeonCompanionEighthLevelImmunitiesDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(AeonCompanionEighthLevelImmunitiesDescriptionKey, AeonCompanionEighthLevelImmunitiesDescription))
                    .AddFacts(AeonFifthLevelImmunitiesList)
                    .SetIcon(AngelWardFromWeakness.Icon)
                    .Configure();

                PrerequisiteFeature _aeonCompanionNinthLevelImmunitiesPrereq = new();
                _aeonCompanionNinthLevelImmunitiesPrereq.m_Feature = _aeonCompanionEighthLevelImmunities.ToReference<BlueprintFeatureReference>();
                _aeonCompanionNinthLevelImmunitiesPrereq.CheckInProgression = true;

                string[] _aeonSeventhLevelImmunities = { _immunityToCurseEffectsGUID, _immunityToDeathEffectsGUID, _immunityToEnergyDrainGUID };
                var _aeonCompanionNinthLevelImmunities = FeatureConfigurator.New(AeonCompanionNinthLevelImmunitiesName, AeonCompanionNinthLevelImmunitiesGUID)
                    .SetDisplayName(LocalizationTool.CreateString(AeonCompanionNinthLevelImmunitiesDisplayNameKey, AeonCompanionNinthLevelImmunitiesDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(AeonCompanionNinthLevelImmunitiesDescriptionKey, AeonCompanionNinthLevelImmunitiesDescription))
                    .AddFacts(_aeonSeventhLevelImmunities)
                    //.SetHideInUi(true)
                    //.SetHideInCharacterSheetAndLevelUp(true)
                    //.PrerequisiteFeature(AeonCompanionEighthLevelImmunitiesGUID)
                    //.PrerequisiteNoFeature(AeonBaneFeatureGUID)
                    .SetIcon(AngelWardFromWeakness.Icon)
                    .Configure();
                _aeonCompanionNinthLevelImmunities.AddComponents(_aeonCompanionNinthLevelImmunitiesPrereq);

                // can't make second set of immunities stop showing up

                var _aeonCompanionNinthLevelImmunitiesSelection = FeatureSelectionConfigurator.New("testselection", "A52C072A-41EF-47D4-AA83-A948BD639EE6")
                    .AddToFeatures(AeonCompanionNinthLevelImmunitiesGUID)
                    .PrerequisiteFeature(AeonCompanionEighthLevelImmunitiesGUID)
                    .PrerequisiteNoFeature(AeonBaneFeatureGUID)
                    .Configure();
                

                var _aeonCompanionChoice = FeatureSelectionConfigurator.New(AeonCompanionChoiceName, AeonCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(AeonCompanionChoiceDisplayNameKey, AeonCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(AeonCompanionChoiceDescriptionKey, AeonCompanionChoiceDescription))
                    .AddToFeatureGroups(FeatureGroup.MythicAdditionalProgressions)
                    .AddToFeatures(AeonBaneFeatureGUID)
                    .AddToFeatures(AeonCompanionEighthLevelImmunitiesGUID)
                    .SetIcon(OathOfPeace.Icon)
                    //.PrerequisitePlayerHasFeature(AeonProgression)
                    //.SetHideInUi(true)
                    .Configure();
                Tools.LogMessage("Built: Aeon Companion Choices -> " + _aeonCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
