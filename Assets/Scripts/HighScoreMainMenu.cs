using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class HighScoreMainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreUI;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHighScore();
        highScoreUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateHighScore()
    {
        highScoreUI.text = "High Score: " + PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}