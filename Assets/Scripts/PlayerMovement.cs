using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    private Animator anim;
    private Vector2 moveDirection;
    private Vector3 moveDirectionAnim;
    bool facingRight = true;

    public AudioSource playerSword;
    public AudioSource playerDeath;
    public AudioSource playerDamage;

    // Animation states
    string currentState;
    const string PLAYER_IDLE = "idle";
    const string PLAYER_RUN = "run";
    string PLAYER_ATTACK = "attack1";
    string PLAYER_ATTACKother = "attack2";
    private bool isAttacking = false;
    const string PLAYER_DEAD = "death";
    private bool isDead = false;
    const string PLAYER_COOLDOWN = "CooldownIdle";
    private bool isPreAttacking = false;
    const string PLAYER_TAKE_DAMAGE = "take hit";
    private bool isHurt = false;

    [Header("Dashing")]
    [SerializeField] private float dashingVelocity = 200f;
    [SerializeField] private float dashingTime = 0.5f;
    [SerializeField] private float dashingCooldown = 5f;
    [SerializeField] private TrailRenderer trail;
    private Vector2 dashingDirection;
    private bool isDashing;
    private bool canDash = true;
    public Joystick joystick;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (isDashing)
        {
            rb.velocity = dashingDirection.normalized * dashingVelocity;
        }
        ProcessInputs();
        PlayAnimation();
        Move();
    }

    void PlayAnimation()
    {
        float moveX = joystick.Horizontal;
        float moveY = joystick.Vertical;

        moveDirection = new Vector2(moveX, moveY).normalized;
        moveDirectionAnim = new Vector3(moveX, 0, moveY);

        if (isDead)
        {
            CheckDeathSFX();
            ChangeAnimationState(PLAYER_DEAD);
            return;
        }
        else if (isHurt)
        {
            CheckDamageSFX();
            ChangeAnimationState(PLAYER_TAKE_DAMAGE);
            float hurtDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("HurtComplete", hurtDelay);
        }
        else if (isAttacking)
        {
            CheckSwordSFX();
            ChangeAnimationState(PLAYER_ATTACK);
            float attackDelay = anim.GetCurrentAnimatorStateInfo(0).length;
            Invoke("AttackComplete", attackDelay);
        }
        else
        {
            if (isPreAttacking)
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

    public void DashOnButton()
    {
        if (canDash)
        {
            Debug.Log("Dash button pressed and canDash is true");
            CheckAchievement();
            Dash();
        }
    }

    void ProcessInputs()
    {
        // Implement any additional input processing here
    }

    void Move()
    {
        if (!isDead)
        {
            rb.velocity = new Vector2(joystick.Horizontal * moveSpeed, joystick.Vertical * moveSpeed);
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

    public void PreAttackComplete()
    {
        isPreAttacking = false;
    }

    public void Dash()
    {
        isDashing = true;
        canDash = false;
        trail.emitting = true;
        dashingDirection = new Vector2(joystick.Horizontal, joystick.Vertical).normalized;
        if (dashingDirection == Vector2.zero)
        {
            dashingDirection = new Vector2(transform.localScale.x, transform.localScale.y).normalized;
        }

        rb.velocity = dashingDirection * dashingVelocity;

        Debug.Log("Dashing with direction: " + dashingDirection);

        StartCoroutine(StopDash());
    }

    private IEnumerator StopDash()
    {
        yield return new WaitForSeconds(dashingTime);
        trail.emitting = false;
        isDashing = false;
        rb.velocity = Vector2.zero;  // Stop the dash
        Debug.Log("Dash stopped");

        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
        Debug.Log("Dash cooldown completed, can dash again");
    }

    private void CheckAchievement()
    {
        GameObject achievementManager = GameObject.Find("AchievementManager");
        AchievementManager achievementScript = achievementManager.GetComponent<AchievementManager>();
        achievementScript.CompleteAchievement("I CAN DO THAT?");
    }

    void CheckSwordSFX()
    {
        if (playerSword != null && !playerSword.isPlaying)
        {
            playerSword.Play();
        }
    }

    void CheckDeathSFX()
    {
        if (playerDeath != null && !playerDeath.isPlaying)
        {
            playerDeath.Play();
        }
    }

    void CheckDamageSFX()
    {
        if (playerDamage != null && !playerDamage.isPlaying)
        {
            playerDamage.Play();
        }
    }
}
