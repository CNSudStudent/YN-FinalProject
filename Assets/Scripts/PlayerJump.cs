using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    public float jumpStrength = 10f;

    public float fallMultiplier = 2.5f;
    public float jumpMultiplier = 2f;

    public bool alwaysJumping = false;

    private bool canJump;

    private Rigidbody rigidBody;

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(rigidBody.linearVelocity.y) < 0.05)
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }

        if (Input.GetButtonDown("Jump") && canJump)
        {
            GetComponent<Rigidbody>().linearVelocity += Vector3.up * jumpStrength;

        }
    }
}
