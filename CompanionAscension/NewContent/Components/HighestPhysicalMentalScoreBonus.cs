// Credit to Vek17 for pieces of this: https://github.com/Vek17/TabletopTweaks-Core

using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.Enums;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.UnitLogic;
using Kingmaker.EntitySystem.Entities;
using System.Collections.Generic;

namespace CompanionAscension.NewContent.Components
{
    [TypeId("4bcc55e9f6c14fd1a93e6e10211dc69e")]
    class HighestPhysicalMentalScoreBonus : UnitFactComponentDelegate
    {
        public override void OnTurnOn()
        {
            var _physicalStats = new StatType[] {
                    StatType.Strength,
                    StatType.Dexterity,
                    StatType.Constitution,
                };

            var _mentalStats = new StatType[] {
                    StatType.Intelligence,
                    StatType.Wisdom,
                    StatType.Charisma
                };

            this.m_HighestPhysicalStat = getHighestStat(base.Owner, _physicalStats);
            this.m_HighestMentalStat = getHighestStat(base.Owner, _mentalStats);

            int value = this.HighestStatBonus.Calculate(base.Context);
            base.Owner.Stats.GetStat(this.m_HighestPhysicalStat).AddModifier(value, base.Runtime, this.Descriptor);
            base.Owner.Stats.GetStat(this.m_HighestMentalStat).AddModifier(value, base.Runtime, this.Descriptor);
        }

        public override void OnTurnOff()
        {
            ModifiableValue stat = base.Owner.Stats.GetStat(this.m_HighestPhysicalStat);
            if (stat != null)
            {
                stat.RemoveModifiersFrom(base.Runtime);
            }
            stat = base.Owner.Stats.GetStat(this.m_HighestMentalStat);
            if (stat != null)
            {
                stat.RemoveModifiersFrom(base.Runtime);
            }
        }

        public ModifierDescriptor Descriptor;

        public ContextValue HighestStatBonus;


        private StatType m_HighestPhysicalStat;
        private StatType m_HighestMentalStat;

        static private StatType getHighestStat(UnitEntityData unit, IEnumerable<StatType> stats)
        {
            StatType highestStat = StatType.Unknown;
            int highestValue = -1;
            foreach (StatType stat in stats)
            {
                var value = unit.Stats.GetStat(stat).ModifiedValue;
                if (value > highestValue)
                {
                    highestStat = stat;
                    highestValue = value;
                }
            }
            return highestStat;
        }
    }
}