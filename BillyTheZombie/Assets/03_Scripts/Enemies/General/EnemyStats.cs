using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO _enemyStatsSO;
    [SerializeField] private EnemyVisuals _enemyVisuals;

    [SerializeField] private GameStatsSO _gameStats;

    [SerializeField] private float _mutagenValue;
    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    public float Speed { get => _speed; private set => _speed = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public void Awake()
    {
        _enemyVisuals = GetComponentInChildren<EnemyVisuals>();

        _mutagenValue = _enemyStatsSO._mutagenValue;
        _health = _enemyStatsSO._health;
        _speed = _enemyStatsSO._speed;
        _damage = _enemyStatsSO._damage;
    }

    void Update()
    {
        if(_health <= 0.0f)
        {
            Die();
        }
    }

    /// <summary>
    /// Lowers the enemy's health according to the damage
    /// </summary>
    /// <param name="damage">amount of damage delt</param>
    public void TakeDamage(float damage)
    {
        _health -= damage;
        _enemyVisuals.HitEffect();
    }

    /// <summary>
    /// Destroys the GameObject
    /// </summary>
    private void Die()
    {
        _gameStats.mutagenPoints += _mutagenValue;
        Destroy(gameObject);
    }
}