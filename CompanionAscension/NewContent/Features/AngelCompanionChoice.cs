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
using Kingmaker.Blueprints.Classes.Spells;

namespace CompanionAscension.NewContent.Features
{
    static class AngelCompanionChoice
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
        private static readonly string AngelIncorporateSpellbookGUID = "e1fbb0e0e610a3a4d91e5e5284587939";
        private static readonly BlueprintFeatureSelectMythicSpellbook AngelIncorporateSpellbook = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelectMythicSpellbook>(AngelIncorporateSpellbookGUID);
        private static readonly BlueprintProgression MythicCompanionProgression = ResourcesLibrary.TryGetBlueprint<BlueprintProgression>("21e74c19da02acb478e32da25abd9d28");
        public static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");

        private static readonly BlueprintSpellbook AccursedWitchSpellbook = ResourcesLibrary.TryGetBlueprint<BlueprintSpellbook>("b897fe0947e4b804082b1a687c21e6e2");

        private static readonly string Spellbooks;

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

                // method to create AddSpellbooks and progressions

                //var _mythicCompanionClassReference = MythicCompanionClass.ToReference<BlueprintCharacterClassReference>();
                //BlueprintProgression.ClassWithLevel _classWithLevel = new();
                //_classWithLevel.m_Class = _mythicCompanionClassReference;
                //_classWithLevel.AdditionalLevel = 0;

                //string _addSpellbookLevelName = "AddCompanionSpellbookLevel";
                //string _addSpellbookLevelGUID = "";
                //string _addSpellbookLevelDisplayName = "AngelCompanionSpellbookSelection";
                //string _addSpellbookLevelnDisplayNameKey = "AngelCompanionSpellbookSelectionNameKey";
                //string _addSpellbookLevelDescription = "do something";
                //string _addSpellbookLevelDescriptionKey = "AngelCompanionSpellbookSelectionDescriptionKey";
                //var _addSpellbookLevel = FeatureConfigurator.New(_addSpellbookLevelName, _addSpellbookLevelGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(_addSpellbookLevelnDisplayNameKey, _addSpellbookLevelDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(_addSpellbookLevelDescriptionKey, _addSpellbookLevelDescription))
                //    .AddSpellbookLevel()
                //    .Configure();

                //string _accursedWitchProgressionName = "AngelCompanionSpellbookSelection";
                //string _accursedWitchProgressionGUID = "";
                //string _accursedWitchProgressionDisplayName = "AngelCompanionSpellbookSelection";
                //string _accursedWitchProgressionnDisplayNameKey = "AngelCompanionSpellbookSelectionNameKey";
                //string _accursedWitchProgressionDescription = "do something";
                //string _accursedWitchProgressionDescriptionKey = "AngelCompanionSpellbookSelectionDescriptionKey";
                //var _accursedWitchProgression = ProgressionConfigurator.New(_accursedWitchProgressionName, _accursedWitchProgressionGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(_accursedWitchProgressionnDisplayNameKey, _accursedWitchProgressionDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(_accursedWitchProgressionDescriptionKey, _accursedWitchProgressionDescription))
                //    .SetGiveFeaturesForPreviousLevels(true)
                //    .Configure();
                //_accursedWitchProgression.m_Classes = new BlueprintProgression.ClassWithLevel[] { _classWithLevel };
                //_accursedWitchProgression.LevelEntries.TemporaryContext(le =>
                //{
                //    le.Where(e => e.Level == 4)
                //        .ForEach(e =>
                //        {
                //            e.m_Features.Add(_addSpellbookLevel.ToReference<BlueprintFeatureBaseReference>());

                //        });
                //    le.Where(e => e.Level == 8)
                //        .ForEach(e =>
                //        {
                //            e.m_Features.Add(_addSpellbookLevel.ToReference<BlueprintFeatureBaseReference>());
                //        });
                //});

                //string _angelCompanionSpellbookSelectionName = "AngelCompanionSpellbookSelection";
                //string _angelCompanionSpellbookSelectionGUID = "E6FA4C32-25B1-4469-A6AB-CA40840731A3";
                //string _angelCompanionSpellbookSelectionDisplayName = "AngelCompanionSpellbookSelection";
                //string _angelCompanionSpellbookSelectionDisplayNameKey = "AngelCompanionSpellbookSelectionNameKey";
                //string _angelCompanionSpellbookSelectionDescription = "do something";
                //string _angelCompanionSpellbookSelectionDescriptionKey = "AngelCompanionSpellbookSelectionDescriptionKey";
                //var _angelCompanionSpellbookSelection = FeatureSelectionConfigurator.New(_angelCompanionSpellbookSelectionName, _angelCompanionSpellbookSelectionGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(_angelCompanionSpellbookSelectionDisplayNameKey, _angelCompanionSpellbookSelectionDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(_angelCompanionSpellbookSelectionDescriptionKey, _angelCompanionSpellbookSelectionDescription))
                //    .AddToFeatures()
                //    .Configure();

                //string _angelCompanionSpellbookName = "AngelCompanionSpellbook";
                //string _angelCompanionSpellbookGUID = "b2b164692b3743259d9d578c98e5bf73";
                //string _angelCompanionSpellbookDisplayName = "AngelCompanionSpellbook";
                //string _angelCompanionSpellbookDisplayNameKey = "AngelCompanionSpellbookNameKey";
                //string _angelCompanionSpellbookDescription = "do something";
                //string _angelCompanionSpellbookDescriptionKey = "AngelCompanionSpellbookDescriptionKey";
                //var _angelCompanionSpellbook = FeatureConfigurator.New(_angelCompanionSpellbookName, _angelCompanionSpellbookGUID)
                //    .SetDisplayName(LocalizationTool.CreateString(_angelCompanionSpellbookDisplayNameKey, _angelCompanionSpellbookDisplayName, false))
                //    .SetDescription(LocalizationTool.CreateString(_angelCompanionSpellbookDescriptionKey, _angelCompanionSpellbookDescription))
                //    .AddSpellbookLevel()
                //    .Configure();
                //_angelCompanionSpellbook.AddSelectionCallback(AngelIncorporateSpellbook, MythicCompanionProgression);
                
                // book selection shows up but can't click anything

                var _angelCompanionChoice = FeatureSelectionConfigurator.New(ShortName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddToFeatures(AngelSwordFeatureGUID)
                    //.AddToFeatures(_angelCompanionSpellbookGUID)
                    //.PrerequisitePlayerHasFeature(AngelProgression)
                    .SetHideInUi(true)
                    .Configure();
                //_angelCompanionChoice.m_AllFeatures = LichUniqueAbilitiesSelection.m_AllFeatures;
                Tools.LogMessage("Built: Angel Companion Choices -> " + _angelCompanionChoice.AssetGuidThreadSafe);
            }
        }
        private static void AddSelectionCallback(this BlueprintFeature Feature, BlueprintFeatureSelectMythicSpellbook Selection, BlueprintProgression Progression)
        {
            Feature.AddComponent(Helpers.Create<AddAdditionalMythicFeatures>(c =>
            {
                c.Features = new BlueprintFeatureBaseReference[] { Selection.ToReference<BlueprintFeatureBaseReference>() };
                c.Source = FeatureSource.GetMythicSource(Progression);
            }));
        }

        private static void BuildProgression()
        {

        }
    }
}