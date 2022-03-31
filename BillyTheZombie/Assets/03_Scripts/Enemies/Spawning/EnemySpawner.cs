using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    [SerializeField] private GameObject _player;

    //reference ScriptableObjects
    [SerializeField] private GameStatsSO _gameStats;


    [SerializeField] GameObject[] _enemyPrefabs;
    [SerializeField] GameObject[] _spawnPositions;

    [SerializeField] private WavesSO[] _waves;

    [SerializeField] private List<GameObject> _enemyTracked;


    [SerializeField] private bool _waveStarted = false;
    [SerializeField] private bool _waveEnded = false;

    [SerializeField] private float rangeAroundPos;

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
    /// Sets the beginning of a waves
    /// </summary>
    private void StartWave(int WaveCount)
    {
        switch (WaveCount)
        {
            //case 3:
            //    InstantiateWave(2);
            //    InstantiateWave(3);
            //    InstantiateWave(4);
            //    break;
            //case 4:
            //    break;
            default:
                if(_gameStats.currentWaveIndex < _waves.Length)
                {
                    InstantiateWave(WaveCount);
                }
                else
                {
                    _waveEnded = true;
                    return;
                }
                break;
        }
        
        _waveStarted = true;
        _waveEnded = false;
    }

    /// <summary>
    /// Instantiate a number of enemies of the enemyIndex
    /// </summary>
    /// <param name="WaveIndex">The Index of the wave we want to spawn</param>
    private void InstantiateWave(int WaveIndex)
    {
        for (int i = 0; i < _waves[WaveIndex].NumberOfEnemies; i++)
        {
            float spawnRange = _spawnPositions[_waves[WaveIndex].PositionIndex].GetComponent<SpawnPosition>().SpawnRange;

            _enemyTracked.Add(Instantiate(
                _enemyPrefabs[_waves[WaveIndex].EnemyIndex],
                _spawnPositions[_waves[WaveIndex].PositionIndex].transform.position +
                new Vector3(Random.Range(spawnRange * -1.0f, spawnRange), Random.Range(spawnRange * -1.0f, spawnRange), 0.0f),
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
