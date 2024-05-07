using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    public MainMenu mainMenuScript;
    void Start()
    {
        mainMenuScript.UpdateMoney();
        mainMenuScript.CheckIfPurchaseAvailable();
    }

    void Update()
    {
        mainMenuScript.UpdateMoney();
        mainMenuScript.CheckIfPurchaseAvailable();
    }
}
