using System;
using System.Collections;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{

    private static readonly Lazy<EnemyPool> lazy =
        new Lazy<EnemyPool>(() => new EnemyPool());

    private EnemyAttack _enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<EnemyAttack> pool = new Queue<EnemyAttack>();

    public static EnemyPool Instance => lazy.Value;

    public void Init(EnemyAttack enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;

        for (int i = 0; i < poolSize; i++)
        {
            EnemyAttack enemy = UnityEngine.Object.Instantiate(_enemyPrefab);
            enemy.gameObject.SetActive(false);
            pool.Enqueue(enemy);
        }      
    }
    public EnemyAttack GetEnemy(Vector3 spawnPosition)
    {
        if (pool.Count > 0)
        {
            EnemyAttack enemy = pool.Dequeue();
            enemy.gameObject.SetActive(true); 
            enemy.transform.position = spawnPosition;
            return enemy;
        }
        else
        {
            Debug.LogWarning("Пул пуст! Возвращаю null.");
            return null;
        }
    }

    public void ReturnEnemy(EnemyAttack enemy)
    {
        pool.Enqueue(enemy);
        enemy.gameObject.SetActive(false);

    }

}
