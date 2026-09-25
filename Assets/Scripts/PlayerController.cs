using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    // get a reference to the animator
    Animator anim;
    // get the move speed from the inspector, but with a default value of 10
    public float moveSpeed = 10f;
    // get RoboMan's rigid body for physics and sprite renderer for visuals
    private Rigidbody2D roboManRigidBody;
    private SpriteRenderer roboManSpriteRenderer;
    // get a reference to the main camera
    private Camera cam;
    // is RoboMan facing right or on the ground?
    private bool roboManIsFacingRight = true;
    private bool roboManIsOnTheGround = true;
    // initialize RoboMan's jump force (y direction only)
    private Vector2 jumpForce = new Vector2(0f, 8f);

    // grab references to RoboMan's components for use in other functions
    void Awake()
    {
        anim = GetComponent<Animator>();
        roboManRigidBody = GetComponent<Rigidbody2D>();
        roboManSpriteRenderer = GetComponent<SpriteRenderer>();
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        // if the jump button is pressed and RoboMan is on the ground
        // then jump!
        if (Input.GetButtonDown("Jump") && roboManIsOnTheGround)
        {
            Jump();
        }
                
        // get and store the value of the user input on the horizontal access
        // this is left or right
        float horizontalInputValue = Input.GetAxis("Horizontal");

        // calculate the velocity based on the user input and the public moveSpeed
        // we are not handling y velocity here, that is done in the Jump function
        // we need to set the new y velocity to be the same exact value as the old y velocity
        Vector2 currentVelocity = new Vector2(horizontalInputValue * moveSpeed, roboManRigidBody.linearVelocity.y);

        // set RoboMan's velocity to be what we calculated
        roboManRigidBody.linearVelocity = currentVelocity;

        // after applying user input, make sure that RoboMan does not go off the screen by
        // making sure RoboMan's x position is always clamped between -8 and 8
        // we don't want to restrict y and z, so we keep the old values
        float clampedRoboManX = Mathf.Clamp(transform.position.x, -8, 8);
        transform.position = new Vector3(clampedRoboManX, transform.position.y, transform.position.z);

        // if the value of the horizontal axis input is 0,
        // then ask the animator to stop showing the IsRunning animation
        // otherwise, ask the animator to show the IsRunning animation
        if(horizontalInputValue == 0)
        {
            anim.SetBool("IsRunning", false);
        }
        else 
        {
            anim.SetBool("IsRunning", true);
        }

        // ask the camera to project the mouse cursor to a world point
        Vector3 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        // if the mouse is to the left of RoboMan and RoboMan is facing right
        // then flip RoboMan
        // if the mouse is to the right of RoboMan and RoboMan is facing left
        // then flip RoboMan
        if (mousePosition.x < transform.position.x && roboManIsFacingRight)
        {
            FlipDirection();
        }
        else if (mousePosition.x > transform.position.x && !roboManIsFacingRight)
        {
            FlipDirection();
        }
    }

    // when Jump is called, we ask Unity to apply an impulse force
    // to RoboMan
    // the force is defined with the private jumpForce variable
    // note: an "impulse" force is applied an one instance in time and not continuously
    void Jump()
    {
        roboManRigidBody.AddForce(jumpForce, ForceMode2D.Impulse);
    }

    // when we want to flip RoboMan
    // ask RoboMan's sprite to flip in the X direction
    // and store the direction that RoboMan is facing
    // note: code that looks like "bool = !bool" is a fancy way
    // to say "toggle the value of the variable"
    // if the value is true, we set it to "not true" or false
    // if the value is false, we set it to "not false" or true
    private void FlipDirection()
    {
        roboManSpriteRenderer.flipX = !roboManSpriteRenderer.flipX;
        roboManIsFacingRight = !roboManIsFacingRight;
    }

    // if RoboMan is colliding with the Ground game object in the scene,
    // then RoboMan is on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Ground")
        {
            roboManIsOnTheGround = true;
        }
    }

    // if RoboMan is not colliding with the Ground game object in the scene,
    // then RoboMan is not on the ground
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            roboManIsOnTheGround = false;
        }
    }
}
