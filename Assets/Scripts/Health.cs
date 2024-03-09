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
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetInteractable(false);

        youDied = FindObjectOfType<TextMeshProUGUI>();
        youDied.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(20);
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            Heal(7);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            youDied.gameObject.SetActive(true);
            Invoke("LoadMenu", 3);
        }
    }
    void Heal(int healAmount)
    {
        currentHealth += healAmount;
        healthBar.SetHealth(currentHealth);
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