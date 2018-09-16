namespace Editor.LevelsManager
{
    using Models;

    using UnityEditor;

    using UnityEngine;

    public class LevelsManager : EditorWindow
    {
        private static LevelsManager _levelsManager;
        private LevelsManagerView _levelsManagerView;
        private LevelsSequence _levelsSequence;
        private Vector2 _scrollPosition;

        /// <summary>
        /// Отобразить окно.
        /// </summary>
        [MenuItem("Window/Levels Manager")]
        public static void ShowWindow()
        {
            _levelsManager = GetWindow<LevelsManager>(false, "Levels Manager");
            _levelsManager.minSize = new Vector2(300, 80);
        }

        private void Load()
        {
            _levelsSequence = LevelsSequence.Instance;
        }

        private void OnEnable()
        {
            Load();
            _levelsManagerView = new LevelsManagerView(_levelsSequence.Levels);
        }

        private void OnGUI()
        {
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition, false, false,
                GUILayout.Width(_levelsManager.position.width),
                GUILayout.Height(_levelsManager.position.height));

            _levelsManagerView.Show();
            EditorGUILayout.EndScrollView();
        }
    }
}