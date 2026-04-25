using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingController : MonoBehaviour
{
    [Header("Префаб снаряда")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("Точка выстрела")]
    [SerializeField] private Transform firePoint;

    [Header("Настройки стрельбы")]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 3f;

    [Header("ZoV")]
    [SerializeField] private bool _possibleShoot = true;
    [SerializeField] private AudioClip _shootAudioSource;

    public void TryShoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab не назначен!");
            return;
        }

        // Создаем снаряд
        if (_possibleShoot == true)
        {
            _possibleShoot = false;
            StartCoroutine(RateFire());
            GameObject projectile = Instantiate
         (
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
         );
            AudioSource.PlayClipAtPoint(_shootAudioSource, transform.position);
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = firePoint.forward * projectileSpeed;
            }
            Destroy(projectile, projectileLifetime);
            Debug.Log("Выстрел произведен!");

        }
    }

    // Визуализация точки выстрела в редакторе
    private void OnDrawGizmosSelected()
    {
        if (firePoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(firePoint.position, 0.1f);
            Gizmos.DrawLine(firePoint.position, firePoint.position + firePoint.forward * 1f);
        }
    }

    IEnumerator RateFire()
    {
       yield return new WaitForSeconds(fireRate); 
        _possibleShoot = true;
    }
}