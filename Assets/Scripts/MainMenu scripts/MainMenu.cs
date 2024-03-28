using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenu : MonoBehaviour
{

    public TMP_Text highscoreText;
    public TMP_Text enemiesKilledText;
    public TMP_Text gamesPlayedText;
    public TMP_Text timePlayedText;
    public TMP_Text totalDamageDealt;
    private int gamesPlayed;

    void Start()
    {
        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
        UpdateHighScore();
        UpdateEnemiesKilled();
        UpdateGamesPlayed();
        UpdateTimePlayed();
        UpdateDamageDealt();
    }
    void Update()
    {

    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Default");
        SetGamesPlayed();
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void UpdateHighScore()
    {
        if (highscoreText != null)
        {
            highscoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
            highscoreText.gameObject.SetActive(true);
        }
    }

    public void UpdateEnemiesKilled()
    {
        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "Enemies Killed: " + PlayerPrefs.GetInt("TotalEnemiesKilled", 0).ToString();
            enemiesKilledText.gameObject.SetActive(true);
        }
    }

    public void UpdateGamesPlayed()
    {
        if (gamesPlayedText != null)
        {
            gamesPlayedText.text = "Games Played: " + PlayerPrefs.GetInt("GamesPlayed", 0).ToString();
            gamesPlayedText.gameObject.SetActive(true);
        }
    }

    public void UpdateTimePlayed()
    {
        if (timePlayedText != null)
        {
            float totalTimePlayed = PlayerPrefs.GetFloat("TotalTimePlayed", 0);
            int minutes = Mathf.FloorToInt(totalTimePlayed / 60);
            int seconds = Mathf.FloorToInt(totalTimePlayed % 60);
            string formattedTime = string.Format("{0} minutes {1} seconds", minutes, seconds);
            timePlayedText.text = "Time Played: " + formattedTime;
            timePlayedText.gameObject.SetActive(true);
        }
    }

    public void UpdateDamageDealt()
    {
        if (timePlayedText != null)
        {
            totalDamageDealt.text = "Damage Dealt: " + PlayerPrefs.GetInt("TotalDamageDealt", 0).ToString();
            totalDamageDealt.gameObject.SetActive(true);
        }
    }

    public void SetGamesPlayed()
    {
        gamesPlayed++;
        PlayerPrefs.SetInt("GamesPlayed", gamesPlayed);
        PlayerPrefs.Save();
    }
}
