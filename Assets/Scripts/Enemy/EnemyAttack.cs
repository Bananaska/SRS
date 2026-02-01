using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //[SerializeField] private int _damage = 1;
    [Header("Префаб снаряда")]
    public GameObject projectilePrefab;

    [Header("Точка выстрела")]
    public Transform firePoint;

    [Header("Игрок")]
    [SerializeField] private Transform _target;

    [Header("Настройки стрельбы")]
    [SerializeField] private float _atackRate = 5;
    [SerializeField] private float _atackRange = 5;

    [Header("Настройки Снаряда")]
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 3f;



    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(AttackCoroutine());
    }

    private void Shoot()
    {
        StartCoroutine(AttackCoroutine());

        Vector3 direction = (_target.position - firePoint.position).normalized;
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab не назначен!");
            return;
        }

        // Создаём снаряд
            GameObject projectile = Instantiate
         (
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
         );

            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * projectileSpeed;
            }
            Destroy(projectile, projectileLifetime);
            Debug.Log("Выстрел произведен!");

        
    }

    IEnumerator AttackCoroutine()
    {
        yield return new WaitForSeconds(_atackRate);
        Shoot();
    }
}
