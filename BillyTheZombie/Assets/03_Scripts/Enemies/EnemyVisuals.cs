using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyVisuals : MonoBehaviour
{
    private Animator _animator;
    private AIPath _aIPath;

    [SerializeField] private SpriteRenderer _spriteRender;
    [SerializeField] private GameObject _rayCaster;

    [SerializeField] private Color _currentColor;
    private float _damageCooldown = 2f;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _spriteRender = GetComponentInChildren<SpriteRenderer>();
        _aIPath = GetComponent<AIPath>();
    }

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

        RetriveNormalColor();
       
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Colors the enemy on hit
        if (collision.gameObject.GetComponent<Arm>())
        {
            _currentColor = Color.red;
            _damageCooldown = 0.0f;
        }
    }

    private void RetriveNormalColor()
    {
        if (_currentColor != Color.white)
        {
            _damageCooldown += Time.deltaTime;
            _currentColor = Color.Lerp(_currentColor, Color.white, _damageCooldown);
        }
        else
        {
            _damageCooldown = 0.0f;
        }
        _spriteRender.color = _currentColor;
    }

}
