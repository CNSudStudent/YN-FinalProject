using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour

{
    // strength of the push
    [Header("Jump Stremgth")]
    public float jumpStrength = 10f;

    [Header("Jump in Midair")]
    public bool alwaysJumping = false;

    [Header("Extra Jump")]
    public float extraJump;
    private float extrajumptracker;
    public float extrajumpdelay = 1f;
    private bool isExtraJumpTimerRunning = false;
    private float extrajumpdelaytracker;
    private float delaybetweenjumps = 0.1f;
    private bool isDelayJumpTimerRunning = false;

    private bool canJump;

    private Rigidbody2D rigidBody;

    void Start()
    {
        extrajumptracker = extraJump;
        extrajumpdelaytracker=extrajumpdelay;
    }
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.linearVelocity.y == 0)
        {
            canJump = true;
            extraJump = extrajumptracker;
            isExtraJumpTimerRunning = false;
            extrajumpdelay=extrajumpdelaytracker;
            isDelayJumpTimerRunning = false;
            delaybetweenjumps = 0.2f;
            

        }
        else
        {
            canJump = false;
        }

        if ((canJump) && Input.GetButton("Jump"))
        {
            // Apply an instantaneous upwards force
            rigidBody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            //canJump = !checkGround;
            isExtraJumpTimerRunning = true;
            delaybetweenjumps = 0.1f;
            isDelayJumpTimerRunning = true;
        }
        if (isExtraJumpTimerRunning)
        {
            if(extrajumpdelay > 0)
            {
                extrajumpdelay -= Time.deltaTime;
            }
            else
            {
                extrajumpdelay = 0;
                isExtraJumpTimerRunning = false;
                
            }
        }
        if (isDelayJumpTimerRunning)
        {
            if(delaybetweenjumps > 0)
            {
                delaybetweenjumps -= Time.deltaTime;
            }
            else
            {
                delaybetweenjumps = 0;
                isDelayJumpTimerRunning = false;
                
            }
        }
        if ((extraJump > 0) && (!canJump) && Input.GetButton("Jump") && (extrajumpdelay == 0) && (delaybetweenjumps == 0))
        {
            // Apply an instantaneous upwards force
            rigidBody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            //canJump = !checkGround;
            extraJump -= 1;
            delaybetweenjumps = 0.1f;
            isDelayJumpTimerRunning = true;
            

        }

    }
}