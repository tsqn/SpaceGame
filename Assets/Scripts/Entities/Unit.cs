namespace Entities
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    using Models;

    using UnityEngine;

    /// <summary>
    /// Юнит.
    /// </summary>
    public class Unit : Entity
    {
        private float _cooldown;

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
        /// Оружие юнита.
        /// </summary>
        public List<Weapon> Weapons;

        private float _weaponDamage;

        /// <summary>
        /// Получение урона.
        /// </summary>
        /// <param name="damage">Колличество урона.</param>
        public void ReceiveDamage(float damage)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Death();
        }

        /// <summary>
        /// Смерть юнита.
        /// </summary>
        protected virtual void Death()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Выстрел.
        /// </summary>
        protected void Shoot()
        {
            if (!CanShoot())
                return;

            Weapons.ForEach(weapon => weapon.Shoot());
            _cooldown = 1 / _currentShootingSpeed;
            StartCoroutine(Reload());
        }

        private void Awake()
        {
            InitializeAttributes();
            Weapons = GetComponentsInChildren<Weapon>().ToList();
            Weapons.ForEach(weapon => weapon.Damage = _weaponDamage);
        }

        /// <summary>
        /// Проверка на возможность выстрела.
        /// </summary>
        private bool CanShoot()
        {
            return _cooldown <= 0;
        }

        /// <summary>
        /// Инициализация характеристик ко-робля.
        /// </summary>
        private void InitializeAttributes()
        {
            var baseShipStats =
                UnitAttributes.Instance.ShipBaseAttributes.FirstOrDefault(stats => Sid.Contains(stats.Type));
            var shipStatsMultipliers =
                UnitAttributes.Instance.ShipAttributesMultipliers.FirstOrDefault(stats => stats.Type == Sid);

            if (baseShipStats == null || shipStatsMultipliers == null)
            {
                Debug.LogWarning($"Не удалось найти характеристики: {Sid}");
                return;
            }

            _currentHealth = baseShipStats.Health * shipStatsMultipliers.Health;
            _currentShootingSpeed = baseShipStats.ShootSpeed * shipStatsMultipliers.ShootSpeed;
            _currentMovingSpeed = baseShipStats.MoveSpeed * shipStatsMultipliers.MoveSpeed;
            _currentMobility = baseShipStats.Mobility * shipStatsMultipliers.Mobility;
            _weaponDamage = shipStatsMultipliers.WeaponDamage;
        }

        /// <summary>
        /// Перезарядка оружия.
        /// </summary>
        private IEnumerator Reload()
        {
            while (_cooldown >= 0)
            {
                _cooldown -= Time.deltaTime;
                yield return null;
            }
        }
    }
}