namespace Editor
{
    using UnityEditor;

    public static class EditorShortcuts
    {
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