//using BlueprintCore.Blueprints.Configurators.Classes;
//using BlueprintCore.Blueprints.Configurators.Classes.Selection;
//using BlueprintCore.Utils;
//using HarmonyLib;
//using Kingmaker.Blueprints.Classes;
//using Kingmaker.Blueprints.Classes.Selection;
//using Kingmaker.Blueprints.JsonSystem;
//using Kingmaker.Blueprints;
//using Kingmaker.UnitLogic.Mechanics.Properties;
//using Kingmaker.Blueprints.Classes.Prerequisites;
//using System;
//using CompanionAscension.Utilities;
//using BlueprintCore.Blueprints.Configurators.UnitLogic;
//using BlueprintCore.Blueprints.Configurators.UnitLogic.Customization;
//using BlueprintCore.Blueprints.Configurators.UnitLogic.Properties;
//using BlueprintCore.Blueprints.Configurators.EntitySystem;
//using Kingmaker.EntitySystem.Stats;
//using Kingmaker.Enums;
//using Kingmaker.Utility;
//using System.Linq;
//using Kingmaker.EntitySystem;
//using Kingmaker.UnitLogic;
//using Kingmaker.UnitLogic.Buffs.Blueprints;
//using Kingmaker.UnitLogic.FactLogic;
//using Kingmaker.UnitLogic.Mechanics.Components;
//using BlueprintCore.Conditions.Builder;
//using Kingmaker.Designers.Mechanics.Facts;
//using Kingmaker.UnitLogic.Abilities.Blueprints;
//using CompanionAscension.Utilities.TTTCore;
//using System.Text.RegularExpressions;
//using CompanionAscension.NewContent.Components;
//using Kingmaker.UnitLogic.Mechanics;
//using Kingmaker.UnitLogic.Abilities;

//namespace CompanionAscension.NewContent.Features
//{
//    class SwarmCompanionChoice
//    {
//        public static readonly string SwarmCompanionChoiceGUID = "4387b5bc3f424b2fa9575d4620d9489c";
//        private static readonly string SwarmCompanionChoiceName = "SwarmCompanionChoice";
//        private static readonly string SwarmCompanionChoiceDisplayName = "Second Companion Ascension";
//        private static readonly string SwarmCompanionChoiceDisplayNameKey = "SwarmCompanionChoiceName";
//        private static readonly string SwarmCompanionChoiceDescription = "";
//        private static readonly string SwarmCompanionChoiceDescriptionKey = "SwarmCompanionChoiceDescription";

//        private static readonly string SwarmThatWalksProgression = "bf5f103ccdf69254abbad84fd371d5c9";

//        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
//        static class BlueprintsCache_Init_patch
//        {
//            static bool Initialized;

//            [HarmonyPriority(Priority.First)]
//            static void Postfix()
//            {
//                if (Initialized) return;
//                Initialized = true;

//                //PatchSwarmCompanionChoice();
//                //try { PatchSwarmCompanionChoice(); }
//                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
//            }

//            public static void PatchSwarmCompanionChoice()
//            {
//                Tools.LogMessage("New Content: Building Swarm Companion Choices");

//                var _swarmCompanionChoice = FeatureSelectionConfigurator.New(SwarmCompanionChoiceName, SwarmCompanionChoiceGUID)
//                    .SetDisplayName(LocalizationTool.CreateString(SwarmCompanionChoiceDisplayNameKey, SwarmCompanionChoiceDisplayName, false))
//                    .SetDescription(LocalizationTool.CreateString(SwarmCompanionChoiceDescriptionKey, SwarmCompanionChoiceDescription))
//                    //.PrerequisitePlayerHasFeature(SwarmProgression)
//                    .SetHideInUI(true)
//                    .SetHideInCharacterSheetAndLevelUp(true)
//                    .SetHideNotAvailibleInUI(true)
//                    .Configure();
//                //_swarmCompanionChoice.m_AllFeatures = SwarmUniqueAbilitiesSelection.m_AllFeatures;
//                Tools.LogMessage("Built: Swarm Companion Choices -> " + _swarmCompanionChoice.AssetGuidThreadSafe);
//            }
//        }
//    }
//}
