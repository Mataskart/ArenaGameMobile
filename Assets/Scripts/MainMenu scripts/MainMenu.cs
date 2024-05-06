using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Playables;
using Michsky.MUIP;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer musicMixer;
    public AudioMixer sfxMixer;
    public TMP_Text highscoreText;
    public TMP_Text enemiesKilledText;
    public TMP_Text gamesPlayedText;
    public TMP_Text timePlayedText;
    public TMP_Text totalDamageDealt;
    public TextMeshProUGUI moneyText;
    private int gamesPlayed;
    public AudioSource hoverSound;
    public AudioSource clickSound;

    void Start()
    {
        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
        UpdateStats();
        UpdateVolume();
    }

    void Update()
    {
        gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
    }
    public void PlayButton()
    {
        SceneManager.LoadScene("Default");
        SetGamesPlayed();
    }

    public void ConfirmYesButton()
    {
        PlayerPrefs.SetInt("HighScore", 0);
        PlayerPrefs.SetInt("TotalEnemiesKilled", 0);
        PlayerPrefs.SetInt("GamesPlayed", 0);
        PlayerPrefs.SetFloat("TotalTimePlayed", 0f);
        PlayerPrefs.SetFloat("TotalDamageDealt", 0f);
        PlayerPrefs.Save();

        UpdateStats();
    }
    public void QuitButton()
    {
        Application.Quit();
    }

    private void UpdateStats()
    {
        UpdateHighScore();
        UpdateEnemiesKilled();
        UpdateGamesPlayed();
        UpdateTimePlayed();
        UpdateDamageDealt();
        UpdateMoney();
    }

    private void UpdateHighScore()
    {
        if (highscoreText != null)
        {
            highscoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
            highscoreText.gameObject.SetActive(true);
        }
    }

    private void UpdateEnemiesKilled()
    {
        if (enemiesKilledText != null)
        {
            enemiesKilledText.text = "Enemies Killed: " + PlayerPrefs.GetInt("TotalEnemiesKilled", 0).ToString();
            enemiesKilledText.gameObject.SetActive(true);
        }
    }

    private void UpdateGamesPlayed()
    {
        if (gamesPlayedText != null)
        {
            gamesPlayedText.text = "Games Played: " + PlayerPrefs.GetInt("GamesPlayed", 0).ToString();
            gamesPlayedText.gameObject.SetActive(true);
        }
    }

    private void UpdateTimePlayed()
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

    private void UpdateDamageDealt()
    {
        if (timePlayedText != null)
        {
            totalDamageDealt.text = "Damage Dealt: " + PlayerPrefs.GetInt("TotalDamageDealt", 0).ToString();
            totalDamageDealt.gameObject.SetActive(true);
        }
    }
    private void SetGamesPlayed()
    {
        gamesPlayed++;
        PlayerPrefs.SetInt("GamesPlayed", gamesPlayed);
        PlayerPrefs.Save();
    }

    private void UpdateMoney()
    {
        if (moneyText != null)
        {
            int money = PlayerPrefs.GetInt("Money", 0);
            moneyText.text = "Money: " + money.ToString();
        }
    }

    private void UpdateVolume()
    {
        musicMixer.SetFloat("volume", PlayerPrefs.GetFloat("musicVolume"));
        sfxMixer.SetFloat("volume", PlayerPrefs.GetFloat("sfxVolume"));
    }

    public void PlayHoverSound()
    {
        hoverSound.PlayOneShot(hoverSound.clip);
    }
    public void PlayClickSound()
    {
        clickSound.PlayOneShot(clickSound.clip);
    }
}