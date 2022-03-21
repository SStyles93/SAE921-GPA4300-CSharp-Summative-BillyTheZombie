using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float tickTime = 0.0f;
    [SerializeField] private float tickTimer = 1.0f;

    [SerializeField] private int _numberOfRays = 4;
    [SerializeField] private Vector3[] _rayDirections;
    [SerializeField] private float _detectionDistance = 10.0f;
    [SerializeField] private Vector3 _playersLastPosition;

    private void Awake()
    {
        //Set TickTime
        tickTime = tickTimer;

        InitRays(_numberOfRays);
        
    }

    private void Update()
    {
        tickTime -= Time.deltaTime;
        if (tickTime <= 0.0f)
        {
            Tick();
            tickTime = tickTimer;
        }
    }

    private void FixedUpdate()
    {
        RayCast();
    }

    /// <summary>
    /// Tick is a delayed Update
    /// </summary>
    protected void Tick()
    {
       
    }

    /// <summary>
    /// Initializes the rays
    /// </summary>
    /// <param name="NumberOfRays">Number of rays we want to Initialize</param>
    protected void InitRays(int NumberOfRays)
    {
        //Set size of rayPosition array
        _rayDirections = new Vector3[_numberOfRays + 1];
        //Set the x value of the rays (forward = +x)
        for (int rayIndex = 0; rayIndex < _rayDirections.Length; rayIndex++)
        {
            _rayDirections[rayIndex].x = 1.0f;
        }

        //Sets the y position of all the rays in _rayPositions
        for (int i = 0; i < NumberOfRays + 1; i++)
        {
            if(i == 0)
            {
                _rayDirections[i].y = 1.0f;
            }
            else
            {
                _rayDirections[i].y = (_rayDirections[i - 1].y - (1.0f / (float)NumberOfRays*2));
            }
        }
    }

    protected void RayCast()
    {
        for (int i = 0; i < _rayDirections.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _rayDirections[i].normalized, _detectionDistance);
            if (hit.collider != null && hit.transform.GetComponent<PlayerController>())
            {
                _playersLastPosition = hit.transform.position;
            }
            
            Debug.DrawRay(transform.position, _rayDirections[i].normalized * _detectionDistance, Color.red);
        }
        
    }

    protected void Attack() { }
}
