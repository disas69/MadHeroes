using System.Collections.Generic;
using Framework.Editor;
using Framework.Editor.GUIUtilities;
using MadHeroes.Configuration;
using MadHeroes.Data;
using MadHeroes.Level;
using UnityEditor;
using UnityEngine;

namespace Source.Editor
{
    [CustomEditor(typeof(GameConfiguration))]
    public class GameConfigurationEditor : CustomEditorBase<GameConfiguration>
    {
        private Vector2 _scrollPosition;

        protected override void DrawInspector()
        {
            base.DrawInspector();

            using (var scroll = new EditorGUILayout.ScrollViewScope(_scrollPosition))
            {
                _scrollPosition = scroll.scrollPosition;

                EditorGUILayout.LabelField("Game Configuration", HeaderStyle);

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    DrawData();
                }

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    DrawSettings();
                }

                using (new EditorGUILayout.VerticalScope(GUI.skin.box))
                {
                    DrawLevels();
                }
            }
        }

        private void DrawData()
        {
            EditorGUILayout.LabelField("Data", HeaderStyle);
            GameData.LevelIndex = EditorGUILayout.IntField("Level Index", GameData.LevelIndex);

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save"))
                {
                    GameData.Save();
                }

                if (GUILayout.Button("Reset"))
                {
                    GameData.Reset();
                    GameData.Save();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void DrawSettings()
        {
            EditorGUILayout.LabelField("Settings", HeaderStyle);
            EditorGUILayout.PropertyField(serializedObject.FindProperty("Game"), true);
        }

        private void DrawLevels()
        {
            EditorGUILayout.LabelField("Levels", HeaderStyle);
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Add"))
                {
                    RecordObject("Game Configuration Change");
                    Target.Levels.Add(new LevelConfiguration());
                }
            }
            EditorGUILayout.EndHorizontal();

            var levels = serializedObject.FindProperty("Levels");
            var count = levels.arraySize;
            for (var i = 0; i < count; i++)
            {
                EditorGUILayout.BeginHorizontal(GUI.skin.box);
                {
                    EditorGUILayout.BeginVertical();
                    {
                        var element = levels.GetArrayElementAtIndex(i);
                        var level = element.FindPropertyRelative("Level");
                        level.intValue = i + 1;

                        using (new GUIEnabled(false))
                        {
                            EditorGUILayout.PropertyField(level);
                        }

                        EditorGUILayout.PropertyField(element.FindPropertyRelative("Scene"));
                    }
                    EditorGUILayout.EndVertical();

                    if (GUILayout.Button("X", GUILayout.Width(20)))
                    {
                        RecordObject("Game Configuration Change");
                        Target.Levels.RemoveAt(i);
                    }
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}