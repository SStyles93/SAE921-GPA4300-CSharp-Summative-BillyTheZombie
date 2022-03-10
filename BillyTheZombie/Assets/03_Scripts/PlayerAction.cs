using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;
    //Reference GameObjects
    [SerializeField] private GameObject _aim;

    void Start()
    {
        _controller = GetComponent<PlayerController>();
    }

    void Update()
    {
        //Look direction
        Vector2 look = _controller.Look;
        if (look != Vector2.zero)
        {
            _aim.transform.localPosition = new Vector3(look.x, look.y, 0.0f);
        }

    }
}
