using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class EnemyPool
{

    private static readonly object _lock = new object();
    private static EnemyPool _instance;

    private EnemyAttack _enemyPrefab;
    private int poolSize = 10;
    private Queue<EnemyAttack> pool = new Queue<EnemyAttack>();

    public static EnemyPool Instance
    {
        get
        {
            lock (_lock)
            {
                return _instance ??= new EnemyPool();
            }
        }
    }

    public void Init(EnemyAttack enemyPrefab)
    {
        _enemyPrefab = enemyPrefab;

        for (int i = 0; i < poolSize; i++)
        {
            EnemyAttack enemy = CreateNewEnemy();
            pool.Enqueue(enemy);
        }

        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public EnemyAttack GetEnemy(Vector3 spawnPosition)
    {
        EnemyAttack enemy;
        if (pool.Count > 0)
            enemy = pool.Dequeue();
        else
            enemy = CreateNewEnemy();

        enemy.gameObject.SetActive(true);
        enemy.transform.position = spawnPosition;
        Debug.Log("POOL COUNT " + pool.Count);
        return enemy;

    }

    private EnemyAttack CreateNewEnemy()
    {
        EnemyAttack enemy = UnityEngine.Object.Instantiate(_enemyPrefab);
        enemy.gameObject.SetActive(false);
        return enemy;
    }

    public void ReturnEnemy(EnemyAttack enemy)
    {
        pool.Enqueue(enemy);
        Debug.Log("Я вернул врага в пулл, сейчас размер пулла " + pool.Count);

    }

    private void OnSceneUnloaded(Scene scene)
    { 
        ClearPool();
    }

    public void ClearPool()
    {
        while (pool.Count > 0)
        {
            var enemy = pool.Dequeue();
            if (enemy != null)
            {
                UnityEngine.Object.Destroy(enemy.gameObject);
            }
        }
    }

    ~EnemyPool()
    {
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }

}
