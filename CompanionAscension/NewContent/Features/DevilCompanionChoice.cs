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
    class DevilCompanionChoice
    {
        public static readonly string DevilCompanionChoiceGUID = "";
        //private static readonly BlueprintFeatureSelection DevilUniqueAbilitiesSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1f646b820a37d3d4a8ab116a24ee0022");
        private static readonly string DevilCompanionChoiceName = "DevilCompanionChoice";
        private static readonly string DevilCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string DevilCompanionChoiceDisplayNameKey = "DevilCompanionChoiceName";
        private static readonly string DevilCompanionChoiceDescription = "";
        private static readonly string DevilCompanionChoiceDescriptionKey = "DevilCompanionChoiceDescription";

        private static readonly string DevilProgression = "87bc9abf00b240a44bb344fea50ec9bc";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchDevilCompanionChoice();
                //try { PatchDevilCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchDevilCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Devil Companion Choices");

                var _devilCompanionChoice = FeatureSelectionConfigurator.New(DevilCompanionChoiceName, DevilCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(DevilCompanionChoiceDisplayNameKey, DevilCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DevilCompanionChoiceDescriptionKey, DevilCompanionChoiceDescription))
                    //.PrerequisitePlayerHasFeature(DevilProgression)
                    .SetHideInUi(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailableInUI(true)
                    .Configure();
                //_devilCompanionChoice.m_AllFeatures = DevilUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Devil Companion Choices -> " + _devilCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
