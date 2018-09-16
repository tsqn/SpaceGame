using Entities;

using Managers;

using UnityEngine;

/// <summary>
/// Снаряд.
/// </summary>
[RequireComponent(typeof(CapsuleCollider))]
[RequireComponent(typeof(Rigidbody))]
public class Projectile : Entity
{
    [SerializeField]
    private Rigidbody _rigidbody;
    public float Damage;

    public AudioClip HitClip;

    /// <summary>
    /// Скорость полёта.
    /// </summary>
    public float Speed;

    /// <summary>
    /// Запуск снаряда.
    /// </summary>
    public void Launch()
    {
        _rigidbody.AddForce(Vector3.forward * Speed, ForceMode.Impulse);
    }

    public override void OnObjectSpawn()
    {
        Launch();
    }

    /// <summary>
    /// Взрыв снаряда.
    /// </summary>
    private void Explode()
    {
        AudioManager.Instance.PlayOneShot(HitClip);
        var vfx = ObjectsPoolsManager.Instance.SpawnFromPool("RocketExplosionVFX", transform.position,
            transform.rotation);
        vfx.GetComponent<ParticleSystem>().Play();
        gameObject.SetActive(false);
    }


    private void OnTriggerEnter(Collider other)
    {
        var unit = other.gameObject.GetComponent<Unit>();

        if (unit == null)
            return;

        unit.ReceiveDamage(Damage);
        Explode();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}