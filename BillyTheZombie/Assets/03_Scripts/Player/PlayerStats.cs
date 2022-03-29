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
    private float _basicArmDamage = 10.0f;
    private float _basicSpeed = 4.0f;
    private float _basicPushPower = 10.0f;

    //player final stats
    [Header("Player's Stats")]
    [SerializeField] private float _pushPower = 10.0f;
    [SerializeField] private float _armDamage = 10.0f;
    [SerializeField] private float _health = 100.0f;
    private float _maxHealth = 100.0f;
    [SerializeField] private float _speed = 2.0f;

    [SerializeField] private bool _isInvicible = false;
    
    //Properties
    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float PushPower { get => _pushPower; set => _pushPower = value; }
    public float ArmDamage { get => _armDamage; set => _armDamage = value; }
    public bool IsInvicible { get => _isInvicible; set => _isInvicible = value; }

    private void Awake()
    {
        _pushPower = _basicPushPower + (_basicPushPower * _statSO._pushPowerPercentage / 100.0f);
        _armDamage = _armDamage + (_basicArmDamage * _statSO._armDamagePercentage / 100.0f);
        _maxHealth = _basicHealth + (_basicHealth * _statSO._healthPercentage / 100.0f);
        _speed = _basicSpeed + (_basicSpeed * _statSO._speedPercentage / 100.0f);
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
        _sceneManagement.Player = gameObject;
        _sceneManagement.SceneIndex = 1;
        _sceneManagement.FadeOut = true;
    }

    /// <summary>
    /// lowers health according to the damage
    /// </summary>
    /// <param name="damage">The damage to substract to health</param>
    public void TakeDamage(float damage)
    {
        if(!_isInvicible)
        _health -= damage;
    }
}
