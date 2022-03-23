using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyVisuals : MonoBehaviour
{
    private Animator _animator;
    private AIPath _aIPath;

    [SerializeField] private GameObject _rayCaster;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _aIPath = GetComponentInParent<AIPath>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _animator.SetFloat("xPosition", _rayCaster.transform.rotation.y);
        if(_aIPath.canMove)
        {
            if (_aIPath.reachedEndOfPath)
            {
                _animator.SetFloat("Movement", 0.0f);
                return;
            }
            _animator.SetFloat("Movement", 1.0f);
        }
        else
        {
            _animator.SetFloat("Movement", 0.0f);
        }
        
        
    }
}
