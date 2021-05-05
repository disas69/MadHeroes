using System;
using MadHeroes.Players;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.Game.Loop.Phases
{
    public class AttackPhase : ActionPhase
    {
        public override Type[] ActionTypes => new[] {typeof(AttackAction)};

        public AttackPhase(Player[] players) : base(players)
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