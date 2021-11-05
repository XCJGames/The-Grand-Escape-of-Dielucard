using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button quitButton;
    private void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(LoadOptionsMenu);
        creditsButton.onClick.AddListener(LoadCreditsMenu);
        quitButton.onClick.AddListener(QuitGame);
        GameManager.Instance.StopTimer();
    }
    private void StartGame() => GameManager.Instance.RestartGame();
    private void LoadOptionsMenu() => LevelManager.Instance.LoadScene(LevelManager.Scenes.Options);
    private void LoadCreditsMenu() => LevelManager.Instance.LoadScene(LevelManager.Scenes.Credits);
    private void QuitGame() => LevelManager.Instance.QuitGame();
}
