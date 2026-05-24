using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyAttack : MonoBehaviour
{
    [Header("Префаб снаряда")]
    [SerializeField] private BulletEnemy _projectilePrefab;

    [Header("Точка выстрела")]
    [SerializeField] private Transform _firePoint;
    
    [Header("Настройки стрельбы")]
    [SerializeField] private float _atackRate = 5;
    [SerializeField] private float _atackRange = 5;

    [Header("Настройки Снаряда")]
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileLifetime = 3f;

    [SerializeField] private EnemyVisual _enemyVisual;
    private EnemyData _enemyData;

    private EnemyHealth _enemyHealth;
    private BulletEnemy _bulletEnemy;

    private Transform _target;

    private void Awake()
    {
        _enemyHealth = GetComponent<EnemyHealth>();

    }

    public void Init(Transform playerPosition, EnemyData enemyData)
    {
        _enemyData = enemyData;
        StopAllCoroutines();
        _enemyVisual.ChangeSprite(enemyData._visual);
        _enemyHealth.EnemyTakeDamage(-(enemyData._enemyHealth - 1));   
        _target = playerPosition;
        _atackRate = enemyData._atackRate;
        _atackRange = enemyData._atackRange;
        gameObject.transform.LookAt(_target);
        StartCoroutine(AttackCoroutine());

    }

    public EnemyHealth GetHealth()
    {
        return _enemyHealth;
    }
    
    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_atackRate);

            Vector3 spread = new Vector3(
            Random.Range(-_atackRange, _atackRange),
            Random.Range(-_atackRange, _atackRange),
            Random.Range(-_atackRange, _atackRange)
            );
            Vector3 direction = (_target.position - _firePoint.position + spread).normalized;

            // Создание снаряда
            BulletEnemy projectile = Instantiate
         (
            _projectilePrefab,
            _firePoint.position,
            _firePoint.rotation
         );
            projectile.ChangeDamage(_enemyData._damage);

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * _projectileSpeed;
            }
            

            Destroy(projectile, _projectileLifetime);
            Debug.Log("Враг выстрелил!");

        }
    }
}
