using System;
using MadHeroes.Players;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.Game.Loop.Phases
{
    public class MovePhase : ActionPhase
    {
        public override Type[] ActionTypes => new[] {typeof(MoveAction)};

        public MovePhase(Player[] players) : base(players)
        {
        }

        public override void Activate()
        {
            base.Activate();
            FireActivated();
            TryExecuteActions();
        }
    }
}