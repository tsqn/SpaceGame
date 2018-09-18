namespace Entities
{
    using System.Collections;

    using Managers;

    using UnityEngine;

    /// <summary>
    /// Оружее.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        /// <summary>
        /// Время до перезарядки.
        /// </summary>
        private float _cooldown;

        /// <summary>
        /// Урон оружия.
        /// </summary>
        [HideInInspector]
        public float Damage;

        /// <summary>
        /// Снаряд.
        /// </summary>
        public Projectile Projectile;

        /// <summary>
        /// Скорость стрельбы.
        /// </summary>
        [HideInInspector]
        public float ShootingSpeed;

        /// <summary>
        /// Выстрел.
        /// </summary>
        public void Shoot()
        {
            if (!CanShoot())
                return;

            Projectile = ObjectsPoolsManager.Instance.SpawnFromPool(Projectile.Sid, transform.position,
                Quaternion.Euler(90, transform.parent.rotation.eulerAngles.y, 0f)).GetComponent<Projectile>();

            Projectile.Target = transform.parent.GetComponent<Player>() ? typeof(Enemy) : typeof(Player);

            Projectile.Damage = Damage;

            Cooldown();
        }

        /// <summary>
        /// Можно ли стрелять.
        /// </summary>
        private bool CanShoot()
        {
            return _cooldown <= 0;
        }

        /// <summary>
        /// Колдаун.
        /// </summary>
        private void Cooldown()
        {
            _cooldown = 1 / ShootingSpeed;
            StartCoroutine(Reload());
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