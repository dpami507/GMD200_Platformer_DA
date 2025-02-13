using UnityEngine;

public class PlatformerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveForce;
    public float counterMovement;
    public float maxSpeed;

    [Header("Jump")]
    public int jumpsCount;
    public float jumpForce;
    float jumpCooldown = .05f;
    public GameObject jumpParticle;
    bool jumpPressed;
    bool readyToJump;
    bool grounded;

    [Header("Ground Check")]
    public Transform castPos;
    public float castRadius;
    public LayerMask castMask;

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

    void RotateTowardVel()
    {
        if(grounded)
        {
            sprite.flipY = false;

            if (inputX == -1)
                sprite.flipX = true;
            else if (inputX == 1)
                sprite.flipX = false;

            sprite.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            //Rotate
            Vector2 rbNorm = rb.velocity.normalized;
            float angle = Mathf.Atan2(rbNorm.y, rbNorm.x) * Mathf.Rad2Deg;
            Quaternion desRot = Quaternion.Euler(0, 0, angle);
            sprite.transform.rotation = Quaternion.Lerp(sprite.transform.rotation, desRot, 25 * Time.deltaTime);

            if (dir < 0)
            {
                sprite.flipX = false;
                sprite.flipY = true;
            }
            else
            {
                sprite.flipX = false;
                sprite.flipY = false;
            }
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

        //Counter existing force
        CounterMove();

        //Get Input
        inputX = Input.GetAxisRaw("Horizontal");
        dir = (vel.x > 0) ? 1 : -1;

        //Animation
        if (Mathf.Abs(inputX) > 0)
            animator.SetBool("Moving", true);
        else animator.SetBool("Moving", false);

        //Calculate force
        if (Mathf.Abs(vel.x) > maxSpeed)
            if (inputX == dir)
                inputX = 0; //If velocity is greater than max speed set to zero

        float forceToAdd = inputX * moveForce * Time.deltaTime;

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
            Vector2 counterVector = new Vector2(moveForce * -dir * Time.deltaTime * counterMovement, 0);
            rb.AddForce(counterVector);
        }
    }

    private void Jump()
    {
        if(readyToJump && jumpsCount > 0)
        {
            //Set Y-Velocity to zero as to have consistant jumps
            rb.velocity = new Vector2(vel.x, 0);

            Destroy(Instantiate(jumpParticle, castPos.position, Quaternion.identity), 1f);

            jumpsCount--;
            readyToJump = false;
            rb.AddForce(Vector2.up * jumpForce);
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    void ResetJump()
    {
        readyToJump = true;
    }

    void CheckGrounded()
    {
        Collider2D objectBelow = Physics2D.OverlapCircle(castPos.position, castRadius, castMask);
        bool touchingGround = (objectBelow) ? true : false;

        animator.SetBool("Jumping", !touchingGround); //Set Animator "Jump" Status

        if(touchingGround)
        {
            jumpsCount = 2;
            grounded = true;
        }
        else grounded = false;

        if(objectBelow && objectBelow.CompareTag("Platform"))
            transform.parent = objectBelow.transform;
        else transform.parent = null;
    }
}
