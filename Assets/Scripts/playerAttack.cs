using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Timeline;

public class playerAttack : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    
    private SpriteRenderer spriteRenderer;
    public PlayerHealth playerHealth;
    public PlayerMovement playerMovement;
    public InputSystem_Actions InputSystem;

    private Animator playerAnimation;
    private InputAction attack; // declare input actions
    public bool isAttacking;


        private void Awake()
    {
        InputSystem = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        attack = InputSystem.Player.Attack;
        attack.Enable();
    }

        private void OnDisable()
    {
        attack.Disable();
    }

    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(attack.WasPressedThisFrame())
        {
            isAttacking = true;
        }
        else if (attack.WasReleasedThisFrame())
        {
            isAttacking = false;
        }
        else
        {
            isAttacking = false;
        }

        playerAnimation.SetBool("isAttacking", isAttacking);
    }
}
