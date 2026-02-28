 using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "ScriptableObjects/GameConfig", order = 1)]
public class GameConfig : ScriptableObject
{
    [SerializeField] private WaveData[] waveDatas;
}


[Serializable]
public class WaveData
{
    public int EnemyCount;
    public EnemyType[] enemyTypes;
}
