using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;

    [SerializeField] private GameObject _ribPrefab;

    [SerializeField] private float _damage;

    private void Awake()
    {
        _enemyStats = GetComponentInParent<EnemyStats>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _damage = _enemyStats.Damage;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LaunchRib()
    {
        Instantiate(_ribPrefab, transform.position, Quaternion.identity);
    }
}
