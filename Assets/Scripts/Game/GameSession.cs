using System;
using UnityEngine;
using MadHeroes.Data;
using MadHeroes.Level;
using MadHeroes.Camera;
using MadHeroes.Game.Loop;
using MadHeroes.Configuration;
using MadHeroes.Game.Loop.Phases;

namespace MadHeroes.Game
{
    public class GameSession : MonoBehaviour, IDisposable
    {
        [SerializeField] private CameraController _camera;

        public CameraController Camera => _camera;
        public LevelController Level { get; private set; }
        public GameLoop GameLoop { get; private set; }

        public void Initialize(int level)
        {
            Camera.Initialize();

            var configuration = GameConfiguration.GetLevelConfiguration(level);
            if (configuration != null)
            {
                LevelLoader.Load(configuration, levelController =>
                {
                    Level = levelController;
                    Level.Initialize(configuration);

                    GameLoop = new GameLoop(Level.Players);
                    GameLoop.PhaseActivated += OnPhaseActivated;
                });
            }
        }

        public void Play()
        {
            GameLoop.Activate(true);
        }

        public void Stop()
        {
            GameLoop.Activate(false);
            GameData.IncreaseLevel();
            GameData.Save();
        }

        private void Update()
        {
            if (GameLoop != null && GameLoop.IsActive)
            {
                GameLoop.Update();
            }
        }

        private void OnPhaseActivated(Phase phase)
        {
            if (phase is AssignActionPhase assignActionPhase)
            {
                Camera.SwitchView(assignActionPhase.GetActivePlayer().Camera);
            }
            else if (phase is MovePhase)
            {
                Camera.SwitchView(Level.BattleCamera);
            }
        }

        public void Dispose()
        {
            if (GameLoop != null)
            {
                GameLoop.PhaseActivated -= OnPhaseActivated;
                GameLoop.Dispose();
            }

            LevelLoader.Unload(() =>
            {
                Resources.UnloadUnusedAssets();
                GC.Collect();
            });
        }
    }
}