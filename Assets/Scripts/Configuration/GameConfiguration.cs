﻿using System.Collections.Generic;
using Framework.Tools.Singleton;
using Framework.Attributes;
using MadHeroes.Data;
using MadHeroes.Level;
using UnityEngine;

namespace MadHeroes.Configuration
{
    [ResourcePath(AssetPath)]
    [CreateAssetMenu(fileName = "GameConfiguration")]
    public class GameConfiguration : ScriptableSingleton<GameConfiguration>
    {
        public const string AssetPath = "GameConfiguration";

        public GameSettings Game;
        public List<LevelConfiguration> Levels;

        public static GameSettings GameSettings => Instance.Game;

        public static LevelConfiguration GetLevelConfiguration(int level)
        {
            if (Instance.Levels.Count == 0)
            {
                return null;
            }

            level %= Instance.Levels.Count;

            var levelConfiguration = Instance.Levels.Find(l => l.Level == level);
            if (levelConfiguration != null)
            {
                return levelConfiguration;
            }

            return Instance.Levels[Instance.Levels.Count - 1];
        }
    }
}