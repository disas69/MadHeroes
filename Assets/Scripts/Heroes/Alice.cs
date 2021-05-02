using MadHeroes.Heroes.Actions;

namespace MadHeroes.Heroes
{
    public class Alice : Hero
    {
        protected override void SetupActions()
        {
            base.SetupActions();
            Actions.Add(new RangedAttackAction(this));
            Actions.Add(new HealAction(this));
        }
    }
}