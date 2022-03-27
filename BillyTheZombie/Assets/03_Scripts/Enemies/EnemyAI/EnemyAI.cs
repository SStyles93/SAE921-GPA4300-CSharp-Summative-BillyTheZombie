using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    private AIPath _aIPath;
    private EnemyRayCaster _rayCaster;
    private AIDestinationSetter _destinationSetter;
    private EnemyStats _enemyStats;
    private EnemyVisuals _enemyVisuals;

    [SerializeField] private float recoveryTimer = 1.0f;
    private float recoveryTime;
    [SerializeField] private float attackTimer = 2.0f;
    [SerializeField] private float attackTime;
    [SerializeField] private bool isHit = false;

    //[SerializeField] float _searchRange = 1.0f;
    //[SerializeField] float _searchMagnitude = 1.0f;

    private void Awake()
    {
        _aIPath = GetComponent<AIPath>();
        _rayCaster = GetComponentInChildren<EnemyRayCaster>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
        _enemyStats = GetComponent<EnemyStats>();
        _enemyVisuals = GetComponentInChildren<EnemyVisuals>();
    }
    private void Start()
    {
        recoveryTime = recoveryTimer;
        attackTime = attackTimer;
        _aIPath.maxSpeed = _enemyStats.Speed;
        _destinationSetter.target = _rayCaster.Target;
    }

    private void Update()
    {
        if(isHit == true)
        {
            _aIPath.canMove = false;
            recoveryTime -= Time.deltaTime;
        }
        if(recoveryTime <= 0.0f)
        {
            recoveryTime = recoveryTimer;
            _aIPath.canMove = true;
            isHit = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isHit = true;
        }
        //Colors the enemy on hit
        if (collision.gameObject.GetComponent<Arm>())
        {
            _enemyVisuals.HitEffect();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            if (_rayCaster.PlayerInSight)
            {
                _aIPath.canMove = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerStats>()) 
        {
            if (_rayCaster.PlayerInSight)
            {
                _aIPath.canMove = false;
                attackTime -= Time.deltaTime;
            }
            if(attackTime < 0.0f)
            {
                collision.GetComponent<PlayerStats>().Health -= _enemyStats.Damage;
                attackTime = attackTimer;
                _enemyVisuals.Attack = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _aIPath.canMove = true;
        _enemyVisuals.Attack = false;
    }
}
