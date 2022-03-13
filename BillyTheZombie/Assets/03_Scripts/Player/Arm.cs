using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ARMSIDE
{
    RIGHT,
    LEFT
}

public class Arm : MonoBehaviour
{
    [SerializeField] private ARMSIDE armSide;

    //Arm stats
    [SerializeField] private float _speed = 1.0f;
    private float _damage = 1.0f;

    //ArmThrow
    private Vector3 _armDirection;
    private bool _canMove;
    private bool _canHurt;

    //Properties
    public Vector3 ArmDirection { get => _armDirection; set => _armDirection = value; }
    public float Damage { get => _damage; set => _damage = value; }

    private void Start()
    {
        _canMove = true;
        _canHurt = true;
    }

    private void Update()
    {
        if (_canMove)
        {
            Move();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _canMove = false;
        
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerActions>().EnablePlayersArm(armSide, true);
            Destroy(gameObject);
        }
        if (collision.gameObject.GetComponent<EnemyStats>())
        {
            if (_canHurt)
            {
                collision.gameObject.GetComponent<EnemyStats>().TakeDamage(_damage);
                _canHurt = false;
            }
        }
    }
    private void Move()
    {
        //transform.Translate(_armDirection * _speed * Time.deltaTime);
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(_armDirection * _speed, ForceMode2D.Impulse);
    }
}
