using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    [Header("Настройки снаряда")]
    [SerializeField] private int _damage = 1;
    [SerializeField] private string targetTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            // Нанесение урона
            HealthPlayer _health = other.gameObject.GetComponent<HealthPlayer>();
            if (_health != null)
            {
                _health.PlayerHealthChanged(-_damage);
                Debug.Log($"Урон от пули: {_damage}");
            }

            Destroy(gameObject);
        }
        else if (other)
        {
            Destroy(gameObject);
        }
    }

    public void ChangeDamage(int change)
    {
        _damage = change;
    } 
}
