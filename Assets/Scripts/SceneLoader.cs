using Data;

using Entities;

using UnityEditor;

using UnityEngine;
using UnityEngine.SceneManagement;

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
            var unit = (GameObject)PrefabUtility.InstantiatePrefab(unitPositionModel.GameObject);
            unit.transform.position = unitPositionModel.Position;
            unit.transform.rotation = unitPositionModel.Rotation;
        }
    }

    /// <summary>
    /// Удаляет всех юнитов со сцены.
    /// </summary>
    private static void ClearScene()
    {
        var units = FindObjectsOfType<Unit>();

        foreach (var unit in units)
        {
            unit.gameObject.SetActive(false);
            DestroyImmediate (unit.gameObject);
        }
    }
}