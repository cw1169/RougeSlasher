using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    public InputSystem_Actions InputSystem;
    public float speed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public PlayerHealth playerHealth;

   
    private InputAction move;
    private InputAction dash;
    private InputAction jump;
    private bool canDash;
    private bool isDashing;
    private Rigidbody2D body;
    private Vector2 moveDirection;
    private bool isGrounded;
    private int doubleJumpCount;
    private bool isDoubleJump;
    private Animator playerAnimation;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;


    private void Awake()
    {
        InputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        move = InputSystem.Player.Move;
        dash = InputSystem.Player.Dash;
        jump = InputSystem.Player.Jump;
        move.Enable();
        dash.Enable();
        jump.Enable();
    }

        private void OnDisable()
    {
        move.Disable();
        dash.Disable();
        jump.Disable();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        playerAnimation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        trailRenderer = GetComponent<TrailRenderer>();
        canDash = true;
    }

    // Update is called once per frame.
    void Update()
    {
        if(playerHealth.isDead || isDashing){
            return;
        }
        
        // move = Input.GetAxis("Horizontal");
        moveDirection = move.ReadValue<Vector2>();

        body.linearVelocity = new Vector2(speed * moveDirection.x, body.linearVelocity.y);

        if (moveDirection.x > 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1); // Face Right
        }
        else if (moveDirection.x < 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1); // Face Left
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if(isGrounded){
            doubleJumpCount = 1;
            isDoubleJump = false;
        }

        if(jump.WasPressedThisFrame() && isGrounded){
            body.AddForce(new Vector2(body.linearVelocity.x, jumpSpeed));
            isDoubleJump = false;
        }
        else if(jump.WasPressedThisFrame() && doubleJumpCount > 0 && isGrounded == false){
            body.linearVelocity = new Vector2(body.linearVelocity.x, doubleJumpSpeed);
            doubleJumpCount = doubleJumpCount - 1;
            isDoubleJump = true;
        }

        if (dash.WasPressedThisFrame() && canDash) 
        {
            StartCoroutine(Dash());
        }


        playerAnimation.SetFloat("Speed", Mathf.Abs(body.linearVelocity.x));
        playerAnimation.SetBool("isGrounded", isGrounded);
        playerAnimation.SetBool("isDoubleJump", isDoubleJump);
    }


    private IEnumerator Dash(){
        canDash = false;
        isDashing = true;
        float originalGravity = body.gravityScale;
        body.gravityScale = 0f;
        body.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        trailRenderer.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        trailRenderer.emitting = false;
        body.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
