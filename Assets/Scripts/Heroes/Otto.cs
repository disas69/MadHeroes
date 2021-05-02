using MadHeroes.Heroes.Actions;

namespace MadHeroes.Heroes
{
    public class Otto : Hero
    {
        protected override void InitializeActions()
        {
            base.InitializeActions();
            Actions.Add(new MeleeAttackAction(this));
            Actions.Add(new PullAction(this));
        }
    }
}