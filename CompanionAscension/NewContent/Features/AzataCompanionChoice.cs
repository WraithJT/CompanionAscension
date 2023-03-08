using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes.Selection;
using Kingmaker.Blueprints.JsonSystem;
using System;

namespace CompanionAscension.NewContent.Features
{
    class AzataCompanionChoice
    {
        public static readonly string Guid = "f7191b869724482b8f1d14b9b195c764";
        private static readonly string Name = "AzataCompanionChoice";
        private static readonly string DisplayName = "Azata Companion Ascension";
        private static readonly string DisplayNameKey = "AzataCompanionChoiceName";
        private static readonly string Description = "Select one Azata Superpower.";
        private static readonly string DescriptionKey = "AzataCompanionChoiceDescription";
        private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");

        private static readonly string AzataProgression = "9db53de4bf21b564ca1a90ff5bd16586";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchAzataCompanionChoice();
                try { PatchAzataCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchAzataCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Azata Companion Choices");

                var _azataCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AzataCompanionChoice.png"))
                    .AddPrerequisitePlayerHasFeature(AzataProgression)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .SetHideInUI(true)
                    .Configure();
                _azataCompanionChoice.m_AllFeatures = AzataSuperpowersSelection.m_AllFeatures;
                Tools.LogMessage("Built: Azata Companion Choices -> " + _azataCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
