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
    class AngelCompanionChoice
    {
        public static readonly string Guid = "29ca6c2414f84577a8ad8c9c7e0742fd";
        private static readonly string ShortName = "AngelCompanionChoice";
        private static readonly string DisplayName = "Angel Companion Ascension";
        private static readonly string DisplayNameKey = "AngelCompanionChoiceName";
        private static readonly string Description = "";
        private static readonly string DescriptionKey = "AngelCompanionChoiceDescription";

        private static readonly string AngelProgression = "2f6fe889e91b6a645b055696c01e2f74";

        private static readonly string AngelSwordFeatureGUID = "7a6080461eaa278428fe3f12df75c8d0";
        //private static readonly BlueprintFeature AngelSwordFeature = ResourcesLibrary.TryGetBlueprint<BlueprintFeature>(AngelSwordFeatureGUID);

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchAngelCompanionChoice();
                //try { PatchAngelCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchAngelCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Angel Companion Choices");

                var _angelCompanionChoice = FeatureSelectionConfigurator.New(ShortName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddToFeatures(AngelSwordFeatureGUID)
                    .PrerequisitePlayerHasFeature(AngelProgression)
                    .SetHideInUi(true)
                    .Configure();
                //_angelCompanionChoice.m_AllFeatures = LichUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Angel Companion Choices -> " + _angelCompanionChoice.AssetGuidThreadSafe);
            }

            private static void ConfigurePrereqs()
            {
                var _mythicCompanionClassReference = Mythics.CompanionAscension.MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();

                var _angelSwordResource = ResourcesLibrary.TryGetBlueprint<BlueprintAbilityResource>("5578b13626344e6409c56bb024ec9529");
                _angelSwordResource.m_MaxAmount.m_ClassDiv = _angelSwordResource.m_MaxAmount.m_ClassDiv.AppendToArray(_mythicCompanionClassReference);
            }
        }
    }
}