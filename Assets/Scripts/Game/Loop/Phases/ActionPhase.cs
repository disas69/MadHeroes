using System;
using MadHeroes.Players;
using Action = MadHeroes.Heroes.Actions.Action;

namespace MadHeroes.Game.Loop.Phases
{
    public abstract class ActionPhase : Phase
    {
        public abstract Type[] ActionTypes { get; }

        protected ActionPhase(Player[] players) : base(players)
        {
        }

        public override void Update()
        {
            base.Update();
            var isComplete = true;

            for (var i = 0; i < Players.Length; i++)
            {
                for (var j = 0; j < Players[i].Heroes.Count; j++)
                {
                    Players[i].Heroes[j].UpdateAction();

                    if (!Players[i].Heroes[j].IsComplete)
                    {
                        isComplete = false;
                    }
                }
            }

            if (isComplete)
            {
                Complete();
            }
        }

        protected void TryExecuteActions()
        {
            for (var i = 0; i < Players.Length; i++)
            {
                for (var j = 0; j < Players[i].Heroes.Count; j++)
                {
                    var hero = Players[i].Heroes[j];
                    if (IsAssignableAction(hero.CurrentAction))
                    {
                        hero.Execute();
                    }
                }
            }
        }

        public bool IsAssignableAction(Action action)
        {
            var type = action.GetType();

            for (var i = 0; i < ActionTypes.Length; i++)
            {
                if (ActionTypes[i].IsAssignableFrom(type))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
