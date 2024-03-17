using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public TextMeshProUGUI youDied;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetInteractable(false);
            youDied = FindObjectOfType<TextMeshProUGUI>();
            youDied.gameObject.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
           // TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
          //  Heal(7);
        }
    }
    public void SetHealth(int maxHealth, int health)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = health;
    }
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (healthBar != null)
            {
                Movement playerMovement = GetComponent<Movement>();
                playerMovement.CheckDeath();
                youDied.gameObject.SetActive(true);
                Invoke("LoadMenu", 3);
            }
            
            else
            {
                // If this is an enemy, check and handle death
                EnemyScript enemyScript = GetComponent<EnemyScript>();
                if (enemyScript != null)
                {
                    enemyScript.CheckDeath();
                }
                else
                {
                    // If it's not an enemy (e.g., the player), handle accordingly
                    Destroy(gameObject);
                }
            }
        }
    }

    void Heal(int healAmount)
    {
        currentHealth += healAmount;
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    void LoadMenu()
    {

        SceneManager.LoadScene("MainMenu");
    }
}
