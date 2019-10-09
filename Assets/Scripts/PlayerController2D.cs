using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    /*


    PUBLIC VARS


     */


    public AudioSource jumpsound;
    public Animator animator; //defining animator
    public Rigidbody2D rb2d; // rigidbody
    SpriteRenderer spriteRenderer;
    [SerializeField]  //makes the variable appear as changeable in the editor
    public Transform firePoint;
    public GameObject bullet;
    public float knockback = 2f;
    public float knockbackLength = 0.4f;
    public float knockbackCount;
    public bool knockFromRight;
    public bool Invincible;
    public float InvincibleDuration;
    public GameObject djParticle;

    /*


    PRIVATE VARS


     */


    private float runspeed = 1.6f;
    private float fireframe = 0f;
    private float fireAnimDelay = 0f;
    private HealthManager healthManager;
    [SerializeField]    private float jumpspeed = 5.4f;
    private bool doubleJump;


    /*


    GROUND CHECKS


     */



    bool isGrounded; //ground checker

        [SerializeField]
        Transform groundCheck;

        [SerializeField]
        Transform groundCheckL;

        [SerializeField]
        Transform groundCheckR;




    // Start is called before the first frame update



    void Start()
    {
        //gets all necessary components
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        healthManager = FindObjectOfType<HealthManager>();
    }


    private void FixedUpdate() { //updates in an fast but inconsistent manner (actual time)
        if (knockbackCount <= 0) {
        healthManager.enabled = true;




        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true; //checking for the ground
        }
        else {
            isGrounded = false;
            if (!doubleJump) {
                if (fireframe > 0f) animator.Play("player_jumpfire"); //check if player is airborne and is firing projectile
                else animator.Play("player_jump"); //regular jump animation
            }
            else {
                if (fireframe > 0f) {
                    animator.Play("player_doublejumpfire");
                }
                else {
                    animator.Play("player_doublejump");
                }
            }
        }

        if (isGrounded) doubleJump = false;




        if (fireframe > 0f) {

                //checking if player is running and firing @ the same time
                 if (Input.GetKey("d") || Input.GetKey("right")) {
                        rb2d.velocity = new Vector2(runspeed,rb2d.velocity.y);
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
                                fireAnimDelay -= Time.deltaTime;
                        }
                        else if (isGrounded) animator.Play("player_runfire");
                        transform.eulerAngles = new Vector2(0,0);
                    }
                    else if (Input.GetKey("a") || Input.GetKey("left")) {
                        rb2d.velocity = new Vector2(-runspeed,rb2d.velocity.y);
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
                                fireAnimDelay -= Time.deltaTime;
                        }
                        else if (isGrounded) animator.Play("player_runfire");
                        transform.eulerAngles = new Vector2(0,180);
                    }
                    //checking if standing still
                    else {
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_fire") && isGrounded && fireAnimDelay>0f){
                                fireAnimDelay -= Time.deltaTime;
                        }
                        else if (isGrounded) animator.Play("player_fire");
                        rb2d.velocity = new Vector2(0,rb2d.velocity.y);
                    }
                    fireframe -= Time.deltaTime; //reduces the firing frame delay by 1
        }




        else {
            //regular run checks


        if (Input.GetKey("d") || Input.GetKey("right")) {
            rb2d.velocity = new Vector2(runspeed,rb2d.velocity.y);
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
            fireAnimDelay -= Time.deltaTime;
            }
            else if (isGrounded) animator.Play("player_run");
            transform.eulerAngles = new Vector2(0,0);
        }
        else if (Input.GetKey("a") || Input.GetKey("left")) {
            rb2d.velocity = new Vector2(-runspeed,rb2d.velocity.y);
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
            fireAnimDelay -= Time.deltaTime;
            }
            else if (isGrounded) animator.Play("player_run");
            transform.eulerAngles = new Vector2(0,180);
        }
        else {
            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_fire") && isGrounded && fireAnimDelay>0f){
            fireAnimDelay -= Time.deltaTime;
            }
            else if (isGrounded) animator.Play("player_idle");
            rb2d.velocity = new Vector2(0,rb2d.velocity.y);
        }
        }





    } else { // knockback applier
        if (knockFromRight) {
            rb2d.velocity = new Vector2 (-knockback, knockback);
        }
        if (!knockFromRight) {
            rb2d.velocity = new Vector2 (knockback, knockback);
        }
        knockbackCount -= Time.deltaTime;
        TurnInvincible();
    }
    }


    private void Update() {  // updates every frame
         if (fireframe > 0f) {
            if (Input.GetKeyDown("space") && isGrounded || Input.GetKeyDown("x")  && isGrounded || Input.GetKeyDown("up")  && isGrounded) {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
                        animator.Play("player_jumpfire");
                        jumpsound.Play();
                    } 
            if (Input.GetKeyDown("space") && !doubleJump && !isGrounded || Input.GetKeyDown("x") && !doubleJump && !isGrounded || Input.GetKeyDown("up") && !doubleJump && !isGrounded) {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed - 1f);
                Instantiate(djParticle, transform.position, transform.rotation);
                jumpsound.Play();
                doubleJump = true;
            }
        }
        else {
            if (Input.GetKeyDown("space") && isGrounded || Input.GetKeyDown("x")  && isGrounded || Input.GetKeyDown("up")  && isGrounded) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
            animator.Play("player_jump");
            jumpsound.Play();
        }

            if (Input.GetKeyDown("space") && !doubleJump && !isGrounded || Input.GetKeyDown("x") && !doubleJump && !isGrounded || Input.GetKeyDown("up") && !doubleJump && !isGrounded) {
                rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed - 1f);
                jumpsound.Play();
                                Instantiate(djParticle, transform.position, transform.rotation);

                doubleJump = true;
            }
            if (Input.GetKeyDown("z")){
                fireframe = 0.2f;
                fireAnimDelay = 0.5f; //frame delay for animation
                Instantiate(bullet, firePoint.position, firePoint.rotation);
            }
        }
    }



    void UndoInvincible()
    {
        Invincible = false;
        StopAllCoroutines();
        spriteRenderer.enabled = true;
        gameObject.layer = 1;
    }

    
    IEnumerator FlashSprite()
    {
        while(true)
        {
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(.02f);
            spriteRenderer.enabled = true;
            yield return new WaitForSeconds(.02f);
        }
    }


    public void TurnInvincible () {
        StopAllCoroutines();
        Invincible = true;
        Invoke("UndoInvincible", InvincibleDuration);
        StartCoroutine(FlashSprite());
        gameObject.layer = 13;
    }
}
