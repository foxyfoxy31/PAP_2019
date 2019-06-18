using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    public Animator animator; //defining animator
    public Rigidbody2D rb2d; // rigidbody
    SpriteRenderer spriteRenderer;
    [SerializeField]  //makes the variable appear as changeable in the editor
    private float runspeed = 1.5f;
    private int fireframe = 0;
    public Transform firePoint;
    public GameObject bullet;

    [SerializeField]    private float jumpspeed = 4f;
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
    }


    private void FixedUpdate() {


        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true; //checking for the ground
        }
        else {
            isGrounded = false;
            if (fireframe != 0) animator.Play("player_jumpfire"); //check if player is airborne and is firing projectile
            else animator.Play("player_jump"); //regular jump animation
        }

        if (fireframe != 0) {
                //checking if +layer is running and firing @ the same time
                 if (Input.GetKey("d") || Input.GetKey("right")) {
                        rb2d.velocity = new Vector2(runspeed,rb2d.velocity.y);
                        if (isGrounded) animator.Play("player_runfire");
                        transform.eulerAngles = new Vector2(0,0);
                    }
                    else if (Input.GetKey("a") || Input.GetKey("left")) {
                        rb2d.velocity = new Vector2(-runspeed,rb2d.velocity.y);
                        if (isGrounded) animator.Play("player_runfire");
                        transform.eulerAngles = new Vector2(0,180);
                    }
                    //checking if standing still
                    else {
                        if (isGrounded) animator.Play("player_fire");
                        rb2d.velocity = new Vector2(0,rb2d.velocity.y);
                    }
                    if (Input.GetKey("space") && isGrounded || Input.GetKey("x")  && isGrounded) {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
                        animator.Play("player_jumpfire");
                    }
                    fireframe--; //reduces the firing frame delay by 1
        }
        else {
            //regular run checks
        if (Input.GetKey("d") || Input.GetKey("right")) {
            rb2d.velocity = new Vector2(runspeed,rb2d.velocity.y);
            if (isGrounded) animator.Play("player_run");
            transform.eulerAngles = new Vector2(0,0);
        }
        else if (Input.GetKey("a") || Input.GetKey("left")) {
            rb2d.velocity = new Vector2(-runspeed,rb2d.velocity.y);
            if (isGrounded) animator.Play("player_run");
            transform.eulerAngles = new Vector2(0,180);
        }
        else {
            if (isGrounded) animator.Play("player_idle");
            rb2d.velocity = new Vector2(0,rb2d.velocity.y);
        }
        if (Input.GetKey("space") && isGrounded || Input.GetKey("x")  && isGrounded) {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
            animator.Play("player_jump");
        }
        //firing projectile script
        if (Input.GetKeyDown("z")){
            fireframe = 10; //frame delay for animation
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        }
    }
}
