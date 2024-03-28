using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class PlayerScore : MonoBehaviour
{
    private int totalEnemiesKilled;
    public int score;
    private int scoreMultiplier; // Multiplier for score, starts at 1x and doubles up to 8x, degrades back to 1x after 5 seconds of no kills
    private float scoreMultiplierTimer; // Timer for tracking score multiplier duration
    private int scoreForBasicEnemy = 10;
    private int scoreForBossEnemy = 100;
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoreMultiplier = 1;
        scoreUI.text = "Score: 0";
        scoreUI.gameObject.SetActive(true);
        EnemyScript.OnEnemyKilled += Enemy_OnEnemyKilled;
        UpdateHighScore();
        highScoreUI.gameObject.SetActive(true);
        totalEnemiesKilled = PlayerPrefs.GetInt("TotalEnemiesKilled", 0);
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = "Score: " + score.ToString();
        if (scoreMultiplier > 1)
        {
            scoreMultiplierTimer += Time.deltaTime;
            if (scoreMultiplierTimer >= 10) // 10 seconds
            {
                scoreMultiplier = 1;
                scoreMultiplierTimer = 0;
            }
        }
    }

    public void Enemy_OnEnemyKilled(EnemyScript enemy)
    {
        totalEnemiesKilled++;
        PlayerPrefs.SetInt("TotalEnemiesKilled", totalEnemiesKilled);
        PlayerPrefs.Save();
        // Increase score and reset timer when an enemy is killed
        score += scoreForBasicEnemy * scoreMultiplier;
        CheckHighScore();
        UpdateHighScore();
        scoreMultiplierTimer = 0;

        // Increase multiplier, up to a maximum of 8
        if (scoreMultiplier < 8)
        {
            scoreMultiplier *= 2;
        }
    }

    private void OnDestroy()
    {
        EnemyScript.OnEnemyKilled -= Enemy_OnEnemyKilled;
    }

    void CheckHighScore()
    {
        if (score > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", score);
        }
    }

    void UpdateHighScore()
    {
        highScoreUI.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}