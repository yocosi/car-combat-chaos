using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using UnityEngine;
using FMODUnity;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Volume")] 
    [Range(0, 1)] 
    private float masterVolume;
    
    [Range(0, 1)]
    private float musicVolume;
    
    [Range(0, 1)]
    private float SFXVolume;

    private Bus masterBus;
    private Bus musicBus;
    private Bus SFXBus;

    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    
    private List<EventInstance> eventInstances;
    
    private EventInstance musicEventInstance;
    
    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one audio manager in the scene.");
        }
        instance = this;

        eventInstances = new List<EventInstance>();

        masterBus = RuntimeManager.GetBus("bus:/");
        musicBus = RuntimeManager.GetBus("bus:/Music");
        SFXBus = RuntimeManager.GetBus("bus:/SFX");

        float masterPrefs = PlayerPrefs.GetFloat("masterVolume", -1);
        float musicPrefs = PlayerPrefs.GetFloat("musicVolume", -1);
        float sfxPrefs = PlayerPrefs.GetFloat("SFXVolume", -1);

        if (masterPrefs != -1)
        {
            masterVolume = masterPrefs;
            masterSlider.value = masterPrefs;
        }
        else
        {
            masterVolume = 1;
            masterSlider.value = 1;
        }

        if (musicPrefs != -1)
        {
            musicVolume = musicPrefs;
            musicSlider.value = musicPrefs;
        }
        else
        {
            musicVolume = 1;
            musicSlider.value = 1;
        }
        
        if (sfxPrefs != -1)
        {
            SFXVolume = sfxPrefs;
            sfxSlider.value = sfxPrefs;
        }
        else
        {
            SFXVolume = 1;
            sfxSlider.value = 1;
        }
    }
    
    private void Start()
    {
        InitializeMusic(FMODEvents.instance.music);
    }

    private void Update()
    {
        masterBus.setVolume(masterVolume);
        musicBus.setVolume(musicVolume);
        SFXBus.setVolume(SFXVolume);
    }

    private void InitializeMusic(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void PlayOneShot(EventReference sound, Vector3 playerPos)
    {
        RuntimeManager.PlayOneShot(sound, playerPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(eventInstance);
        return eventInstance;
    }

    private void CleanUp()
    {
        foreach (EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

    public void MasterVolumeSliderUpdate(float value)
    {
        PlayerPrefs.SetFloat("masterVolume", value);
        PlayerPrefs.Save();
        masterVolume = value;
    }
    
    public void MusicVolumeSliderUpdate(float value)
    {
        PlayerPrefs.SetFloat("musicVolume", value);
        PlayerPrefs.Save();
        musicVolume = value;
    }
    
    public void SfxVolumeSliderUpdate(float value)
    {
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
        SFXVolume = value;
    }
}
