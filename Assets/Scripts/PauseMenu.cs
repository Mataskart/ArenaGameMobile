using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour  // THE BUTTON FOR THE PAUSE MENU IS ESCAPE (ESC)
{
    public GameObject pauseMenu;
    public GameObject winMenu;
    public Level levelScript;
    private TextMeshProUGUI victoryText;

    public static bool isPaused;
    private bool winMenuShown = false;

    public Joystick joystick;
    public GameObject pauseButton;
    public GameObject attackButton;
    public GameObject dashButton;

    // Start is called before the first frame update
    void Start()
    {
        levelScript = GetComponent<Level>();
        victoryText = winMenu.GetComponentInChildren<TextMeshProUGUI>();

        pauseMenu.SetActive(false);
        winMenu.SetActive(false);
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        // victory text colorful animation effect
        if (winMenu.activeSelf)
        {
            float t = Mathf.PingPong(Time.time, 1f) / 1f;
            victoryText.color = Color.Lerp(Color.red, Color.blue, t);
        }
        if (levelScript.gameOver && !winMenuShown)
        {
            // Show the win menu
            winMenu.SetActive(true);
            isPaused = true;
            Time.timeScale = 1f;
            winMenuShown = true; // Set winMenuShown to true
        }
    }

    public void PauseGame()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        joystick.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        attackButton.gameObject.SetActive(false);
        dashButton.gameObject.SetActive(false);
        AudioListener.pause = true; // also pause all audio
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        joystick.gameObject.SetActive(true);
        pauseButton.gameObject.SetActive(true);
        attackButton.gameObject.SetActive(true);
        dashButton.gameObject.SetActive(true);
        Time.timeScale = 1f;
        isPaused = false;
        AudioListener.pause = false; // unpause all audio
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

    public void PauseButton()
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
}
