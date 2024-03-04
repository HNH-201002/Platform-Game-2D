using System.Collections;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private readonly int _jumpRemainingHash = Animator.StringToHash("jumpsRemaining");
    private readonly int _jumpAniHash = Animator.StringToHash("jump");
    private readonly int _doubleJumpHash = Animator.StringToHash("doubleJump");
    private readonly int _groundHash = Animator.StringToHash("isGrounded");
    private readonly int _speedHash = Animator.StringToHash("speed");

    [SerializeField] private float runSpeed = 0.6f;
    [SerializeField] private float jumpForce = 2.6f;
    [SerializeField] private float doubleJumpMultiplier;

    private Rigidbody2D body;
    private Animator animator;

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

    private bool canDoubleJump = false;
    const string JUMP_SOUND_NAME = "Jump";
    const string Double_JUMP_SOUND_NAME = "DoubleJump";
    private bool isPlayerDied;

    private float jumpGracePeriod = 0.2f;
    private bool isGracePeriodActive = false; 
    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); 
        playerInput = new CharacterAction();
    }

    private void Start()
    {
        jumpsRemaining = MaxJumps;
        Health.OnPlayerDied += PlayerDied;
        if (animator) animator.SetInteger(_jumpRemainingHash, jumpsRemaining);
    }

    private void Update()
    {
        if (isPlayerDied) return;

        if (playerInput.move.Jump.triggered)
        {
            if (isGrounded || isGracePeriodActive)
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
        if (!IsObstacle(horizontalInput.x))
        {
            body.velocity = new Vector2(runSpeed * horizontalInput.x, body.velocity.y);
            if (horizontalInput.x != 0f)
            {
                transform.localScale = new Vector3(horizontalInput.x < 0 ? -1 : 1, 1, 1);
            }
        }
        if (animator)
        {
            animator.SetBool(_groundHash, isGrounded);
            animator.SetFloat(_speedHash, Mathf.Abs(body.velocity.x));
        }
    }



    private void Jump()
    {
        SoundManager.Instance.PlaySound(JUMP_SOUND_NAME);
        body.velocity = new Vector2(body.velocity.x, jumpForce);
        isGrounded = false;
        canDoubleJump = true; 
        animator?.SetTrigger(_jumpAniHash); 
    }

    private void CheckGrounded()
    {
        bool wasGrounded = isGrounded;
        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);

        if (wasGrounded && !isGrounded)
        {
            StartCoroutine(JumpGracePeriod());
        }
        else if (isGrounded)
        {
            StopAllCoroutines();
            isGracePeriodActive = false;
            jumpsRemaining = MaxJumps;
        }
    }

    private void DoubleJump()
    {
        SoundManager.Instance.PlaySound(Double_JUMP_SOUND_NAME);
        animator?.SetTrigger(_doubleJumpHash);
        body.velocity = new Vector2(body.velocity.x, jumpForce * doubleJumpMultiplier);
        canDoubleJump = false;
        jumpsRemaining--; 
    }

    private IEnumerator JumpGracePeriod()
    {
        isGracePeriodActive = true;
        yield return new WaitForSeconds(jumpGracePeriod);
        isGracePeriodActive = false;
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