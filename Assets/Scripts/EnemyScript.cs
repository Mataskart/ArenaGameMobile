using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public Animator anim;
    private Vector3 moveDirectionAnim;

    [SerializeField]
    private int damage = 5;
    [SerializeField]
    private float speed = 1.5f;
    [SerializeField]
    private float attackCooldown = 0.5f; // Cooldown period in seconds
    [SerializeField]
    private float initialAttackDelay = 1f; // Initial delay before first attack

    [SerializeField]
    private EnemyData data;
    private GameObject player;
    private float attackTimer; // Timer for tracking cooldown
    private bool firstContact = false; // Flag for tracking first contact with player

    bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
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
        // Add a small jitter to the enemy's position
        transform.position = new Vector2(transform.position.x + Random.Range(-0.001f, 0.001f), transform.position.y + Random.Range(-0.001f, 0.001f));
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
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float moveX = direction.x;
        float moveY = direction.y;
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        
        if(moveX < 0 && facingRight)
        {
            Flip();
        }
        moveDirectionAnim = new Vector3(moveX,0, moveY);
        if (moveDirectionAnim == Vector3.zero)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 0.1f);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            firstContact = true;
            attackTimer = initialAttackDelay; // Set the timer to the initial delay
        }
    }
    private void OnTriggerStay2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
        {
            if(collider.GetComponent<Health>() != null && attackTimer <= 0)
            {
                collider.GetComponent<Health>().TakeDamage(damage);
                if (firstContact)
                {
                    firstContact = false; // Reset the flag
                    attackTimer = attackCooldown; // Reset the timer to the regular cooldown
                }
            }
        }
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }
}