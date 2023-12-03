using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject audioMenu;
    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnAudioSettingsOpen()
    {
        audioMenu.SetActive(true);
    }
    
    public void OnAudioSettingsClose()
    {
        audioMenu.SetActive(false);
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }
}


