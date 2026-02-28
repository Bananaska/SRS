using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _enemyCountInWave;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private Transform _enemySpawnPointPosition;

    private void Start()
    {
        CreateBasicEnemy();
    }

    private void CreateBasicEnemy()
    {
        _enemyFactory.CreateEnemy(EnemyType.Basic, _enemySpawnPointPosition.position);
    }
}
