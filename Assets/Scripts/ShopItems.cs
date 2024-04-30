using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    private Health playerHealth;
    private Movement playerMovement;
    private PlayerScore playerScore;

    public ParticleSystem regenParticles;
    public RawImage healthPotionImage;
    public bool isHealthPotionPurchased = false;
    private bool isRegenerating = false;
    public int regenerationRate = 5;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<Health>();
        playerMovement = GetComponent<Movement>();
        playerScore = GetComponent<PlayerScore>();

        if (regenParticles != null)
        {
            regenParticles.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isHealthPotionPurchased = true;
        }
        if (healthPotionImage != null)
        {
            //if health potion is purchased and player's health is less than max health and not already regenerating, start regenerating health
            if(isHealthPotionPurchased && playerHealth.currentHealth < playerHealth.maxHealth && !isRegenerating)
            {
                StartCoroutine(RegenerateHealth());
            }

            if (isHealthPotionPurchased)
            {
                healthPotionImage.gameObject.SetActive(true);
                if (!regenParticles.isPlaying)
                {
                    regenParticles.Play();
                }
            }
            else
            {
                healthPotionImage.gameObject.SetActive(false);
                if (regenParticles.isPlaying)
                {
                    regenParticles.Stop();
                }
            }
        }
        
    }

    IEnumerator RegenerateHealth()
    {
        isRegenerating = true;
        while (playerHealth.currentHealth < playerHealth.maxHealth)
        {
            if ((playerHealth.currentHealth + regenerationRate) > playerHealth.maxHealth)
            {
                playerHealth.currentHealth = playerHealth.maxHealth;
            }
            else
            {
                playerHealth.currentHealth += regenerationRate;
            }
            yield return new WaitForSeconds(2); // Wait for 2 seconds
        }
        isRegenerating = false;
    }
}
