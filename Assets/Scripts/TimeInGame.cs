using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ElapsedTime : MonoBehaviour
{
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
        startTime = Time.time;
        timeUI.text = "0";
        timeUI.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        float t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString("00");
        string seconds = ((int)t % 60).ToString("00");

        timeUI.text = minutes + ":" + seconds;
    }
}