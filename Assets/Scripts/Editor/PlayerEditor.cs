using MadHeroes.Configuration;
using MadHeroes.Players;
using Framework.Editor;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace MadHeroes.Assets.Scripts.Editor
{
    [CustomEditor(typeof(Player))]
    public class PlayerEditor : CustomEditorBase<Player>
    {
        private int _selectedIndex = -1;

        protected override void DrawInspector()
        {
            base.DrawInspector();

            var configurations = GameConfiguration.Instance.Heroes;
            var heroes = serializedObject.FindProperty("_heroEntries");
            ListPropertyDrawer.OnGUI(heroes, (property, i) =>
            {
                var hero = property.FindPropertyRelative("Hero");

                EditorGUI.BeginChangeCheck();
                {
                    var index = Mathf.Max(0, configurations.FindIndex(n => n.Name == hero.stringValue));
                    if (index != _selectedIndex)
                    {
                        _selectedIndex = index;
                        GUI.changed = true;
                    }

                    _selectedIndex = EditorGUILayout.Popup(_selectedIndex, configurations.Select(h => h.Name).ToArray());
                }
                if (EditorGUI.EndChangeCheck())
                {
                    hero.stringValue = configurations[_selectedIndex].Name;
                }

                EditorGUILayout.PropertyField(property.FindPropertyRelative("Position"));
                EditorGUILayout.PropertyField(property.FindPropertyRelative("Rotation"));
            });

            EditorGUILayout.PropertyField(serializedObject.FindProperty("_camera"));
        }
    }
}