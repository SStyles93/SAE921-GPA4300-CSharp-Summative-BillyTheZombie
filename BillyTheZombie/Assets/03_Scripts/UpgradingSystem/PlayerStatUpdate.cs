using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatUpdate : MonoBehaviour
{
    [SerializeField] private PlayerStatsSO _playerStats;
    private enum STATTYPE
    {
        PUSHPOWER,
        RIGHTDAMAGE,
        LEFTDAMAGE,
        HEALTH,
        SPEED
    }
    [SerializeField] private STATTYPE statType;

    [SerializeField] private float stat;

    public float Stat { get => stat; set => stat = value; }

    public void Start()
    {
        switch (statType)
        {
            case STATTYPE.PUSHPOWER:
                stat = _playerStats._pushPowerPercentage;
                break;
            case STATTYPE.RIGHTDAMAGE:
                stat = _playerStats._damageRightPercentage;
                break;
            case STATTYPE.LEFTDAMAGE:
                stat = _playerStats._damageLeftPercentage;
                break;
            case STATTYPE.HEALTH:
                stat = _playerStats._healthPercentage;
                break;
            case STATTYPE.SPEED:
                stat = _playerStats._speedPercentage;
                break;
        }
    }

    public void UpdateStat()
    {
        switch (statType)
        {
            case STATTYPE.PUSHPOWER:
                _playerStats._pushPowerPercentage = stat;
                break;
            case STATTYPE.RIGHTDAMAGE:
                _playerStats._damageRightPercentage = stat;
                break;
            case STATTYPE.LEFTDAMAGE:
                _playerStats._damageLeftPercentage = stat;
                break;
            case STATTYPE.HEALTH:
                _playerStats._healthPercentage = stat;
                break;
            case STATTYPE.SPEED:
                _playerStats._speedPercentage = stat;
                break;
        }
    }

}
