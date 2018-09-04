namespace Editor
{
    using System;

    using UnityEditor;

    using UnityEngine;

    public class EditorCommon
    {
        public static string ShowLine(string labelText, string value)
        {
            var result = value;
            ShowInput(labelText, () => result = EditorGUILayout.TextField(value));
            return result;
        }

        public static float ShowLine(string labelText, float value)
        {
            var result = value;
            ShowInput(labelText, () => result = EditorGUILayout.FloatField(value));
            return result;
        }

        private static void ShowInput(string labelText, Action guiLayoutAction)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(labelText, GUILayout.Width(120));
            guiLayoutAction.Invoke();
            EditorGUILayout.EndHorizontal();
        }
    }
}