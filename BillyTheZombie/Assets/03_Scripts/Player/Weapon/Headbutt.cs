using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Headbutt : MonoBehaviour
{
    [SerializeField] private float _pushPower;

    public float PushPower { get => _pushPower; set => _pushPower = value; }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyStats>())
        {
            Vector2 forceDirection = collision.gameObject.transform.position -
                gameObject.transform.position;
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(forceDirection * _pushPower, ForceMode2D.Impulse);
        }
    }
}
