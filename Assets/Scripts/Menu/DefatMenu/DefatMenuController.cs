using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefatMenuController : MonoBehaviour
{
    [SerializeField] private DefatMenuView _defatMenu;

    private void Awake()
    {
        _defatMenu.OnButtonMenuClicked += PlayGame;
    }

    private void PlayGame()
    {
        //SceneLoader.Instance.LoadNextScene();

    }

    private void OnDestroy()
    {

        _defatMenu.OnButtonMenuClicked -= PlayGame;
    }
}
