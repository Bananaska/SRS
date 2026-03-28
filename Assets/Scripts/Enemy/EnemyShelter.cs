using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShelter : MonoBehaviour
{
    private EnemyHealth _enemyHealth;

    public bool IsEnemyHere;
    private EnemyAttack _enemy;

    public void Fill(EnemyAttack enemy)
    {
        _enemyHealth = enemy.GetComponent<EnemyHealth>();
        EnemyHere();
        _enemyHealth.OnEnemyDeath += ShelterFree;
    }
    private void EnemyHere()
    {
        IsEnemyHere = true;
    }

    private void ShelterFree()
    {
        _enemyHealth.OnEnemyDeath -= ShelterFree;
        _enemyHealth = null;
        IsEnemyHere = false;
    } 
    
}
