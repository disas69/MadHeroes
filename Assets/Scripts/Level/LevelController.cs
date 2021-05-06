using System;
using Cinemachine;
using Framework.Tools.Misc;
using MadHeroes.Players;
using UnityEngine;

namespace MadHeroes.Level
{
    public class LevelController : ActivatableMonoBehaviour, IDisposable
    {
        private LevelConfiguration _configuration;

        [SerializeField] private CinemachineVirtualCamera _battleCamera;

        public Player[] Players { get; private set; }
        public CinemachineVirtualCamera BattleCamera => _battleCamera;

        private void Awake()
        {
            _battleCamera.Priority = 0;
            _battleCamera.enabled = false;
        }

        public virtual void Initialize(LevelConfiguration configuration)
        {
            _configuration = configuration;

            Players = GetComponentsInChildren<Player>();
            for (var i = 0; i < Players.Length; i++)
            {
                Players[i].Initialize();
            }
        }

        public virtual void Dispose()
        {
            for (var i = 0; i < Players.Length; i++)
            {
                Players[i].Dispose();
            }
        }
    }
}