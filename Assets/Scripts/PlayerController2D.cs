using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    public Animator animator;
    public Rigidbody2D rb2d;
    SpriteRenderer spriteRenderer;
    [SerializeField]
    private float runspeed = 1.5f;
    private int fireframe = 0;
    public Transform firePoint;
    public GameObject bullet;

    [SerializeField]    private float jumpspeed = 4f;
    bool isGrounded;

        [SerializeField]
        Transform groundCheck;

        [SerializeField]
        Transform groundCheckL;

        [SerializeField]
        Transform groundCheckR;
    // Start is called before the first frame update
    void Start()
    {
        
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    private void FixedUpdate() {


        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground")) || 
        Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else {
            isGrounded = false;
            if (fireframe != 0) animator.Play("player_jumpfire");
            else animator.Play("player_jump");
        }

        if (fireframe != 0) {
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
                    else {
                        if (isGrounded) animator.Play("player_fire");
                        rb2d.velocity = new Vector2(0,rb2d.velocity.y);
                    }
                    if (Input.GetKey("space") && isGrounded || Input.GetKey("x")  && isGrounded) {
                        rb2d.velocity = new Vector2(rb2d.velocity.x, jumpspeed);
                        animator.Play("player_jumpfire");
                    }
                    fireframe--;
                    Debug.Log(fireframe);
        }
        else {
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
        if (Input.GetKeyDown("z")){
            fireframe = 15;
            Instantiate(bullet, firePoint.position, firePoint.rotation);
        }
        }
    }
}
