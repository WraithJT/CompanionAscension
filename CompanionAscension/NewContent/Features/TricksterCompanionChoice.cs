using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.UnitLogic;
using System;

namespace CompanionAscension.NewContent.Features
{
    class TricksterCompanionChoice
    {
        public static readonly string Guid = "095edab4d08f4b7dab6eb7450e93cfca";
        private static readonly string TricksterCompanionChoiceName = "TricksterCompanionChoice";
        private static readonly string TricksterCompanionChoiceDisplayName = "Trickster Companion Ascension";
        private static readonly string TricksterCompanionChoiceDisplayNameKey = "TricksterCompanionChoiceName";
        private static readonly string TricksterCompanionChoiceDescription = "";
        private static readonly string TricksterCompanionChoiceDescriptionKey = "TricksterCompanionChoiceDescription";

        private static readonly string TricksterProgression = "cc64789b0cc5df14b90da1ffee7bbeea";
        private static readonly BlueprintFeatureSelection TricksterRank1Selection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("4fbc563529717de4d92052048143e0f1");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchTricksterCompanionChoice();
                try { PatchTricksterCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchTricksterCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Trickster Companion Choices");

                UnitState unitState = new();
                //AddMechanicsFeature

                var _tricksterCompanionChoice = FeatureSelectionConfigurator.New(TricksterCompanionChoiceName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(TricksterCompanionChoiceDisplayNameKey, TricksterCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(TricksterCompanionChoiceDescriptionKey, TricksterCompanionChoiceDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_TricksterAscension.png"))
                    .AddPrerequisitePlayerHasFeature(TricksterProgression)
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
                _tricksterCompanionChoice.m_AllFeatures = TricksterRank1Selection.m_AllFeatures;
                Tools.LogMessage("Built: Trickster Companion Choices -> " + _tricksterCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
