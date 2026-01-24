using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuView : MonoBehaviour
{
    [SerializeField] private Button _playButton;

    public event Action OnPlayButtonClicked;

    private void Awake()
    {
        _playButton.onClick.AddListener(PlayButtonClicked);
    }

    private void PlayButtonClicked()
    {
        OnPlayButtonClicked?.Invoke();
    }
    private void OnDestroy()
    {
        _playButton.onClick.RemoveListener(PlayButtonClicked);
    }

}
