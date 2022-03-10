using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;
    //Reference GameObjects
    [SerializeField] private GameObject _aim;

    [SerializeField] private GameObject _body;
    [SerializeField] private GameObject _armR;
    [SerializeField] private GameObject _armL;

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
        else
        {
            _aim.transform.localPosition = new Vector3(_controller.Movement.x, _controller.Movement.y, 0.0f);
        }
    }
}
