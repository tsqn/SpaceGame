using UnityEngine;

public class Settings : MonoBehaviour
{
    private const string DEFAULT_PLAYER_SHIP_SID = "Player 1";
    public static Settings Instance;

    public string PlayerShipSid;

    // Метод инициализации менеджера
    private void InitializeManager()
    {
        if (string.IsNullOrEmpty(PlayerShipSid))
            PlayerShipSid = DEFAULT_PLAYER_SHIP_SID;
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

        InitializeManager();
    }
}