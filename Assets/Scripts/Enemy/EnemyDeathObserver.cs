using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathObserver : MonoBehaviour
{
    private const string DeathAnimationName = "EnemyDeath";

    [SerializeField] private EnemyHealth _enemyHealth;
    [SerializeField] private Animator _animator;
    [SerializeField] private float _destroyTime;

    private void Awake()
    {
        _enemyHealth.OnEnemyHealthChange += HandleEnemyDeath;
        
    }

    private void HandleEnemyDeath()
    {
        GameContext.Instance.AddKillsPoints();
        StartCoroutine(DestroyEnemy());
        _animator.SetBool(DeathAnimationName, true);
    }

    private void OnDestroy()
    {
        _enemyHealth.OnEnemyHealthChange -= HandleEnemyDeath;

    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(_destroyTime);
        Destroy(gameObject);
    }
}
