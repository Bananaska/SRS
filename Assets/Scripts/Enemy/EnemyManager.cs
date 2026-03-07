using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int _enemyCountInWave;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private EnemyShelter[] _enemyShelters;


    private void Start()
    {
        CreateBasicEnemy();
    }

    private void CreateBasicEnemy()
    {
        while (true)
        {
            int randomIndex = Random.Range(0, _enemyShelters.Length);
            if (_enemyShelters[randomIndex].IsEnemyHere == false)
            {
                _enemyFactory.CreateEnemy(EnemyType.Basic, _enemyShelters[randomIndex].transform.position);
                _enemyShelters[randomIndex]
            }
        }
    }
}
