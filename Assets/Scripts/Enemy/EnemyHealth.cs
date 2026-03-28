using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _enemyHealth = 1f;
    [SerializeField] private CollisionDetector _collisionDetector;

    public event Action OnEnemyDeath;

    private void Awake()
    {
        _collisionDetector.OnDamageCollision += EnemyTakeDamage;
    }

    public void EnemyTakeDamage(int damage)
    {
        _enemyHealth -= damage;
        Debug.Log(_enemyHealth);
        if (_enemyHealth <= 0)
        {
            OnEnemyDeath?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _collisionDetector.OnDamageCollision -= EnemyTakeDamage;
    }
}
