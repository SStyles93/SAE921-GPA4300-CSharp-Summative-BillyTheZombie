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
    //Refenrence Components
    private Rigidbody2D _rb;

    //ArmType
    [SerializeField] private ARMSIDE armSide;
    private enum ARMTYPE
    {
        BASIC,
        EXPLOSIVE,
        LAWNMOWER,
        BOOMERANG
    }
    [SerializeField] private ARMTYPE armType;

    //Arm stats
    [SerializeField] private float _speed = 1.0f;
    private float _damage = 1.0f;

    //ArmThrow
    private Vector3 _armDirection;
    [Tooltip("Updates the phisical movement of the Arm")]
    private bool _canMove;
    [Tooltip("Updates the phisical movement of the Arm")]
    private bool _canBePickedUp = false;

    //Vec3 Used to Boomerang
    private Vector3 throwPosition;

    //Properties
    public Vector3 ArmDirection { get => _armDirection; set => _armDirection = value; }
    public float Damage { get => _damage; set => _damage = value; }
    public Vector3 ThrowPosition { get => throwPosition; set => throwPosition = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        switch (armType)
        {
            case ARMTYPE.BASIC:
                _rb.drag = 3.0f;
                _rb.sharedMaterial.bounciness = 0.25f;
                _speed = 1.0f;
                break;
            case ARMTYPE.EXPLOSIVE:
                _rb.drag = 10.0f;
                _rb.sharedMaterial.bounciness = 0.0f;
                _speed = 1.0f;
                break;
            case ARMTYPE.LAWNMOWER:
                _rb.drag = 0.1f;
                _rb.sharedMaterial.bounciness = 0.0f;
                _speed = 1.0f;
                _rb.mass = 10.0f;
                break;
            case ARMTYPE.BOOMERANG:
                _rb.drag = 0.0f;
                _rb.sharedMaterial.bounciness = 1.0f;
                _speed = 2.0f;
                break;
        }

        _canMove = true;
        _canBePickedUp = false;
    }

    private void Update()
    {
            Move();   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (armType)
        {
            case ARMTYPE.BOOMERANG:
                _rb.velocity = Vector2.zero;
                _canMove = false;
                _canBePickedUp = false;
                if (!_canBePickedUp)
                {
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    _canBePickedUp = true;
                }
                break;
            case ARMTYPE.LAWNMOWER:
                _canMove = true;
                break;
            default:
                _canMove = false;
                //Deals damage once before enabling pickup
                if (!_canBePickedUp)
                {
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    _canBePickedUp = true;
                }
                break;
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(armType == ARMTYPE.LAWNMOWER)
        {
            collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
            _canBePickedUp = true;
        }
    }
    
    /// <summary>
    /// Updates the movement of the Arm
    /// </summary>
    private void Move()
    {
        if (_canMove)
        {
            //Physic movement
            _rb.AddForce(_armDirection * _speed / 10.0f, ForceMode2D.Impulse);
        }
        //Boomerang return
        if (armType == ARMTYPE.BOOMERANG && !_canMove)
        {
            transform.position = Vector3.Lerp(transform.position, throwPosition, _speed * 2.0f * Time.deltaTime);
        }
        
        
    }

    /// <summary>
    /// This OnTiggerEnter2D is used to let the Arm be picked up by the player
    /// </summary>
    /// <param name="collision">collision with player</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && _canBePickedUp)
        {
            collision.gameObject.GetComponent<PlayerActions>().EnablePlayersArm(armSide, true);
            Destroy(gameObject);
        }
    }
}
