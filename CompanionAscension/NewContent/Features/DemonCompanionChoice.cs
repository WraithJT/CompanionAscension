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
    class DemonCompanionChoice
    {
        public static readonly string DemonCompanionChoiceGUID = "57e6f40e11ec421c9b2e3edb34e2beb2";
        //private static readonly BlueprintFeatureSelection AzataSuperpowersSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("8a30e92cd04ff5b459ba7cb03584fda0");
        private static readonly string DemonCompanionChoiceName = "DemonCompanionChoice";
        private static readonly string DemonCompanionChoiceDisplayName = "Second Companion Ascension";
        private static readonly string DemonCompanionChoiceDisplayNameKey = "DemonCompanionChoiceName";
        private static readonly string DemonCompanionChoiceDescription = "";
        private static readonly string DemonCompanionChoiceDescriptionKey = "DemonCompanionChoiceDescription";

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

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]

        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchDemonCompanionChoice();
                //try { PatchDemonCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchDemonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Demon Companion Choices");

                var _demonCompanionChoice = FeatureSelectionConfigurator.New(DemonCompanionChoiceName, DemonCompanionChoiceGUID)
                    .SetDisplayName(LocalizationTool.CreateString(DemonCompanionChoiceDisplayNameKey, DemonCompanionChoiceDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DemonCompanionChoiceDescriptionKey, DemonCompanionChoiceDescription))
                    //.PrerequisitePlayerHasFeature(DemonProgression)
                    //.SetHideInUi(true)
                    .Configure();
                //_demonCompanionChoice.m_AllFeatures = DemonUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Demon Companion Choices -> " + _demonCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
