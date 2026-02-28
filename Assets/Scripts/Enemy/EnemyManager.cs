using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _enemyCountInWave;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Vector3 _enemySpawnPointPosition;
    private void CreateEnemyManager()
    {
        _enemyFactory.CreateEnemy(_enemyType, _enemySpawnPointPosition);




    }

}
