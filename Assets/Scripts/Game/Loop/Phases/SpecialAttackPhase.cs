using DG.Tweening;
using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public class SpecialAttackPhase : Phase
    {
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
