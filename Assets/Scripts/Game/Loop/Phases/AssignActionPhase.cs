using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public class AssignActionPhase : Phase
    {
        public AssignActionPhase(Player[] players) : base(players)
        {
            for (var i = 0; i < Players.Length; i++)
            {
                Players[i].AssignedActions += OnAssignedActions;
            }
        }

        public override void Activate()
        {
            base.Activate();
            TrySelectNextPlayer();
        }

        private void OnAssignedActions()
        {
            TrySelectNextPlayer();
        }

        private void TrySelectNextPlayer()
        {
            if (ActivateNextPlayer())
            {
                FireActivated();
            }
            else
            {
                Complete();
            }
        }

        public override void Dispose()
        {
            base.Dispose();

            for (var i = 0; i < Players.Length; i++)
            {
                Players[i].AssignedActions -= OnAssignedActions;
            }
        }
    }
}