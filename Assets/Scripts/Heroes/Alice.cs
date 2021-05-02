using MadHeroes.Heroes.Actions;

namespace MadHeroes.Heroes
{
    public class Alice : Hero
    {
        protected override void InitializeActions()
        {
            base.InitializeActions();
            Actions.Add(new RangedAttackAction(this));
            Actions.Add(new HealAction(this));
        }
    }
}