using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPLayer : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _health = 5;

    public event Action<int> OnPlayerHealthChanged;
    public event Action OnDeath;

    public static HealthPLayer Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("HealthPLayer уже существует");
            return;
        }
        Instance = this;

    }

    public void PlayerDeath()
    {
        OnDeath?.Invoke();

    }

    public void PlayerHealthChanged(int value)
    {
        _health += value;

        if (_health < 0)
        {
            OnDeath?.Invoke();
            _health = 0;
        }
        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
        OnPlayerHealthChanged?.Invoke(_health);
    }
    
    private void OnDestroy()
    {
        OnDeath -= PlayerDeath;
        OnPlayerHealthChanged -= PlayerHealthChanged;
    }
}
