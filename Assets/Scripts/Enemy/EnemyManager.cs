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
    private int _remainingToSpawnEnemies = 2;
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
        _remainingToSpawnEnemies = _enemyCountInWave;
        _aliveInWaveEnemies = _enemyCountInWave;
        StartCoroutine(CreateBasicEnemy());
    }

    private IEnumerator CreateBasicEnemy()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(IsAnyShelterEmpty);

        // Получаем случайный свободный спавнер
        int shelterIndex = FindEmptyShelterIndex();
        CreateEnemy(shelterIndex);

        _remainingToSpawnEnemies--;
        _enemysInFight++;
        if (_remainingToSpawnEnemies > 0 && _enemysInFight <= _enemyShelters.Length)
        {
            StartCoroutine(CreateBasicEnemy());
        }
    }

    private void CreateEnemy(int shelterIndex)
    {
        if (shelterIndex == -1) return;

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

    // Возвращает индекс случайного свободного убежища, либо -1
    private int FindEmptyShelterIndex()
    {
        List<int> emptyIndices = new List<int>();
        for (int i = 0; i < _enemyShelters.Length; i++)
        {
            if (_enemyShelters[i] != null && !_enemyShelters[i].IsEnemyHere)
                emptyIndices.Add(i);
        }

        if (emptyIndices.Count == 0)
            return -1;

        return emptyIndices[Random.Range(0, emptyIndices.Count)];
    }

    IEnumerator WaveCouldown()
    {
        yield return new WaitForSeconds(10f);
        _smallWave++;

        if (_smallWave >= _gameConfig.tenWave[_bigWave].waveDatas.Length)
        {
            _bigWave++;
            _smallWave = 0;
        }
        if (_bigWave > _gameConfig.tenWave.Length)
        {
            Debug.Log("100 волн!!! - 'Перезагрузка'");
            _bigWave = 0;
            _smallWave = 0;
        }
        StartNewWave();
    }

    public void EnemyDeath()
    {
        _aliveInWaveEnemies--;
        _enemysInFight--;
        if (_aliveInWaveEnemies <= 0 && _remainingToSpawnEnemies <= 0)
        {
            StartCoroutine(WaveCouldown());
        }
    }
}