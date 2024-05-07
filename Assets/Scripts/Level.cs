using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Michsky.MUIP;
using UnityEngine.Tilemaps;
using System;

public class Level : MonoBehaviour
{
    [SerializeField] private ProgressBar progressBar;
    private int level = 1; // Non-static level variable
    public TextMeshProUGUI levelUI;
    public TextMeshProUGUI playerLevelUI;
    private float timeSinceLastIncrement = 0f;
    private const float levelDuration = 30f;
    public static Level Instance { get; private set; }
    public GameObject tilemap_level_1;
    public GameObject tilemap_level_2;
    public GameObject tilemap_level_3;
    public GameObject tilemap_level_4;
    public GameObject tilemap_boss;
    public Animator transition;
    private const float transitionDuration = 1f;
    void Start()
    {
        progressBar.maxValue = levelDuration;
        UpdateLevelUI();
        levelUI.gameObject.SetActive(true);
        playerLevelUI.text = "Level " + ToRomanNumeral(level);
        playerLevelUI.gameObject.SetActive(true);
        Invoke("StopLevelBig", 3f);
        transition.gameObject.SetActive(false);
        progressBar.isOn = false;
    }

    // Update is called once per frame
    void Update()
    {
        CheckAchievement();
        progressBar.isOn = true;
        timeSinceLastIncrement += Time.deltaTime;

        if (timeSinceLastIncrement >= levelDuration)
        {
            level++;
            UpdateLevelUI();
            UpdateNewLevel();
            timeSinceLastIncrement = 0f;
            UpdateTilemap(tilemap_level_1, tilemap_level_2, 2);
            UpdateTilemap(tilemap_level_2, tilemap_level_3, 3);
            UpdateTilemap(tilemap_level_3, tilemap_boss, 4);
            UpdateTilemap(tilemap_boss, tilemap_level_4, 5);
        }
    }

    private void UpdateLevelUI()
    {
        levelUI.text = ToRomanNumeral(level);
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
    public void CheckAchievement()
    {
        int gamesPlayed = PlayerPrefs.GetInt("GamesPlayed", 0);
        GameObject achievementManager = GameObject.Find("AchievementManager");
        AchievementManager achievementScript = achievementManager.GetComponent<AchievementManager>();

        GameObject player = GameObject.Find("Player");
        PlayerScore scoreScript = player.GetComponent<PlayerScore>();
        int enemiesKilled = scoreScript.GetEnemiesKilled();

        if (level == 2 && enemiesKilled == 0)
        {
            achievementScript.CompleteAchievement("FRIENDLY WARRIOR");
        }
        if (level == 3)
        {
            achievementScript.CompleteAchievement("SURVIVAL I");
        }
        if (level == 40)
        {
            achievementScript.CompleteAchievement("UNBEATABLE WARRIOR");
        }

        if (gamesPlayed == 1)
        {
            achievementScript.CompleteAchievement("WELCOME WARRIOR");
        }


        achievementScript.CheckLast();
    }

    void UpdateTilemap(GameObject tilemap_current, GameObject tilemap_new, int levelNeeded)
    {
        // Toggle the active tilemap based on the level
        if (level == levelNeeded)
        {
            StartCoroutine(TransitionAndDeactivate(tilemap_current, tilemap_new));
        }
    }

    IEnumerator TransitionAndDeactivate(GameObject tilemap_current, GameObject tilemap_new)
    {
        transition.gameObject.SetActive(false);
        transition.gameObject.SetActive(true);
        // Start the transition animation
        transition.SetTrigger("Start");

        // Wait for 0.5 seconds
        yield return new WaitForSeconds(1f);

        // Deactivate the current tilemap
        tilemap_current.SetActive(false);
        SpawnPlayer();
        // Activate the new tilemap
        tilemap_new.SetActive(true);
    }

    void SpawnPlayer()
    {
        // Spawn the player at the start of the level
        GameObject player = GameObject.Find("Player");
        player.transform.position = new Vector3(0, 0, 0);
    }

    string ToRomanNumeral(int number)
    {
        if (number < 1 || number > 3999)
            throw new ArgumentOutOfRangeException(nameof(number), "The number must be between 1 and 3999.");

        string[] thousands = { "", "M", "MM", "MMM" };
        string[] hundreds = { "", "C", "CC", "CCC", "CD", "D", "DC", "DCC", "DCCC", "CM" };
        string[] tens = { "", "X", "XX", "XXX", "XL", "L", "LX", "LXX", "LXXX", "XC" };
        string[] ones = { "", "I", "II", "III", "IV", "V", "VI", "VII", "VIII", "IX" };

        // Break the number into its thousands, hundreds, tens, and ones components
        int thousand = number / 1000;
        int hundred = (number % 1000) / 100;
        int ten = (number % 100) / 10;
        int one = number % 10;

        // Build the Roman numeral representation
        return thousands[thousand] + hundreds[hundred] + tens[ten] + ones[one];
    }

}
