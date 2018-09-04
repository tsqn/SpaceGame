namespace Editor
{
    using System;

    using UnityEditor;

    using UnityEngine;

    public class EditorCommon
    {
        public static void ShowLine(string labelText, string value)
        {
            ShowInput(labelText, () => EditorGUILayout.TextField(value));
        }

        public static void ShowLine(string labelText, float value)
        {
            ShowInput(labelText, () => EditorGUILayout.FloatField(value));
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