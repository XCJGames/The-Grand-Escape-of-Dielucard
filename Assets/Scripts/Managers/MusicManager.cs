using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : Singleton<MusicManager>
{
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PlayerPrefsManager.CheckIfPrefsExist())
        {
            audioSource.volume = PlayerPrefsManager.GetMasterVolume();
        }
        else
        {
            audioSource.volume = 0.8f;
            PlayerPrefsManager.SetMasterVolume(0.8f);
        }
    }
    public void SetVolume(float volume) => audioSource.volume = volume;
}
