using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KilsPointVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private int _kilsPoints = 0;

    //private EnemyHealth _enemyHealth;

    public static KilsPointVisual Instance;


    private void Awake()
    {
        //_enemyHealth.OnEnemyDeath += AddPoints;
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("KilsPointVisual уже существует");
            return;
        }
        Instance = this;
    }
    
    public void AddPoints()
    {
        _kilsPoints++;
        Debug.Log(_kilsPoints);
        _scoreText.text = _kilsPoints.ToString();
    }

    private void OnDestroy()
    {
       // _enemyHealth.OnEnemyDeath -= AddPoints;

    }
}
