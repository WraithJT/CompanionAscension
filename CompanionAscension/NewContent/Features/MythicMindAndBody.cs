using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Utils;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints.JsonSystem;
using System;

namespace CompanionAscension.NewContent.Features
{
    class MythicMindAndBody
    {
        public static readonly string Guid = "628730c77f664bb5954a50f0cf5acaf7";
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

                //PatchMythicMindAndBody();
                try { PatchMythicMindAndBody(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchMythicMindAndBody()
            {
                var _mythicMindAndBody = FeatureConfigurator.New(MythicMindAndBodyName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(MythicMindAndBodyDisplayNameKey, MythicMindAndBodyDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(MythicMindAndBodyDescriptionKey, MythicMindAndBodyDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_MythicMindAndBody.png"))
                    .AddFacts(new() { MythicSavingThrowBonus.MythicSavingThrowBonusGUID, MythicAbilityScoreBonus.MythicAbilityScoreBonusGUID })
                    .SetReapplyOnLevelUp(true)
                    .Configure();
                Tools.LogMessage("Built: Mythic Mind and Body -> " + _mythicMindAndBody.AssetGuidThreadSafe);
            }
        }
    }
}
