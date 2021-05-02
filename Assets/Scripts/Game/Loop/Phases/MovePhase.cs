using DG.Tweening;
using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public class MovePhase : Phase
    {
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
