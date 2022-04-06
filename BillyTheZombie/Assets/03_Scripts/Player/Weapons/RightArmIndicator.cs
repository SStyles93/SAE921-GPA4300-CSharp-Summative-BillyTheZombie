using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightArmIndicator : MonoBehaviour
{
    [SerializeField] private PlayerActions _playerActions;
    [SerializeField] private SpriteRenderer _arrow;

    private void Awake()
    {
        _playerActions = GetComponentInParent<PlayerActions>();
        _arrow = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_playerActions.CurrentRightArm == null)
        {
            _arrow.enabled = false;
            return;
        }
        else
        {
            _arrow.enabled = true;
            Vector3 direction = _playerActions.transform.position - _playerActions.CurrentRightArm.transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
            rotation.x = 0f;
            rotation.y = 0f;
            transform.rotation = rotation;
        }
    }
}