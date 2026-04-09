using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class kilsPointVisual : MonoBehaviour
{
    private int _kilsPoints = 0;

    private GameContext _gameContext;


    private void Awake()
    {
        _gameContext.OnAddKillPoint += AddPoints;

    }

    private void AddPoints()
    {
        _kilsPoints++;

    }
}
