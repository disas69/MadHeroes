using System;
using MadHeroes.Players;

namespace MadHeroes.Game.Loop.Phases
{
    public abstract class Phase : IDisposable
    {
        protected Player[] Players;
        protected Player ActivePlayer;

        public bool IsComplete { get; protected set; }

        public event Action<Phase> Activated;

        protected Phase(Player[] players)
        {
            Players = players;
        }

        public Player GetActivePlayer()
        {
            return ActivePlayer;
        }

        public virtual void Activate()
        {
            IsComplete = false;
            ActivePlayer = null;
        }

        public virtual void Update()
        {
        }

        protected bool ActivateNextPlayer()
        {
            var index = Array.IndexOf(Players, ActivePlayer);
            if (index < 0)
            {
                index = 0;
            }
            else if (index + 1 >= Players.Length)
            {
                return false;
            }
            else
            {
                index++;
            }

            ActivePlayer = Players[index];
            return true;
        }

        protected virtual void Complete()
        {
            IsComplete = true;
            ActivePlayer = null;
        }

        protected void FireActivated()
        {
            Activated?.Invoke(this);
        }

        public virtual void Dispose()
        {
        }
    }
}