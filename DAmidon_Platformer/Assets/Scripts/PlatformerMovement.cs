using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    public PlayerSettings playerSettings;

    [Header("Ground Check")]
    public Transform castPos;

    [Header("Animation")]
    public SpriteRenderer sprite;
    Animator animator;

    //Private Jump Variables
    float jumpCooldown = .05f;
    int jumpsLeft;
    bool jumpPressed;
    bool readyToJump;
    bool grounded;

    //Private Movement Variables
    float inputX;
    float dir;
    float threshold = 0.01f;
    Vector2 vel;

    //Other
    PlayerManager playerManager;
    Rigidbody2D rb;
    float afkTime;

    void Start()
    {
        playerManager = GetComponent<PlayerManager>();
        animator = GetComponentInChildren<Animator>();  
        rb = GetComponent<Rigidbody2D>();   
        readyToJump = true;
        sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        dir = 1;
    }

    void Update()
    {
        //Death Check
        if(!playerManager.dead)
            GetInput();
        else rb.velocity = Vector2.zero;

        //Turn off sprite if dead
        sprite.gameObject.SetActive(!playerManager.dead);

        CheckGrounded();
        RotateTowardVel();
        CheckIfAFK();

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

        if (vel.x > threshold)
            dir = 1;
        else if (vel.x < -threshold)
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
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    void CheckGrounded()
    {
        //Get object below for groundCheck and future use
        Collider2D objectBelow = Physics2D.OverlapCircle(castPos.position, playerSettings.castRadius, playerSettings.castMask);
        bool touchingGround = (objectBelow) ? true : false;

        animator.SetBool("Jumping", !touchingGround); //Set Animator "Jump" Status

        if(touchingGround && readyToJump)
        {
            jumpsLeft = playerSettings.jumpsCount;
            grounded = true;
        }
        else grounded = false;

        //If object below is platform parent to move with it
        if(objectBelow && objectBelow.CompareTag("Platform"))
            transform.parent = objectBelow.transform;
        else transform.parent = null;
    }

    //Plays AFK animation
    void CheckIfAFK()
    {
        afkTime += Time.deltaTime;
        if (vel.magnitude > threshold)
            afkTime = 0;

        animator.SetBool("Idling", (afkTime > playerSettings.afkTimeSeconds));
    }
}
