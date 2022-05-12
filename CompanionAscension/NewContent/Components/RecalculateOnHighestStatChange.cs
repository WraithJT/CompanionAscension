using System;
using Kingmaker.Blueprints;
using Kingmaker.Blueprints.Facts;
using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.EntitySystem.Stats;
using Kingmaker.UnitLogic;
using Kingmaker.Utility;

namespace CompanionAscension.NewContent.Components
{
	[AllowedOn(typeof(BlueprintUnitFact), false)]
	[TypeId("05684bc28f444613a5b8e6220028d8b6")]
	public class RecalculateOnAnyStatChange : UnitFactComponentDelegate<RecalculateOnAnyStatChange.ComponentData>
	{
		public override void OnPostLoad()
		{
			base.OnPostLoad();
			base.Fact.RecalculateContextBeforeTurnOn = true;
		}

		public override void OnPreSave()
		{
			base.OnPreSave();
			base.Fact.RecalculateContextBeforeTurnOn = true;
		}

		public override void OnTurnOn()
		{
			StatType statType = this.Stat;
			
			base.Data.AppliedToStat = base.Owner.Stats.GetStat(statType);
			base.Data.AppliedToStat.AddDependentFact(base.Fact);
		}

		public override void OnTurnOff()
		{
			ModifiableValue appliedToStat = base.Data.AppliedToStat;
			if (appliedToStat != null)
			{
				appliedToStat.RemoveDependentFact(base.Fact);
			}
			base.Data.AppliedToStat = null;
		}

		public StatType Stat;

		public class ComponentData
		{
			public ModifiableValue AppliedToStat;
		}
	}
}
