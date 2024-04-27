using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using Michsky.MUIP;

public class PlayerMoney : MonoBehaviour
{
    public int money;
    public TextMeshProUGUI moneyUI;

    void Start()
    {
        money = PlayerPrefs.GetInt("Money", 0);
        moneyUI.text = "Money: " + money.ToString();
        moneyUI.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        AddMoney();
    }

    void AddMoney()
    {
        int score = GetComponent<PlayerScore>().score;
        money += score / 10;
        PlayerPrefs.SetInt("Money", money);
        PlayerPrefs.Save();
        moneyUI.text = "Money: " + money.ToString();
    }
}