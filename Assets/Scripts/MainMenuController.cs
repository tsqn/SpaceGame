using UnityEngine;

/// <summary>
/// Контроллер главного меню.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    /// <summary>
    /// Выход из игры.
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Старт игры.
    /// </summary>
    public void StartGame()
    {
        ScenesManager.SwitchScene("PlayScene");
    }
}