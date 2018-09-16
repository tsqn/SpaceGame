namespace Managers
{
    using Entities;

    using Models;

    using UnityEngine;

    /// <summary>
    /// Менеджер уровня.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        /// <summary>
        /// Спрайт бекграунда.
        /// </summary>
        public Sprite Background;

        /// <summary>
        /// Название уровня.
        /// </summary>
        public string Name;

        /// <summary>
        /// Точка спавна игрока.
        /// </summary>
        public PlayerSpawnPoint PlayerSpawnPoint;

        /// <summary>
        /// Расположение юнитов.
        /// </summary>
        public LevelUnitPositions UnitPositions;

        /// <summary>
        /// Список юнитов.
        /// </summary>
        public UnitsCollection UnitsCollection;

        private void Start()
        {
            if (PlayerSpawnPoint == null)
                PlayerSpawnPoint = FindObjectOfType<PlayerSpawnPoint>();

            Name = UnitPositions.Name;
            SceneLoader.LoadScene(UnitPositions);

            var player = UnitsCollection.GetUnitPrefabBySid(SettingsManager.Instance.PlayerShipSid);
            Instantiate(player, PlayerSpawnPoint.transform.position, PlayerSpawnPoint.transform.rotation);
        }
    }
}