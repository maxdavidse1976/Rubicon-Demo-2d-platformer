using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField] private Rigidbody2D player;
    public float jumpHeight;

    [SerializeField] private Transform groundPoint;
    [SerializeField] private LayerMask whatIsGround;
    private bool isGrounded;
    private bool canDoubleJump;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    public float knockBackDuration, knockBackForce;
    private float knockBackCounter;
    public static PlayerController instance;

    public float bounceForce;
    public bool stopInput;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (!PauzeMenu.instance.isPaused && !stopInput)
        {
            if (knockBackCounter <= 0)
            {
                player.velocity = new Vector2(moveSpeed * Input.GetAxis("Horizontal"), player.velocity.y);

                isGrounded = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);

                if (isGrounded)
                {
                    canDoubleJump = true;
                }
                if (Input.GetButtonDown("Jump"))
                {
                    if (isGrounded)
                    {
                        player.velocity = new Vector2(player.velocity.x, jumpHeight);
                        AudioManager.instance.PlaySFX(10);
                    }
                    else
                    {
                        if (canDoubleJump)
                        {
                            player.velocity = new Vector2(player.velocity.x, jumpHeight);
                            AudioManager.instance.PlaySFX(10);
                            canDoubleJump = false;
                        }
                    }
                }
                if (player.velocity.x < 0)
                {
                    spriteRenderer.flipX = true;
                }
                else if (player.velocity.x > 0)
                {
                    spriteRenderer.flipX = false;
                }
            }
            else
            {
                knockBackCounter -= Time.deltaTime;
                if (spriteRenderer.flipX)
                {
                    player.velocity = new Vector2(knockBackForce, player.velocity.y);
                }
                else
                {
                    player.velocity = new Vector2(-knockBackForce, player.velocity.y);
                }
            }
            // Make sure to always use a positive number to coincide with the setting in the animator of Unity.
            animator.SetFloat("moveSpeed", Mathf.Abs(player.velocity.x));
            animator.SetBool("isGrounded", !isGrounded);
        }
    }

    public void KnockBack()
    {
        knockBackCounter = knockBackDuration;
        player.velocity = new Vector2(0f, knockBackForce);
        animator.SetTrigger("playerHurt");
    }

    public void Bounce()
    {
        player.velocity = new Vector2(player.velocity.x, bounceForce);
        AudioManager.instance.PlaySFX(10);
    }
}
