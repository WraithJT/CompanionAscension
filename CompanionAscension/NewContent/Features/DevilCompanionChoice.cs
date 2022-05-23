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

namespace CompanionAscension.NewContent.Features
{
    class DevilCompanionChoice
    {
        public static readonly string Guid = "b39923a18b9f487694c1e3a44ebcbb6b";
        private static readonly string Name = "DevilCompanionChoice";
        private static readonly string DisplayName = "Devil Companion Ascension";
        private static readonly string DisplayNameKey = "DevilCompanionChoiceName";
        private static readonly string Description = "At 8th mythic rank, the Devil's companions can gain further power.";
        private static readonly string DescriptionKey = "DevilCompanionChoiceDescription";

        private static readonly string DevilProgression = "87bc9abf00b240a44bb344fea50ec9bc";

        [HarmonyPatch(typeof(BlueprintsCache), "Init")]
        static class BlueprintsCache_Init_patch
        {
            static bool Initialized;

            [HarmonyPriority(Priority.First)]
            static void Postfix()
            {
                if (Initialized) return;
                Initialized = true;

                PatchDevilCompanionChoice();
                //try { PatchDevilCompanionChoice(); }
                //catch (Exception ex) { Tools.LogMessage("EXCEPTION: " + ex.ToString()); }
            }

            public static void PatchDevilCompanionChoice()
            {
                Tools.LogMessage("New Content: Building Devil Companion Choices");

                ContextRankConfig _devilContractContextRankConfig = new()
                {
                    m_BaseValueType = ContextRankBaseValueType.MythicLevel,
                    m_Type = AbilityRankType.Default,
                    m_Progression = ContextRankProgression.OnePlusDivStep,
                    m_StepLevel = 3,
                    m_Max = 10,
                    m_Stat = StatType.Unknown
                };
                ContextValue _devilContractContextValue = new()
                {
                    ValueType = ContextValueType.Rank,
                    Value = 1,
                    ValueShared = AbilitySharedValue.StatBonus,
                    Property = UnitProperty.None,
                    ValueRank = AbilityRankType.Default
                };

                // contracts
                // Deimavigga - DEX/CHA, increased reach, extended spell range?
                // Pit Fiend - STR/WIS, 
                // Puragaus - CON/INT, +1damage/die on all? spells (DraconicBloodlineArcana, no descriptor)

                string _devilContractWithDeimaviggaName = "ContractWithDeimavigga";
                string _devilContractWithDeimaviggaGUID = "caddd62c3b1c4be69e9b3e8f481bc589";
                string _devilContractWithDeimaviggaDisplayName = "Contract with Deimavigga";
                string _devilContractWithDeimaviggaDisplayNameKey = "ContractWithDeimaviggaNameKey";
                string _devilContractWithDeimaviggaDescription =
                    "You sign a contract with a deimavigga, granting you a bonus to your Dexterity and Charisma " +
                    "equal to one-third of your mythic level plus 1 (maximum of 4). \nAdditionally, the reach of your melee weapons is increased by 5 feet and " +
                    "the range of all of your spells is increased, as though using the Reach Spell feat.";
                string _devilContractWithDeimaviggaDescriptionKey = "ContractWithDeimaviggaDescriptionKey";
                var _devilContractWithDeimavigga = FeatureConfigurator.New(_devilContractWithDeimaviggaName, _devilContractWithDeimaviggaGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_devilContractWithDeimaviggaDisplayNameKey, _devilContractWithDeimaviggaDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_devilContractWithDeimaviggaDescriptionKey, _devilContractWithDeimaviggaDescription))
                    .AddContextRankConfig(_devilContractContextRankConfig)
                    .AddContextStatBonus(
                        descriptor: ModifierDescriptor.Mythic,
                        stat: StatType.Dexterity,
                        value: _devilContractContextValue)
                    .AddContextStatBonus(
                        descriptor: ModifierDescriptor.Mythic,
                        stat: StatType.Charisma,
                        value: _devilContractContextValue)
                    .AddAutoMetamagic(
                        allowedAbilities: AutoMetamagic.AllowedType.Any,
                        metamagic: Metamagic.Reach,
                        once: false,
                        checkSpellbook: false)
                    .AddStatBonus(
                        descriptor: ModifierDescriptor.UntypedStackable,
                        stat: StatType.Reach,
                        value: 5,
                        scaleByBasicAttackBonus: false)
                    .Configure();

                BuffEnchantAnyWeapon _devilPuragausEnchantPrimary = new()
                {
                    m_EnchantmentBlueprint = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>("234177d5807909f44b8c91ed3c9bf7ac").ToReference<BlueprintItemEnchantmentReference>(),
                    Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand
                };
                BuffEnchantAnyWeapon _devilPuragausEnchantSecondary = new()
                {
                    m_EnchantmentBlueprint = ResourcesLibrary.TryGetBlueprint<BlueprintWeaponEnchantment>("234177d5807909f44b8c91ed3c9bf7ac").ToReference<BlueprintItemEnchantmentReference>(),
                    Slot = Kingmaker.UI.GenericSlot.EquipSlotBase.SlotType.PrimaryHand
                };
                BonusDamageOnAllSpells _devilPuragausSpellDamageBonus = new()
                {
                    SpellsOnly = true,
                    UseContextBonus = false
                };
                string _devilContractWithPuragausName = "ContractWithPuragaus";
                string _devilContractWithPuragausGUID = "44657880a2ec49199c20b41042ec4fbe";
                string _devilContractWithPuragausDisplayName = "Contract with Puragaus";
                string _devilContractWithPuragausDisplayNameKey = "ContractWithPuragausNameKey";
                string _devilContractWithPuragausDescription =
                    "You sign a contract with a puragaus, granting you a bonus to your Constitution and Intelligence " +
                    "equal to one-third of your mythic level plus 1 (maximum of 4). \nAdditionally, your spells deal 1 additional damage per die rolled " +
                    "and your weapons are treated as if they had the Chaotic Outsider Bane enchantment.";
                string _devilContractWithPuragausDescriptionKey = "ContractWithPuragausDescriptionKey";
                var _devilContractWithPuragaus = FeatureConfigurator.New(_devilContractWithPuragausName, _devilContractWithPuragausGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_devilContractWithPuragausDisplayNameKey, _devilContractWithPuragausDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_devilContractWithPuragausDescriptionKey, _devilContractWithPuragausDescription))
                    .AddContextRankConfig(_devilContractContextRankConfig)
                    .AddContextStatBonus(
                        descriptor: ModifierDescriptor.Mythic,
                        stat: StatType.Constitution,
                        value: _devilContractContextValue)
                    .AddContextStatBonus(
                        descriptor: ModifierDescriptor.Mythic,
                        stat: StatType.Intelligence,
                        value: _devilContractContextValue)
                    .AddComponent(_devilPuragausSpellDamageBonus)
                    .AddComponent(_devilPuragausEnchantPrimary)
                    .AddComponent(_devilPuragausEnchantSecondary)
                    .Configure();

                // armor class? resists?
                string _devilContractWithPitFiendName = "ContractWithPitFiend";
                string _devilContractWithPitFiendGUID = "a5690c7afbed4fe7afdc848a39a43ff4";
                string _devilContractWithPitFiendDisplayName = "Contract with Pit Fiend";
                string _devilContractWithPitFiendDisplayNameKey = "ContractWithPitFiendNameKey";
                string _devilContractWithPitFiendDescription =
                    "You sign a contract with a pit fiend, granting you a bonus to your Strength and Wisdom " +
                    "equal to one-third of your mythic level plus 1 (maximum of 4). \nAdditionally, ";
                string _devilContractWithPitFiendDescriptionKey = "ContractWithPitFiendDescriptionKey";
                var _devilContractWithPitFiend = FeatureConfigurator.New(_devilContractWithPitFiendName, _devilContractWithPitFiendGUID)
                    .SetDisplayName(LocalizationTool.CreateString(_devilContractWithPitFiendDisplayNameKey, _devilContractWithPitFiendDisplayName, false))
                    .SetDescription(LocalizationTool.CreateString(_devilContractWithPitFiendDescriptionKey, _devilContractWithPitFiendDescription))
                    .AddContextRankConfig(_devilContractContextRankConfig)
                    .AddContextStatBonus(
                        descriptor: ModifierDescriptor.Mythic,
                        stat: StatType.Strength,
                        value: _devilContractContextValue)
                    .AddContextStatBonus(
                        descriptor: ModifierDescriptor.Mythic,
                        stat: StatType.Wisdom,
                        value: _devilContractContextValue)
                    .Configure();

                var _devilCompanionChoice = FeatureSelectionConfigurator.New(Name, Guid)
                        .SetDisplayName(LocalizationTool.CreateString(DisplayNameKey, DisplayName, false))
                        .SetDescription(LocalizationTool.CreateString(DescriptionKey, Description))
                        .AddToAllFeatures(new Blueprint<BlueprintFeature, BlueprintFeatureReference>[] {
                            _devilContractWithDeimavigga.AssetGuidThreadSafe,
                            _devilContractWithPuragaus.AssetGuidThreadSafe,
                            _devilContractWithPitFiend.AssetGuidThreadSafe})
                        //.PrerequisitePlayerHasFeature(DevilProgression)
                        .SetHideInUI(true)
                        .SetHideInCharacterSheetAndLevelUp(true)
                        .SetHideNotAvailibleInUI(true)
                        .Configure();
                Tools.LogMessage("Built: Devil Companion Choices -> " + _devilCompanionChoice.AssetGuidThreadSafe);
            }
        }
    }
}
