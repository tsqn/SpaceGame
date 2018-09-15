using Data;

using Entities;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Загрузчик сцен.
/// </summary>
public class SceneLoader : MonoBehaviour
{
    /// <summary>
    /// Загружает указанный уровень.
    /// </summary>
    /// <param name="levelUnitPositions">Описание уровня.</param>
    public static void LoadScene(LevelUnitPositions levelUnitPositions)
    {
        SceneManager.GetActiveScene();

        ClearScene();
        
        FillScene(levelUnitPositions);
    }

    /// <summary>
    /// Заполняет активную сцену в соответвии с описанием.
    /// </summary>
    /// <param name="levelUnitPositions">Описание положения юнитов.</param>
    private static void FillScene(LevelUnitPositions levelUnitPositions)
    {
        foreach (var unitPositionModel in levelUnitPositions.UnitPositionModels)
        {
#if UNITY_EDITOR
            var unit = (GameObject)PrefabUtility.InstantiatePrefab(unitPositionModel.GameObject);
#else
            var unit = (GameObject)Instantiate(unitPositionModel.GameObject);
#endif
            unit.transform.position = unitPositionModel.Position;
            unit.transform.rotation = unitPositionModel.Rotation;
        }
    }

    /// <summary>
    /// Удаляет всех юнитов со сцены.
    /// </summary>
    public static void ClearScene()
    {
        var units = FindObjectsOfType<Unit>();

        foreach (var unit in units)
        {
            unit.gameObject.SetActive(false);
            DestroyImmediate (unit.gameObject);
        }
    }
}