using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShelter : MonoBehaviour
{
    [SerializeField] private EnemyHealth _enemyHealth;

    public bool IsEnemyHere;
    private EnemyAttack _enemy;

    private void Awake()
    {
        _enemyHealth.OnEnemyDeath += ShelterFree;
        
    }

    public void Fill()
    {
        
    }
    private void EnemyHere()
    {
        IsEnemyHere = true;
    }

    private void ShelterFree()
    {
        IsEnemyHere = false;
    }
}
