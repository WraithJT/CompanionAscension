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
    class LegendCompanionChoice
    {
        public static readonly string LegendCompanionChoiceGUID = "4387b5bc3f424b2fa9575d4620d9489c";
        //private static readonly BlueprintFeatureSelection LegendUniqueAbilitiesSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1f646b820a37d3d4a8ab116a24ee0022");
        private static readonly string LegendCompanionChoiceName = "LegendCompanionChoice";
        private static readonly string LegendCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string LegendCompanionChoiceDisplayNameKey = "LegendCompanionChoiceName";
        private static readonly string LegendCompanionChoiceDescription = "";
        private static readonly string LegendCompanionChoiceDescriptionKey = "LegendCompanionChoiceDescription";

        private static readonly string LegendProgression = "905383229aaf79e4b8d7e2d316b68715";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchLegendCompanionChoice();
                //try { PatchLegendCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchLegendCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Legend Companion Choices");

                var _legendCompanionChoice = FeatureSelectionConfigurator.New(LegendCompanionChoiceName, LegendCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(LegendCompanionChoiceDisplayNameKey, LegendCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(LegendCompanionChoiceDescriptionKey, LegendCompanionChoiceDescription))
                    //.PrerequisitePlayerHasFeature(LegendProgression)
                    //.SetHideInUi(true)
                    .Configure();
                //_legendCompanionChoice.m_AllFeatures = LegendUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Legend Companion Choices -> " + _legendCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
