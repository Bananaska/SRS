using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData", menuName = "ScriptableObjects/EnemyData", order = 1)]
public class EnemyData : ScriptableObject
{
    [SerializeField] public int _enemyHealth = 1;
    [SerializeField] public int _damage = 1;
    [SerializeField] public float _atackRate = 5;
    [SerializeField] public float _atackRange = 5;
    [SerializeField] public Sprite _visual;
    //[SerializeField] private EnemyType _enemyType;
}
