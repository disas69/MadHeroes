using MadHeroes.Heroes.Actions;

namespace MadHeroes.Heroes
{
    public class Otto : Hero
    {
        protected override void SetupActions()
        {
            base.SetupActions();
            Actions.Add(new MeleeAttackAction(this));
            Actions.Add(new PullAction(this));
        }
    }
}