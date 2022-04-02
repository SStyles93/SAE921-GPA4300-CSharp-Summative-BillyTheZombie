using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] protected GameStatsSO _gameStatsSO;
    
    [SerializeField] protected GameObject _leftDoor;
    [SerializeField] protected GameObject _rightDoor;
    [SerializeField] protected GameObject _leftGate;
    [SerializeField] protected GameObject _rightGate;

    [SerializeField] protected bool _openGate = false;
    [SerializeField] protected int _indexToOpen;

    protected Vector3 _leftDoorOpenPosition;
    protected Vector3 _rightDoorOpenPosition;


    protected void Start()
    {
        _leftDoorOpenPosition = _leftGate.transform.position;
        _leftDoorOpenPosition.y += 0.001f;
        _rightDoorOpenPosition = _rightGate.transform.position;
        _rightDoorOpenPosition.y += 0.001f;

        //Set to open pos if already opened
        if (_gameStatsSO.maxReachedWaveCount >= _indexToOpen ||_openGate)
        {
            _leftDoor.transform.position = _leftDoorOpenPosition;
            _rightDoor.transform.position = _rightDoorOpenPosition;
        }

    }

    protected void Update()
    {
        if (_gameStatsSO.maxReachedWaveCount >= _indexToOpen ||_openGate)
        {
            OpenGate();
        }
    }

    /// <summary>
    /// Opens the gate once the level index is reached
    /// </summary>
    protected void OpenGate()
    {
        _leftDoor.transform.position = Vector3.Lerp(_leftDoor.transform.position, _leftGate.transform.position, Time.deltaTime);
        _leftDoor.GetComponent<BoxCollider2D>().enabled = false;
        _rightDoor.transform.position = Vector3.Lerp(_rightDoor.transform.position, _rightGate.transform.position, Time.deltaTime);
        _rightDoor.GetComponent<BoxCollider2D>().enabled = false;
    }
}
