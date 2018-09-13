using System;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEditor;

using UnityEngine;

using Object = UnityEngine.Object;

public static class Utils
{
    private const string DEFAULT_PACKAGE_ROOT = "Assets/Resources/Data";

    public static string DataRoot
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

    public static string ShipStatsDataPath => $"{DataRoot}/Ships";


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
    /// Split string by capital symbols.
    /// </summary>
    /// <param name="inputString">String to split.</param>
    /// <returns>"Word1 Word2"</returns>
    public static string SplitByCamelCase(string inputString)
    {
        var result = "";

        var splitString = Regex.Split(inputString, @"(?<!^)(?=[A-Z])").ToList();

        if (splitString.Count > 1 && !result.Contains(" "))
            result = splitString.Aggregate(result, (current, word) => current + $"{word} ");


        return result.Trim();
    }
}