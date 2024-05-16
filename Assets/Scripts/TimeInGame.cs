using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElapsedTime : MonoBehaviour
{
    private static bool timerRunning = false;
    private static float startTime;
    private static float totalTimePlayed;
    private Movement death;
    public static ElapsedTime Instance { get; private set; }

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
        death = GetComponent<Movement>();
        StartTimer();
        totalTimePlayed = PlayerPrefs.GetFloat("TotalTimePlayed", 0f);
    }

    void OnDestroy()
    {
        PlayerPrefs.SetFloat("TotalTimePlayed", totalTimePlayed);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if (death.GetDeath())
        {
            StopTimer();
        }
        if (timerRunning)
        {
            totalTimePlayed += Time.deltaTime;
        }
    }

    public void StartTimer()
    {
        timerRunning = true;
        startTime = Time.time;
    }

    public static void StopTimer()
    {
        timerRunning = false;
    }
}