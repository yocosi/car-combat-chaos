using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicManager : MonoBehaviour
{
    
    public GameObject gameOverScreen;
    public GameObject winScreen;
    public GameObject pauseMenu;
    public GameObject audioMenu;
    public Text score;
    public Text highestScore;
    public int playerScore;
    private bool isWin;
    private bool isOnPause;

    private void Start()
    {
        isWin = false;
        isOnPause = false;
        highestScore.text = PlayerPrefs.GetInt("playerHighestScore", 0).ToString();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOnPause)
            {
                OnPauseMenuOpen();
                isOnPause = true;
            }
            else
            {
                OnPauseMenuClose();
                isOnPause = false;
            }
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OnQuitButton()
    {
        Application.Quit();
    }

    private void OnPauseMenuOpen()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }
    
    public void OnPauseMenuClose()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
    
    public void OnAudioSettingsOpen()
    {
        audioMenu.SetActive(true);
    }
    
    public void OnAudioSettingsClose()
    {
        audioMenu.SetActive(false);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        if (PlayerPrefs.GetInt("playerHighestScore") < playerScore)
        {
            PlayerPrefs.SetInt("playerHighestScore", playerScore);
            PlayerPrefs.Save();
        }
    }

    public void Win()
    {
        isWin = true;
        winScreen.SetActive(true);
    }

    public void AddScore(int scoreToAdd)
    {
        playerScore += scoreToAdd;
        score.text = playerScore.ToString();
        if (playerScore == 100)
        {
            PlayerPrefs.SetInt("playerHighestScore", 100);
            PlayerPrefs.Save();
            Win();
        }
    }

    public bool GetWinCheck()
    {
        return isWin;
    }
}
