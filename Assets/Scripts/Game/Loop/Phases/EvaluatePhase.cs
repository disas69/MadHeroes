using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public class EvaluatePhase : Phase
    {
        public EvaluatePhase(Player[] players) : base(players)
        {
        }

        public override void Activate()
        {
            base.Activate();

            var players = Players.Length;

            for (var i = 0; i < Players.Length; i++)
            {
                var player = Players[i];
                player.RemoveDeadHeroes();

                if (player.IsDefeated)
                {
                    players--;
                }
            }

            if (players == 1)
            {
                GameController.Instance.SetState(GameState.GameOver);
            }
            else
            {
                Complete();
            }
        }
    }
}