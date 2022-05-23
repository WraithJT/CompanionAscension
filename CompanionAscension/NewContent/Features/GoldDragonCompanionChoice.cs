using BlueprintCore.Blueprints.Configurators.Classes;
using BlueprintCore.Blueprints.Configurators.Classes.Selection;
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
using Kingmaker.Blueprints.Items.Ecnchantments;
using Kingmaker.Blueprints.Classes.Spells;
using Kingmaker.Enums.Damage;

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

        public static readonly string GoldDragonCompanionFeatGUID = "4d13d31796a1490db39eb252b53dd87d";

        private static readonly BlueprintFeatureSelection BasicFeatSelection = ResourcesLibrary.TryGetBlueprint<BlueprintFeatureSelection>("247a4068296e8be42890143f451b4b45");
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

                //PatchGoldDragonCompanionChoice();
                try { PatchGoldDragonCompanionChoice(); }
                catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchGoldDragonCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Gold Dragon Companion Choices");

                string DragonLevel1StrengthOverride = "b7524715218c4c14ea4f75881797fc6c";
                string DragonLevel1DexterityOverride = "fc2120ccd6a528c46a48e3b8c93c2685";
                string DragonLevel1ConstitutionOverride = "65a0a742502de6b49b57cb719680cce5";
                string DragonLevel1CharismaOverride = "21e3e9903481aec4b8eee1e9af17ce92";
                string DragonLevel1IntelligenceOverride = "6f86213d9732eec4abfb94561e2acc09";
                string DragonLevel1WisdomOverride = "8aebc472d039bc944b3a88c729f0e013";

                HighestAbilityScoreBonus _goldDragonHighestAbilityScoreBonus = new()
                {
                    Descriptor = ModifierDescriptor.Mythic,
                    name = "$HighestAbilityScoreBonus$c48be3da280f452cb366c65e00701beb",
                    HighestStatBonus = 4
                };
                BuffEnchantAnyWeapon _goldDragonEnchantPrimary = new()
                {
                    m_EnchantmentBlueprint = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>("bdba267e951851449af552aa9f9e3992").ToReference<BlueprintItemEnchantmentReference>(),
                    Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand
                };
                BuffEnchantAnyWeapon _goldDragonEnchantSecondary = new()
                {
                    m_EnchantmentBlueprint = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>("bdba267e951851449af552aa9f9e3992").ToReference<BlueprintItemEnchantmentReference>(),
                    Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.SecondaryHand
                };
                string _goldDragonProwessName = "GoldDragonProwess";
                string _goldDragonProwessGUID = "d921137953ed4278a4d03bdd43a0960b";
                string _goldDragonProwessDisplayName = "Gold Dragon Prowess";
                string _goldDragonProwessDisplayNameKey = "GoldDragonProwessNameKey";
                string _goldDragonProwessDescription =
                    "If any of your attributes before modifiers is less than 14, you gain a bonus up to that number. " +
                    "\nYour highest ability score gets a +4 bonus." +
                    "\nAll your weapons count as +5 weapons." +
                    "\nAt the next mythic level, you can select any feat, ignoring its prerequisites.";
                string _goldDragonProwessDescriptionKey = "GoldDragonProwessDescriptionKey";
                var _goldDragonProwess = FeatureConfigurator.New(_goldDragonProwessName, _goldDragonProwessGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_goldDragonProwessDisplayNameKey, _goldDragonProwessDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_goldDragonProwessDescriptionKey, _goldDragonProwessDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_GoldDragonProwess.png"))
                    .AddComponent(_goldDragonHighestAbilityScoreBonus)
                    .AddComponent(_goldDragonEnchantPrimary)
                    .AddComponent(_goldDragonEnchantSecondary)
                    .AddFacts(new()
                    {
                        DragonLevel1StrengthOverride,
                        DragonLevel1DexterityOverride,
                        DragonLevel1ConstitutionOverride,
                        DragonLevel1CharismaOverride,
                        DragonLevel1IntelligenceOverride,
                        DragonLevel1WisdomOverride
                    })
                    .AddRecalculateOnStatChange(StatType.Strength)
                    .AddRecalculateOnStatChange(StatType.Dexterity)
                    .AddRecalculateOnStatChange(StatType.Constitution)
                    .AddRecalculateOnStatChange(StatType.Intelligence)
                    .AddRecalculateOnStatChange(StatType.Wisdom)
                    .AddRecalculateOnStatChange(StatType.Charisma)
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                BuffDescriptorImmunity _goldDragonBuffDescriptorImmunity = new()
                {
                    name = "$BuffDescriptorImmunity$06fe408f83a94197b1d25955ca126cb0",
                    Descriptor = SpellDescriptor.Poison
                        | SpellDescriptor.Disease
                        | SpellDescriptor.Fear
                        | SpellDescriptor.Confusion
                        | SpellDescriptor.Paralysis
                        | SpellDescriptor.Sleep,
                    CheckFact = false
                };
                SpellImmunityToSpellDescriptor _goldDragonSpellImmunityToSpellDescriptor = new()
                {
                    name = "$SpellImmunityToSpellDescriptor$25491a550f8b4142a1c6c23f546cc6ac",
                    Descriptor = SpellDescriptor.Poison
                        | SpellDescriptor.Disease
                        | SpellDescriptor.Fear
                        | SpellDescriptor.Confusion
                        | SpellDescriptor.Paralysis
                        | SpellDescriptor.Sleep
                };
                ContextRankConfig _goldDragonCharacterLevelContextRankConfig = new()
                {
                    name = "$ContextRankConfig$8cebaa2f4bbf45a3ba318665b26bd402",
                    m_BaseValueType = ContextRankBaseValueType.CharacterLevel,
                    m_Progression = ContextRankProgression.Div2,
                    m_Max = 10,
                    m_Stat = StatType.Unknown,
                    m_Type = AbilityRankType.Default
                };
                ContextRankConfig _goldDragonMythicLevelContextRankConfig = new()
                {
                    name = "$ContextRankConfig$727625fad2c04503a3f7754c4d6e20b1",
                    m_BaseValueType = ContextRankBaseValueType.MythicLevel,
                    m_Progression = ContextRankProgression.Div2,
                    m_Max = 5,
                    m_Stat = StatType.Unknown,
                    m_Type = AbilityRankType.DamageDice
                };
                ContextCalculateSharedValue _goldDragonContextCalculateSharedValue = new()
                {
                    name = "$ContextCalculateSharedValue$8dd1649e676240839d6546a1ed72bedf",
                    ValueType = AbilitySharedValue.Damage,
                    Value = new ContextDiceValue()
                    {
                        DiceType = Kingmaker.RuleSystem.DiceType.One,
                        BonusValue = new ContextValue()
                        {
                            Value = 0,
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.DamageDice,
                            ValueShared = AbilitySharedValue.Damage
                        },
                        DiceCountValue = new ContextValue()
                        {
                            Value = 0,
                            ValueType = ContextValueType.Rank,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage
                        }
                    }
                };
                string FeatureWingsDraconicGold = "6929bac6c67ae194c8c8446e3d593953";
                string DragonLevel1SavingThrowsOverrideFortitude = "a54cb7d8d86e6cc4099fc1c8c13bdecc";
                string DragonLevel1SavingThrowsOverrideReflex = "379db58af291030438d2647975e8c16f";
                string DragonLevel1SavingThrowsOverrideWill = "9a160f24043d9534897f08350f938b99";
                string _goldDragonDefensesName = "GoldDragonDefenses";
                string _goldDragonDefensesGUID = "81d28dbec3c64e7e8be61da6ef0c78c1";
                string _goldDragonDefensesDisplayName = "Gold Dragon Defenses";
                string _goldDragonDefensesDisplayNameKey = "GoldDragonDefensesNameKey";
                string _goldDragonDefensesDescription =
                    "You gain immunity to poison, disease, fear, confusion, paralysis and sleep. " +
                    "You also gain resistance to all energies equal to (half your character level plus half your mythic rank)." +
                    "\nIf any of your saving throws bonuses before modifiers is less than (5 + your mythic rank), you gain a bonus up to that number." +
                    "\nYou gain wings that grant a +3 dodge bonus to AC against melee attacks and an immunity to ground based effects, such as difficult terrain." +
                    "\nAt the next mythic level, you can select any feat, ignoring its prerequisites.";
                string _goldDragonDefensesDescriptionKey = "GoldDragonDefensesDescriptionKey";
                var _goldDragonDefenses = FeatureConfigurator.New(_goldDragonDefensesName, _goldDragonDefensesGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_goldDragonDefensesDisplayNameKey, _goldDragonDefensesDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_goldDragonDefensesDescriptionKey, _goldDragonDefensesDescription))
                    .SetIcon(AssetLoader.LoadInternal(Main.ModContext_CA, folder: "Abilities", file: "Icon_GoldDragonDefenses.png"))
                    .AddComponent(_goldDragonBuffDescriptorImmunity)
                    .AddComponent(_goldDragonSpellImmunityToSpellDescriptor)
                    .AddComponent(_goldDragonCharacterLevelContextRankConfig)
                    .AddComponent(_goldDragonMythicLevelContextRankConfig)
                    .AddComponent(_goldDragonContextCalculateSharedValue)
                    .AddDamageResistanceEnergy(
                        type: DamageEnergyType.Acid,
                        value: new ContextValue()
                        {
                            Value = 5,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            ValueType = ContextValueType.Shared
                        })
                     .AddDamageResistanceEnergy(
                        type: DamageEnergyType.Cold,
                        value: new ContextValue()
                        {
                            Value = 5,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            ValueType = ContextValueType.Shared
                        })
                     .AddDamageResistanceEnergy(
                        type: DamageEnergyType.Electricity,
                        value: new ContextValue()
                        {
                            Value = 5,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            ValueType = ContextValueType.Shared
                        })
                    .AddDamageResistanceEnergy(
                        type: DamageEnergyType.Fire,
                        value: new ContextValue()
                        {
                            Value = 5,
                            ValueRank = AbilityRankType.Default,
                            ValueShared = AbilitySharedValue.Damage,
                            ValueType = ContextValueType.Shared
                        })
                    .AddFacts(new()
                    {
                        FeatureWingsDraconicGold,
                        DragonLevel1SavingThrowsOverrideFortitude,
                        DragonLevel1SavingThrowsOverrideReflex,
                        DragonLevel1SavingThrowsOverrideWill
                    })
                    .SetReapplyOnLevelUp(true)
                    .Configure();

                string _goldDragonCompanionFeatName = "GoldDragonCompanionFeat";
                
                string _goldDragonCompanionFeatDisplayName = "Gold Dragon Companion Feat";
                string _goldDragonCompanionFeatDisplayNameKey = "GoldDragonCompanionFeatNameKey";
                string _goldDragonCompanionFeatDescription = "You can select any feat, ignoring its prerequisites.";
                string _goldDragonCompanionFeatDescriptionKey = "GoldDragonCompanionFeatDescriptionKey";
                var _goldDragonCompanionFeat = FeatureSelectionConfigurator.New(_goldDragonCompanionFeatName, GoldDragonCompanionFeatGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_goldDragonCompanionFeatDisplayNameKey, _goldDragonCompanionFeatDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_goldDragonCompanionFeatDescriptionKey, _goldDragonCompanionFeatDescription))
                    .SetIgnorePrerequisites(true)
                    .AddPrerequisiteFeature(_goldDragonProwess, group: Prerequisite.GroupType.Any, checkInProgression: true)
                    .AddPrerequisiteFeature(_goldDragonDefenses, group: Prerequisite.GroupType.Any, checkInProgression: true)
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
                _goldDragonCompanionFeat.m_AllFeatures = BasicFeatSelection.m_AllFeatures;

                var _goldDragonCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                    .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                    .SetIcon(BloodlineDraconicGoldProgression.Icon)
                    .AddToAllFeatures(new Blueprint<BlueprintFeatureReference>[] {
                        _goldDragonProwess.AssetGuidThreadSafe,
                        _goldDragonDefenses.AssetGuidThreadSafe })
                    //.AddPrerequisitePlayerHasFeature(GoldDragonProgression)
                    .SetHideInUI(true)
                    .SetHideInCharacterSheetAndLevelUp(true)
                    .SetHideNotAvailibleInUI(true)
                    .Configure();
                Tools.LogMessage("Built: Gold Dragon Companion Choices -> " + _goldDragonCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
