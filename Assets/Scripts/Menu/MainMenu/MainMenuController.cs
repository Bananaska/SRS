using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private MainMenuView _mainMenu;

    private void Awake()
    {
        _mainMenu.OnPlayButtonClicked += PlayGame;
    }

    private void PlayGame()
    {
        SceneLoader.Instance.LoadNextScene();

    }
}
