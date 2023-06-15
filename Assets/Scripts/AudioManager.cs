using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField]private Sound[] musicThemes;
    [SerializeField]private Sound[] sfxSounds;
    [SerializeField]private Sound clickSound;
    [SerializeField] private AudioSource clickSource;
    public AudioSource musicSource;
    public AudioSource sfxSource;
   
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        PlayMusic("Menu");
    }


 
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicThemes, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySfx(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);
        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }

    public void ClickSound()
    {
        clickSource.PlayOneShot(clickSound.clip);
    }

    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute; 
    }

    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute;
    }

    public void VolumeMusic(float volume)
    {
        musicSource.volume = volume;
    }

    public void VolumeSFX(float volume)
    {
        sfxSource.volume = volume;
    }
}
