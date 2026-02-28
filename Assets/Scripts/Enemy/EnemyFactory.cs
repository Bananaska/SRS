using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyData[] _enemyDatas;
    [SerializeField] private GameObject _enemyPrefab;

    private EnemyPool _enemyPool;
    private void Awake()
    {
        _enemyPool = new EnemyPool();
        _enemyPool.Init(_enemyPrefab);
    }

    public void CreateEnemy(EnemyType type, Vector3 position)
    {
        _enemyPool.GetEnemy();
    }
}
