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

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Optional: Use only if animations are present
    }

    private void Start()
    {
        jumpsRemaining = MaxJumps;
        if (animator) animator.SetInteger("jumpsRemaining", jumpsRemaining);
    }

    private void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        Move();
    }

    private void Move()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        // Check if player is grounded and if there's an obstacle in front
        if (!IsObstacle(horizontalInput))
        {
            body.velocity = new Vector2(runSpeed * horizontalInput, body.velocity.y);
            if (horizontalInput != 0f)
            {
                transform.localScale = new Vector3(horizontalInput < 0 ? -1 : 1, 1, 1);
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
        if (isGrounded || jumpsRemaining > 0)
        {
            float jumpVelocity = jumpForce;
            if (!isGrounded)
            {
                jumpVelocity = doubleJumpMultiplier * 2; // Reduce the jump force for double jump
                animator?.SetTrigger("doubleJump"); // Optional: Trigger double jump animation
            }
            else
            {
                animator?.SetTrigger("jump"); // Optional: Trigger jump animation
            }

            body.velocity = new Vector2(body.velocity.x, jumpVelocity);
            jumpsRemaining--;
            if (animator) animator.SetInteger("jumpsRemaining", jumpsRemaining);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (isGrounded)
        {
            jumpsRemaining = MaxJumps;
        }
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
        body.velocity = new Vector2(0, 10);
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
}