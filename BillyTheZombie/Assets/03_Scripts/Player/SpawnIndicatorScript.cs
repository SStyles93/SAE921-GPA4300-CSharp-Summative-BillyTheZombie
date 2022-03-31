using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnIndicatorScript : MonoBehaviour
{
    [SerializeField] private EnemySpawner _enemySpawner;
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private Image _arrow;


    // Update is called once per frame
    void Update()
    {
        if(_enemySpawner.EnemyTracked.Count == 0)
        {
            _arrow.enabled = false;
            return;
        }
        else
        {
            if(_enemySpawner.EnemyTracked[0] != null)
            {
                _arrow.enabled = true;
                Vector3 direction = _playerController.transform.position - _enemySpawner.EnemyTracked[0].transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction, Vector3.forward);
                rotation.x = 0f;
                rotation.y = 0f;
                _arrow.transform.rotation = rotation;
            }
        }
    }
}