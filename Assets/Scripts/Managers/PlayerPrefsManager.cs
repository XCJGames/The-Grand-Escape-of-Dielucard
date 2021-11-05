using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager : MonoBehaviour
{
    // keys
    const string MASTER_VOLUME_KEY = "master volume";
    const string DIFFICULTY_KEY = "difficulty";
    const string HAS_STATS = "has stats";
    const string BEST_TIME_KEY = "best time";
    const string MIN_DEATHS_KEY = "min deaths";
    const string MAX_ENEMIES_KEY = "max enemies";
    const string MAX_TREASURES_KEY = "max treasures";

    // constraints
    const float MIN_VOLUME = 0f;
    const float MAX_VOLUME = 1f;
    const float MIN_DIFFICULTY = -2f;
    const float MAX_DIFFICULTY = 2f;

    public static bool CheckIfPrefsExist() 
        => PlayerPrefs.HasKey(MASTER_VOLUME_KEY);
    public static float GetMasterVolume() 
        => PlayerPrefs.GetFloat(MASTER_VOLUME_KEY);
    public static void SetMasterVolume(float volume)
    {
        if (volume >= MIN_VOLUME && volume <= MAX_VOLUME)
        {
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogError("Master volume is out of range");
        }
    }
    public static float GetDifficulty() 
        => PlayerPrefs.GetFloat(DIFFICULTY_KEY);
    public static void SetDifficulty(float difficulty)
    {
        if (difficulty >= MIN_DIFFICULTY && difficulty <= MAX_DIFFICULTY)
        {
            PlayerPrefs.SetFloat(DIFFICULTY_KEY, difficulty);
        }
        else
        {
            Debug.LogError("Difficulty is out of range");
        }
    }
    public static bool HasStats()
        => PlayerPrefs.GetInt(HAS_STATS) == 1 ? true : false;
    public static void SetHasStats() 
        => PlayerPrefs.SetInt(HAS_STATS, 1);
    public static float GetBestTime()
        => PlayerPrefs.GetFloat(BEST_TIME_KEY);
    public static void SetBestTime(float time)
    {
        if(!HasStats() || time < GetBestTime())
        {
            PlayerPrefs.SetFloat(BEST_TIME_KEY, time);
        }
    }
    public static int GetMinDeaths()
        => PlayerPrefs.GetInt(MIN_DEATHS_KEY);
    public static void SetMinDeaths(int deaths)
    {
        if (!HasStats() || deaths < GetMinDeaths())
        {
            PlayerPrefs.SetInt(MIN_DEATHS_KEY, deaths);
        }
    }
    public static float GetMaxEnemies()
        => PlayerPrefs.GetInt(MAX_ENEMIES_KEY);
    public static void SetMaxEnemies(int enemies)
    {
        if (!HasStats() || enemies > GetMaxEnemies())
        {
            PlayerPrefs.SetInt(MAX_ENEMIES_KEY, enemies);
        }
    }
    public static int GetMaxTreasures()
        => PlayerPrefs.GetInt(MAX_TREASURES_KEY);
    public static void SetMaxTreasures(int treasures)
    {
        if (!HasStats() || treasures > GetMaxTreasures())
        {
            PlayerPrefs.SetInt(MAX_TREASURES_KEY, treasures);
        }
    }
}
