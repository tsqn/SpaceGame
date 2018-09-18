using System;

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
    /// <summary>
    /// Оставшееся время жизни.
    /// </summary>
    private float _currentLifeTime;

    [SerializeField]
    private Rigidbody _rigidbody;

    /// <summary>
    /// Урон снаряда.
    /// </summary>
    public float Damage;

    /// <summary>
    /// Звук попадания.
    /// </summary>
    public AudioClip HitClip;

    /// <summary>
    /// Время жизни станярда.
    /// </summary>
    public float LifeTime;

    /// <summary>
    /// Скорость полёта.
    /// </summary>
    public float Speed;

    /// <summary>
    /// Тип поражаемой цели.
    /// </summary>
    public Type Target;

    /// <summary>
    /// Запуск снаряда.
    /// </summary>
    public void Launch()
    {
        _rigidbody.AddForce(transform.rotation * Vector3.right * Speed, ForceMode.Impulse);
    }

    public override void OnObjectSpawn()
    {
        _rigidbody.velocity = Vector3.zero;
        _currentLifeTime = LifeTime;
        Launch();
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    /// <summary>
    /// Отключение снаряда.
    /// </summary>
    private void Disable()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Взрыв снаряда.
    /// </summary>
    private void Explode()
    {
        AudioManager.Instance.PlayOneShot(HitClip);

        ObjectsPoolsManager.Instance.SpawnParticleFromPool("RocketExplosionVFX", transform.position);
        Disable();
    }

    /// <summary>
    /// Возвращает цель для попадания.
    /// </summary>
    /// <param name="collision">Колизия.</param>
    private Unit GetHitTarget(Collider collision)
    {
        if (Target == typeof(Enemy))
            return collision.gameObject.GetComponent<Enemy>();
        if (Target == typeof(Player))
            return collision.gameObject.GetComponent<Player>();
        return null;
    }


    private void OnTriggerEnter(Collider other)
    {
        var target = GetHitTarget(other);

        if (target == null)
            return;

        target.ReceiveDamage(Damage);
        Explode();
    }

    private void Update()
    {
        _currentLifeTime -= Time.deltaTime;

        if (_currentLifeTime < 0)
            Disable();
    }
}