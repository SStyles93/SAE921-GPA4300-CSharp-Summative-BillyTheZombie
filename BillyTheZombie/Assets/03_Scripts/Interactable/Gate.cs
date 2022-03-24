using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameStatsSO _gameStats;
    
    [SerializeField] private GameObject _leftDoor;
    [SerializeField] private GameObject _rightDoor;
    [SerializeField] private GameObject _leftGate;
    [SerializeField] private GameObject _rightGate;

    [SerializeField] bool _openGate = false;
    [SerializeField] int _indexToOpen;

    private Vector3 _leftDoorOpenPosition;
    private Vector3 _rightDoorOpenPosition;


    private void Start()
    {
        _leftDoorOpenPosition = _leftGate.transform.position;
        _leftDoorOpenPosition.y += 0.001f;
        _rightDoorOpenPosition = _rightGate.transform.position;
        _rightDoorOpenPosition.y += 0.001f;

        //Set to open pos if already opened
        if (_gameStats.maxReachedWaveIndex >= _indexToOpen ||
            _openGate ||
            _gameStats.currentWaveIndex >= _indexToOpen)
        {
            _leftDoor.transform.position = _leftDoorOpenPosition;
            _rightDoor.transform.position = _rightDoorOpenPosition;
        }

    }

    public void Update()
    {
        if(_gameStats.maxReachedWaveIndex >= _indexToOpen ||
            _openGate ||
            _gameStats.currentWaveIndex >= _indexToOpen)
        {
            OpenGate();
        }
    }

    /// <summary>
    /// Opens the gate once the level index is reached
    /// </summary>
    private void OpenGate()
    {
        _leftDoor.transform.position = Vector3.Lerp(_leftDoor.transform.position, _leftGate.transform.position, Time.deltaTime);
        _leftDoor.GetComponent<BoxCollider2D>().enabled = false;
        _rightDoor.transform.position = Vector3.Lerp(_rightDoor.transform.position, _rightGate.transform.position, Time.deltaTime);
        _rightDoor.GetComponent<BoxCollider2D>().enabled = false;
    }
}
