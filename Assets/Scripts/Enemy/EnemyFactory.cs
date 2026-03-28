using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyData[] _enemyDatas;
    [SerializeField] private EnemyAttack _enemyPrefab;
    [SerializeField] private Transform _target;

    private EnemyPool _enemyPool;
    private void Awake()
    {
        _enemyPool = new EnemyPool();
        _enemyPool.Init(_enemyPrefab);
    }

    public void CreateEnemy(EnemyType type, Vector3 position)
    {
        if (_enemyPrefab == null) return;
        EnemyAttack enemy = _enemyPool.GetEnemy(position);
        enemy.Init(_target);
    }
}
