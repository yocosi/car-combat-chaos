using System;
using System.Collections;
using System.Collections.Generic;
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
    public int playerScore;
    private bool isWin;
    private bool isOnPause;

    private void Start()
    {
        isWin = false;
        isOnPause = false;
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
        if (playerScore == 15)
        {
            Win();
        }
    }

    public bool GetWinCheck()
    {
        return isWin;
    }
}
