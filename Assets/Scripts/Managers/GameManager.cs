using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] 
    private int healthAtStartOfLevel;
    [SerializeField]
    private int startingHealth = 3;

    [Header("Game Stats")]
    [SerializeField] 
    private int enemyCounter = 0;
    [SerializeField] 
    private int deathCounter = 0;
    [SerializeField] 
    private float gameTime = 0f;
    [SerializeField]
    private int treasures = 0;

    private bool inGame = false;

    public int EnemyCounter { get => enemyCounter; }
    public int DeathCounter { get => deathCounter; }
    public float GameTime { get => gameTime; }
    public int Treasures { get => treasures; }

    private void Start() 
        => healthAtStartOfLevel = startingHealth - Mathf.RoundToInt(PlayerPrefsManager.GetDifficulty());
    private void Update()
    {
        if (inGame)
        {
            gameTime += Time.deltaTime;
        }
    }

    public int GetHealthAtStartOfLevel() => healthAtStartOfLevel;
    public void SetHealthAtStartOfLevel(int health) => healthAtStartOfLevel = health;
    public void RestartLevel() => LevelManager.Instance.LoadSameScene();
    public void IncreaseEnemyCounter() => enemyCounter++;
    public void IncreaseDeathCounter() => deathCounter++;
    public void IncreaseTreasures() => treasures++;
    public void EndOfGame()
    {
        StopTimer();
        PlayerPrefsManager.SetBestTime(gameTime);
        PlayerPrefsManager.SetMinDeaths(deathCounter);
        PlayerPrefsManager.SetMaxEnemies(enemyCounter);
        PlayerPrefsManager.SetMaxTreasures(treasures);
        PlayerPrefsManager.SetHasStats();
    }
    public void StopTimer() => inGame = false;
    public void RestartGame()
    {
        LevelManager.Instance.LoadScene(LevelManager.Scenes.Level1);
        healthAtStartOfLevel = startingHealth - Mathf.RoundToInt(PlayerPrefsManager.GetDifficulty());
        enemyCounter = 0;
        deathCounter = 0;
        gameTime = 0f;
        treasures = 0;
        inGame = true;
    }
}
