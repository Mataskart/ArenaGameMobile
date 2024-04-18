using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveDirection;
    private Vector3 moveDirectionAnim;
    bool facingRight = true;

    //Animation states
    string currentState;
    const string PLAYER_IDLE = "idle";
    const string PLAYER_RUN = "run";
    private bool isRunning = false;
    const string PLAYER_ATTACK = "attack1";
    private bool isAttacking = false;
    const string PLAYER_DEAD = "death";
    private bool isDead = false;
    const string PLAYER_COOLDOWN = "CooldownIdle";
    private bool isPreAttacking = false;
    const string PLAYER_TAKE_DAMAGE = "take hit";
    private bool isHurt = false;
    Vector3 lastPosition;

    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        ProcessInputs();
        Move();
    }
    // Fixedupdate is called at a fixed interval and is independent of frame rate.
    void FixedUpdate()
    { 
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        moveDirectionAnim = new Vector3(moveX, 0, moveY);
        if (isDead)
        {
            ChangeAnimationState(PLAYER_DEAD);
            return;
        }
        else if (isHurt == true)
        {
            ChangeAnimationState(PLAYER_TAKE_DAMAGE);
            float hurtDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("HurtComplete", hurtDelay);
            
        }
        else if (isAttacking == true)
        {
            ChangeAnimationState(PLAYER_ATTACK);
            float attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AttackComplete", attackDelay);
        }
        else
        {
            if (isPreAttacking == true)
            {
                ChangeAnimationState(PLAYER_COOLDOWN);
                float PreAttackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
                Invoke("PreAttackComplete", PreAttackDelay);
            }
            else if (moveDirectionAnim == Vector3.zero)
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            else
            {
                ChangeAnimationState(PLAYER_RUN);
            }
        }
         
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        if (moveX < 0 && facingRight)
        {
            Flip();
        }
    }

    void ProcessInputs()
    {
    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void Flip()
    {
        Vector3 currentScale = gameObject.transform.localScale;
        currentScale.x *= -1;
        gameObject.transform.localScale = currentScale;

        facingRight = !facingRight;
    }

    public void CheckDeath()
    {
        if (GetComponent<Health>().currentHealth <= 0)
        {
            isDead = true;
            rb.velocity = Vector2.zero;
            Destroy(gameObject, 5f); // Destroy after 5 seconds
        }
    }

    public bool GetDeath()
    {
        return isDead;
    }

    void ChangeAnimationState(string newState)
    {
        if (currentState == newState) return;
        anim.Play(newState);
        currentState = newState;
    }
    public void HurtComplete()
    {
        isHurt = false;
    }
    public void HurtAnim()
    {
        isHurt = true;
    }
    public void AttackAnim()
    {
        isAttacking = true;
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
}
