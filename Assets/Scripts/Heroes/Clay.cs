using MadHeroes.Heroes.Actions;

namespace MadHeroes.Heroes
{
    public class Clay : Hero
    {
        protected override void InitializeActions()
        {
            base.InitializeActions();
            Actions.Add(new MeleeAttackAction(this));
        }
    }
}