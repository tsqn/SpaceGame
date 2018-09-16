namespace Entities
{
    using Managers;

    using UnityEngine;

    /// <summary>
    /// Оружее.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        public Projectile Projectile;

        public void Shoot()
        {
            ObjectsPoolsManager.Instance.SpawnFromPool(Projectile.Sid, transform.position);
        }

        private void Start()
        {
            Shoot();
        }
    }
}