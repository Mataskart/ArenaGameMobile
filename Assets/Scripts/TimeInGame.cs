using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElapsedTime : MonoBehaviour
{

    public float timestart;
    private bool timerActive = false;
    private float startTime;
    public TextMeshProUGUI timeUI;
    
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
        StartTimer();
        timeUI.text = "0";
        timeUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerActive)
        {
            float t = Time.time - startTime;
            string minutes = ((int) t / 60).ToString("00");
            string seconds = ((int) t % 60).ToString("00");

            timeUI.text = minutes + ":" + seconds;
        }
    }

    public void StartTimer()
    {
        startTime = Time.time;
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = false;
    }
}