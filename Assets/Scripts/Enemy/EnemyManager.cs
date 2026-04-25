using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private EnemyShelter[] _enemyShelters;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private AudioSource _damageAudioSource;

    private int _enemyCountInWave = 2;
    private int remainingToSpawnEnemies = 2;
    private int _aliveInWaveEnemies = 2;
    private int _enemysInFight = 0;

    private int _smallWave;
    private int _bigWave;
    

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
        StartNewWave();
    }

    private void StartNewWave()
    {
        _enemyCountInWave = _gameConfig.tenWave[_bigWave].waveDatas[_smallWave].EnemyCount;
        remainingToSpawnEnemies = _enemyCountInWave;
        _aliveInWaveEnemies = _enemyCountInWave;
        StartCoroutine(CreateBasicEnemy());

    }
    
    private IEnumerator CreateBasicEnemy()
    {
        yield return new WaitForSeconds(1f);

        yield return new WaitUntil(IsAnyShelterEmpty);

        CreateEnemy(FindEmptyShelterIndex());

        remainingToSpawnEnemies--;
        _enemysInFight++;
        if (remainingToSpawnEnemies > 0 && _enemysInFight <= _enemyShelters.Length)
        {
            StartCoroutine(CreateBasicEnemy());
        }
    }

    private void CreateEnemy(int shelterIndex)
    {
        if (shelterIndex == -1) return;

        //if (_enemyShelters[randomIndex].IsEnemyHere == false)   
        EnemyAttack enemy = _enemyFactory.CreateEnemy(EnemyType.Basic, _enemyShelters[shelterIndex].transform.position);
        _enemyShelters[shelterIndex].Fill(enemy);
    }

    public bool IsAnyShelterEmpty()
    {
        foreach (EnemyShelter shelter in _enemyShelters)
        {
            if (shelter != null && !shelter.IsEnemyHere)
                return true;

        }
        return false;
    }

    private int FindEmptyShelterIndex()
    {
        for (int i = 0; i < _enemyShelters.Length; i++)
        {
            if (_enemyShelters[i] != null && !_enemyShelters[i].IsEnemyHere)
                return i;
        }
        return -1;
    }

    IEnumerator WaveCouldown()
    {
        yield return new WaitForSeconds(10f);
        StartNewWave();
    }

    public void EnemyDeath()
    {
        _aliveInWaveEnemies--;
        _enemysInFight--;
        if (_aliveInWaveEnemies <= 0 && remainingToSpawnEnemies <= 0)
        {
            StartCoroutine(WaveCouldown());
        }
    }

}


