using MadHeroes.Players;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.Game.Loop.Phases
{
    public class MovePhase : ActionPhase
    {
        public MovePhase(Player[] players) : base(players, new[] {typeof(MoveAction)})
        {
        }
    }
}