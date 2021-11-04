using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    [SerializeField]
    private Button mainMenuButton;

    private void Start() => mainMenuButton.onClick.AddListener(LoadMainMenu);

    private void LoadMainMenu() => LevelManager.Instance.LoadScene(LevelManager.Scenes.Main);
}
