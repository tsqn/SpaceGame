using System;
using System.Linq;
using System.Text.RegularExpressions;

/// <summary>
/// Утилитарные методы.
/// </summary>
public static class Utils
{
    /// <summary>
    /// Дефолтный путь до папки с данными.
    /// </summary>
    private const string DEFAULT_PACKAGE_ROOT = "Assets/Resources/Data";

    /// <summary>
    /// Возвращает путь до папки с таддными.
    /// </summary>
    public static string DataRoot => DEFAULT_PACKAGE_ROOT;

    /// <summary>
    /// Возвращает путь до папки с данными уровней.
    /// </summary>
    public static string LevelsDataPath => $"{DataRoot}/Levels";

    /// <summary>
    /// Возвращает путь до папки с данными кораблей.
    /// </summary>
    public static string ShipStatsDataPath => $"{DataRoot}/Ships";

    /// <summary>
    /// Возвращает путь до ресурса.
    /// </summary>
    /// <param name="resourceType">Тип ресурсов.</param>
    /// <param name="fileName">Имя файла ресурса.</param>
    public static string GetResourcePath(ResourceType resourceType, string fileName)
    {
        string dataRoot;
        switch (resourceType)
        {
            case ResourceType.General:
                dataRoot = DataRoot;
                break;
            case ResourceType.Ship:
                dataRoot = ShipStatsDataPath;
                break;
            case ResourceType.Level:
                dataRoot = LevelsDataPath;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(resourceType), resourceType, null);
        }

        var path = $"{dataRoot}/{fileName}";

#if !UNITY_EDITOR
        path = path.Replace("Assets/Resources/", string.Empty);
        path = path.Replace($".{path.Split('.').Last()}", string.Empty);
#endif

        return path;
    }

    /// <summary>
    /// Разделяет слова написанные в CamelCase.
    /// </summary>
    /// <param name="inputString">Строка для разделения.</param>
    /// <returns>"Word1 Word2"</returns>
    public static string SplitByCamelCase(string inputString)
    {
        var result = inputString;

        var splitString = Regex.Split(inputString, @"(?<!^)(?=[A-Z])").ToList();

        if (splitString.Count > 1 && !result.Contains(" "))
            result = splitString.Aggregate("", (current, word) => current + $"{word} ");


        return result.Trim();
    }
}