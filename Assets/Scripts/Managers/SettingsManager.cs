using UnityEngine;

namespace Managers
{
    /// <summary>
    /// Глобальные настройки.
    /// </summary>
    public class SettingsManager : MonoBehaviour
    {
        /// <summary>
        /// Дефолтный корабль.
        /// </summary>
        private const string DEFAULT_PLAYER_SHIP_SID = "Player 1";

        /// <summary>
        /// Инстанс настроек (синглтон).
        /// </summary>
        public static SettingsManager Instance;

        /// <summary>
        /// Текущий корабль игрока.
        /// </summary>
        public string PlayerShipSid;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else if (Instance != this)
                Destroy(gameObject);

            DontDestroyOnLoad(gameObject);

            Initialize();
        }

        /// <summary>
        /// Инициализирует настройки.
        /// </summary>
        private void Initialize()
        {
            if (string.IsNullOrEmpty(PlayerShipSid))
                PlayerShipSid = DEFAULT_PLAYER_SHIP_SID;
        }
    }
}