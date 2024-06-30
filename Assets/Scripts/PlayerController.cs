using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public float airDashSpeed = 10f;
    public float airDashDuration = 0.2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isInverseWorld;
    private bool facingRight = true;
    private bool canDoubleJump;
    private bool hasAirDashed;
    private bool isAirDashing;
    private float airDashTime;

    private int flowerCount = 0;
    public int totalFlowers = 3;
    public GameObject bed2;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        isInverseWorld = SceneManager.GetActiveScene().name == "InverseWorld";
        ResetJumpDash();
    }

    void Update()
    {
        if (isAirDashing)
        {
            AirDash();
            return;
        }

        // Movement
        float moveInput = Input.GetAxis("Horizontal");
        if (isInverseWorld)
        {
            moveInput = -moveInput;
        }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update animator parameters
        animator.SetFloat("Speed", Mathf.Abs(moveInput));
        animator.SetBool("IsGrounded", IsGrounded());

        // Jumping and Double Jumping
        if (Input.GetButtonDown("Jump"))
        {
            if (IsGrounded())
            {
                Jump();
                canDoubleJump = true;
                hasAirDashed = false;
            }
            else if (canDoubleJump && !hasAirDashed)
            {
                Jump();
                canDoubleJump = false;
            }
        }

        // Air Dash
        if (Input.GetKeyDown(KeyCode.LeftShift) && !hasAirDashed && !IsGrounded() && canDoubleJump)
        {
            StartAirDash();
            canDoubleJump = false; // Disallow double jump after air dash
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

        if (flowerCount == totalFlowers && Vector2.Distance(transform.position, bed2.transform.position) < 1f)
        {
            SaveGame();
            LoadNextScene();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        animator.SetBool("IsJumping", true);
    }

    void StartAirDash()
    {
        isAirDashing = true;
        hasAirDashed = true;
        airDashTime = airDashDuration;
        animator.SetBool("IsJumping", true);
    }

    void AirDash()
    {
        if (airDashTime > 0)
        {
            rb.velocity = new Vector2(facingRight ? airDashSpeed : -airDashSpeed, 0);
            airDashTime -= Time.deltaTime;
        }
        else
        {
            isAirDashing = false;
            ResetJumpDash();
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1f, groundLayer);
        bool grounded = hit.collider != null;
        if (grounded)
        {
            animator.SetBool("IsJumping", false);
        }
        return grounded;
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void ResetJumpDash()
    {
        canDoubleJump = false;
        hasAirDashed = false;
        isAirDashing = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flower"))
        {
            flowerCount++;
        }

        if (other.CompareTag("LowGround"))
        {
            SceneManager.LoadScene("EndScreen");
        }
    }

    void SaveGame()
    {
        Debug.Log("Game Saved");
    }

    void LoadNextScene()
    {
        // Check the current scene and load the appropriate next scene
        string currentSceneName = SceneManager.GetActiveScene().name;
     
        if (currentSceneName == "RealWorld")
        {
            SceneManager.LoadScene("InverseWorld");
        }

        else if (currentSceneName == "InverseWorld")
        {
            SceneManager.LoadScene("EndScreen");
        }
        
    }
}
