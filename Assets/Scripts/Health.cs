using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Security.Cryptography;
using Leguar.LowHealth;
using UnityEngine.SocialPlatforms.Impl;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    public HealthBar healthBar;
    public FloatingHealthBar floatingHealthBar;
    public TextMeshProUGUI youDied;
    private Animator animator;
    public Slider slider;
    private float originalSpeed;
    private bool isEnemy;
    public LowHealthController lowHealthController;
    public Image damageTakenEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        if (healthBar != null)
        {
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetInteractable(false);
            //youDied.gameObject.SetActive(false);
        }
        if (slider != null)
        {
            slider.value = 100f;
        }
        animator = GetComponent<Animator>();
        Movement playerMovement = GetComponent<Movement>();
        if (playerMovement != null) // Add this null check
        {
            originalSpeed = playerMovement.moveSpeed;
            isEnemy = false;
        }
        else
        {
            isEnemy = true;
        }
    }
    void Update()
    {
        CheckAchievement();
        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    // TakeDamage(20);
        //}
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    //  Heal(7);
        //}
    }
    public void SetHealth(int maxHealth, int health)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = health;
    }
    public void TakeDamage(int damage)
    {
        Movement playerMovement = GetComponent<Movement>();
        currentHealth -= damage;
        Debug.Log("Player health: " + currentHealth);

        if (GetComponent<EnemyScript>() == null)
        {
            float setLowHealth = (float)currentHealth / 100;
            //lowHealthController.SetPlayerHealthSmoothly(setLowHealth, 0.5f);

            if (currentHealth > 0)
            {
                damageTakenEffect.gameObject.SetActive(true);
                Invoke("ResetDmgTaken", 0.2f);
            }
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            if (healthBar != null)
            {
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
        else
        {
            // Trigger the hurt animation here
            if (animator != null && GetComponent<EnemyScript>() == null)
            {
                Movement movement = GetComponent<Movement>();
                movement.HurtAnim();
            }
            if (animator != null && GetComponent<EnemyScript>() != null)
            {
                EnemyScript enemyScript = GetComponent<EnemyScript>();
                enemyScript.HurtAnim();
            }

            if (isEnemy == false)
            {
                playerMovement.moveSpeed *= 0.7f; // Slow down player movement when taking damage
                Invoke("ResetSpeed", 1f); // Reset speed after 1 second
            }
        }

        if (healthBar != null)
        {
            healthBar.SetHealth(currentHealth);
        }
        if (slider != null)
        {
            slider.value = currentHealth;
        }
    }

    void ResetSpeed()
    {
        Movement playerMovement = GetComponent<Movement>();
        playerMovement.moveSpeed = originalSpeed; // Reset player movement speed to normal
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

    void ResetDmgTaken()
    {
        damageTakenEffect.gameObject.SetActive(false);
    }
    void CheckAchievement()
    {
        GameObject player = GameObject.Find("Player");
        PlayerScore scoreScript = player.GetComponent<PlayerScore>();
        int enemiesKilled = scoreScript.GetEnemiesKilled();

        GameObject achievementManager = GameObject.Find("AchievementManager");
        AchievementManager achievementScript = achievementManager.GetComponent<AchievementManager>();
        int enemiesTotal = PlayerPrefs.GetInt("TotalEnemiesKilled");

        if (enemiesTotal == 1 && enemiesKilled == 1)
        {
            achievementScript.CompleteAchievement("BORN TO KILL");
        }


        if (currentHealth == 100 && enemiesKilled == 10)
        {
            achievementScript.CompleteAchievement("NO TIME TO DIE");
        }

        Level levelScript = player.GetComponent<Level>();
        int currentLevel = levelScript.GetLevel();
        if (currentHealth == 100 && currentLevel == 5)
        {
            achievementScript.CompleteAchievement("MASTER OF COMBAT");
        }
    }
}
