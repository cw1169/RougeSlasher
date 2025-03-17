using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;

    public bool isDead;

    private Animator playerAnimation;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        playerAnimation.SetBool("isDead", isDead);
    }

    public void TakeDamage(int damage){
        health -= damage;
        StartCoroutine(DamageFlash());
        if(health <= 0){
            isDead = true;
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
    }

    public IEnumerator DamageFlash(){
        spriteRenderer.color = Color.magenta;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;

    }
}