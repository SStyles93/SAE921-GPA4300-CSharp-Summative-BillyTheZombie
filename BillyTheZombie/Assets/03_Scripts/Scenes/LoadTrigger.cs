using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadTrigger : MonoBehaviour
{
    [SerializeField] private SceneManagement _sceneManagement;
    [SerializeField] private int _sceneIndex = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            _sceneManagement.PlayerStats = collision.GetComponent<PlayerStats>();
            _sceneManagement.SceneIndex = _sceneIndex;
            _sceneManagement.FadeOut = true;
        } 
    }
}
