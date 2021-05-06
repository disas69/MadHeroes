using MadHeroes.Players;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.Game.Loop.Phases
{
    public class AttackPhase : ActionPhase
    {
        public AttackPhase(Player[] players) : base(players, new[] {typeof(AttackAction)})
        {
        }
    }
}