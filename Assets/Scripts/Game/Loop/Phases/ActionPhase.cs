using System;
using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public abstract class ActionPhase : Phase
    {
        public abstract Type[] ActionTypes { get; }

        protected ActionPhase(Player[] players) : base(players)
        {
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
