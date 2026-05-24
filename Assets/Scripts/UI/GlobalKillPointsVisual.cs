using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GlobalKillPointsVisual : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;

    [SerializeField] private int _kilsPoints = 0;

    public static GlobalKillPointsVisual Instance;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("KilsPointVisual уже существует");
            return;
        }
        Instance = this;

    }

    public void AddPoints(int i)
    {
        _kilsPoints = i;
        _scoreText.text = _kilsPoints.ToString();
    }
}
