using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemiesConfig _enemiesConfig;
    [SerializeField] private EnemyAttack _enemyPrefab;
    [SerializeField] private Transform _target;

    private void Awake()
    { 
        EnemyPool.Instance.Init(_enemyPrefab);
    }

    public EnemyAttack CreateEnemy(EnemyType type, Vector3 position)
    {
        EnemyData enemyData =null;
        for (int i = 0; i < _enemiesConfig.configs.Length; i++)
        {
            if (_enemiesConfig.configs[i].enemyType == type)
            {
                enemyData = _enemiesConfig.configs[i].enemyData;
            }
        }
        if (_enemyPrefab == null) return null;
        EnemyAttack enemy = EnemyPool.Instance.GetEnemy(position);
        enemy.Init(_target, enemyData);
        return enemy;
    }
}
