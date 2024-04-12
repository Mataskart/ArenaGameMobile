using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelUI;
    public TextMeshProUGUI playerLevelUI;
    private float timeSinceLastIncrement = 0f;
    private const float levelDuration = 30f;

    public static Level Instance { get; private set; }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        levelUI.text = "Level: 1";
        levelUI.gameObject.SetActive(true);
        playerLevelUI.text = "Level 1";
        playerLevelUI.gameObject.SetActive(true);
        Invoke("StopLevelBig", 3f);
    }

    // Update is called once per frame
    void Update()
    {
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
}
