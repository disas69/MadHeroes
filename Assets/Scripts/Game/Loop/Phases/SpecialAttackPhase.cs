using System;
using DG.Tweening;
using MadHeroes.Players;
using MadHeroes.Heroes.Actions;

namespace MadHeroes.Game.Loop.Phases
{
    public class SpecialAttackPhase : ActionPhase
    {
        public override Type[] ActionTypes => new[] {typeof(SpecialAttackAction)};

        public SpecialAttackPhase(Player[] players) : base(players)
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