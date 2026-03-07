using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private EnemyAttack _enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<EnemyAttack> pool = new Queue<EnemyAttack>();

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
        enemy.gameObject.SetActive(false); 
        pool.Enqueue(enemy);   
    }

}
