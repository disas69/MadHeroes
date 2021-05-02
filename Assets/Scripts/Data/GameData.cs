using UnityEngine;

namespace MadHeroes.Data
{
    public static class GameData
    {
        private const string LevelIndexKey = "Level_Index";

        public static int LevelIndex
        {
            get => PlayerPrefs.GetInt(LevelIndexKey, 1);
            set => PlayerPrefs.SetInt(LevelIndexKey, value);
        }

        public static void IncreaseLevel()
        {
            LevelIndex++;
        }

        public static void Reset()
        {
            LevelIndex = 1;
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }
    }
}