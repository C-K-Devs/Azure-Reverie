using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 5f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isInverseWorld;
    private bool facingRight = true; // Track the direction the character is facing

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isInverseWorld = SceneManager.GetActiveScene().name == "InverseWorld";
    }

    void Update()
    {
        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        if (isInverseWorld)
        {
            moveInput = -moveInput;
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveInput));

        // Jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            animator.SetBool("IsJumping", true);
        }
        else if (IsGrounded())
        {
            animator.SetBool("IsJumping", false);
        }

        // Flip character direction based on movement
        if (moveInput > 0 && !facingRight)
        {
            Flip();
        }
        else if (moveInput < 0 && facingRight)
        {
            Flip();
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1; // Flip the character by inverting the x scale
        transform.localScale = theScale;
    }
}
