using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isInverseWorld;

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
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        return hit.collider != null;
    }
}
