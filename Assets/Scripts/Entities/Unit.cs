namespace Entities
{
    using System.Collections.Generic;
    using System.Linq;

    using Managers;

    using Models;

    using UnityEngine;

    /// <summary>
    /// Юнит.
    /// </summary>
    public class Unit : Entity
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

        [SerializeField]
        private AudioClip _deathClip;

        /// <summary>
        /// Урон оружия.
        /// </summary>
        [SerializeField]
        private float _weaponDamage;

        /// <summary>
        /// Оружие юнита.
        /// </summary>
        public List<Weapon> Weapons;

        /// <summary>
        /// Получение урона.
        /// </summary>
        /// <param name="damage">Колличество урона.</param>
        public void ReceiveDamage(float damage)
        {
            var avoidChance = 100 - _currentMobility;
            var chance = Random.Range(0, 100);

            if (chance > avoidChance)
                return;

            _currentHealth -= damage;

            if (_currentHealth <= 0)
                Death();
        }

        /// <summary>
        /// Смерть юнита.
        /// </summary>
        protected virtual void Death()
        {
            AudioManager.Instance.PlayOneShot(_deathClip);
            ObjectsPoolsManager.Instance.SpawnFromPool("ShipExplosionVFX", transform.position,
                transform.rotation);
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Выстрел.
        /// </summary>
        protected void Shoot()
        {
            Weapons.ForEach(weapon => weapon.Shoot());
        }

        private void Awake()
        {
            InitializeAttributes();
            Weapons = GetComponentsInChildren<Weapon>().ToList();
            Weapons.ForEach(weapon =>
            {
                weapon.Damage = _weaponDamage;
                weapon.ShootingSpeed = _currentShootingSpeed;
            });
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
    }
}