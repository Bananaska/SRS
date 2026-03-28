using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private int _enemyCountInWave=2;
    [SerializeField] private EnemyType _enemyType;
    [SerializeField] private EnemyFactory _enemyFactory;
    [SerializeField] private EnemyShelter[] _enemyShelters;


    private void Start()
    {
        StartCoroutine(CreateBasicEnemy());
    }

    IEnumerator CreateBasicEnemy()
    {

        yield return new WaitForSeconds(4f);
        int randomIndex = Random.Range(0, _enemyShelters.Length);
        if (_enemyShelters[randomIndex].IsEnemyHere == false)
        {
            EnemyAttack enemy = _enemyFactory.CreateEnemy(EnemyType.Basic, _enemyShelters[randomIndex].transform.position);
            _enemyShelters[randomIndex].Fill(enemy);
        }
        StartCoroutine(CreateBasicEnemy());

    }
}


