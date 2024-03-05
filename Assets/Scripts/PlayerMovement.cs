using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator anim;
    private Vector2 moveDirection;
    private Vector3 moveDirectionAnim;
    bool facingRight = true;

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
    { }

    void ProcessInputs() {         
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        if (moveX > 0 && !facingRight)
        {
            Flip();
        }
        
        if(moveX < 0 && facingRight)
        {
            Flip();
        }

        moveDirection = new Vector2(moveX, moveY).normalized;
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
}
