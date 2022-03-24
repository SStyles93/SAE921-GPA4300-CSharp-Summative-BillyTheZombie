using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameStatsSO _gameStats;
    [SerializeField] GameObject[] _enemyPrefabs;
    [SerializeField] private List<GameObject> _enemyTracked;

    [SerializeField] GameObject[] _spawnPositions;

    [SerializeField] private bool _waveStarted = false;
    [SerializeField] private bool _waveEnded = false;

    /// <summary>
    /// Instantiate a number of enemies of the enemyIndex
    /// </summary>
    /// <param name="NumberOfEnemies">The number of enemies to spawn</param>
    /// <param name="EnemyIndex">The wanted enemy Index</param>
    /// <param name="PositionIndex">The Index of the position in spawnPosition</param>
    /// <param name="rangeAroundPosition">Range around the spawnPosition</param>
    private void InstantiateWave(int NumberOfEnemies, int EnemyIndex, int PositionIndex, float rangeAroundPosition)
    {
        for (int i = 0; i < NumberOfEnemies; i++)
        {
            _enemyTracked.Add(Instantiate(
                _enemyPrefabs[EnemyIndex],
                _spawnPositions[PositionIndex].transform.position +
                new Vector3(Random.Range(-rangeAroundPosition, rangeAroundPosition),
                Random.Range(-rangeAroundPosition, rangeAroundPosition), 0.0f),
                Quaternion.identity));
        }
    }

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
            case 1:
                InstantiateWave(1, 0, 0, 0.0f);
                break;
            case 2:
                InstantiateWave(4, 0, 0, 1.0f);
                break;
            case 3:
                InstantiateWave(100, 0, 0, 5.0f);
                break;
            default:
                return;
        }
        
        _waveStarted = true;
        _waveEnded = false;
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
