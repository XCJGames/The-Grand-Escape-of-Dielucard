using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] int healthAtStartOfLevel;
    private void Start()
    {
        healthAtStartOfLevel -= Mathf.RoundToInt(PlayerPrefsManager.GetDifficulty());
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
        LevelManager.Instance.LoadSameScene();
    }

    public void RestartGame()
    {
        LevelManager.Instance.LoadScene(LevelManager.Scenes.Level1);
        Destroy(gameObject);
    }
}
