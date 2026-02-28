using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefeatMenuController : MonoBehaviour
{
    [SerializeField] private DefeatMenuView _defeatMenu;
    [SerializeField] private int _sceneIndex = 1;
    

    private void Awake()
    {
        _defeatMenu.OnButtonMenuClicked += LoadMenuScene;
    }

    private void LoadMenuScene()
    {
        SceneLoader.Instance.LoadSceneByIndex(_sceneIndex);

    }

    private void OnDestroy()
    {
        _defeatMenu.OnButtonMenuClicked -= LoadMenuScene;
    }
}
