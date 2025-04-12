using System.Collections;
using System.Threading;
using Unity.VisualScripting.ReorderableList.Element_Adder_Menu;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PlayerMovement : MonoBehaviour
{

    public InputSystem_Actions InputSystem;
    public float speed;
    public float jumpSpeed;
    public float doubleJumpSpeed;
    public float dashingPower;
    public float dashingTime;
    public float dashingCooldown;
    public float wallSlideSpeed;
    public float wallCheckRadius;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    public PlayerHealth playerHealth;

   
    private bool hasLanded = false;
    private InputAction move, dash, jump, crouch, attack; // declare input actions
    private bool canDash;
    private bool isDashing;
    private Rigidbody2D body;
    private Vector2 moveDirection;
    private bool isGrounded;
    private int doubleJumpCount;
    private bool isDoubleJump;
    private bool isCrouching;
    private Animator playerAnimation;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;
    private bool facingRight;
    private bool facingLeft;
    public bool isOnWall;

    private void Awake()
    {
        InputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        move = InputSystem.Player.Move;
        dash = InputSystem.Player.Dash;
        jump = InputSystem.Player.Jump;
        crouch = InputSystem.Player.Crouch;
        attack = InputSystem.Player.Attack;
        move.Enable();
        dash.Enable();
        jump.Enable();
        crouch.Enable();
        attack.Enable();
    }

        private void OnDisable()
    {
        move.Disable();
        dash.Disable();
        jump.Disable();
        crouch.Disable();
        attack.Disable();
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

        // moves playerr left and right
        body.linearVelocity = new Vector2(speed * moveDirection.x, body.linearVelocity.y);

        // Switch player direction based on movement
        if (moveDirection.x > 0)
        {
            body.transform.localScale = new Vector3(1, 1, 1); // Face Right
        }
        else if (moveDirection.x < 0)
        {
            body.transform.localScale = new Vector3(-1, 1, 1); // Face Left
        }

        // checks for grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if(isGrounded && hasLanded == false){
            doubleJumpCount = 1;
            isDoubleJump = false;
            hasLanded = true;
        }

        // jump and double jump 
        if(jump.WasPressedThisFrame() && isGrounded){
            body.AddForce(new Vector2(body.linearVelocity.x, jumpSpeed));
            isDoubleJump = false;
            hasLanded = false;
        }
        else if(jump.WasPressedThisFrame() && doubleJumpCount > 0 && isGrounded == false){
            body.linearVelocity = new Vector2(body.linearVelocity.x, doubleJumpSpeed);
            doubleJumpCount = doubleJumpCount - 1;
            isDoubleJump = true;
            hasLanded = false;
        }

        // Dashing
        if (dash.WasPressedThisFrame() && canDash) 
        {
            StartCoroutine(Dash());
        }

        // controls player crouching NOT WORKING
        if (moveDirection.x == 0 && isGrounded == true && crouch.WasPressedThisFrame())
        {
            isCrouching = true;
        }
        else if (moveDirection.x == 0 && isGrounded == true && crouch.WasReleasedThisFrame()){
            isCrouching = false;
        }

        // onWALL
        wallCheckRayCast();
        if (isOnWall && !isGrounded)
        {
            body.transform.localScale = new Vector3(1, 1, 1); // Face Right
            body.linearVelocity = new Vector2(body.linearVelocity.x, wallSlideSpeed);
            
        }

        
        // returning animation conditions
        playerAnimation.SetFloat("Speed", Mathf.Abs(moveDirection.x));
        playerAnimation.SetBool("isGrounded", isGrounded);
        playerAnimation.SetBool("isDoubleJump", isDoubleJump);
        playerAnimation.SetBool("hasLanded", hasLanded);
        playerAnimation.SetBool("isDashing", isDashing);
        playerAnimation.SetBool("isCrouching", isCrouching);
        playerAnimation.SetBool("isOnWall", isOnWall);

    }



    // check if player is on the wall
    public void wallCheckRayCast()
    {
        Debug.DrawRay(transform.position, Vector3.left*wallCheckRadius, Color.white, 0);
        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, wallCheckRadius, wallLayer);
        if (hit.collider != null && hit.collider.CompareTag("Wall"))
        {
            isOnWall = true;
            Debug.DrawRay(transform.position, Vector3.left * wallCheckRadius, Color.red, 0);
            Debug.Log("Hit Wall");
        }
        else
        {
            isOnWall = false;
        }
    }

    // dash code
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