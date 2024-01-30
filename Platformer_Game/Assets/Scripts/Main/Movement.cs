using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed = 0.6f; // Running speed.
    public float jumpForce = 2.6f; // Jump height.

    public Sprite jumpSprite; // Sprite that shows up when the character is not on the ground. [OPTIONAL]

    private Rigidbody2D body; // Variable for the RigidBody2D component.
    private SpriteRenderer sr; // Variable for the SpriteRenderer component.
    private Animator animator; // Variable for the Animator component. [OPTIONAL]

    private bool isGrounded; // Variable that will check if character is on the ground.
    private bool doubleJump = false;

    public GameObject groundCheckPoint; // The object through which the isGrounded check is performed.
    public float groundCheckRadius; // isGrounded check radius.
    public LayerMask groundLayer; // Layer wich the character can jump on.

    private bool jumpPressed = false; // Variable that will check is "Space" key is pressed.
    private int jumpsRemaining = 2;
    private int maxJumps =2;


    void Awake()
    {
        body = GetComponent<Rigidbody2D>(); // Setting the RigidBody2D component.
        sr = GetComponent<SpriteRenderer>(); // Setting the SpriteRenderer component.
        animator = GetComponent<Animator>(); // Setting the Animator component. [OPTIONAL]
    }
    private void Start()
    {
        jumpsRemaining = maxJumps;
        animator.SetInteger("jumpsRemaining", jumpsRemaining);
    }

    // Update() is called every frame.
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGrounded)
            {
                jumpsRemaining = maxJumps;
            }
            Jump();
        }
    }

    // Update using for physics calculations.
    void FixedUpdate()
    {
        Move();   
    }

    private void Move()
    {
        float horizontalCharacter = Input.GetAxis("Horizontal");
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.transform.position, groundCheckRadius, groundLayer); // Checking if character is on the ground.
        animator.SetBool("isGrounded", isGrounded);
        animator.SetFloat("speed", Mathf.Clamp(runSpeed * Mathf.Abs(horizontalCharacter), 0, 1));
        // Left/Right movement.
        if (horizontalCharacter < 0)
        {
            body.velocity = new Vector2(runSpeed * horizontalCharacter, body.velocity.y); // Move left physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z); // Rotating the character object to the left.
        }
        else if (horizontalCharacter > 0)
        {
            body.velocity = new Vector2(runSpeed * horizontalCharacter, body.velocity.y); // Move right physics.
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 0, transform.eulerAngles.z); // Rotating the character object to the right.
        }
        else body.velocity = new Vector2(0, body.velocity.y);

   
    }

    private void Jump()
    {

        float jumpForceCalculated = jumpsRemaining == maxJumps ? jumpForce : jumpForce/1.4f + body.velocity.y;
        if (jumpsRemaining == maxJumps)
        {
            animator.SetTrigger("jump");
            body.velocity = new Vector2(0, jumpForceCalculated); // Jump physics.
        }
        else if(jumpsRemaining == 1)
        {
            animator.SetTrigger("doubleJump");
            body.velocity = new Vector2(0, jumpForceCalculated); // Jump physics.
        }
        jumpsRemaining--;
        animator.SetInteger("jumpsRemaining", jumpsRemaining);
    }
}
