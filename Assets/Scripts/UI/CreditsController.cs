using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    [SerializeField]
    private Button mainMenuButton;

    [Header("Stats")]
    [SerializeField]
    private TextMeshProUGUI time;
    [SerializeField]
    private TextMeshProUGUI bestTime;
    [SerializeField]
    private TextMeshProUGUI deaths;
    [SerializeField]
    private TextMeshProUGUI minDeaths;
    [SerializeField]
    private TextMeshProUGUI enemies;
    [SerializeField]
    private TextMeshProUGUI maxEnemies;
    [SerializeField]
    private TextMeshProUGUI treasures;
    [SerializeField]
    private TextMeshProUGUI maxTreasures;

    private void Start()
    {
        mainMenuButton.onClick.AddListener(LoadMainMenu);
        DrawStats();
        GameManager.Instance.StopTimer();
    }
    private void DrawStats()
    {
        time.text = FormatTime(GameManager.Instance.GameTime);
        deaths.text = GameManager.Instance.DeathCounter.ToString();
        enemies.text = GameManager.Instance.EnemyCounter.ToString();
        treasures.text = GameManager.Instance.Treasures.ToString();
        bestTime.text = FormatTime(PlayerPrefsManager.GetBestTime());
        minDeaths.text = PlayerPrefsManager.GetMinDeaths().ToString();
        maxEnemies.text = PlayerPrefsManager.GetMaxEnemies().ToString();
        maxTreasures.text = PlayerPrefsManager.GetMaxTreasures().ToString();
    }

    private string FormatTime(float gameTime)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(gameTime);
        return timeSpan.ToString("hh':'mm':'ss");
    }

    private void LoadMainMenu() 
        => LevelManager.Instance.LoadScene(LevelManager.Scenes.Main);
}
