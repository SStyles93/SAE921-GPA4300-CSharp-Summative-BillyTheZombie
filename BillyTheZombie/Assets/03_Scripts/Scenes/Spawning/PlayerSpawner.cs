using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [Header("PlayerSpawn")]
    [Tooltip("The player prefab to spawn, leave empty if not a combat scene")]
    [SerializeField] private GameObject _playerPrefab;
    [Tooltip("The player Stats")]
    [SerializeField]private PlayerStatsSO _playerStatsSO;
    [Tooltip("The index of the spawn position at which the player will spawn")]
    [SerializeField] private PlayerSpawnPositionIndexSO _spawnIndexSO;
    [Tooltip("The transform on which to spawn the player")]
    [SerializeField] private Transform[] _playerSpawns;

    private GameObject _player;
    private EnemySpawner _enemySpawner;
    private SceneManagement _sceneManagement;

    public GameObject Player { get => _player; set => _player = value; }

    private void Awake()
    {
        if (_playerPrefab != null)
        {
            _enemySpawner = GetComponent<EnemySpawner>();
            _sceneManagement = GetComponent<SceneManagement>();
        }

    }

    void Start()
    {

        if (_playerPrefab != null)
        {
            InitPlayer(_playerSpawns[_spawnIndexSO.positionIndex]);
            //Send the player ref to the EnemySpawner
            _enemySpawner.Player = _player;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Initializes the player 
    /// </summary>
    /// <param name="playerSpawn"></param>
    private void InitPlayer(Transform playerSpawn)
    {
        _player = Instantiate(_playerPrefab, playerSpawn.position, Quaternion.identity);
        PlayerStats playerStats = _player.GetComponent<PlayerStats>();
        playerStats.SceneManagement = _sceneManagement;
        playerStats.Health = _playerStatsSO.currentHealth;
        _player.GetComponentInChildren<SpawnIndicator>().EnemySpawner = _enemySpawner;
    }
}
