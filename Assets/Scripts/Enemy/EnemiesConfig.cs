using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemiesConfig", menuName = "ScriptableObjects/EnemiesConfig", order = 1)]
public class EnemiesConfig : ScriptableObject
{
    [SerializeField] public Configs[] configs;

    [Serializable]
    public class Configs
    {
        public EnemyType enemyType;
        public EnemyData enemyData;

    }
}
