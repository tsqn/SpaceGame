namespace Data
{
    using System;

    using UnityEditor;

    public static class Utils
    {
        private const string DEFAULT_PACKAGE_ROOT = "Assets/Data";

        public static string PackageRoot
        {
            get
            {
#if UNITY_EDITOR
                var guids = AssetDatabase.FindAssets("PresentationWindow t:Script");
                if (guids.Length == 0) 
                    return DEFAULT_PACKAGE_ROOT;

                var path = AssetDatabase.GUIDToAssetPath(guids[0]);
                return path.Substring(0, path.IndexOf("Editor/PresentationWindow.cs", StringComparison.Ordinal));
#else
				return DEFAULT_PACKAGE_ROOT;
#endif
            }
        }
    }
}