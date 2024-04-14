using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Unity.PlasticSCM.Editor.WebApi;

public class EnemyScript : MonoBehaviour
{
    public Animator anim;
    private Vector2 moveDirectionAnim;

    public static event Action<EnemyScript> OnEnemyKilled;

    [SerializeField]
    private int damage;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float attackCooldown = 0.5f; // Cooldown period in seconds
    [SerializeField]
    private float initialAttackDelay = 1f; // Initial delay before first attack

    [SerializeField]
    private EnemyData data;
    private GameObject player;
    private float attackTimer; // Timer for tracking cooldown
    bool facingRight = true;
    public Slider slider;
    private float timeUntilFlip = 0.25f;

    //Animation states
    string currentState;
    const string ENEMY_IDLE = "idle";
    const string ENEMY_RUN = "run";
    private bool isRunning = false;
    const string ENEMY_ATTACK = "attack";
    private bool isAttacking = false;
    const string ENEMY_DEAD = "dead";
    private bool isDead = false;
    const string ENEMY_PRE_ATTACK = "preAttack";
    private bool isPreAttacking = false;
    const string ENEMY_TAKE_DAMAGE = "takeDamage";
    private bool isHurt = false;
    Vector3 lastPosition;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        anim = GetComponent<Animator>();
        SetEnemyValues();
        attackTimer = 0f; // Initialize timer
        slider = GetComponentInChildren<Slider>();

    }

    // Update is called once per frame
    void Update()
    {

        if (isDead)
        {
            slider.gameObject.SetActive(false);
            return;
        }

        Swarm();

        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        lastPosition = transform.position;
        bool isMoving = Vector3.Distance(transform.position, lastPosition) > 0.1f;
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        float moveX = direction.x;
        float moveY = direction.y;
        moveDirectionAnim = new Vector2(moveX, moveY);
        if(isDead == true){
            ChangeAnimationState(ENEMY_DEAD);
        }
        else if(isHurt == true){
            ChangeAnimationState(ENEMY_TAKE_DAMAGE);
            float hurtDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("HurtComplete",hurtDelay);
        }
        else if(isAttacking == true){
            ChangeAnimationState(ENEMY_ATTACK);
            float attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AttackComplete",attackDelay);
        }
        else if(isRunning){
            ChangeAnimationState(ENEMY_RUN);
            Invoke("RunComplete",0f);
        }
        else
        {
            if(isPreAttacking == true){
            ChangeAnimationState(ENEMY_PRE_ATTACK);
            float PreAttackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("PreAttackComplete",PreAttackDelay);
            }
            else if(!isMoving)
                ChangeAnimationState(ENEMY_IDLE);
        }
        timeUntilFlip -= Time.deltaTime;
        if(timeUntilFlip <= 0)
        {
            if (moveX > 0 && !facingRight && attackTimer <= 0)
            {
                Flip();
            }

            if (moveX < 0 && facingRight && attackTimer <= 0)
            {
                Flip();
            }
            SetTimeUntilFlip();
        }

    }
    private void SetEnemyValues()
    {
        GetComponent<Health>().SetHealth(data.hp, data.hp);
        damage = data.damage;
        speed = data.speed;
        slider.maxValue = data.hp;
    }

    private void Swarm()
    {
        float minimumDistance = 1f; // Adjust this value as needed
        if (Vector2.Distance(transform.position, player.transform.position) > minimumDistance)
        {
            isRunning = true;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(player.transform.position.x,player.transform.position.y + 0.5f), speed * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
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
            isAttacking = true;
            attackTimer = attackCooldown; // Reset the timer to the regular cooldown
        }
            else if (attackTimer > 0)
            {
                isPreAttacking = true;
            }
        }
    }

    public void CheckDeath()
    {
        Debug.Log("CheckDeath() has been called."); // This will print a message to the Unity Console
        if (isDead) return; // If the enemy is already dead, exit the method

        if (GetComponent<Health>().currentHealth <= 0)
        {
            isDead = true;
            Debug.Log("health <- 0."); // This will print a message to the Unity Console
            speed = 0; // Stop moving

            // disable all colliders so that the corpse cannot be interacted with
            Collider2D[] colliders = GetComponents<Collider2D>();
            foreach (Collider2D col in colliders)
            {
                col.enabled = false;
            }

            OnEnemyKilled?.Invoke(this);
            Destroy(gameObject, 3f); // Destroy after 3 seconds
        }
    }   
    public void HurtAnim()
    {
        isHurt = true;
    }

    public void AttackComplete()
    {
        isAttacking = false;
    }
    public void RunComplete()
    {
        isRunning = false;
    }
    public void PreAttackComplete()
    {
        isPreAttacking = false;
    }
    public void HurtComplete()
    {
        isHurt = false;
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    void ChangeAnimationState(string newState){
        if(currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }

    private void SetTimeUntilFlip()
    {
        timeUntilFlip = 0.25f;
    }
}