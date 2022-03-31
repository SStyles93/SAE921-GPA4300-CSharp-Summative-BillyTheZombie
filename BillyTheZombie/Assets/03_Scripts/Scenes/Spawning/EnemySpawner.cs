using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{


    //reference ScriptableObjects
    [Tooltip("The GameStats ScriptableObject to update in game")]
    [SerializeField] private GameStatsSO _gameStats;

    [Tooltip("Enemy prefabs to spawn, Index of list used in WavesSO")]
    [SerializeField] GameObject[] _enemyPrefabs;
    [Tooltip("The spawn positions, Index of list used in WavesSO")]
    [SerializeField] GameObject[] _spawnPositions;

    [Tooltip("The list of waves to spawn")]
    [SerializeField] private WavesSO[] _waves;
    
    //The list used to "track" enemies at runtime
    [Tooltip("List of in-game enemies")]
    [SerializeField] private List<GameObject> _enemyTracked;


    private GameObject _player;
    //Flags to keep track of the state of the game
    private bool _waveStarted = false;
    private bool _waveEnded = false;
    //Range used to spawn around a certain position
    private float _spawnRange;

    public GameObject Player { get => _player; set => _player = value; }
    public List<GameObject> EnemyTracked { get => _enemyTracked; set => _enemyTracked = value; }

    public void Update()
    {
        //Start
        if (!_waveStarted)
        {
            StartWave(_gameStats.currentWaveIndex);
        }
        //Check for enemy death
        for (int i = 0; i < _enemyTracked.Count; i++)
        {
            
            if(_enemyTracked[i] == null)
            {
                _enemyTracked.RemoveAt(i);
                _player.GetComponentInChildren<PlayerUI>().GainPoints();
            }
            else
            {
                if ((_enemyTracked[i].transform.position - _player.transform.position).magnitude <= 12f)
                {
                    _enemyTracked[i].gameObject.SetActive(true);
                }
                else
                {
                    _enemyTracked[i].gameObject.SetActive(false);
                }
            }
        }
        //End
        if (!_waveEnded && _enemyTracked.Count == 0)
        {
            EndWave();
        }
        
    }

    /// <summary>
    /// Sets the beginning of a wave or a set of subwaves
    /// </summary>
    /// <param name="waveIndex">The index of the wave to call</param>
    private void StartWave(int waveIndex)
    {
        //Check if there are still waves to Instantiate
        if(_gameStats.currentWaveIndex < _waves.Length)
        {
            InstantiateWave(waveIndex);
            //If a wave is a subwave call next one too
            if (_waves[waveIndex].isSubWaves)
            {
                _gameStats.currentWaveIndex++;
                StartWave(waveIndex + 1);

            }
        }
        else
        {
            _waveEnded = true;
            return;
        }
        
        _waveStarted = true;
        _waveEnded = false;
    }

    /// <summary>
    /// Instantiate a number of enemies of the enemyIndex
    /// </summary>
    /// <param name="waveIndex">The Index of the wave we want to spawn</param>
    private void InstantiateWave(int waveIndex)
    {
        //Instantiates 
        for (int enemyNumber = 0; enemyNumber < _waves[waveIndex].NumberOfEnemies; enemyNumber++)
        {
            _spawnRange = _spawnPositions[_waves[waveIndex].PositionIndex].GetComponent<SpawnPosition>().SpawnRange;

            _enemyTracked.Add(Instantiate(
                _enemyPrefabs[_waves[waveIndex].EnemyIndex],
                _spawnPositions[_waves[waveIndex].PositionIndex].transform.position +
                new Vector3(Random.Range(_spawnRange * -1.0f, _spawnRange), Random.Range(_spawnRange * -1.0f, _spawnRange), 0.0f),
                Quaternion.identity));
        }
    }

    /// <summary>
    /// Sets the end of a wave
    /// </summary>
    private void EndWave()
    {
        
        //Update waveIndex
        if (!_waveEnded)
        {
            _gameStats.currentWaveIndex++;
        }
        //Update maxWaveReached if currentWave is higher
        if (_gameStats.maxReachedWaveIndex < _gameStats.currentWaveIndex)
        {
            _gameStats.maxReachedWaveIndex = _gameStats.currentWaveIndex;
        }
        _waveEnded = true;
        _waveStarted = false;
    }
}
