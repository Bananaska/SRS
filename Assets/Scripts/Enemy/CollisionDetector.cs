using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action<int> OnDamageCollision;

    public void DamageCollision(int damage)
    {
        OnDamageCollision?.Invoke(damage);
    }
}
