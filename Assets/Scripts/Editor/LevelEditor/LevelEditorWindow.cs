namespace Editor.LevelEditor
{
    using UnityEditor;

    using UnityEngine;

    public class LevelEditorWindow : EditorWindow
    {
        private static LevelEditorWindow _levelEditorWindow;

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Level Editor")]
        public static void ShowWindow()
        {
            _levelEditorWindow = GetWindow<LevelEditorWindow>(false, "Level Editor");
            _levelEditorWindow.minSize = new Vector2(300, 80);
        }
    }
}