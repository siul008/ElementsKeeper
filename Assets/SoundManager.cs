using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource musicSource;
    public AudioSource sfxSource;
    public Sound music;
    public Sound[] sfxSounds;

    private Dictionary<string, int> soundPlayCounts = new Dictionary<string, int>();
    private int maxSoundInstances = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic();
    }

    public void PlayMusic()
    {
        musicSource.loop = true;
        //musicSource.volume = music.volume;
        musicSource.clip = music.clip;
        musicSource.Play();
    }

    public IEnumerator PlaySfx(string name, float delay)
    {
        if (!soundPlayCounts.ContainsKey(name))
        {
            soundPlayCounts[name] = 0;
        }

        if (soundPlayCounts[name] >= maxSoundInstances)
        {
            yield break; // Skip playing this sound if the max count is reached
        }

        Sound sound = Array.Find(sfxSounds, s => s.name == name);

        if (sound != null)
        {
            float volume = sound.volume;
            yield return new WaitForSeconds(delay);
            if (soundPlayCounts[name] > 0)
            {
                volume = sound.volume / (soundPlayCounts[name] * 3);
            }
            sfxSource.PlayOneShot(sound.clip, volume);
            soundPlayCounts[name]++;
            yield return new WaitForSeconds(sound.clip.length);
            soundPlayCounts[name]--;
        }
    }

    public void InstantPlaySfx(string name, bool reduce)
    {
        if (!soundPlayCounts.ContainsKey(name))
        {
            soundPlayCounts[name] = 0;
        }

        if (soundPlayCounts[name] >= maxSoundInstances)
        {
            return ;
        }

        Sound sound = Array.Find(sfxSounds, s => s.name == name);

        if (sound != null)
        {
            float volume = sound.volume;
            if (soundPlayCounts[name] > 0 && reduce)
            {
                volume = sound.volume / ((soundPlayCounts[name] + 1) * 3);
            }
            soundPlayCounts[name]++;
            sfxSource.PlayOneShot(sound.clip, volume);
            StartCoroutine(ResetSoundCount(name, sound.clip.length));
        }
    }

    public void PlayUISound()
    {
        Sound sound = Array.Find(sfxSounds, s => s.name == "UI_Button");

        float volume = sound.volume;
        sfxSource.PlayOneShot(sound.clip, volume);
    }

    public void PlayUISoundDisabled()
    {
        Sound sound = Array.Find(sfxSounds, s => s.name == "UI_Button_Disabled");

        float volume = sound.volume;
        sfxSource.PlayOneShot(sound.clip, volume);
    }

    private IEnumerator ResetSoundCount(string name, float duration)
    {
        yield return new WaitForSeconds(duration);
        soundPlayCounts[name]--;
    }

    [System.Serializable]
    public class Sound
    {
        public string name;
        public float volume;
        public AudioClip clip;
    }
}
