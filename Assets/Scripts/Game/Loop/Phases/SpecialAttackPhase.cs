using MadHeroes.Players;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.Game.Loop.Phases
{
    public class SpecialAttackPhase : ActionPhase
    {
        public SpecialAttackPhase(Player[] players) : base(players, new[] {typeof(SpecialAttackAction)})
        {
        }
    }
}