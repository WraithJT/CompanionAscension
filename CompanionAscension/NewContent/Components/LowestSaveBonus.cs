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
	[TypeId("39dc6e2da8694882945d20584698b090")]
	class LowestSaveBonus : UnitFactComponentDelegate
	{
		public override void OnTurnOn()
		{
			var stats = new StatType[] {
					StatType.SaveFortitude,
					StatType.SaveReflex,
					StatType.SaveWill
				};

			this.m_LowestScore = getLowestScore(base.Owner, stats);

			int value = this.LowestScoreBonus.Calculate(base.Context);
			base.Owner.Stats.GetStat(this.m_LowestScore).AddModifier(value, base.Runtime, this.Descriptor);
		}

		public override void OnTurnOff()
		{
			ModifiableValue stat = base.Owner.Stats.GetStat(this.m_LowestScore);
			if (stat != null)
			{
				stat.RemoveModifiersFrom(base.Runtime);
			}
		}

		public ModifierDescriptor Descriptor;

		public ContextValue LowestScoreBonus;

		private StatType m_LowestScore;

		static private StatType getLowestScore(UnitEntityData unit, IEnumerable<StatType> stats)
		{
			StatType lowestStat = StatType.Unknown;
			int lowestValue = 1000;
			foreach (StatType stat in stats)
			{
				var value = unit.Stats.GetStat(stat).ModifiedValue;
				if (value < lowestValue)
				{
					lowestStat = stat;
					lowestValue = value;
				}
			}
			return lowestStat;
		}
	}
}
