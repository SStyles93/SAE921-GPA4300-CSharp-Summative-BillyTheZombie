using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats",
    menuName = "ScriptableObject/Stats/PlayerStats", order = 2)]
public class PlayerStatsSO : ScriptableObject
{
    public float _pushPowerPercentage = 0.0f;
    public float _damageRightPercentage = 0.0f;
    public float _damageLeftPercentage = 0.0f;
    public float _healthPercentage = 0.0f;
    public float _speedPercentage = 0.0f;
}
