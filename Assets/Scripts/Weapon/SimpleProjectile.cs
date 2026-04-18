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
        if (other.CompareTag("Enemy"))
        {
            Debug.Log("Попадание");

            var collisionDetector = other.gameObject.GetComponent<CollisionDetector>();
            collisionDetector.DamageCollision(_damage);
            Destroy(gameObject);

        }
        else if (other)
        {
            Debug.Log("снаряд игрока уничтожен");
            Destroy(gameObject);
        }
    }
}
