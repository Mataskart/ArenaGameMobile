using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class NewMain : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }

    public void PlayButton()
    {
        SceneManager.LoadScene("Default");
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    // FIGHTER SELECTION STARTS FROM HERE
}
