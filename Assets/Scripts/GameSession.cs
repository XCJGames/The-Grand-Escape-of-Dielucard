using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSession : MonoBehaviour
{
    [SerializeField] int healthAtStartOfLevel;
    private void Awake()
    {
        if(FindObjectsOfType<GameSession>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
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
        FindObjectOfType<LevelLoader>().LoadSameScene();
    }

    public void RestartGame()
    {
        FindObjectOfType<LevelLoader>().LoadFirstLevel();
        Destroy(gameObject);
    }
}
