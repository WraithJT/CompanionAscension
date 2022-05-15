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
    class MythicMindAndBody
    {
        public static readonly string MythicMindAndBodyGUID = "628730c77f664bb5954a50f0cf5acaf7";
        private static readonly string MythicMindAndBodyName = "MythicMindAndBody";
        private static readonly string MythicMindAndBodyDisplayName = "Mythic Mind and Body";
        private static readonly string MythicMindAndBodyDisplayNameKey = "MythicMindAndBodyName";
        private static readonly string MythicMindAndBodyDescription =
            "Increases your highest ability score by an amount equal to 1 plus half your mythic level. " +
            "\nIncreases your lowest saving throw by an amount equal to your mythic level.";
        private static readonly string MythicMindAndBodyDescriptionKey = "MythicMindAndBodyDescription";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchMythicMindAndBody();
                //try { PatchMythicMindAndBody(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchMythicMindAndBody()
            {
                var _mythicMindAndBody = FeatureConfigurator.New(MythicMindAndBodyName, MythicMindAndBodyGUID)
                    .SetDisplayName(LocalizationTool.CreateString(MythicMindAndBodyDisplayNameKey, MythicMindAndBodyDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(MythicMindAndBodyDescriptionKey, MythicMindAndBodyDescription))
                    .AddFacts(new string[] { MythicSavingThrowBonus.MythicSavingThrowBonusGUID, MythicAbilityScoreBonus.MythicAbilityScoreBonusGUID })
                    .SetReapplyOnLevelUp(true)
                    .Configure();
                //_MythicMindAndBody.AddComponents(new BlueprintComponent[] {
                //    _highestPhysicalMentalScoreBonus,
                //    _highestPhysicalMentalScoreBonusContextRankConfig,
                //    });
                Tools.LogMessage("Built: Mythic Mind and Body -> " + _mythicMindAndBody.AssetGuidThreadSafe);
            }
        }
    }
}
