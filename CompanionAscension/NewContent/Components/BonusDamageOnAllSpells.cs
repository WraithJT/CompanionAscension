using Kingmaker.Blueprints.JsonSystem;
using Kingmaker.PubSubSystem;
using Kingmaker.RuleSystem.Rules.Damage;
using Kingmaker.UnitLogic;
using Kingmaker.UnitLogic.Abilities.Blueprints;
using Kingmaker.UnitLogic.Mechanics;
using Kingmaker.Utility;

namespace CompanionAscension.NewContent.Components
{
    [TypeId("e395b5829cc34a18b55257d45e802e55")]
    public class BonusDamageOnAllSpells : UnitFactComponentDelegate, IInitiatorRulebookHandler<RuleCalculateDamage>, IRulebookHandler<RuleCalculateDamage>, ISubscriber, IInitiatorRulebookSubscriber
    {
        public void OnEventAboutToTrigger(RuleCalculateDamage evt)
        {
            MechanicsContext context = evt.Reason.Context;
            if (((context != null) ? context.SourceAbility : null) == null)
            {
                return;
            }
            if ((!context.SourceAbility.IsSpell && this.SpellsOnly) || context.SourceAbility.Type == AbilityType.Physical)
            {
                return;
            }
            foreach (BaseDamage baseDamage in evt.DamageBundle)
            {
                if (!baseDamage.Precision)
                {
                    int bonus = this.UseContextBonus ? (this.Value.Calculate(context) * baseDamage.Dice.Rolls) : baseDamage.Dice.Rolls;
                    baseDamage.AddModifier(bonus, base.Fact);
                }
            }
        }

        public void OnEventDidTrigger(RuleCalculateDamage evt)
        {
        }

        public bool SpellsOnly = true;

        public bool UseContextBonus;

        [ShowIf("UseContextBonus")]
        public ContextValue Value;
    }
}
