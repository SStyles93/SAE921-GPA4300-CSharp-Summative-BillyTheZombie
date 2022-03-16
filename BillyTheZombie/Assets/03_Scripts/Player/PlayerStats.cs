using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private SceneManagement _sceneManagement;

    //statSO is used to increse the players stats (in %)
    [Header("Player Stats ScriptableObject")]
    [SerializeField] private PlayerStatsSO _statSO;

    //players basic stats
    private float _basicHealth = 100.0f;
    private float _basicSpeed = 2.0f;
    private float _basicPushPower = 10.0f;
    private float _basicDamageRight = 10.0f;
    private float _basicDamageLeft = 10.0f;

    //player final stats
    [Header("Player's Stats")]
    [SerializeField] private float _health = 100.0f;
    private float _maxHealth = 100.0f;
    [SerializeField] private float _speed = 2.0f;
    [SerializeField] private float _pushPower = 10.0f;
    [SerializeField] private float _damageRight = 10.0f;
    [SerializeField] private float _damageLeft = 10.0f;
    
    //Properties
    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float PushPower { get => _pushPower; set => _pushPower = value; }
    public float DamageRight { get => _damageRight; set => _damageRight = value; }
    public float DamageLeft { get => _damageLeft; set => _damageLeft = value; }

    private void Awake()
    {
        _maxHealth = _basicHealth + (_basicHealth * _statSO._healthPercentage / 100.0f);
        _speed = _basicSpeed + (_basicSpeed * _statSO._speedPercentage / 100.0f);
        _pushPower = _basicPushPower + (_basicPushPower * _statSO._pushPowerPercentage / 100.0f);
        _damageRight = _basicDamageRight + (_basicDamageRight * _statSO._damageRightPercentage / 100.0f);
        _damageLeft = _basicDamageLeft + (_basicDamageLeft * _statSO._damageLeftPercentage / 100.0f);
    }
    private void Start()
    {
        _health = _maxHealth;
    }
    private void Update()
    {
        if(_health <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        _sceneManagement.ActivateScene(2);
    }
}
