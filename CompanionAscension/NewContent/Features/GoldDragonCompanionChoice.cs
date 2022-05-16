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
    class GoldDragonCompanionChoice
    {
        public static readonly string GoldDragonCompanionChoiceGUID = "4387b5bc3f424b2fa9575d4620d9489c";
        //private static readonly BlueprintFeatureSelection GoldDragonUniqueAbilitiesSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("1f646b820a37d3d4a8ab116a24ee0022");
        private static readonly string GoldDragonCompanionChoiceName = "GoldDragonCompanionChoice";
        private static readonly string GoldDragonCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string GoldDragonCompanionChoiceDisplayNameKey = "GoldDragonCompanionChoiceName";
        private static readonly string GoldDragonCompanionChoiceDescription = "";
        private static readonly string GoldDragonCompanionChoiceDescriptionKey = "GoldDragonCompanionChoiceDescription";

        private static readonly string GoldDragonProgression = "a6fbca43902c6194c947546e89af64bd";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchGoldDragonCompanionChoice();
                //try { PatchGoldDragonCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchGoldDragonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Gold Dragon Companion Choices");

                var _goldDragonCompanionChoice = FeatureSelectionConfigurator.New(GoldDragonCompanionChoiceName, GoldDragonCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(GoldDragonCompanionChoiceDisplayNameKey, GoldDragonCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(GoldDragonCompanionChoiceDescriptionKey, GoldDragonCompanionChoiceDescription))
                    //.PrerequisitePlayerHasFeature(GoldDragonProgression)
                    //.SetHideInUi(true)
                    .Configure();
                //_goldDragonCompanionChoice.m_AllFeatures = GoldDragonUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Gold Dragon Companion Choices -> " + _goldDragonCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
