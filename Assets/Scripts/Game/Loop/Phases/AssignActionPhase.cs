using DG.Tweening;
using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public class AssignActionPhase : Phase
    {
        public AssignActionPhase(Player[] players) : base(players)
        {
        }

        public override void Activate()
        {
            base.Activate();
            ActivateNextPlayer();
            FireActivated();

            DOTween.Sequence()
                .AppendInterval(5f)
                .AppendCallback(() =>
                {
                    ActivateNextPlayer();
                    FireActivated();
                })
                .AppendInterval(5f)
                .AppendCallback(Complete);
        }
    }
}
