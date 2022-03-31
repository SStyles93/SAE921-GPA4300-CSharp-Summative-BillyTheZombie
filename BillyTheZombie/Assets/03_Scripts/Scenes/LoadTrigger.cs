using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneManagement _sceneManagement;
    [SerializeField] private int _sceneIndex = 1;
    [SerializeField] private SpawnPositionIndexSO _spawnIndexSO;
    [SerializeField] private int _spawnIndex = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _spawnIndexSO.positionIndex = _spawnIndex;
            _sceneManagement.Player = collision.gameObject;
            _sceneManagement.SceneIndex = _sceneIndex;
            _sceneManagement.FadeOut = true;
        } 
    }
}
