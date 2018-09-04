namespace Editor
{
    using System;

    using UnityEditor;

    using UnityEngine;

    [Flags]
    public enum EditorListOption
    {
        None = 0,
        ListSize = 1,
        ListLabel = 2,
        ElementLabels = 4,
        Buttons = 8,
        Default = ListSize | ListLabel | ElementLabels,
        NoElementLabels = ListSize | ListLabel,
        All = Default | Buttons
    }

    public static class EditorList
    {
        private static readonly GUIContent
            addButtonContent = new GUIContent("+", "add element");

        private static readonly GUIContent
            deleteButtonContent = new GUIContent("-", "delete");

        private static readonly GUIContent
            duplicateButtonContent = new GUIContent("+", "duplicate");

        private static readonly GUILayoutOption miniButtonWidth = GUILayout.Width(20f);

        private static readonly GUIContent
            moveButtonContent = new GUIContent("\u21b4", "move down");

        public static void Show(SerializedProperty list, EditorListOption options = EditorListOption.Default)
        {
            if (!list.isArray)
            {
                EditorGUILayout.HelpBox(list.name + " is neither an array nor a list!", MessageType.Error);
                return;
            }

            bool
                showListLabel = (options & EditorListOption.ListLabel) != 0,
                showListSize = (options & EditorListOption.ListSize) != 0;

            if (showListLabel)
            {
                EditorGUILayout.PropertyField(list);
                EditorGUI.indentLevel += 1;
            }

            if (!showListLabel || list.isExpanded)
            {
                var size = list.FindPropertyRelative("Array.size");
                if (showListSize)
                    EditorGUILayout.PropertyField(size);
                if (size.hasMultipleDifferentValues)
                    EditorGUILayout.HelpBox("Not showing lists with different sizes.", MessageType.Info);
                else
                    ShowElements(list, options);
            }

            if (showListLabel)
                EditorGUI.indentLevel -= 1;
        }

        private static void ShowElements(SerializedProperty list, EditorListOption options)
        {
            bool
                showElementLabels = (options & EditorListOption.ElementLabels) != 0,
                showButtons = (options & EditorListOption.Buttons) != 0;

            for (var i = 0; i < list.arraySize; i++)
            {
                if (showButtons)
                    EditorGUILayout.BeginHorizontal();
                if (showElementLabels)
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i));
                else
                    EditorGUILayout.PropertyField(list.GetArrayElementAtIndex(i), GUIContent.none);
                if (showButtons)
                    EditorGUILayout.EndHorizontal();
            }

            if (showButtons && list.arraySize == 0 && GUILayout.Button(addButtonContent, EditorStyles.miniButton))
                list.arraySize += 1;
        }
    }
}