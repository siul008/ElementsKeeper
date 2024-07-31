using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPrefs : MonoBehaviour
{
    float musicVolume;
    float sfxVolume;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        musicVolume = 0.5f;
        sfxVolume = 0.5f;
    }

    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
    }

    public void SetSfxVolume(float volume)
    {
        sfxVolume = volume;
    }

    public float GetMusicMult()
    {
        return musicVolume;
    }

    public float GetSfxSound()
    {
        return sfxVolume;
    }
}
