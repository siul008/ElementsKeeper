using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip musicClip;
    [SerializeField] float volume;
    void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.clip = musicClip;
        audioSource.Play();
    }
}
