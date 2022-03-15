using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [SerializeField] private EnemyStatsSO enemyStats;

    [SerializeField] private float _health;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;

    private void Awake()
    {
        
    }

    void Start()
    {
        _health = enemyStats._health;
        _speed = enemyStats._speed;
        _damage = enemyStats._damage;
    }

    // Update is called once per frame
    void Update()
    {
        if(_health <= 0.0f)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Lowers the enemy's health according to the damage
    /// </summary>
    /// <param name="damage">amount of damage delt</param>
    public void TakeDamage(float damage)
    {
        _health -= damage;
    }
}
