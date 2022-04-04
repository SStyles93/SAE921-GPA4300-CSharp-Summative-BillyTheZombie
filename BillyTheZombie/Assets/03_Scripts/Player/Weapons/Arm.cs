using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : MonoBehaviour
{
    //Reference GameObject
    private GameObject _player;

    //Reference Components
    private Rigidbody2D _rb;
    private ParticleSystem _particleSystem;

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
    [SerializeField] private float _explosiveArmRadius = 0.5f;
    [SerializeField] private float _pushPower = 250.0f;
    private float _pickUpTimer = 0.5f;

    //ArmThrow
    private Vector3 _armDirection;
    [Tooltip("Updates the phisical movement of the Arm")]
    private bool _canMove;
    [Tooltip("Updates the phisical movement of the Arm")]
    private bool _canBePickedUp = false;

    //Vec3 Used to Boomerang
    private Vector3 throwPosition;
    //ParticleSystem
    private bool _particlewasPlayed = false;

    //Properties
    public float Damage { get => _damage; set => _damage = value; }
    public float PushPower { get => _pushPower; set => _pushPower = value; }
    public Vector3 ArmDirection { get => _armDirection; set => _armDirection = value; }
    public Vector3 ThrowPosition { get => throwPosition; set => throwPosition = value; }
    public GameObject Player { get => _player; set => _player = value; }

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
                _pickUpTimer = 1.0f;
                break;

            case ARMTYPE.BOOMERANG:
                _rb.drag = 0.0f;
                _rb.sharedMaterial.bounciness = 1.0f;
                _pickUpTimer = 0.25f;
                //If the collision is with the player Ignore
                Physics2D.IgnoreCollision(
                    transform.GetComponent<BoxCollider2D>(),
                    _player.gameObject.GetComponent<CapsuleCollider2D>());
                break;

            case ARMTYPE.LAWNMOWER:
                _rb.drag = 0.1f;
                _rb.sharedMaterial.bounciness = 0.0f;
                _rb.mass = 10.0f;
                _pickUpTimer = 2.0f;
                break;

            case ARMTYPE.EXPLOSIVE:
                _rb.drag = 10.0f;
                _rb.sharedMaterial.bounciness = 0.0f;
                _pickUpTimer = 2.0f;
                GetComponent<CircleCollider2D>().radius = _explosiveArmRadius;
                GetComponent<CircleCollider2D>().enabled = false;
                Physics2D.IgnoreCollision(
                        transform.GetComponent<CircleCollider2D>(),
                        _player.gameObject.GetComponent<CapsuleCollider2D>());
                _particleSystem = GetComponent<ParticleSystem>();
                _particleSystem.Stop();
                break;
        }

        _canMove = true;
        _canBePickedUp = false;
    }

    private void Update()
    {
        Move();
        _pickUpTimer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (armType)
        {
            case ARMTYPE.BOOMERANG:
                if (collision.gameObject.CompareTag("Player"))
                {
                    _canBePickedUp = true;
                    return;
                }
                //on collision stops the rb from moving
                _rb.velocity = Vector2.zero;
                //stops applying force to the object
                _canMove = false;
                if (!_canBePickedUp)
                {
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    _canBePickedUp = true;
                }
                break;
                
            case ARMTYPE.LAWNMOWER:
                _canMove = true;
                if (collision.gameObject.CompareTag("Enemy"))
                {
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = _armDirection * _speed;
                    _canBePickedUp = true;
                }
                else
                {
                    _canBePickedUp = _pickUpTimer <= 0.0f ? true : false;
                }
                break;

            case ARMTYPE.EXPLOSIVE:


                _canBePickedUp = _pickUpTimer <= 0.0f ? true : false;

                if (collision.gameObject.CompareTag("Player"))
                {
                    if (_canBePickedUp)
                        GetComponent<BoxCollider2D>().isTrigger = true;
                }
                if (!collision.gameObject.CompareTag("Player"))
                {
                    //on collision stops the rb from moving
                    _rb.constraints = RigidbodyConstraints2D.FreezeAll;
                    //stops applying force to the object
                    _canMove = false;

                    //collision with Enemy
                    if (collision.gameObject.CompareTag("Enemy"))
                    {
                        GetComponent<CircleCollider2D>().enabled = true;
                        //Send enemy in opposite direction from player
                        Vector2 forceDirection = collision.gameObject.transform.position -
                            gameObject.transform.position;
                        collision.gameObject.GetComponent<Rigidbody2D>()?.AddForce(forceDirection * PushPower, ForceMode2D.Force);
                        //Damages enemy
                        collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);

                        if (_canBePickedUp)
                        {
                            GetComponent<CircleCollider2D>().enabled = false;
                            return;
                        }

                        if (!_particlewasPlayed)
                        {
                            _particleSystem.Play();
                            _particlewasPlayed = true;
                        } 
                    }
                }
                break;

            default:
                if (!collision.gameObject.CompareTag("Player"))
                {
                    //stops applying force to the object
                    _canMove = false;
                    if(!_canBePickedUp)
                    collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
                    _canBePickedUp = true;
                }
                break;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(armType == ARMTYPE.LAWNMOWER)
        {
            collision.gameObject.GetComponent<EnemyStats>()?.TakeDamage(_damage);
            collision.gameObject.GetComponent<Rigidbody2D>().velocity = _armDirection * _speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canBePickedUp)
        {
            collision.gameObject.GetComponent<PlayerActions>()?.EnablePlayersArm(armSide, true);
            Destroy(gameObject);
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && _canBePickedUp)
        {
            collision.gameObject.GetComponent<PlayerActions>()?.EnablePlayersArm(armSide, true);
            Destroy(gameObject);
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
            _rb.velocity = _armDirection * _speed;
            transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
        }
        else
        {
            _rb.velocity = Vector2.zero;
        }
        //Boomerang return
        if (armType == ARMTYPE.BOOMERANG && !_canMove)
        {
            transform.position = Vector3.Lerp(transform.position, throwPosition, _speed/10.0f * Time.deltaTime);
        }
        //Lawnmower rotation
        if(armType == ARMTYPE.LAWNMOWER)
        {
            transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
        }
        //Explosive constraints
        if(armType == ARMTYPE.EXPLOSIVE && !_canMove)
        {
            _rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

}
