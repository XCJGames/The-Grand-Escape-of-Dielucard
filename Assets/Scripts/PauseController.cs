﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseController : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void PauseAndShowMenu()
    {
        Time.timeScale = 0f;
        gameObject.SetActive(true);
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().RestartLevel();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        FindObjectOfType<GameSession>().RestartGame();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        Destroy(FindObjectOfType<GameSession>());
        FindObjectOfType<LevelLoader>().LoadMainMenu();
    }
}
