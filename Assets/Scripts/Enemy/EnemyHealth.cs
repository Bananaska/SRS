using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _enemyHealth = 1f;

    public event Action OnEnemyHealthChange;

    public void EnemyTakeDamage(int damage)
    {
        _enemyHealth -= damage;
        Debug.Log(_enemyHealth);
        if (_enemyHealth < 0)
        {
            OnEnemyHealthChange?.Invoke();
        }
    }


}
