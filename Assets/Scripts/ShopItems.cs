using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItems : MonoBehaviour
{
    private Health playerHealth;
    private Movement playerMovement;
    private PlayerScore playerScore;

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
    }

    // Update is called once per frame
    void Update()
    {
        //if health potion is purchased and player's health is less than max health and not already regenerating, start regenerating health
        if(isHealthPotionPurchased && playerHealth.currentHealth < playerHealth.maxHealth && !isRegenerating)
        {
            StartCoroutine(RegenerateHealth());
        }

        if (isHealthPotionPurchased)
        {
            healthPotionImage.gameObject.SetActive(true);
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
