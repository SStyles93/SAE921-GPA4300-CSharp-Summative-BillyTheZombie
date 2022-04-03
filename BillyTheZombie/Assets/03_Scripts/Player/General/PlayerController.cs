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


    private bool _canRepeateActions = false;
    private float _repeatingTimer = 0.1f;
    float _repeatTimerHead;
    float _repeatTimerArmR;
    float _repeatTimerArmL;
    bool _repeatingHead = false;
    bool _repeatingArmR = false;
    bool _repeatingArmL = false;

    
    public string ControlScheme { get => _controlScheme; set => _controlScheme = value; }
    public Vector2 Movement { get => _movement; set => _movement = value; }
    public Vector2 Look { get => _look; set => _look = value; }
    public bool Head { get => _head; set => _head = value; }
    public bool ArmR { get => _armR; set => _armR = value; }
    public bool ArmL { get => _armL; set => _armL = value; }
    public bool CanRepeateActions { get => _canRepeateActions; set => _canRepeateActions = value; }

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        _controlScheme = _playerInput.currentControlScheme;

        if (!_canRepeateActions)
        {
            //Prevents from repeating input HEAD
            if (_repeatingHead)
            {
                _repeatTimerHead += Time.deltaTime;
            }
            if (_repeatTimerHead >= _repeatingTimer)
            {
                _head = true;
                _repeatingHead = false;
                _repeatTimerHead = 0.0f;
            }
            else
            {
                _head = false;
            }

            //Prevents from repeating input ArmR
            if (_repeatingArmR)
            {
                _repeatTimerArmR += Time.deltaTime;
            }
            if (_repeatTimerArmR >= _repeatingTimer)
            {
                _armR = true;
                _repeatingArmR = false;
                _repeatTimerArmR = 0.0f;
            }
            else
            {
                _armR = false;
            }

            //Prevents from repeating input ArmL
            if (_repeatingArmL)
            {
                _repeatTimerArmL += Time.deltaTime;
            }
            if (_repeatTimerArmL >= _repeatingTimer)
            {
                _armL = true;
                _repeatingArmL = false;
                _repeatTimerArmL = 0.0f;
            }
            else
            {
                _armL = false;
            }
        }
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
        if (!_canRepeateActions)
        {
            if (value.isPressed)
                _repeatingHead = true;

        }
        else
        {
            _head = value.isPressed;
        }
    }
    public void OnArmR(InputValue value)
    {
        if (!_canRepeateActions)
        {
            if (value.isPressed)
                _repeatingArmR = true;

        }
        else
        {
            _armR = value.isPressed;
        }
    }
    public void OnArmL(InputValue value)
    {
        if (!_canRepeateActions)
        {
            if(value.isPressed)
            _repeatingArmL = true;

        }
        else
        {
            _armL = value.isPressed;
        }
    }
}
