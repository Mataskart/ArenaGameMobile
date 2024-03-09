using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float attackCooldown = 1f; // Cooldown period in seconds

    [SerializeField]
    private EnemyData data;
    private GameObject player;
    private float attackTimer; // Timer for tracking cooldown

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        SetEnemyValues();
        attackTimer = 0f; // Initialize timer
    }

    // Update is called once per frame
    void Update()
    {
        Swarm();
        // Reduce the timer by the time passed since the last frame
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }
    
    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
    }

    private void Swarm()
    {
        transform.position = Vector2.MoveTowards(transform.position,player.transform.position,speed*Time.deltaTime);
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(collider.GetComponent<Health>() != null && attackTimer <= 0)
            {
                collider.GetComponent<Health>().TakeDamage(damage);
                attackTimer = attackCooldown; // Reset the timer
            }
        }
    }
}