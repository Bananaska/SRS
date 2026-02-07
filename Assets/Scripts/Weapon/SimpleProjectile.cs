using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : MonoBehaviour
{
    [Header("Настройки снаряда")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private string targetTag = "Enemy";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log("Попадание");

            // Нанесение урона
            EnemyHealth health = other.gameObject.GetComponent<EnemyHealth>();
            if (health != null)
            {
                health.TakeDamage(_damage);
                Debug.Log("Попадание");
            }

            //Destroy(gameObject);
        }
        else if (other)
        {
            Destroy(gameObject);
        }
    }
}
