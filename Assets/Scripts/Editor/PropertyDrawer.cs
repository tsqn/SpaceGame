namespace Editor
{
    using Data;

    using UnityEditor;

    using UnityEngine;

    [CustomPropertyDrawer(typeof(BaseShipStats))]
    public class ShipStatsPropertyDrawer : PropertyDrawer
    {
        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;


            // Set indent back to what it was
            EditorGUI.indentLevel = indent;
        }

        private static void Show()
        {
        }
    }
}