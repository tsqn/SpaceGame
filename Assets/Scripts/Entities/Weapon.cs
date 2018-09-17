namespace Entities
{
    using Managers;

    using UnityEngine;

    /// <summary>
    /// Оружее.
    /// </summary>
    public class Weapon : MonoBehaviour
    {
        public float Damage;
        public Projectile Projectile;

        public void Shoot()
        {
            Projectile = ObjectsPoolsManager.Instance.SpawnFromPool(Projectile.Sid, transform.position,
                Quaternion.Euler(90, transform.parent.rotation.eulerAngles.y, 0f)).GetComponent<Projectile>();
            Projectile.Damage = Damage;
        }
    }
}