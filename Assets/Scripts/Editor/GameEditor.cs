using MadHeroes.Data;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

namespace Source.Editor
{
    public static class GameEditor
    {
        public const string MainScenePath = "Assets/Scenes/First Scene.unity";

        [MenuItem("Game/Play", priority = 1)]
        public static void PlayGame()
        {
            EditorSceneManager.OpenScene(MainScenePath);
            EditorApplication.isPlaying = true;
        }

        [MenuItem("Game/Configuration", priority = 2)]
        public static void OpenGameConfiguration()
        {
            var window = EditorWindow.GetWindow<GameConfigurationWindow>("Game Configuration");
            window.minSize = new Vector2(350f, 450f);
            window.Show();
        }

        [MenuItem("Game/Reset Data", priority = 3)]
        public static void ClearGameData()
        {
            GameData.Reset();
            GameData.Save();
        }
    }
}