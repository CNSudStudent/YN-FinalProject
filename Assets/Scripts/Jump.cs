/*
using UnityEngine;

public class Jump : MonoBehaviour
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
*/
using UnityEngine;

public class Jump : MonoBehaviour
{
    [Header("Hollow Knight Movement")]
    public float moveSpeed = 10f; // Instant max speed, no slippery acceleration
    private float horizontalInput;
    private Rigidbody2D rb;

    [Header("Precise Jump Heights")]
    public float jumpForce = 16f;
    public int extraJumpsValue = 1; // Monarch Wings double jump
    private int extraJumps;

    [Header("Ground & Platform Check")]
    private bool isGrounded;
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;
    public LayerMask whatIsPlatform; // New: Dedicated layer for platforms

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
    private int lastWallJumped = 0; // 0 = None, -1 = Left Wall, 1 = Right Wall

    public int extraJumpWallReset = 1;
    private int saveExtraJumpWallReset;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        extraJumps = extraJumpsValue;
        saveExtraJumpWallReset = extraJumpWallReset;
    }

    void Update()
    {
        // Gather input only if not locked in a wall jump blast
        if (!isWallJumping)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal"); // GetAxisRaw gives instant 1 or -1 (no sliding)
        }

        // 1. Environment Detection (Checks both Ground and Platforms)
        bool touchingGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        bool touchingPlatform = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsPlatform);
        isGrounded = touchingGround || touchingPlatform;

        isTouchingLeftWall = Physics2D.OverlapCircle(leftWallCheck.position, checkRadius, whatIsWall);
        isTouchingRightWall = Physics2D.OverlapCircle(rightWallCheck.position, checkRadius, whatIsWall);

        //Fail Safe For The Extra Jumps Reset On Walls
        if(extraJumpWallReset < 0 && isGrounded)
        {
            extraJumpWallReset = saveExtraJumpWallReset;
            Debug.Log("***Extra Jump Reset On Walls fail safe #1 has been activated***");
        }
        else if(extraJumpWallReset <0)
        {
            extraJumpWallReset = 0;
            Debug.Log("***Extra Jump Reset On Walls fail safe #2 activated***");
        }
        else if(extraJumpWallReset == 0 && isGrounded)
        {
            extraJumpWallReset = saveExtraJumpWallReset;
            Debug.Log("***Extra Jump Reset On Walls fail safe #3 has been activated***");
        }
        // 2. Refresh Jumps & Reset Wall Jump Memory on Ground/Platform
        if (isGrounded)
        {
            extraJumps = extraJumpsValue;
            lastWallJumped = 0; // Reset wall jump memory upon landing
            extraJumpWallReset = saveExtraJumpWallReset;
        }
        else if (isWallSliding && extraJumpWallReset > 0)
        {
            extraJumps = extraJumpsValue; 
            extraJumpWallReset -= 1;
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
            // Can only wall jump if touching the wall AND it's a different wall than the last one jumped from
            else if (isTouchingLeftWall && lastWallJumped != -1)
            {
                WallJump(-1f); // Blast right
                lastWallJumped = -1; // Remember we just jumped off the left wall
                isTouchingLeftWall = false;
                
            }
            else if (isTouchingRightWall && lastWallJumped != 1)
            {
                WallJump(1f); // Blast left
                lastWallJumped = 1; // Remember we just jumped off the right wall
                isTouchingRightWall = false;
            }
            else if (extraJumps > 0)
            {
                NormalJump();
                extraJumps -= 1; 
            }
        }

        // 5. Hollow Knight Variable Jump Height (The Secret Sauce)
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
        if(extraJumpWallReset <=0)
        {
            isWallJumping = false;
        }
    }
}