using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats",
    menuName = "ScriptableObject/Stats/PlayerStats", order = 2)]
public class PlayerStatsSO : ScriptableObject
{
    public float _pushPowerPercentage = 0.0f;
    public float _armDamagePercentage = 0.0f;
    public float _healthPercentage = 0.0f;
    public float _speedPercentage = 0.0f;

    public int _rightArmType = 0;
    public int _leftArmType = 0;

}
