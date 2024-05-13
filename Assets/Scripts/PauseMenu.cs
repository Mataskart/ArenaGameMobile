using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PauseMenu : MonoBehaviour  // THE BUTTON FOR THE PAUSE MENU IS ESCAPE (ESC)
{
    public GameObject pauseMenu;
    public GameObject winMenu;
    public Level levelScript;

    public static bool isPaused;
    private bool winMenuShown = false;

    // Start is called before the first frame update
    void Start()
    {
        levelScript = GetComponent<Level>();

        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        if (levelScript.GetLevel() >= 6 && !winMenuShown)
        {
            // Show the win menu
            winMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 1f; // Unpause the game
            winMenuShown = true; // Set winMenuShown to true
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
