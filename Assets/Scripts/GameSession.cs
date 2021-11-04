using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : Singleton<GameSession>
{
    [SerializeField] int healthAtStartOfLevel;
    private void Awake()
    {
        healthAtStartOfLevel -= Mathf.RoundToInt(PlayerPrefsController.GetDifficulty());
    }

    public int GetHealthAtStartOfLevel()
    {
        return healthAtStartOfLevel;
    }

    public void SetHealthAtStartOfLevel(int health)
    {
        healthAtStartOfLevel = health;
    }

    public void RestartLevel()
    {
        LevelLoader.Instance.LoadSameScene();
    }

    public void RestartGame()
    {
        LevelLoader.Instance.LoadFirstLevel();
        Destroy(gameObject);
    }
}
