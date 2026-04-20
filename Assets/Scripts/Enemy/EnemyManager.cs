using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private EnemyShelter[] _enemyShelters;
    [SerializeField] private GameConfig _gameConfig;

    private int _enemyCountInWave = 2;
    private int remainingEnemys = 2;
    private int _liveEnemys = 2;
    private int _enemysInFight = 0;

    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("EnemyManager уже существует");
            return;
        }
        Instance = this;
        Wave();
    }

    private void Wave()
    {
        _enemyCountInWave += 2;
        remainingEnemys = _enemyCountInWave;
        _liveEnemys = _enemyCountInWave;
        StartCoroutine(CreateBasicEnemy());
    }
    
    public void EnemyDeath()
    {
        _liveEnemys--;
        _enemysInFight--;
        if (_liveEnemys <=0 && remainingEnemys <= 0)
        {
            StartCoroutine(WaveCouldown());
        }
    }

    IEnumerator CreateBasicEnemy()
    {
        yield return new WaitForSeconds(1f);
        int randomIndex = Random.Range(0, _enemyShelters.Length);
        if (_enemyShelters[randomIndex].IsEnemyHere == false)
        {
            EnemyAttack enemy = _enemyFactory.CreateEnemy(EnemyType.Basic, _enemyShelters[randomIndex].transform.position);
            _enemyShelters[randomIndex].Fill(enemy);
        }
        remainingEnemys--;
        _enemysInFight++;
        if (remainingEnemys > 0 && _enemysInFight <= 10)
        {
            StartCoroutine(CreateBasicEnemy());
        }
    }

    IEnumerator WaveCouldown()
    {
        yield return new WaitForSeconds(10f);
        Wave();
    }
}


