using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameStats", menuName = "ScriptableObject/Stats/GameStats", order = 1)]
public class GameStatsSO : ScriptableObject
{
    public float mutagenPoints;
    public int currentWaveIndex;
    public int maxWaveIndex;
}
