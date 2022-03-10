using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Vector2 _movement;
    [SerializeField] private Vector2 _look;

    [SerializeField] private bool _head;
    [SerializeField] private bool _armR;
    [SerializeField] private bool _armL;
    
    public Vector2 Movement { get => _movement; set => _movement = value; }
    public Vector2 Look { get => _look; set => _look = value; }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {
        _look = value.Get<Vector2>();
    }
    public void OnHead(InputValue value)
    {
        _head = value.isPressed;
    }
    public void OnArmR(InputValue value)
    {
        _armR = value.isPressed;
    }
    public void OnArmL(InputValue value)
    {
        _armL = value.isPressed;
    }
}
