using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField] 
    private Slider volumeSlider, difficultySlider;
    [SerializeField] 
    private float defaultVolume = 0.8f;
    [SerializeField] 
    private float defaultDifficulty = 0;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button defaultButton;

    void Start()
    {
        if (PlayerPrefsManager.CheckIfPrefsExist())
        {
            volumeSlider.value = PlayerPrefsManager.GetMasterVolume();
            difficultySlider.value = PlayerPrefsManager.GetDifficulty();
        }
        else
        {
            volumeSlider.value = defaultVolume;
            difficultySlider.value = defaultDifficulty;
        }
        mainMenuButton.onClick.AddListener(SaveAndExit);
        defaultButton.onClick.AddListener(SetDefaults);
    }
    void Update() 
        => MusicManager.Instance.SetVolume(volumeSlider.value);
    private void SetDefaults()
    {
        volumeSlider.value = defaultVolume;
        difficultySlider.value = defaultDifficulty;
    }
    private void SaveAndExit()
    {
        PlayerPrefsManager.SetMasterVolume(volumeSlider.value);
        PlayerPrefsManager.SetDifficulty(difficultySlider.value);
        LevelManager.Instance.LoadScene(LevelManager.Scenes.Main);
    }
}
