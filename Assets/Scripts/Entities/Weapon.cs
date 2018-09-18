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
        private float _cooldown;

        [HideInInspector]
        public float Damage;

        public Projectile Projectile;

        [HideInInspector]
        public float ShootingSpeed;

        public void Shoot()
        {
            if (!CanShoot())
                return;

            Projectile = ObjectsPoolsManager.Instance.SpawnFromPool(Projectile.Sid, transform.position,
                Quaternion.Euler(90, transform.parent.rotation.eulerAngles.y, 0f)).GetComponent<Projectile>();


            if (transform.parent.GetComponent<Player>())
                Projectile.Target = typeof(Enemy);
            else
                Projectile.Target = typeof(Player);

            Projectile.Damage = Damage;

            Cooldown();
        }

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