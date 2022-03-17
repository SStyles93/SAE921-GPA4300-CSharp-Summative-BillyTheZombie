using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillyHouseScene : MonoBehaviour
{
    [SerializeField] GameObject _player;

    private void Start()
    {
        OverrideStats();
    }
    private void OverrideStats()
    {
        _player.GetComponent<PlayerStats>().Speed = 4.0f;
        PlayerActions playerActions = _player.GetComponent<PlayerActions>();
        playerActions.CanHit = false;
        playerActions.IsInCombat = false;
       
    }
}
