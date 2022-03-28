using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rib : MonoBehaviour
{
    //Reference Components
    private Rigidbody2D _rb;

    //RibThrow direction
    private Vector3 _ribDirection;

    //Rib Stats
    [SerializeField] private float _speed = 1.0f;
    [SerializeField] private float _damage = 1.0f;
    private bool _canMove;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    private void Move()
    {
        if (_canMove)
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            //Physic movement
            _rb.AddForce(_ribDirection * _speed / 10.0f, ForceMode2D.Impulse);
            transform.Rotate(Vector3.back * Time.deltaTime * 1000f);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Uses the Trigger to hit
        if (collision)
        {
            _canMove = false;
            collision.GetComponent<PlayerStats>()?.TakeDamage(_damage);
        }
    }
}
