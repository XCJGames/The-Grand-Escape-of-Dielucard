using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public enum Scenes
    {
        Main,
        Level1,
        Level2,
        Level3,
        Credits,
        Options
    }
    private bool isTransitioning = false;
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoad;
    }
    public void LoadScene(Scenes scene)
    {
        if (!isTransitioning)
        {
            isTransitioning = true;
            Transition();
            StartCoroutine(WaitAndLoad(scene));
        }
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void LoadSameScene()
    {
        LoadScene((Scenes)SceneManager.GetActiveScene().buildIndex);
    }
    public void EndOfTransition()
    {
        isTransitioning = false;
    }
    private IEnumerator WaitAndLoad(Scenes scene)
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene((int)scene);
    }
    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        Transition();
    }
    private void Transition()
    {
        GetComponent<Animator>().SetTrigger("Transition");
    }
}
