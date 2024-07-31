using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicSlider : MonoBehaviour
{
    public Slider musicSlider;
    public Slider sfxSlider;
    [SerializeField] PlayerPrefs prefs;
    public AudioSource musicSource;
    public AudioSource sfxSource;
    // Start is called before the first frame update

    void Start()
    {
        musicSlider.value = prefs.GetMusicMult();
        musicSlider.minValue = 0;
        musicSlider.maxValue = 1;
        sfxSlider.value = prefs.GetSfxSound();
        sfxSlider.minValue = 0;
        sfxSlider.maxValue = 1;
    }

    // Update is called once per frame
    void Update()
    {
        prefs.SetMusicVolume(musicSlider.value);
        prefs.SetSfxVolume(sfxSlider.value);
        musicSource.volume = 0.20f * prefs.GetMusicMult();
        sfxSource.volume = prefs.GetSfxSound();
    }
}
