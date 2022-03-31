using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    //Reference Scripts
    private AIPath _aIPath;
    private EnemyRayCaster _rayCaster;
    private AIDestinationSetter _destinationSetter;
    private EnemyStats _enemyStats;
    private EnemyVisuals _enemyVisuals;

    //Variables
    [SerializeField] private float recoveryTimer = 1.0f;
    private float recoveryTime;
    [SerializeField] private float attackTimer = 2.0f;
    [SerializeField] private float attackTime;
    [SerializeField] private bool _stopMoving = false;

    [SerializeField] private float _detectionRadius = 0.0f;

    //Reference Component
    [SerializeField] private CircleCollider2D _colliderTrigger;
    [SerializeField] private BoxCollider2D _boxCollider;

    private void Awake()
    {
        _aIPath = GetComponent<AIPath>();
        _rayCaster = GetComponentInChildren<EnemyRayCaster>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
        _enemyStats = GetComponent<EnemyStats>();
        _enemyVisuals = GetComponentInChildren<EnemyVisuals>();

        _colliderTrigger = GetComponent<CircleCollider2D>();
        _boxCollider = GetComponent<BoxCollider2D>();
    }
    private void Start()
    {
        recoveryTime = recoveryTimer;
        attackTime = attackTimer;
        _aIPath.maxSpeed = _enemyStats.Speed;
        _destinationSetter.target = _rayCaster.Target;

        _detectionRadius = GetComponent<CircleCollider2D>().radius * 3f;
        _colliderTrigger.enabled = false;
    }

    private void Update()
    {
        if(_stopMoving == true)
        {
            _aIPath.canMove = false;
            recoveryTime -= Time.deltaTime;
        }
        if(recoveryTime <= 0.0f)
        {
            recoveryTime = recoveryTimer;
            _aIPath.canMove = true;
            _stopMoving = false;
        }

        float targetDistance = (_destinationSetter.target.position - transform.position).magnitude;
        if (_rayCaster.PlayerInSight)
        {
            _colliderTrigger.enabled = targetDistance < _detectionRadius ? true : false;
            
        }
        else
        {
            _colliderTrigger.enabled = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _stopMoving = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_rayCaster.PlayerInSight)
            {
                _aIPath.canMove = false;
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            if (_rayCaster.PlayerInSight)
            {
                _aIPath.canMove = false;
                attackTime -= Time.deltaTime;
            }
            else
            {
                _aIPath.canMove = true;
            }
            if(attackTime < 0.0f)
            {
                attackTime = attackTimer;
                //Launches the Attack of the enemy
                _enemyVisuals.Attack = true;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _aIPath.canMove = true;
            _enemyVisuals.Attack = false;
        }
        
    }
}
