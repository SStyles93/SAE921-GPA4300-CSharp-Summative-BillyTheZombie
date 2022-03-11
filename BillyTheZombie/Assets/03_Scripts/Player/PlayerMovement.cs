using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //Reference Scripts
    private PlayerController _controller;
    //Variables
    [SerializeField] private float _speed = 2.0f;

    private bool _canMove = true;
    public bool CanMove { get => _canMove; set => _canMove = value; }

    void Awake()
    {
        _controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        //Movement
        if (_canMove)
        {
            Vector3 movement = new Vector3(_controller.Movement.x, _controller.Movement.y, 0.0f);
            transform.Translate(movement * _speed * Time.deltaTime);
        }
    }
}

