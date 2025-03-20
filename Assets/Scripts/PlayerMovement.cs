using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public PlayerHealth playerHealth;
   
    private DashMove dashMove;
    private Rigidbody2D body;
    private float move;
    private bool isGrounded;
    private int doubleJumpCount;
    private bool isDoubleJump;
    private Vector2 lastDirection;
    private Animator playerAnimation;
    private SpriteRenderer spriteRenderer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        dashMove = GetComponent<DashMove>();
    }

    // Update is called once per frame.
    void Update()
    {
        if(playerHealth.isDead == false){
            move = Input.GetAxis("Horizontal");
    
            body.linearVelocity = new Vector2(speed * move, body.linearVelocity.y);

            spriteRenderer.flipX = body.linearVelocity.x < 0;

            isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
            if(isGrounded){
                doubleJumpCount = 1;
                isDoubleJump = false;
            }

            if(Input.GetKeyDown(KeyCode.Space) && isGrounded){
                body.AddForce(new Vector2(body.linearVelocity.x, jumpSpeed));
                isDoubleJump = false;
            }
            else if(Input.GetKeyDown(KeyCode.Space) && doubleJumpCount > 0 && isGrounded == false){
                body.linearVelocity = new Vector2(body.linearVelocity.x, doubleJumpSpeed);
                doubleJumpCount = doubleJumpCount - 1;
                isDoubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)) 
            {
                StartCoroutine(dashMove.DoDash(move));
            }


            playerAnimation.SetFloat("Speed", Mathf.Abs(body.linearVelocity.x));
            playerAnimation.SetBool("isGrounded", isGrounded);
            playerAnimation.SetBool("isDoubleJump", isDoubleJump);
        }
    }
}
