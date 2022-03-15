using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyHouseScene : MonoBehaviour
{
    [SerializeField] PlayerStats _playerStats;

    private void Start()
    {
        OverrideStats();
    }
    private void OverrideStats()
    {
        _playerStats.Speed = 4.0f;
    }
}
