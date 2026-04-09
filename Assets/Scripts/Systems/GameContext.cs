using System;
using Unity.VisualScripting;
using UnityEngine;
public class GameContext : MonoBehaviour
{
    private int _kilsPoints = 0;

    public static GameContext Instance;

    public event Action OnAddKillPoint;


    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            Debug.Log("GameContext уже существует");
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        SceneLoader.Instance.LoadNextScene();
    }

    public void AddKillsPoints()
    {
        _kilsPoints++;
        OnAddKillPoint.Invoke();
    }

}
