using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Michsky.MUIP;

public class Level : MonoBehaviour
{
    private int level = 1; // Non-static level variable
    public TextMeshProUGUI levelUI;
    public TextMeshProUGUI playerLevelUI;
    private float timeSinceLastIncrement = 0f;
    private const float levelDuration = 30f;
    public static Level Instance { get; private set; }

    [SerializeField]
    private NotificationManager newAchievement;
    void Start()
    {
        levelUI.text = "Level: " + level.ToString();
        levelUI.gameObject.SetActive(true);
        playerLevelUI.text = "Level " + level.ToString();
        playerLevelUI.gameObject.SetActive(true);
        Invoke("StopLevelBig", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        CheckLvlAchievement();
        timeSinceLastIncrement += Time.deltaTime;

        if (timeSinceLastIncrement >= levelDuration)
        {
            level++;
            UpdateLevelUI();
            UpdateNewLevel();
            timeSinceLastIncrement = 0f;
        }

    }

    private void UpdateLevelUI()
    {
        levelUI.text = "Level: " + level.ToString();
    }

    private void UpdateNewLevel()
    {
        playerLevelUI.text = "Level " + level.ToString();
        playerLevelUI.gameObject.SetActive(true);
        Invoke("StopLevelBig", 3f);
    }

    private void StopLevelBig()
    {
        playerLevelUI.gameObject.SetActive(false);
    }

    public int GetLevel() // Updated method
    {
        return level;
    }

    public void CheckLvlAchievement()
    {
        if (level == 3 && PlayerPrefs.GetInt("SURVIVAL I", 0) == 0)
        {
            PlayerPrefs.SetInt("SURVIVAL I", 1);
            newAchievement.Open();
        }
    }
}
