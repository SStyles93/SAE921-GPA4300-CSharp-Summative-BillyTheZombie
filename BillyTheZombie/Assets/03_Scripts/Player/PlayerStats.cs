using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private SceneManagement _sceneManagement;

    //statSO contains all the player stats
    [Header("Player Stats ScriptableObject")]
    [SerializeField] private PlayerStatsSO _statSO;

    [Header("Player's Stats")]
    [SerializeField] private float _pushPower = 10.0f;
    [SerializeField] private float _armDamage = 10.0f;
    [SerializeField] private float _health = 100.0f;
    [SerializeField] private float _maxHealth = 100.0f;
    [SerializeField] private float _speed = 2.0f;

    [SerializeField] private bool _isInvicible = false;
    
    //Properties
    public SceneManagement SceneManagement { get => _sceneManagement; set => _sceneManagement = value; }
    public float Health { get => _health; set => _health = value; }
    public float MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public float Speed { get => _speed; set => _speed = value; }
    public float PushPower { get => _pushPower; set => _pushPower = value; }
    public float ArmDamage { get => _armDamage; set => _armDamage = value; }
    public bool IsInvicible { get => _isInvicible; set => _isInvicible = value; }

    private void Awake()
    {
        _pushPower = _statSO.basicPushPower + (_statSO.basicPushPower * _statSO.pushPowerPercentage / 100.0f);
        _armDamage = _armDamage + (_statSO.basicArmDamage * _statSO.armDamagePercentage / 100.0f);
        _maxHealth = _statSO.basicHealth + (_statSO.basicHealth * _statSO.healthPercentage / 20.0f);
        _speed = _statSO.basicSpeed + (_statSO.basicSpeed * _statSO.speedPercentage / 100.0f);

    }

    private void Update()
    {
        _health = _statSO.currentHealth;

        if (_statSO.currentHealth <= 0.0f)
        {
            Die();
        }
    }

    private void Die()
    {
        IsInvicible = true;
        _statSO.currentHealth = _statSO.maxHealth;
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
            _statSO.currentHealth -= damage;
    }
}
