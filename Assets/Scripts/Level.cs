using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Level : MonoBehaviour
{
    public int level;
    public TextMeshProUGUI levelUI;

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
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastIncrement += Time.deltaTime;

        if (timeSinceLastIncrement >= levelDuration)
        {
            level++;
            UpdateLevelUI();
            timeSinceLastIncrement = 0f;
        }

    }

    private void UpdateLevelUI()
    {
        levelUI.text = "Level: " + level.ToString();
    }
}
