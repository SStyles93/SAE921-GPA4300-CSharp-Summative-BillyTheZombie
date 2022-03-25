using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRayCaster : MonoBehaviour
{
    [SerializeField] private bool DEBUGMODE = false;
    [Header("RayCasting variables")]
    [Tooltip("The time between raycasts")]
    [SerializeField] private float tickTimer = 1.0f;
    [Tooltip("Number of rays to cast")]
    [SerializeField] private int _numberOfRays = 4;
    [Tooltip("The angle in which the raycasts are done")]
    [SerializeField] private float _angleOfDetection = 90.0f;
    [Tooltip("The distance of the raycasts")]
    [SerializeField] private float _detectionDistance = 10.0f;

    private float tickTime = 0.0f;
    private Vector3[] _rayDirections;
    private Vector3 _playersLastPosition;
    [SerializeField] private Transform _target;
    [SerializeField] private bool _playerInSight = false;

    public Vector3 PlayersLastPosition { get => _playersLastPosition; private set => _playersLastPosition = value; }
    public bool PlayerInSight { get => _playerInSight; set => _playerInSight = value; }
    public Transform Target { get => _target; set => _target = value; }

    private void Awake()
    {
        //Set TickTime
        tickTime = tickTimer;        
    }
    private void Update()
    {
        if (DEBUGMODE && _rayDirections != null)
        {
            for (int i = 0; i < _rayDirections.Length; i++)
            {
                Debug.DrawRay(transform.position, _rayDirections[i] * _detectionDistance, Color.red);
            }
        }
    }

    private void FixedUpdate()
    {
        
        tickTime -= Time.deltaTime;
        if (tickTime <= 0.0f)
        {
            CastRays(_numberOfRays, _angleOfDetection);
            Tick();
            tickTime = tickTimer;
        }
    }

    /// <summary>
    /// Tick is a delayed Update
    /// </summary>
    private void Tick()
    {
        GetPosition();
        transform.LookAt(_target);
    }

    /// <summary>
    /// Initializes the rays
    /// </summary>
    /// <param name="NumberOfRays">Number of rays we want to Initialize</param>
    protected void CastRays(int NumberOfRays, float angle)
    {
        float[] newAngles = new float[NumberOfRays+1];        
        //Set size of rayPosition array
        _rayDirections = new Vector3[_numberOfRays + 1];
        for (int i = 0; i < _rayDirections.Length; i++)
        {
            if(i == 0)
            {
                newAngles[i] = (angle / 2.0f * -1.0f);
                Quaternion spreadAngle = Quaternion.AngleAxis(newAngles[i], Vector3.forward);
                Vector3 newVector = spreadAngle * transform.forward;
                _rayDirections[i] = newVector;
            }
            else
            {
                newAngles[i] = newAngles[i-1] + angle / NumberOfRays;
                Quaternion spreadAngle = Quaternion.AngleAxis(newAngles[i], Vector3.forward);
                Vector3 newVector = spreadAngle * transform.forward;
                _rayDirections[i] = newVector;
            }
        }
    }

    protected void GetPosition()
    {
        for (int i = 0; i < _rayDirections.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, _rayDirections[i].normalized, _detectionDistance);
            if (hit.collider != null && hit.transform.GetComponent<PlayerController>())
            {
                _playerInSight = true;
                _playersLastPosition = hit.transform.position;
                _target.transform.position = hit.transform.position;
                return;
            }
            else
            {
                if (transform.position == _playersLastPosition)
                {
                    return;
                }

                _target.transform.position = _playersLastPosition;
                _playerInSight = false;
            }
        }
        
    }
}
