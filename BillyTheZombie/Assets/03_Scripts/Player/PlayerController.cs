using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private string _controlScheme;

    private Vector2 _movement;
    private Vector2 _look;

    private bool _head;
    private bool _armR;
    private bool _armL;

    
    public string ControlScheme { get => _controlScheme; set => _controlScheme = value; }
    public Vector2 Movement { get => _movement; set => _movement = value; }
    public Vector2 Look { get => _look; set => _look = value; }
    public bool Head { get => _head; set => _head = value; }
    public bool ArmR { get => _armR; set => _armR = value; }
    public bool ArmL { get => _armL; set => _armL = value; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }
    private void Update()
    {
        _controlScheme = _playerInput.currentControlScheme;
    }

    public void OnMove(InputValue value)
    {
        _movement = value.Get<Vector2>();
    }
    public void OnLook(InputValue value)
    {
        switch (_controlScheme)
        {
            case "Keyboard":
                break;
            case "Gamepad":
                _look = value.Get<Vector2>();
                break;
            default:
                break;
        }
        
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
