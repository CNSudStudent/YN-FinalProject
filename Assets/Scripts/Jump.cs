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

    private bool canJump;

    private Rigidbody2D rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        extrajumptracker = extraJump;
    }

    // Update is called once per frame
    void Update()
    {
        if (rigidBody.linearVelocity.y == 0)
        {
            canJump = true;
            extraJump = extrajumptracker;
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

        }
        if ((extraJump>=1) && (canJump=false) && Input.GetButton("Jump"))
        {
            // Apply an instantaneous upwards force
            rigidBody.AddForce(Vector2.up * jumpStrength, ForceMode2D.Impulse);
            //canJump = !checkGround;
            extraJump -= 1;

        }

    }

}
