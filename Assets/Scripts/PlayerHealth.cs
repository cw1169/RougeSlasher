using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int health;
    public bool isDead;
    
    private Animator playerAnimation;
    private Rigidbody2D body;
    private SpriteRenderer spriteRenderer;
    private GameObject player;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerAnimation = GetComponent<Animator>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        health = maxHealth;
        isDead = false;
        player = GetComponent<GameObject>();
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
            StartCoroutine(ReloadScene());
        }
    }

    public IEnumerator DamageFlash(){
        spriteRenderer.color = Color.magenta;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = Color.white;

    }

    public IEnumerator ReloadScene(){
         yield return new WaitForSeconds(1);
         Scene currentScene = SceneManager.GetActiveScene();
         SceneManager.LoadScene(currentScene.name);
    }
}