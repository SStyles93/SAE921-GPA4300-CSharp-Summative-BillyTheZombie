using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaserAttack : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;

    private void Awake()
    {
        _enemyStats.GetComponentInParent<EnemyStats>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses the Trigger to hit
        collision.GetComponent<PlayerStats>()?.TakeDamage(_enemyStats.Damage);
    }
}
