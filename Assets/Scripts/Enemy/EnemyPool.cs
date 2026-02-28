using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPool
{
    private GameObject _enemyPrefab;
    [SerializeField] private int poolSize = 10;

    private Queue<GameObject> pool = new Queue<GameObject>();
    void Start()
    {

        
    }
    public void Init(GameObject enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;

        for (int i = 0; i < poolSize; i++)
        {
            GameObject enemy = UnityEngine.Object.Instantiate(_enemyPrefab);
            enemy.SetActive(false);
            pool.Enqueue(enemy);
        }
    }
    public GameObject GetEnemy()
    {
        if (pool.Count > 0)
        {
            GameObject enemy = pool.Dequeue();
            enemy.SetActive(true); 
            return enemy;
        }
        else
        {
            Debug.LogWarning("Пул пуст! Возвращаю null.");
            return null;
        }
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false); 
        pool.Enqueue(enemy);   
    }

}
