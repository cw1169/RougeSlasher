using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    
    private Rigidbody2D body;
    private float move;
    private bool isGrounded;
    private int doubleJumpCount;
    private bool isDoubleJump;
    private Animator playerAnimation;
    private SpriteRenderer spriteRenderer;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame.
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(speed * move, body.linearVelocity.y);

        spriteRenderer.flipX = body.linearVelocity.x < 0;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if(isGrounded){
            doubleJumpCount = 1;
            isDoubleJump = false;
        }

        if(Input.GetButtonDown("Jump") && isGrounded){
            body.AddForce(new Vector2(body.linearVelocity.x, jumpSpeed));
            isDoubleJump = false;
        }
        else if(Input.GetButtonDown("Jump") && doubleJumpCount > 0 && isGrounded == false){
            body.linearVelocity = new Vector2(body.linearVelocity.x, doubleJumpSpeed);
            doubleJumpCount = doubleJumpCount - 1;
            isDoubleJump = true;
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(body.linearVelocity.x));
        playerAnimation.SetBool("isGrounded", isGrounded);
        playerAnimation.SetBool("isDoubleJump", isDoubleJump);
    }
}
