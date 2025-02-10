using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce;
    public float counterMovement;
    public float maxSpeed;

    [Header("Jump")]
    public float jumpForce;
    public Transform castPos;
    public float castRadius;
    public LayerMask castMask;
    float jumpCooldown = .05f;
    bool readyToJump;

    [Header("Animation")]
    public SpriteRenderer sprite;
    public Animator animator;

    float inputX;
    float threshold = 0.05f;
    float dir;
    Rigidbody2D rb;
    Vector2 vel;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        readyToJump = true;
    }

    void Update()
    {
        vel = rb.velocity; //Set vel variable
        animator.SetBool("Jumping", !IsGrounded()); //Set Animator "Jump" Status

        //Stop violent shaking
        if (Mathf.Abs(inputX) < threshold && Mathf.Abs(rb.velocity.x) < 0.5f)
            rb.velocity = new Vector2(0, vel.y);

        //Jump
        if (Input.GetButton("Jump"))
            Jump();
    }

    private void Jump()
    {
        if(IsGrounded() && readyToJump)
        {
            //Set Y-Velocity to zero as to have consistant jumps
            rb.velocity = new Vector2(vel.x, 0);

            readyToJump = false;
            rb.AddForce(Vector2.up * jumpForce);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        //Extra Gravity
        rb.AddForce(Vector2.down * 10);

        //Counter existing force
        CounterMove();

        //Get Input
        inputX = Input.GetAxisRaw("Horizontal");
        dir = (vel.x > 0) ? 1 : -1;

        //Animation
        if (Mathf.Abs(inputX) > threshold)
            animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);

        if(inputX == -1)
            sprite.flipX = true;
        else if (inputX == 1)
            sprite.flipX = false;

        //Calculate force
        if (Mathf.Abs(vel.x) > maxSpeed) 
            if(inputX == dir)
                inputX = 0; //If velocity is greater than max speed set to zero

        float forceToAdd = inputX * moveForce * Time.deltaTime;

        //Add force
        Vector2 moveVector = new Vector2(forceToAdd, 0);
        rb.AddForce(moveVector);
    }

    void CounterMove()
    {
        if (!IsGrounded()) { return; }

        if (Mathf.Abs(inputX) < threshold && Mathf.Abs(rb.velocity.x) > threshold)
        {
            //Counter
            Vector2 counterVector = new Vector2(moveForce * -dir * Time.deltaTime * counterMovement, 0);
            rb.AddForce(counterVector);
        }
    }

    bool IsGrounded()
    {
        return Physics2D.OverlapCircle(castPos.position, castRadius, castMask);
    }

    void ResetJump()
    {
        readyToJump = true;
    }
}
