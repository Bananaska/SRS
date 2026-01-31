using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Префаб снаряда")]
    public GameObject projectilePrefab;

    [Header("Точка выстрела")]
    public Transform firePoint;

    [Header("Настройки стрельбы")]
    [SerializeField] private float fireRate = 0.5f;
    [SerializeField] private float projectileSpeed = 10f;
    [SerializeField] private float projectileLifetime = 3f;

    private float nextFireTime = 0f;
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;

        // Автоматически ищем точку выстрела если не назначена
        if (firePoint == null)
        {
            firePoint = transform.Find("FirePoint");
            if (firePoint == null)
            {
                firePoint = transform;
                Debug.LogWarning("FirePoint не найден, использую позицию объекта");
            }
        }
    }

    private void Update()
    {
        HandleShooting();
    }

    private void HandleShooting()
    {
        // Проверяем нажатие ЛКМ и кулдаун
        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    private void Shoot()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError("Projectile prefab не назначен!");
            return;
        }

        // Создаем снаряд
        GameObject projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        // Добавляем скорость снаряду
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = firePoint.forward * projectileSpeed;
        }
        else
        {
            // Для 2D игр
            Rigidbody2D rb2D = projectile.GetComponent<Rigidbody2D>();
            if (rb2D != null)
            {
                rb2D.velocity = firePoint.right * projectileSpeed;
            }
        }

        // Уничтожаем снаряд через время
        Destroy(projectile, projectileLifetime);

        // Визуальная обратная связь
        Debug.Log("Выстрел произведен!");
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
}