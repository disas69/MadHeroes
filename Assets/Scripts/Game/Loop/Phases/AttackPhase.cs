using DG.Tweening;
using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public class AttackPhase : Phase
    {
        public AttackPhase(Player[] players) : base(players)
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
