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
using Kingmaker.UnitLogic;
using System;

namespace CompanionAscension.NewContent.Features
{
    static class LichCompanionChoice
    {
        public static readonly string Guid = "4387b5bc3f424b2fa9575d4620d9489c";
        private static readonly string LichCompanionChoiceName = "LichCompanionChoice";
        private static readonly string LichCompanionChoiceDisplayName = "Lich Companion Ascension";
        private static readonly string LichCompanionChoiceDisplayNameKey = "LichCompanionChoiceName";
        private static readonly string LichCompanionChoiceDescription = "";
        private static readonly string LichCompanionChoiceDescriptionKey = "LichCompanionChoiceDescription";

        private static readonly BlueprintFeatureSelection LichUniqueAbilitiesSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1f646b820a37d3d4a8ab116a24ee0022");
        private static readonly string LichProgression = "ccec4e01b85bf5d46a3c3717471ba639";
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

                //PatchLichCompanionChoice();
                try { PatchLichCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchLichCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Lich Companion Choices");

                var _undeadType = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>("734a29b693e9ec346ba2951b27987e33");

                string _lichCompanionUndeadGUID = "9b3ef8d3e904478ebbf35df03699e4bc";
                string _lichCompanionUndeadName = "UndeadCompanion";
                string _lichCompanionUndeadDisplayName = "Undead Companion";
                string _lichCompanionUndeadDisplayNameKey = "UndeadCompanionNameKey";
                string _lichCompanionUndeadDescription =
                    "The Lich's companion gains the traits and immunities of an undead creature." +
                    "\nNo Constitution score. Undead use their Charisma score in place of their Constitution " +
                    "score when calculating hit points, Fortitude saves, and any special ability that relies " +
                    "on Constitution (such as when calculating a breath weapon's DC). " +
                    "\nImmunity to bleed, death effects, disease, paralysis, poison, sleep effects, and stunning.";
                string _lichCompanionUndeadDescriptionKey = "UndeadCompanionDescriptionKey";
                var _lichCompanionUndead = FeatureConfigurator.New(_lichCompanionUndeadName, _lichCompanionUndeadGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_lichCompanionUndeadDisplayNameKey, _lichCompanionUndeadDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_lichCompanionUndeadDescriptionKey, _lichCompanionUndeadDescription))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.MythicAdditionalProgressions })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_LichUndeadCompanion.png"))
                    .AddFacts(new() { _undeadType })
                    .Configure();
                //_lichCompanionUndead.Components = _undeadType.Components;

                string _lichAspectChoiceName = "LichCompanionPowers";
                string _lichAspectChoiceGUID = "9ced957271df4b43aea59441f58c87b9";
                string _lichAspectChoiceDisplayName = "Lich Powers";
                string _lichAspectChoiceDisplayNameKey = "LichCompanionPowersNameKey";
                string _lichAspectChoiceDescription =
                    "The Lich's companion gains the benefits of a Lich power.";
                string _lichAspectChoiceDescriptionKey = "LichCompanionPowersDescriptionKey";
                var _lichCompanionAbilities = FeatureConfigurator.New(_lichAspectChoiceName, _lichAspectChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_lichAspectChoiceDisplayNameKey, _lichAspectChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_lichAspectChoiceDescriptionKey, _lichAspectChoiceDescription))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.LichUniqueAbility, FeatureGroup.MythicAdditionalProgressions })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_LichPowers.png"))
                    .Configure();
                _lichCompanionAbilities.AddSelectionCallback(LichUniqueAbilitiesSelection, MythicCompanionProgression);

                var _lichCompanionChoice = FeatureSelectionConfigurator.New(LichCompanionChoiceName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(LichCompanionChoiceDisplayNameKey, LichCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(LichCompanionChoiceDescriptionKey, LichCompanionChoiceDescription))
                    .AddToGroups(new FeatureGroup[] { FeatureGroup.MythicAdditionalProgressions })
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] { _lichCompanionUndead.AssetGuidThreadSafe, _lichCompanionAbilities.AssetGuidThreadSafe })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_LichCompanionChoice.png"))
                    .AddPrerequisitePlayerHasFeature(LichProgression)
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
                Tools.LogMessage("Built: Lich Companion Choices -> " + _lichCompanionChoice.AssetGuidThreadSafe);
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