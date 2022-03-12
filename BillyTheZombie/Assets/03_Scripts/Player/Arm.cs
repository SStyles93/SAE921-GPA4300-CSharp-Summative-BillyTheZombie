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
    [SerializeField] private ARMSIDE arm;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            collision.gameObject.GetComponent<PlayerAction>().EnablePlayersArm(arm, true);
            Destroy(gameObject);
        }
    }
}
