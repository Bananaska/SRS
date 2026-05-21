using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KilsPointVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private int _kilsPoints = 0;

    [SerializeField] private HealthPlayer _healthPlayer;


    public static KilsPointVisual Instance;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("KilsPointVisual уже существует");
            return;
        }
        Instance = this;

        _healthPlayer.OnDeath += ResetToZero;
    }
    
    public void AddPoints(int i)
    {
        _kilsPoints = i;
        Debug.Log(_kilsPoints);
        _scoreText.text = _kilsPoints.ToString();
    }
    private void ResetToZero()
    {
        _kilsPoints = 0;
        _healthPlayer.OnDeath -= ResetToZero;

    }

    private void OnDestroy()
    {

    }
}
