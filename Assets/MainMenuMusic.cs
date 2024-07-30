using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMusic : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource UISource;
    [SerializeField] AudioClip musicClip;
    [SerializeField] float volume;
    [SerializeField] AudioClip UIClick;
    [SerializeField] AudioClip UIClickDisabled;
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

    public void PlayUISound()
    {
        UISource.PlayOneShot(UIClick, volume);
    }

    public void PlayUISoundDisabled()
    {
        UISource.PlayOneShot(UIClickDisabled);
    }
}
