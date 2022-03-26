using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //Refenrence Components
    private Rigidbody2D _rb;

    //ArmType
    [SerializeField] private BODYPART armSide;
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
    [SerializeField] private float _damage = 1.0f;
    [SerializeField] private float _explosiveArmRadius = 1.0f;
    [SerializeField] private float _pushPower = 50.0f;

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
    public float PushPower { get => _pushPower; set => _pushPower = value; }

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
                GetComponent<CircleCollider2D>().radius = _explosiveArmRadius;
                GetComponent<CircleCollider2D>().enabled = false;
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
                if (!collision.gameObject.GetComponent<PlayerActions>())
                {
                    //on collision stops the rb from moving
                    _rb.velocity = Vector2.zero;
                    //stops applying force to the object
                    _canMove = false;
                }
                else
                {
                    _canBePickedUp = false;
                    //If the collision is with the player Ignore
                    Physics2D.IgnoreCollision(
                        transform.GetComponent<BoxCollider2D>(),
                        collision.gameObject.GetComponent<CapsuleCollider2D>());
                }
                if (!_canBePickedUp)
                {
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    _canBePickedUp = true;
                }
                break;
                
            case ARMTYPE.LAWNMOWER:
                _canMove = true;
                break;

            case ARMTYPE.EXPLOSIVE:
                
                if (!collision.gameObject.GetComponent<PlayerActions>())
                {
                    //enables the outer collider
                    GetComponent<CircleCollider2D>().enabled = true;
                    GetComponent<BoxCollider2D>().isTrigger = true;
                    //on collision stops the rb from moving
                    _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    //stops applying force to the object
                    _canMove = false;

                    if (collision.gameObject.GetComponent<EnemyStats>())
                    {
                        //Send enemy in opposite direction from player
                        Vector2 forceDirection = collision.gameObject.transform.position -
                            gameObject.transform.position;
                        collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * PushPower, ForceMode2D.Force);

                        collision.gameObject.GetComponent<EnemyStats>().TakeDamage(_damage);
                    }

                }
                else
                {
                    
                    _canBePickedUp = false;
                    //If the collision is with the player Ignore
                    Physics2D.IgnoreCollision(
                        transform.GetComponent<CircleCollider2D>(),
                        collision.gameObject.GetComponent<CapsuleCollider2D>());
                    Physics2D.IgnoreCollision(
                           transform.GetComponent<CircleCollider2D>(),
                           collision.gameObject.GetComponent<BoxCollider2D>());
                    
                }
                if (!_canBePickedUp)
                {
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    _canBePickedUp = true;
                }
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
        switch (armType)
        {
            case ARMTYPE.BOOMERANG:
                if (collision.gameObject.GetComponent<PlayerController>())
                {
                    _canBePickedUp = true;
                }
                break;

            case ARMTYPE.LAWNMOWER:
                collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                _canBePickedUp = true;
                break;
            default:
                break;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (armType)
        {
            case ARMTYPE.EXPLOSIVE:
                if (collision.gameObject.GetComponent<PlayerController>())
                {
                    Physics2D.IgnoreCollision(
                                transform.GetComponent<CircleCollider2D>(),
                                collision.gameObject.GetComponent<CapsuleCollider2D>());
                    Physics2D.IgnoreCollision(
                           transform.GetComponent<CircleCollider2D>(),
                           collision.gameObject.GetComponent<BoxCollider2D>());

                    if (collision.gameObject.GetComponent<PlayerController>() && _canBePickedUp)
                    {
                        collision.gameObject.GetComponent<PlayerActions>()?.EnablePlayersArm(armSide, true);
                        Destroy(gameObject);
                    }
                }
                break;
            default:
                if (collision.gameObject.GetComponent<PlayerController>() && _canBePickedUp)
                {
                    collision.gameObject.GetComponent<PlayerActions>()?.EnablePlayersArm(armSide, true);
                    Destroy(gameObject);
                }
                break;
        }
        
        
    }
    
    /// <summary>
    /// Updates the movement of the Arm
    /// </summary>
    private void Move()
    {
        if (_canMove)
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            //Physic movement
            _rb.AddForce(_armDirection * _speed / 10.0f, ForceMode2D.Impulse);
            transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
        }
        else
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
        //Boomerang return
        if (armType == ARMTYPE.BOOMERANG && !_canMove)
        {
            transform.position = Vector3.Lerp(transform.position, throwPosition, _speed * 2.0f * Time.deltaTime);
        }
        if(armType == ARMTYPE.LAWNMOWER)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
        }
        
        
    }

}
