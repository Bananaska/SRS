using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DefatMenuView : MonoBehaviour
{
    [SerializeField] private Button _menuButton;

    public event Action OnButtonMenuClicked;

    private void Awake()
    {
        _menuButton.onClick.AddListener(MenuButtonClicked);
    }

    private void MenuButtonClicked()
    {
        OnButtonMenuClicked?.Invoke();
    }
    private void OnDestroy()
    {
        _menuButton.onClick.RemoveListener(MenuButtonClicked);
    }
}
