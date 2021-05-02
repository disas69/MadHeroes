using System;
using DG.Tweening;
using MadHeroes.Heroes.Actions;
using MadHeroes.Players;

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

            DOTween.Sequence()
                .AppendInterval(5f)
                .AppendCallback(Complete); 
        }
    }
}
