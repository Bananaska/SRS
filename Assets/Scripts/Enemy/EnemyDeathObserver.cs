using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathObserver : MonoBehaviour
{
    private const string DeathAnimationName = "EnemyDeath";

   
    [SerializeField] private Animator _animator;
    [SerializeField] private float _destroyTime = 0.51f;
    [SerializeField] private EnemyAttack _enemy;

    private EnemyHealth _enemyHealth;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();
        _enemyHealth.OnEnemyDeath += HandleEnemyDeath;
        _destroyTime = 0.51f;
    }

    private void HandleEnemyDeath()
    {
        GameContext.Instance.AddKillsPoints();
        KilsPointVisual.Instance.AddPoints();

        StartCoroutine(DestroyEnemy());
        _animator.SetBool(DeathAnimationName, true);
        EnemyPool.Instance.ReturnEnemy(_enemy);
        Debug.Log("Лошокпристрелен");
    }

    private void OnDestroy()
    {
        _enemyHealth.OnEnemyDeath -= HandleEnemyDeath;

    }

    IEnumerator DestroyEnemy()
    {
        yield return new WaitForSeconds(_destroyTime);
        _enemy.gameObject.SetActive(false);
        EnemyPool.Instance.ReturnEnemy(_enemy);
    }
}
