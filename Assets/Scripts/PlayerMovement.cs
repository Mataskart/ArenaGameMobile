using System;
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
    string PLAYER_ATTACK = "attack1";
    string PLAYER_ATTACKother = "attack2";
    private bool isAttacking = false;
    const string PLAYER_DEAD = "death";
    private bool isDead = false;
    const string PLAYER_COOLDOWN = "CooldownIdle";
    private bool isPreAttacking = false;
    const string PLAYER_TAKE_DAMAGE = "take hit";
    private bool isHurt = false;
    float randomValue = 1;
    Vector3 lastPosition;

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 14f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 5f;
    [SerializeField] private TrailRenderer trail;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;

    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && canDash)
        {
            CheckAchievement();
            Dash();
        }
        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized * dashingVelocity;
            return;
        }
        ProcessInputs();
        PlayAnimation();
        Move();
    }

    void PlayAnimation()
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
            else if (moveDirectionAnim == Vector3.zero && !Input.GetKeyDown(KeyCode.Space))
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
            else if (!Input.GetKeyDown(KeyCode.Space))
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
        if (!isDead)
        {
            rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
        }
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
        var temp = PLAYER_ATTACK;
        PLAYER_ATTACK = PLAYER_ATTACKother;
        PLAYER_ATTACKother = temp;
    }
    public void RunComplete()
    {
        isRunning = false;
    }
    public void PreAttackComplete()
    {
        isPreAttacking = false;
    }

    private void Dash()
    {
        isDashing = true;
        canDash = false;
        trail.emitting = true;
        dashingDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, 0);
        }
        StartCoroutine(StopDash());
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashingTime);
        trail.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    private void CheckAchievement()
    {
        GameObject achievementManager = GameObject.Find("AchievementManager");
        AchievementManager achievementScript = achievementManager.GetComponent<AchievementManager>();
        achievementScript.CompleteAchievement("I CAN DO THAT?");
    }

}
