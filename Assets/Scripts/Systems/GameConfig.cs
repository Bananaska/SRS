 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    [SerializeField] public TenWaveData[] tenWave;
}


[Serializable]
public class WaveData
{
    public int EnemyCount;
    public List<EnemyType> enemyType;
}


[Serializable]
public class TenWaveData

{
    public WaveData[] waveDatas;
}



