using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] private float _enemyHealth = 1f;
    [SerializeField] private int _damage = 1;
    [SerializeField] private float _atackRate = 5;
    [SerializeField] private float _atackRange = 5;
    [SerializeField] private Sprite _visual;
    [SerializeField] private EnemyType _enemyType;
}
