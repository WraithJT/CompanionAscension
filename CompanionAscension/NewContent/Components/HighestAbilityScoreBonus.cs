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
	[TypeId("ec6d735c011b47748bb10b133d3b5ac6")]
	class HighestAbilityScoreBonus : UnitFactComponentDelegate
	{
		public override void OnTurnOn()
		{
			var stats = new StatType[] {
					StatType.Strength,
					StatType.Dexterity,
					StatType.Constitution,
					StatType.Intelligence,
					StatType.Wisdom,
					StatType.Charisma
				};

			this.m_HighestStat = getHighestStat(base.Owner, stats);
			
			int value = this.HighestStatBonus.Calculate(base.Context);
			base.Owner.Stats.GetStat(this.m_HighestStat).AddModifier(value, base.Runtime, this.Descriptor);
		}

		public override void OnTurnOff()
		{
			ModifiableValue stat = base.Owner.Stats.GetStat(this.m_HighestStat);
			if (stat != null)
			{
				stat.RemoveModifiersFrom(base.Runtime);
			}
		}

		public ModifierDescriptor Descriptor;

		public ContextValue HighestStatBonus;

		private StatType m_HighestStat;

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