using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio Sources")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;
    [Header("Audio Clips")]

    public AudioClip level1;
    public AudioClip level2;
    public AudioClip level3;
    public AudioClip level4;
    public AudioClip playerAttack;
    public AudioClip enemyAttack;
    public AudioClip playerDeath;

    public void PlayLevel(AudioClip clip)
    {
        musicSource.PlayOneShot(clip);
    }
}
