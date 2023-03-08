using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
using BlueprintCore.Blueprints.CustomConfigurators.Classes;
using BlueprintCore.Blueprints.CustomConfigurators.Classes.Selection;
using BlueprintCore.Utils;
using CompanionAscension.NewContent.Components;
using CompanionAscension.Utilities;
using CompanionAscension.Utilities.TTTCore;
using HarmonyLib;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Classes;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.Enums;
using Kingmaker.UnitLogic;
using System;

namespace CompanionAscension.NewContent.Features
{
    static class AngelCompanionChoice
    {
        public static readonly string Guid = "29ca6c2414f84577a8ad8c9c7e0742fd";
        private static readonly string ShortName = "AngelCompanionChoice";
        private static readonly string DisplayName = "Angel Companion Ascension";
        private static readonly string DisplayNameKey = "AngelCompanionChoiceName";
        private static readonly string Description = "At 8th mythic rank, Angel's companions can gain further power.";
        private static readonly string DescriptionKey = "AngelCompanionChoiceDescription";

        private static readonly string AngelProgression = "2f6fe889e91b6a645b055696c01e2f74";
        private static readonly string AngelSwordFeatureGUID = "7a6080461eaa278428fe3f12df75c8d0";
        public static readonly BlueprintCharacterClass MythicCompanionClass = ResourcesLibrary.TryGetBlueprint<BlueprintCharacterClass>("530b6a79cb691c24ba99e1577b4beb6d");

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                //PatchAngelCompanionChoice();
                try { PatchAngelCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
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
                //    .AddToAllFeatures()
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

                var _angelCompanionAngelTypeBonus = FeatureConfigurator.New("AngelProtections", "c75c84aaf1784c6996ad65e2caaa6a0c")
                    .SetDisplayName(LocalizationTool.CreateString("AngelProtectionsKey", "Angel Protections", false))
                    .SetDescription(LocalizationTool.CreateString("AngelProtectionsDescriptionKey",
                        "You gain a +2 deflection bonus to AC and +2 resistance " +
                        "bonus on saving throws against the attacks and spells from Evil creatures."))
                    .AddSavingThrowBonusAgainstAlignment(
                        alignment: AlignmentComponent.Evil,
                        descriptor: ModifierDescriptor.Resistance,
                        bonus: 2)
                    .AddArmorClassBonusAgainstAlignment(
                        alignment: AlignmentComponent.Evil,
                        descriptor: ModifierDescriptor.Deflection,
                        bonus: 2)
                    .Configure();

                string AcidImmunityGUID = "c994f1a0dfce1c54f94420588da61617";
                string ImmunityToPetrificationGUID = "b625283fc6eb72c47a2fc5e2a3ff9eb4";
                string ColdImmunityGUID = "9ae23798a9284e044ad2716a772a410e";
                string FireResistance10GUID = "24700a71dd3dc844ea585345f6dd18f6";
                string ElectricityResistance10GUID = "a5049e0d1b1a1454aa1a221a6e20b714";
                string FeatureWingsAngelGUID = "d9bd0fde6deb2e44a93268f2dfb3e169";

                string[] _angelCompanionAngelFacts = new string[]
                {
                    AcidImmunityGUID,
                    ImmunityToPetrificationGUID,
                    ColdImmunityGUID,
                    FireResistance10GUID,
                    ElectricityResistance10GUID,
                    FeatureWingsAngelGUID,
                    _angelCompanionAngelTypeBonus.AssetGuidThreadSafe
                };

                string _angelCompanionAngelTypeGUID = "ea5c6dba3dab4d87b42578894b31151a";
                string _angelCompanionAngelTypeName = "AngelCompanion";
                string _angelCompanionAngelTypeDisplayName = "Angelic Companion";
                string _angelCompanionAngelTypeDisplayNameKey = "AngelCompanionNameKey";
                string _angelCompanionAngelTypeDescription =
                    "The Angel's companion gains some of the traits of an Angel creature. " +
                    "\nResistances and immunities. Angel companions gain resist fire 10, resist " +
                    "electricity 10, and immunity to acid and cold effects, as well as petrification." +
                    "\nWings. They gain a pair of wings that grant a +3 dodge bonus to AC against " +
                    "melee attacks and an immunity to ground based effects, such as difficult terrain." +
                    "\nProtections. They gain a further +2 deflection bonus to AC and +2 resistance " +
                    "bonus on saving throws against the attacks and spells from Evil creatures.";
                string _angelCompanionAngelTypeDescriptionKey = "AngelTypeCompanionDescriptionKey";
                var _angelCompanionAngelType = FeatureConfigurator.New(_angelCompanionAngelTypeName, _angelCompanionAngelTypeGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_angelCompanionAngelTypeDisplayNameKey, _angelCompanionAngelTypeDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_angelCompanionAngelTypeDescriptionKey, _angelCompanionAngelTypeDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AngelicCompanion.png"))
                    .AddFacts(new()
                    {
                        AcidImmunityGUID,
                        ImmunityToPetrificationGUID,
                        ColdImmunityGUID,
                        FireResistance10GUID,
                        ElectricityResistance10GUID,
                        FeatureWingsAngelGUID,
                        _angelCompanionAngelTypeBonus.AssetGuidThreadSafe
                    })
                    .Configure();

                var _angelCompanionChoice = FeatureSelectionConfigurator.New(ShortName, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] { AngelSwordFeatureGUID, _angelCompanionAngelTypeGUID })
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_AngelCompanionChoice.png"))
                    .AddPrerequisitePlayerHasFeature(AngelProgression)
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
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
    }
}