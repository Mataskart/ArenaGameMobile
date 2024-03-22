using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class ElapsedTime : MonoBehaviour
{
    private static bool timerRunning = false;
    private static float startTime;
    public TextMeshProUGUI timeUI;

    public Movement death;
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
        timeUI.text = "00:00";
        timeUI.gameObject.SetActive(true);
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
            float t = Time.time - startTime;
            string minutes = ((int)t / 60).ToString("00");
            string seconds = ((int)t % 60).ToString("00");

            timeUI.text = minutes + ":" + seconds;
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