using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class kilsPointVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    private int _kilsPoints = 0;

    private GameContext _gameContext;


    private void Awake()
    {
        _gameContext.OnAddKillPoint += AddPoints;

    }

    private void AddPoints()
    {
        _kilsPoints++;
        _scoreText.text = _kilsPoints.ToString();
    }
}
