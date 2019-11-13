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
    public GameObject dashParticle;
    public float startAttackFrame;

    public Transform attackPos;
    public float attackRange;
    public LayerMask whatIsEnemies;
    public int attackDamage;

    public float startAttackCancelFrame;

    public AudioSource swordSound;
    /*


    PRIVATE VARS


     */


    private float runspeed = 1.6f;
    private float fireframe = 0f;
    private float fireAnimDelay = 0f;
    private HealthManager healthManager;
    [SerializeField]    private float jumpspeed = 5.4f;
    private bool doubleJump;
    [SerializeField]    private float dashSpeed;
    [SerializeField]    private float startDashTime;
    [SerializeField]    private float dashTime;
    private bool isDashing;
    private bool dashRight;
    private float attackFrame;
    private bool isAttacking;
    private bool canCancel = true;

    private float attackCancelFrame;



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
        attackFrame = startAttackFrame;
    }


    private void FixedUpdate() { //updates in an fast but inconsistent manner (actual time)
        if (knockbackCount <= 0) {
        healthManager.enabled = true;

        if (attackFrame <= 0 && isAttacking) {
            attackCancelFrame = startAttackCancelFrame;
            attackFrame = startAttackFrame;
            isAttacking = false;
            canCancel = true;
        }
        else if (isAttacking) {
            attackFrame = attackFrame - Time.deltaTime;
            attackCancelFrame = attackCancelFrame - Time.deltaTime;
            isAttacking = true;
        }

        if (attackCancelFrame <= 0) {
            canCancel = true;
        }

        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true; //checking for the ground
        }
        else {
            isGrounded = false;
            if (isAttacking) {
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_sword_air")){
                            fireAnimDelay -= Time.deltaTime;
                }
            }
            else {
                if (!isDashing) {
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
                else {
                    if (fireframe > 0f) {
                            animator.Play("player_dashfire");
                        }
                        else {
                            animator.Play("player_dash");
                        }                
                }
            }
        }

        if (isGrounded) doubleJump = false;




        if (fireframe > 0f && canCancel) {


                //checking if player is running and firing @ the same time
                 if (Input.GetKey("d") || Input.GetKey("right")) {
                        if (!isDashing || !dashRight) {
                            rb2d.velocity = new Vector2(runspeed,rb2d.velocity.y);
                            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
                                    fireAnimDelay -= Time.deltaTime;
                            }
                            else if (isGrounded) animator.Play("player_runfire");
                            transform.eulerAngles = new Vector2(0,0);
                        }
                        else {
                            if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_dashfire") && fireAnimDelay>0f){
                                fireAnimDelay -= Time.deltaTime;
                            }
                            else {
                                animator.Play("player_dashfire");
                            }
                        }
                }

                else if (Input.GetKey("a") || Input.GetKey("left")) {
                    if (!isDashing || dashRight) {
                        rb2d.velocity = new Vector2(-runspeed,rb2d.velocity.y);
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
                            fireAnimDelay -= Time.deltaTime;
                        }
                        else if (isGrounded) animator.Play("player_runfire");
                        transform.eulerAngles = new Vector2(0,180);
                    }
                    else {
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_dashfire") && fireAnimDelay>0f){
                            fireAnimDelay -= Time.deltaTime;
                        }
                        else {
                            animator.Play("player_dashfire");
                        }
                        }                   
                }

                //checking if standing still
                else {
                    if (!isDashing) {
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_fire") && isGrounded && fireAnimDelay>0f){
                            fireAnimDelay -= Time.deltaTime;
                        }
                        if (isGrounded) animator.Play("player_fire");
                        rb2d.velocity = new Vector2(0,rb2d.velocity.y);
                    }
                    else {
                        if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_dashfire") && fireAnimDelay>0f){
                            fireAnimDelay -= Time.deltaTime;
                        }
                        else {
                            animator.Play("player_dashfire");
                        }
                        }

                }

                fireframe -= Time.deltaTime; //reduces the firing frame delay by 1
        }




        else if (canCancel) {
            //regular run checks


        if (Input.GetKey("d") || Input.GetKey("right")) {
            if (!isDashing || !dashRight) {
                isDashing = false;
                rb2d.velocity = new Vector2(runspeed,rb2d.velocity.y);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
                fireAnimDelay -= Time.deltaTime;
                }
                else if (isGrounded) animator.Play("player_run");
                transform.eulerAngles = new Vector2(0,0);
            }
        }
        else if (Input.GetKey("a") || Input.GetKey("left")) {
            if (!isDashing || dashRight) {
                isDashing = false;
                rb2d.velocity = new Vector2(-runspeed,rb2d.velocity.y);
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_runfire") && isGrounded && fireAnimDelay>0f){
                fireAnimDelay -= Time.deltaTime;
                }
                else if (isGrounded) animator.Play("player_run");
                transform.eulerAngles = new Vector2(0,180);
            }
        }
        else {
            if (!isDashing) {
                if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_fire") && isGrounded && fireAnimDelay>0f){
                    fireAnimDelay -= Time.deltaTime;
                }
                else if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("player_sword") && isGrounded && isAttacking){
                            attackFrame -= Time.deltaTime;
                }
                else if (isGrounded && canCancel) animator.Play("player_idle");
                rb2d.velocity = new Vector2(0,rb2d.velocity.y);
            }
            else if (isAttacking) {
                
            }
            else {
                animator.Play("player_dash");
            }
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

        if(dashTime > 0) dashTime = dashTime - Time.deltaTime;
        else isDashing = false;


        if (fireframe > 0f && canCancel) {

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
        else if (canCancel) {
            if (Input.GetKeyDown("space") && isGrounded || Input.GetKeyDown("x")  && isGrounded || Input.GetKeyDown("up")  && isGrounded) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
            animator.Play("player_jump");
            jumpsound.Play();
        }

            if (Input.GetKeyDown("space") && !doubleJump && !isGrounded && !isAttacking || Input.GetKeyDown("x") && !doubleJump && !isGrounded && !isAttacking || Input.GetKeyDown("up") && !doubleJump && !isGrounded && !isAttacking) {
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

            if ( Input.GetKeyDown("c") && !isDashing && dashTime <= 0) {
                animator.Play("player_dash");
                Instantiate(dashParticle, transform.position, transform.rotation);
                dashTime = startDashTime;
                if (transform.eulerAngles == new Vector3(0,0,0)) {
                    dashRight = true;
                    isDashing = true;
                    rb2d.velocity = new Vector2(dashSpeed,rb2d.velocity.y);

                }
                else {
                    dashRight = false;
                    isDashing = true;
                    rb2d.velocity = new Vector2(-dashSpeed,rb2d.velocity.y);
                }
            }

            if (Input.GetKeyDown("v") && !isAttacking) {
               isAttacking = true;
               Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
               swordSound.Play();
               for (int i = 0; i < enemiesToDamage.Length; i++) {
                   enemiesToDamage[i].GetComponent<EnemyHealthManager>().giveDamage(attackDamage, 0.3f);
               }
               if (isGrounded) {
                   canCancel = false;
                   animator.Play("player_sword");
                   rb2d.velocity = new Vector2(0,0);
               }
               else {
                canCancel = true;
                animator.Play("player_sword_air");
               }
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

    void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

}
