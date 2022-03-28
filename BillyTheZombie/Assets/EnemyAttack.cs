using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;

    private void Awake()
    {
        _enemyStats.GetComponentInParent<EnemyStats>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>())
        {
            collision.GetComponent<PlayerStats>().TakeDamage(_enemyStats.Damage);
        }
    }
}
