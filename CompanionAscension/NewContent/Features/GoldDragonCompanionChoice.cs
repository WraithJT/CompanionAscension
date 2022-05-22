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
        internal static readonly string Guid = "d43a226ead4b41759dfd6f1669e5b76a";
        private static readonly string Name = "GoldDragonCompanionChoice";
        private static readonly string DisplayName = "Gold Dragon Companion Ascension";
        private static readonly string DisplayNameKey = "GoldDragonCompanionChoiceName";
        private static readonly string Description = "";
        private static readonly string DescriptionKey = "GoldDragonCompanionChoiceDescription";

        private static readonly string GoldDragonProgression = "a6fbca43902c6194c947546e89af64bd";
        static BlueprintFeatureSelection DragonLevel2FeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("a21acdafc0169f5488a9bd3256e2e65b");

        static readonly BlueprintFeatureSelection BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");

        private static readonly BlueprintProgression BloodlineDraconicGoldProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("6c67ef823db8d7d45bb0ef82f959743d");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchGoldDragonCompanionChoice();
                //try { PatchGoldDragonCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchGoldDragonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Gold Dragon Companion Choices");

                // option 1 - Gold Dragon Prowess
                // 
                // bonus to highest ability score: +4, +6 at MR9
                // increase scores to 12 - scale to 14 at MR9?
                //
                //
                // option 2 - Gold Dragon Defenses
                // wings
                // increase saves to 12 - scale to 14 at MR9?
                // immunities
                //
                //
                // dragon feat at MR9



                string _goldDragonProwessName = "GoldDragonProwess";
                string _goldDragonProwessGUID = "d921137953ed4278a4d03bdd43a0960b";
                string _goldDragonProwessDisplayName = "Gold Dragon Prowess";
                string _goldDragonProwessDisplayNameKey = "GoldDragonProwessNameKey";
                string _goldDragonProwessDescription = "";
                string _goldDragonProwessDescriptionKey = "GoldDragonProwessDescriptionKey";
                var _goldDragonProwess = FeatureConfigurator.New(_goldDragonProwessName, _goldDragonProwessGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_goldDragonProwessDisplayNameKey, _goldDragonProwessDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_goldDragonProwessDescriptionKey, _goldDragonProwessDescription))
                    .Configure();

                string _goldDragonDefensesName = "GoldDragonDefenses";
                string _goldDragonDefensesGUID = "81d28dbec3c64e7e8be61da6ef0c78c1";
                string _goldDragonDefensesDisplayName = "Gold Dragon Defenses";
                string _goldDragonDefensesDisplayNameKey = "GoldDragonDefensesNameKey";
                string _goldDragonDefensesDescription = "";
                string _goldDragonDefensesDescriptionKey = "GoldDragonDefensesDescriptionKey";
                var _goldDragonDefenses = FeatureConfigurator.New(_goldDragonDefensesName, _goldDragonDefensesGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_goldDragonDefensesDisplayNameKey, _goldDragonDefensesDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_goldDragonDefensesDescriptionKey, _goldDragonDefensesDescription))
                    .Configure();

                string _goldDragonCompanionFeatName = "GoldDragonCompanionFeat";
                string _goldDragonCompanionFeatGUID = "4d13d31796a1490db39eb252b53dd87d";
                string _goldDragonCompanionFeatDisplayName = "Gold Dragon Companion Feat";
                string _goldDragonCompanionFeatDisplayNameKey = "GoldDragonCompanionFeatNameKey";
                string _goldDragonCompanionFeatDescription = "You can select any feat, ignoring its prerequisites.";
                string _goldDragonCompanionFeatDescriptionKey = "GoldDragonCompanionFeatDescriptionKey";
                var _goldDragonCompanionFeat = FeatureSelectionConfigurator.New(_goldDragonCompanionFeatName, _goldDragonCompanionFeatGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_goldDragonCompanionFeatDisplayNameKey, _goldDragonCompanionFeatDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_goldDragonCompanionFeatDescriptionKey, _goldDragonCompanionFeatDescription))
                    .SetIgnorePrerequisites(true)
                    //.PrerequisiteFeature(_goldDragonProwess.AssetGuidThreadSafe)
                    .SetHideInUi(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailableInUI(true)
                    .Configure();
                _goldDragonCompanionFeat.m_AllFeatures = BasicFeatSelection.m_AllFeatures;

                var _goldDragonCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetIcon(BloodlineDraconicGoldProgression.Icon)
                    .AddToFeatures(new string[] { _goldDragonProwess.AssetGuidThreadSafe, _goldDragonDefenses.AssetGuidThreadSafe })
                    .AddToFeatures(new string[] { _goldDragonCompanionFeat.AssetGuidThreadSafe })
                    //.PrerequisitePlayerHasFeature(GoldDragonProgression)
                    .SetHideInUi(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailableInUI(true)
                    .Configure();
                Tools.LogMessage("Built: Gold Dragon Companion Choices -> " + _goldDragonCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
