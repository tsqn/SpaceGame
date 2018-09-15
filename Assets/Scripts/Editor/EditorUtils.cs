namespace Editor
{
    using System;
    using System.Linq;

    using UnityEditor;

    using UnityEngine;

    using Object = UnityEngine.Object;

    /// <summary>
    /// Вспомогательные методы для работы с окнами редактора.
    /// </summary>
    public static class EditorUtils
    {
        /// <summary>
        /// Возвращает уникальное имя файла по пути.
        /// </summary>
        /// <param name="assetPath">Путь по умолчанию.</param>
        /// <typeparam name="T"></typeparam>
        public static string GetUniqueAssetName<T>(string assetPath) where T : Object
        {
            var newAssetPath = assetPath;

            var i = 0;
            while (true)
            {
                if (AssetDatabase.LoadAssetAtPath<T>(newAssetPath) != null)
                {
                    i++;
                    newAssetPath = $"{assetPath.Split('.').First()} {i}.asset";
                    continue;
                }

                break;
            }

            return newAssetPath;
        }

        /// <summary>
        /// Возвращает префаб инстанса.
        /// </summary>
        /// <param name="instance">Инстанс префаба.</param>
        public static GameObject GetPrefab(this Object instance)
        {
            var prefabRoot = PrefabUtility.GetPrefabParent(instance);
            var assetPath = AssetDatabase.GetAssetPath(prefabRoot);
            return AssetDatabase.LoadAssetAtPath<GameObject>(assetPath).gameObject;
        }

        
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
        /// Отображает строку с заголовком и полем для объекта.
        /// </summary>
        /// <param name="labelText">Текст заголовка.</param>
        /// <param name="value">Значение поля для ввода.</param>
        /// <returns>Строка ввода.</returns>
        public static T ShowLine<T>(string labelText, T value) where T: Object
        {
            var result = value;
            ShowInput(labelText, () => result = (T)EditorGUILayout.ObjectField(value, typeof(T), false));
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