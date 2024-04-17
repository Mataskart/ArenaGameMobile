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
    private bool isDead = false;

    // Update is called once per frame
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (isDead)
        {
            // If the enemy is dead, maybe prevent further actions or movement
            anim.SetBool("isDead", true); // Trigger death animation
            return; // Skip the rest of the update
        }
        transform.position = new Vector2(transform.position.x + Random.Range(-0.00001f, 0.00001f), transform.position.y + Random.Range(-0.00001f, 0.00001f));
        ProcessInputs();
        Move();
    }
    // Fixedupdate is called at a fixed interval and is independent of frame rate.
    void FixedUpdate()
    { }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }

        if (moveX < 0 && facingRight)
        {
            Flip();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
        moveDirectionAnim = new Vector3(moveX, 0, moveY);
        if (moveDirectionAnim == Vector3.zero)
        {
            anim.SetFloat("Speed", 0);
        }
        else
        {
            anim.SetFloat("Speed", 0.1f);
        }
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
            anim.SetBool("isDead", true); // Trigger death animation
            Destroy(gameObject, 5f); // Destroy after 5 seconds
        }
    }

    public bool GetDeath()
    {
        return isDead;
    }
}
