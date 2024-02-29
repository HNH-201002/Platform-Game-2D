using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float runSpeed = 0.6f;
    public float jumpForce = 2.6f;
    public float doubleJumpMultiplier;

    private Rigidbody2D body;
    private Animator animator; // Optional: Use only if animations are present

    private bool isGrounded;
    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public LayerMask checkLayer;

    private int jumpsRemaining;
    [SerializeField] private int MaxJumps = 1;
    public Vector2 offset1; // Center of the box for checking obstacles
    public Vector2 offset2;
    public Vector2 boxCheckSize; // Size of the box for checking obstacles

    public CharacterAction playerInput;

    private float lastJumpTime;

    private bool jumpRequested;
    private bool canDoubleJump = false;
    const string jumpSoundName = "Jump";
    const string doubleJumpSoundName = "DoubleJump";
    private bool isPlayerDied;
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Optional: Use only if animations are present
        playerInput = new CharacterAction();
    }

    private void Start()
    {
        jumpsRemaining = MaxJumps;
        Health.OnPlayerDied += PlayerDied;
        if (animator) animator.SetInteger("jumpsRemaining", jumpsRemaining);
    }

    private void Update()
    {
        if (isPlayerDied) return;
        if (playerInput.move.Jump.triggered)
        {
            if (isGrounded)
            {
                Jump();
            }
            else if (canDoubleJump)
            {
                DoubleJump();
            }
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        Vector2 horizontalInput = playerInput.move.Movement.ReadValue<Vector2>();
        // Check if player is grounded and if there's an obstacle in front
        if (!IsObstacle(horizontalInput.x))
        {
            body.velocity = new Vector2(runSpeed * horizontalInput.x, body.velocity.y);
            if (horizontalInput.x != 0f)
            {
                transform.localScale = new Vector3(horizontalInput.x < 0 ? -1 : 1, 1, 1);
            }
        }

        // Optional: Update animator parameters
        if (animator)
        {
            animator.SetBool("isGrounded", isGrounded);
            animator.SetFloat("speed", Mathf.Abs(body.velocity.x));
        }
    }



    private void Jump()
    {
        SoundManager.Instance.PlaySound(jumpSoundName);
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isGrounded = false;
        canDoubleJump = true; // Allow double jump after initial jump
        lastJumpTime = Time.time;
        animator?.SetTrigger("jump"); // Optional: Trigger jump animation
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            StartCoroutine(ResetJumpsAfterDelay());
        }
    }
    private void DoubleJump()
    {
        SoundManager.Instance.PlaySound(doubleJumpSoundName);
        animator?.SetTrigger("doubleJump");
        body.velocity = new Vector2(body.velocity.x, jumpForce * doubleJumpMultiplier);
        canDoubleJump = false; // Disable double jump until grounded again
        jumpsRemaining--; // Decrement the jumps remaining
        lastJumpTime = Time.time;
    }

    private IEnumerator ResetJumpsAfterDelay()
    {
        yield return new WaitForSeconds(0.1f); // Small delay before resetting jumps
        jumpsRemaining = MaxJumps; // Reset the jumps remaining
    }

    private bool IsObstacle(float horizontalInput)
    {
        if (horizontalInput == 0f) return false;

        Vector2 direction = horizontalInput < 0 ? Vector2.left : Vector2.right;
        Vector2 origin = (Vector2)transform.position + offset1 * direction;
        RaycastHit2D hit = Physics2D.BoxCast(origin + offset2, boxCheckSize, 0, direction, 0.1f,checkLayer);

        return hit.collider != null;
    }
    public void Attack()
    {
        body.velocity = new Vector2(0, 12);
    }

    void OnDrawGizmos()
    {
        // Draw the boxcast used for obstacle detection
        Gizmos.color = Color.red;
        Vector2 originLeft = (Vector2)transform.position + offset1 * Vector2.left;
        Vector2 originRight = (Vector2)transform.position + offset1 * Vector2.right;
        Gizmos.DrawWireCube(originLeft + offset2, boxCheckSize);
        Gizmos.DrawWireCube(originRight + offset2, boxCheckSize);
    }
    public void PlayerDied()
    {
        isPlayerDied = true;
    }
    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }
}