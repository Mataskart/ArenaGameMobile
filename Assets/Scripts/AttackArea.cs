using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int damage = 20;
    private int totalDamageDealt;

    void Start()
    {
        totalDamageDealt = PlayerPrefs.GetInt("TotalDamageDealt", 0);
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Debug.Log("Player attacking enemy."); // This will print a message to the Unity Console
            Health health = collider.GetComponent<Health>();
            health.TakeDamage(damage/2);
            // Increment total damage dealt
            totalDamageDealt += damage;
            // Save the updated total damage dealt to PlayerPrefs
            PlayerPrefs.SetInt("TotalDamageDealt", totalDamageDealt);
            PlayerPrefs.Save();
        }
    }
}
