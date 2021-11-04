using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : Singleton<MusicPlayer>
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefsController.CheckIfPrefsExist())
        {
            audioSource.volume = PlayerPrefsController.GetMasterVolume();
        }
        else
        {
            audioSource.volume = 0.8f;
            PlayerPrefsController.SetMasterVolume(0.8f);
        }
    }

    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
