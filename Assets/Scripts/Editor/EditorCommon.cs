namespace Editor
{
    using UnityEditor;

    using UnityEngine;

    public class EditorCommon
    {
        public static void ShowLine(string labelText, string valueText)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(labelText, GUILayout.Width(120));
            EditorGUILayout.TextField(valueText);
            EditorGUILayout.EndHorizontal();
        }
    }
}