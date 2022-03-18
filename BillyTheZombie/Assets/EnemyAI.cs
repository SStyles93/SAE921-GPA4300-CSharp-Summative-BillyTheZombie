using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private float tickTime = 0.0f;
    [SerializeField] private float tickTimer = 1.0f;

    [SerializeField] private int _numberOfRays = 4;
    [SerializeField] private Vector3[] _rayPositions; 

    private void Awake()
    {
        //Set TickTime
        tickTime = tickTimer;
        //Set size of rayPosition array
        _rayPositions = new Vector3[_numberOfRays];
        //Set the x value of the rays (forward = +x)
        for (int rayIndex = 0; rayIndex < _rayPositions.Length; rayIndex++)
        {
            _rayPositions[rayIndex].x = 1.0f;
        }
    }

    public void Update()
    {
        tickTime -= Time.deltaTime;
        if (tickTime <= 0.0f)
        {
            Tick();
            tickTime = tickTimer;
        }
    }

    /// <summary>
    /// Tick is a delayed Update
    /// </summary>
    private void Tick()
    {
        RayCast(_numberOfRays);
    }

    private void RayCast(int NumberOfRays)
    {
        //Sets the y position of all the rays in _rayPositions
        for (int rayIndex = 0; rayIndex < _rayPositions.Length / 2; rayIndex++)
        {
            int count = 1;
            _rayPositions[rayIndex].y = (1.0f / (NumberOfRays/ (NumberOfRays *  count)));
            _rayPositions[rayIndex + (NumberOfRays / 2)].y = (1.0f / (NumberOfRays / (NumberOfRays * -count)));
            count += rayIndex;
        }


    }
}
