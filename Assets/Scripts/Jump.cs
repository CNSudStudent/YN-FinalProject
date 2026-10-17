
/*
public class PlayerController2D : MonoBehaviour
{
    [Header("Movement & Base Jump")]
    public float jumpForce = 12f;
    public int extraJumpsValue = 1;
    private int extraJumps;
    private Rigidbody2D rb;

    [Header("Ground Check")]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    [Header("Wall Check & Slide")]
    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public LayerMask whatIsWall;
    public float wallSlideSpeed = 2f;
    private bool isTouchingLeftWall;
    private bool isTouchingRightWall;
    private bool isSliding;

    [Header("Wall Jump Forces")]
    public float wallJumpForceX = 10f;
    public float wallJumpForceY = 14f;
    public float wallJumpDuration = 0.15f;
    private bool isWallJumping;

    void Start()
    {
        extraJumps = extraJumpsValue;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // 1. Environment Detection (Both Sides)
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, checkRadius, whatIsWall);
        isTouchingRightWall = Physics2D.OverlapCircle(rightWallCheck.position, checkRadius, whatIsWall);

        // 2. Reset Extra Jumps
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
        }

        // 3. Wall Sliding Logic
        // Slide if touching any wall, in the air, and falling downwards
        if ((isTouchingLeftWall || isTouchingRightWall) && !isGrounded && rb.linearVelocity.y < 0)
        {
            isSliding = true;
        }
        else
        {
            isSliding = false;
        }

        if (isSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
        }

        // 4. Jump & Wall Jump Input
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                NormalJump();
            }
            else if (isTouchingLeftWall && !isGrounded)
            {
                WallJump(1f); // Push right away from left wall
            }
            else if (isTouchingRightWall && !isGrounded)
            {
                WallJump(-1f); // Push left away from right wall
            }
            else if (extraJumps > 0)
            {
                NormalJump();
                extraJumps--;
            }
        }
    }

    void NormalJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void WallJump(float directionModifier)
    {
        isWallJumping = true;
        
        // Apply horizontal push away from the wall and a clean upward blast
        rb.linearVelocity = new Vector2(directionModifier * wallJumpForceX, wallJumpForceY);
        
        Invoke(nameof(StopWallJump), wallJumpDuration);
    }

    void StopWallJump()
    {
        isWallJumping = false;
    }

    public bool CanMove()
    {
        return !isWallJumping;
    }
}
*/

using UnityEngine;

public class HollowKnightController2D : MonoBehaviour
{
    [Header("Hollow Knight Movement")]
    public float moveSpeed = 10f; // Instant max speed, no slippery acceleration
    private float horizontalInput;
    private Rigidbody2D rb;

    [Header("Precise Jump Heights")]
    public float jumpForce = 16f;
    public int extraJumpsValue = 1; // Monarch Wings double jump
    private int extraJumps;

    [Header("Ground Check")]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    [Header("Mantis Claw Wall Slide")]
    public Transform leftWallCheck;
    public Transform rightWallCheck;
    public LayerMask whatIsWall;
    public float wallSlideSpeed = 2.5f;
    private bool isTouchingLeftWall;
    private bool isTouchingRightWall;
    private bool isWallSliding;

    [Header("Mantis Claw Wall Jump")]
    public float wallJumpForceX = 14f;
    public float wallJumpForceY = 16f;
    public float wallJumpDuration = 0.15f; // Duration player control is locked
    private bool isWallJumping;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
    }

    void Update()
    {
        // Gather input only if not locked in a wall jump blast
        if (!isWallJumping)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); // GetAxisRaw gives instant 1 or -1 (no sliding)
        }

        // 1. Environment Detection
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isTouchingLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, checkRadius, whatIsWall);
        isTouchingRightWall = Physics2D.OverlapCircle(rightWallCheck.position, checkRadius, whatIsWall);

        // 2. Refresh Jumps & Monarch Wings
        if (isGrounded || isWallSliding)
        {
            extraJumps = extraJumpsValue; 
        }

        // 3. Automatic Wall Sliding Logic (Mantis Claw)
        if ((isTouchingLeftWall || isTouchingRightWall) && !isGrounded && rb.linearVelocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        // 4. Jump & Wall Jump Input Triggers
        if (Input.GetButtonDown("Jump"))
        {
            if (isGrounded)
            {
                NormalJump();
            }
            else if (isTouchingLeftWall)
            {
                WallJump(1f); // Blast right
            }
            else if (isTouchingRightWall)
            {
                WallJump(-1f); // Blast left
            }
            else if (extraJumps > 0)
            {
                NormalJump();
                extraJumps--; // Use Monarch Wings
            }
        }

        // 5. Hollow Knight Variable Jump Height (The Secret Sauce)
        // The instant you release the jump button while going up, immediately cut vertical velocity to 0
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);
        }
    }

    void FixedUpdate()
    {
        // Apply horizontal movement if player control isn't locked by a wall jump
        if (!isWallJumping)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }

        // Apply clamped wall sliding speed
        if (isWallSliding)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }

    void NormalJump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }

    void WallJump(float directionModifier)
    {
        isWallJumping = true;
        
        // Instant snappy pause-then-blast feeling
        rb.linearVelocity = new Vector2(directionModifier * wallJumpForceX, wallJumpForceY);
        
        // Lock player directional input briefly
        Invoke(nameof(StopWallJumpControlLock), wallJumpDuration);
    }

    void StopWallJumpControlLock()
    {
        isWallJumping = false;
    }
}
