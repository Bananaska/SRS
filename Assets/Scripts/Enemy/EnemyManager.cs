using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private List<EnemyType> _enemyTypesInSmallWave = new();
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private EnemyShelter[] _enemyShelters;
    [SerializeField] private GameConfig _gameConfig;
    [SerializeField] private AudioSource _damageAudioSource;
    [SerializeField] private SheltersAssigner _sheltersAssigner;

    private int _enemyCountInWave = 2;
    private int _remainingToSpawnEnemies = 2;
    private int _aliveInWaveEnemies = 2;
    private int _enemysInFight = 0;
    private int _heavyCount = 0;

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

        if (_gameConfig == null || _gameConfig.tenWave == null || _gameConfig.tenWave.Length == 0)
        {

            Debug.LogError("EnemyManager: GameConfig не назначен или пустой");

            return;

        }

        if (_bigWave >= _gameConfig.tenWave.Length) return;

        if (_smallWave >= _gameConfig.tenWave[_bigWave].waveDatas.Length) return;

        _sheltersAssigner.Mixing();

        StartNewWave();
    }

    private void StartNewWave()
    {
        _enemyTypesInSmallWave = null;

        _heavyCount = 0;
        _enemyCountInWave = _gameConfig.tenWave[_bigWave].waveDatas[_smallWave].EnemyCount;
        _enemyTypesInSmallWave = _gameConfig.tenWave[_bigWave].waveDatas[_smallWave].enemyType;
        _remainingToSpawnEnemies = _enemyCountInWave;
        _aliveInWaveEnemies = _enemyCountInWave;
        StartCoroutine(CreateEnemyCoroutine());
    }

    private IEnumerator CreateEnemyCoroutine()
    {
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(IsAnyShelterEmpty);

        // Получаем случайный свободный спавнер
        int shelterIndex = FindEmptyShelterIndex();
        int randomIndex = Random.Range(0, _enemyTypesInSmallWave.Count);
        if (randomIndex == 1 && _heavyCount < 2)
        {
            _heavyCount ++;
        }
        else if (randomIndex == 1 && _heavyCount >= 2)
        {
            randomIndex = 0;
        }
        CreateEnemy(shelterIndex, _enemyTypesInSmallWave[randomIndex]);

        _remainingToSpawnEnemies--;
        _enemysInFight++;
        if (_remainingToSpawnEnemies > 0 && _enemysInFight <= _enemyShelters.Length)
        {
            StartCoroutine(CreateEnemyCoroutine());
        }
    }

    private void CreateEnemy(int shelterIndex,
         EnemyType type)
    {
        if (shelterIndex == -1) return;

        EnemyAttack enemy = _enemyFactory.CreateEnemy(type, _enemyShelters[shelterIndex].transform.position);
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
            _sheltersAssigner.Mixing();

        }
        if (_bigWave >= _gameConfig.tenWave.Length)
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