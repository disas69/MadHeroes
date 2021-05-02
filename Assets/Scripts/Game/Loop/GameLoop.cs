using System;
using MadHeroes.Players;
using MadHeroes.Game.Loop.Phases;
using System.Collections.Generic;
using UnityEngine;

namespace MadHeroes.Game.Loop
{
    public class GameLoop : IDisposable
    {
        private Player[] _players;
        private List<Phase> _phases;
        private Phase _activePhase;

        public bool IsActive { get; private set; }

        public event Action<Phase> PhaseActivated;

        public void Activate(bool isActive)
        {
            IsActive = isActive;

            if (isActive)
            {
                ActivateNextPhase();
            }
        }

        public GameLoop(Player[] players)
        {
            _players = players;
            _phases = new List<Phase>
            {
                new AssignActionPhase(players),
                new MovePhase(players),
                new SpecialAttackPhase(players),
                new AttackPhase(players),
                new EvaluatePhase(players)
            };

            for (var i = 0; i < _phases.Count; i++)
            {
                _phases[i].Activated += OnPhaseActivated;
            }
        }

        public void Update()
        {
            if (!IsActive || _activePhase == null)
            {
                return;
            }

            _activePhase.Update();

            if (_activePhase.IsComplete)
            {
                ActivateNextPhase();
            }
        }

        private void ActivateNextPhase()
        {
            var index = _phases.FindIndex(p => p == _activePhase);
            if (index < 0 || index + 1 >= _phases.Count)
            {
                index = 0;
            }
            else
            {
                index++;
            }

            var nextPhase = _phases[index];
            nextPhase.Activate();

            _activePhase = nextPhase;
            Debug.Log($"Phase {_activePhase.GetType().Name} is activated");
        }

        private void OnPhaseActivated(Phase phase)
        {
            PhaseActivated?.Invoke(phase);
        }

        public void Dispose()
        {
            for (var i = 0; i < _phases.Count; i++)
            {
                _phases[i].Activated -= OnPhaseActivated;
            }

            _activePhase = null;
        }
    }
}