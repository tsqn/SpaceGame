namespace Editor
{
    using System;

    using UnityEditor;

    using UnityEngine;

    /// <summary>
    /// Вспомогательные методы для работы с окнами редактора.
    /// </summary>
    public static class EditorUtils
    {
        /// <summary>
        /// Отображает строку с заголовком и полем для ввода текста.
        /// </summary>
        /// <param name="labelText">Текст заголовка.</param>
        /// <param name="value">Значение поля для ввода.</param>
        /// <returns>Строка ввода.</returns>
        public static string ShowLine(string labelText, string value)
        {
            var result = value;
            ShowInput(labelText, () => result = EditorGUILayout.TextField(value));
            return result;
        }

        /// <summary>
        /// Отображает строку с заголовком и полем для ввода значения с плавующей точкой.
        /// </summary>
        /// <param name="labelText">Текст заголовка.</param>
        /// <param name="value">Значение поля для ввода.</param>
        /// <returns>Строка ввода.</returns>
        public static float ShowLine(string labelText, float value)
        {
            var result = value;
            ShowInput(labelText, () => result = EditorGUILayout.FloatField(value));
            return result;
        }

        /// <summary>
        /// Отображает строку с заголовком и полем для ввода в одну строку.
        /// </summary>
        /// <param name="labelText">Текст ввода.</param>
        /// <param name="guiLayoutInput">Колбек отображения поля ввода.</param>
        private static void ShowInput(string labelText, Action guiLayoutInput)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(labelText, GUILayout.Width(120));
            guiLayoutInput.Invoke();
            EditorGUILayout.EndHorizontal();
        }
    }
}