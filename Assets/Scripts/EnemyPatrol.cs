using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{

    public float moveSpeed;

    public bool moveRight;

    public Rigidbody2D rb2d;

    bool hitWall;

        [SerializeField]
        Transform wallCheck;



    private bool notAtEdge;

    public Transform edgeCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        hitWall = Physics2D.Linecast(transform.position, wallCheck.position, 1 << LayerMask.NameToLayer("Ground"));
        
        notAtEdge = Physics2D.Linecast(transform.position, edgeCheck.position, 1 << LayerMask.NameToLayer("Ground"));

        if (hitWall || !notAtEdge) {
            moveRight = !moveRight;
        }

        if (moveRight) {
            transform.localScale = new Vector3 (-1f, 1f, 1f);
            rb2d.velocity = new Vector2 (moveSpeed, rb2d.velocity.y);
        } else {
            rb2d.velocity = new Vector2 (-moveSpeed, rb2d.velocity.y);
            transform.localScale = new Vector3 (1f, 1f, 1f);
        }
    }
}
