using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _enemyHealth;
    [SerializeField] private CollisionDetector _collisionDetector;
    [SerializeField] private float _enemyHealthMax = 1f;

    public event Action OnEnemyDeath;

    private void Awake()
    {
        _collisionDetector.OnDamageCollision += EnemyTakeDamage;
        _enemyHealth = _enemyHealthMax;
    }

    public void EnemyTakeDamage(int damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0)
        {
            _enemyHealth = _enemyHealthMax;
            OnEnemyDeath?.Invoke();
        }
    }

    private void OnDestroy()
    {
        _collisionDetector.OnDamageCollision -= EnemyTakeDamage;
    }
}
