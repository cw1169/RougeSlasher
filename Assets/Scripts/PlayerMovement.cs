using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    public float jumpSpeed;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    
    private Rigidbody2D body;
    private float move;
    private bool isGrounded;
    private Animator playerAnimation;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        move = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(speed * move, body.linearVelocity.y);

        spriteRenderer.flipX = body.linearVelocity.x < 0;

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);

        if(Input.GetButtonDown("Jump") && isGrounded){
            body.AddForce(new Vector2(body.linearVelocity.x, jumpSpeed));
        }

        playerAnimation.SetFloat("Speed", Mathf.Abs(body.linearVelocity.x));
        playerAnimation.SetBool("isGrounded", isGrounded);
    }
}
