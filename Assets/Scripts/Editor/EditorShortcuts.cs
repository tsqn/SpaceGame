namespace Editor
{
    using UnityEditor;

    using UnityEngine.SceneManagement;

    public static class EditorShortcuts
    {
        /// <summary>
        /// Перезагрузка сцены.
        /// </summary>
        [MenuItem("Tools/Reload Scene %q")]
        // Ctrl + Q
        private static void ReloadScene()
        {
            if (EditorApplication.isPlaying)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }

            EditorApplication.isPlaying = true;
        }

        /// <summary>
        /// Блокировка инспектора.
        /// </summary>
        [MenuItem("Tools/Toggle Inspector Lock %t")]
        // Ctrl + T
        private static void ToggleInspectorLock()
        {
            ActiveEditorTracker.sharedTracker.isLocked = !ActiveEditorTracker.sharedTracker.isLocked;
            ActiveEditorTracker.sharedTracker.ForceRebuild();
        }
    }
}