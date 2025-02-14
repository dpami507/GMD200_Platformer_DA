using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    public PlayerSettings playerSettings;

    float jumpCooldown = .05f;
    int jumpsLeft;
    bool jumpPressed;
    bool readyToJump;
    bool grounded;

    [Header("Ground Check")]
    public Transform castPos;

    [Header("Animation")]
    public SpriteRenderer sprite;
    Animator animator;

    float inputX;
    float threshold = 0.05f;
    float dir;
    Rigidbody2D rb;
    Vector2 vel;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();  
        rb = GetComponent<Rigidbody2D>();   
        readyToJump = true;
    }

    void Update()
    {
        GetInput();
        CheckGrounded();
        RotateTowardVel();

        vel = rb.velocity; //Set vel variable

        //Stop violent shaking
        if (Mathf.Abs(inputX) < threshold && Mathf.Abs(rb.velocity.x) < 0.5f)
            rb.velocity = new Vector2(0, vel.y);

        //Jump
        if (Input.GetButtonDown("Jump"))
            jumpPressed = true;
    }

    void GetInput()
    {
        //Get Input
        inputX = Input.GetAxisRaw("Horizontal");
    }

    void RotateTowardVel()
    {
        if(grounded)
        {
            sprite.flipY = false;

            if (dir < -threshold)
                sprite.transform.rotation = Quaternion.Euler(0, 180, 0);
            else if (dir > threshold)
                sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
            else
                sprite.transform.rotation = Quaternion.Euler(0, sprite.transform.eulerAngles.y, 0);
        }
        else
        {
            if (dir < 0)
                sprite.flipY = true;
            else
                sprite.flipY = false;

            //Rotate
            Vector2 rbNorm = rb.velocity.normalized;
            float angle = Mathf.Atan2(rbNorm.y, rbNorm.x) * Mathf.Rad2Deg;
            Quaternion desRot = Quaternion.Euler(0, 0, angle);
            sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, desRot, 25 * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        Move();
        if(jumpPressed)
        {
            jumpPressed = false;
            Jump();
        }
    }

    void Move()
    {
        //Extra Gravity
        rb.AddForce(Vector2.down * 10);

        if (vel.x > 0)
            dir = 1;
        else if (vel.x < 0)
            dir = -1;

        //Counter existing force
        CounterMove();

        //Animation
        animator.SetBool("Moving", (Mathf.Abs(inputX) > threshold));

        //Calculate force
        if (Mathf.Abs(vel.x) > playerSettings.maxSpeed)
            if (inputX == dir)
                inputX = 0; //If velocity is greater than max speed set to zero

        float forceToAdd = inputX * playerSettings.moveForce * Time.deltaTime;

        //Add force
        Vector2 moveVector = new Vector2(forceToAdd, 0);
        rb.AddForce(moveVector);
    }

    void CounterMove()
    {
        if (!grounded) { return; }

        if (Mathf.Abs(inputX) < threshold && Mathf.Abs(rb.velocity.x) > threshold)
        {
            //Counter
            Vector2 counterVector = new Vector2(playerSettings.moveForce * -dir * Time.deltaTime * playerSettings.counterMovement, 0);
            Debug.Log($"Adding force: {counterVector}");
            rb.AddForce(counterVector);
        }
    }

    private void Jump()
    {
        if (readyToJump && jumpsLeft > 0)
        {
            //Set Y-Velocity to zero as to have consistant jumps
            rb.velocity = new Vector2(vel.x, 0);

            jumpsLeft--;
            readyToJump = false;
            rb.AddForce(Vector2.up * playerSettings.jumpForce);
            Invoke(nameof(ResetJump), jumpCooldown);

            if (!grounded)
                Destroy(Instantiate(playerSettings.jumpParticle, castPos.position, Quaternion.identity), 1f);
        }
        else
            Debug.Log($"Cant Jump: {jumpsLeft} || {readyToJump}");
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    void CheckGrounded()
    {
        Collider2D objectBelow = Physics2D.OverlapCircle(castPos.position, playerSettings.castRadius, playerSettings.castMask);
        bool touchingGround = (objectBelow) ? true : false;

        animator.SetBool("Jumping", !touchingGround); //Set Animator "Jump" Status

        if(touchingGround && readyToJump)
        {
            jumpsLeft = playerSettings.jumpsCount;
            grounded = true;
        }
        else grounded = false;

        if(objectBelow && objectBelow.CompareTag("Platform"))
            transform.parent = objectBelow.transform;
        else transform.parent = null;
    }
}
