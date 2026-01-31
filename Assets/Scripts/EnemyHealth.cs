using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float _enemyHealth = 1f;

    public void TakeDamage(int damage)
    {
        _enemyHealth -= damage;
        Debug.Log(_enemyHealth);
    }


}
