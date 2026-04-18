using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFactory : MonoBehaviour
{
    [SerializeField] private EnemyData[] _enemyDatas;
    [SerializeField] private EnemyAttack _enemyPrefab;
    [SerializeField] private Transform _target;

    private void Awake()
    { 
        EnemyPool.Instance.Init(_enemyPrefab);
    }

    public EnemyAttack CreateEnemy(EnemyType type, Vector3 position)
    {
        if (_enemyPrefab == null) return null;
        EnemyAttack enemy = EnemyPool.Instance.GetEnemy(position);
        enemy.Init(_target);
        return enemy;
    }
}
