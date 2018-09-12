namespace Entities
{
    using System.Linq;

    using Data;

    using UnityEngine;

    /// <summary>
    /// Юнит.
    /// </summary>
    public class Unit : MonoBehaviour
    {
        /// <summary>
        /// Текущиее здоровье юнита.
        /// </summary>
        [SerializeField]
        private float _currentHealth;

        /// <summary>
        /// Подвижность.
        /// </summary>
        [SerializeField]
        private float _currentMobility;

        /// <summary>
        /// Скорость передвижения.
        /// </summary>
        [SerializeField]
        private float _currentMovingSpeed;

        /// <summary>
        /// Скорострельность.
        /// </summary>
        [SerializeField]
        private float _currentShootingSpeed;

        /// <summary>
        /// Строковый идетификатор юнита.
        /// </summary>
        public string Sid;

        /// <summary>
        /// Инициализация характеристик коробля.
        /// </summary>
        private void InitializeAttributes()
        {
            var baseShipStats =
                UnitAttributes.Instance.ShipBaseAttributes.FirstOrDefault(stats => Sid.Contains(stats.Type));
            var shipStatsMultipliers =
                UnitAttributes.Instance.ShipAttributesMultipliers.FirstOrDefault(stats => stats.Type == Sid);

            if (baseShipStats == null || shipStatsMultipliers == null)
            {
                Debug.Log($"Не удалось найти характеристики: {Sid}");
                return;
            }

            _currentHealth = baseShipStats.Health * shipStatsMultipliers.Health;
            _currentShootingSpeed = baseShipStats.ShootSpeed * shipStatsMultipliers.ShootSpeed;
            _currentMovingSpeed = baseShipStats.MoveSpeed * shipStatsMultipliers.MoveSpeed;
            _currentMobility = baseShipStats.Mobility * shipStatsMultipliers.Mobility;
        }

        private void Start()
        {
            InitializeAttributes();
        }
    }
}