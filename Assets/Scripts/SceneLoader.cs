using Data;

using Entities;

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadScene(LevelUnitPositions levelUnitPositions)
    {
        SceneManager.GetActiveScene();

        ClearScene();
        
        FillScene(levelUnitPositions);
    }

    private static void FillScene(LevelUnitPositions levelUnitPositions)
    {
        
    }
    
    private static void ClearScene()
    {
        var units = FindObjectsOfType<Unit>();

        foreach (var unit in units)
        {
            Destroy(unit);
        }
    }
}