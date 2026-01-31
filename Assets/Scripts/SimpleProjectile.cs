using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    [Header("Настройки снаряда")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private string targetTag = "Enemy";
    [SerializeField] private int _destroyTime = 3;



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Нанесение урона
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(_damage);
            }

            //Destroy(gameObject);
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Projectile"))
        {
            // Уничтожаем при столкновении с окружением
            Destroy(gameObject);


        }
    }

    IEnumerator DestroyTime()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);

    }
}
