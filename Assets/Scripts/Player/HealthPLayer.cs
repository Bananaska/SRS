using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPLayer : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 5;
    [SerializeField] private float _health = 5;
    public event Action OnPlayerHealthChanged;

    private void Awake()
    {
        
    }

    private void HealthChanged(float health)
    {

    }
}
