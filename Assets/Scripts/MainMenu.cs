using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using System.Threading;

public class MainMenu : MonoBehaviour
{
    public AudioSource sound;
    public AudioClip click;
    public AudioClip hover;
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ClickSound()
    {
        sound.PlayOneShot(click);
    }

        public void HoverSound()
    {
        sound.PlayOneShot(hover);
    }
}
