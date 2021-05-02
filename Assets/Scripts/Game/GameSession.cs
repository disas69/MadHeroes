using System;
using UnityEngine;
using MadHeroes.Data;
using MadHeroes.Level;
using MadHeroes.Camera;
using Framework.Effects;
using MadHeroes.Configuration;

namespace MadHeroes.Game
{
    public class GameSession : MonoBehaviour, IDisposable
    {
        [SerializeField] private CameraController _camera;

        public CameraController Camera => _camera;
        public LevelController Level { get; private set; }

        public void Initialize(int level)
        {
            Camera.Initialize();

            var configuration = GameConfiguration.GetLevelConfiguration(level);
            if (configuration != null)
            {
                LevelLoader.Load(configuration, levelController =>
                {
                    Level = levelController;
                    Level.Initialize(level, configuration);
                    Debug.Log($"Level {Level.Level} loaded");
                });
            }
        }

        public void Play()
        {
        }

        public void Stop()
        {
            GameData.IncreaseLevel();
            GameData.Save();
        }

        public void Dispose()
        {
            VisualEffectsManager.Clear();
            LevelLoader.Unload(() =>
            {
                Resources.UnloadUnusedAssets();
                GC.Collect();
            });
        }
    }
}