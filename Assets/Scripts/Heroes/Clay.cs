using MadHeroes.Heroes.Actions;

namespace MadHeroes.Heroes
{
    public class Clay : Hero
    {
        protected override void SetupActions()
        {
            base.SetupActions();
            Actions.Add(new MeleeAttackAction(this));
        }
    }
}