using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterAttack : MonoBehaviour
{
    [SerializeField] private EnemyStats _enemyStats;
    [SerializeField] private EnemyRayCaster _enemyRayCaster;

    [SerializeField] private GameObject _ribPrefab;

    [SerializeField] private float _damage = 1.0f;
    [SerializeField] private float _ribSpeed = 0.5f;

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
        GameObject currentRib = Instantiate(_ribPrefab, transform.position, Quaternion.identity);
        Rib rib = currentRib.GetComponent<Rib>();
        rib.RibDirection = _enemyRayCaster.Target.position - transform.position;
        rib.Damage = _damage;
        rib.Speed = _ribSpeed;

    }
}
