using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [Header("Префаб снаряда")]
    [SerializeField] private GameObject _projectilePrefab;

    [Header("Точка выстрела")]
    [SerializeField] private Transform _firePoint;

    [Header("Игрок")]
    [SerializeField] private Transform _target;

    [Header("Настройки стрельбы")]
    [SerializeField] private float _atackRate = 5;
    [SerializeField] private float _atackRange = 5;

    [Header("Настройки Снаряда")]
    [SerializeField] private float _projectileSpeed = 10f;
    [SerializeField] private float _projectileLifetime = 3f;



    private void Start()
    {
        if (_target != null)
        {
            _target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        StartCoroutine(AttackCoroutine());

    }

    IEnumerator AttackCoroutine()
    {
        while (true)
        {
            Vector3 spread = new Vector3(
            Random.Range(-_atackRange, _atackRange),
            Random.Range(-_atackRange, _atackRange),
            Random.Range(-_atackRange, _atackRange)
            );
            Vector3 direction = (_target.position - _firePoint.position + spread).normalized;

            // Создаём снаряд
            GameObject projectile = Instantiate
         (
            _projectilePrefab,
            _firePoint.position,
            _firePoint.rotation
         );

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * _projectileSpeed;
            }
            Destroy(projectile, _projectileLifetime);
            Debug.Log("Враг выстрелил!");
            yield return new WaitForSeconds(_atackRate);

        }
    }
}
