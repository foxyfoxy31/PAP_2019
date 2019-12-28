using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletcontroller : MonoBehaviour
{

    public float speed;

    public PlayerController2D player;

    public GameObject enemyDeathEffect;

    public GameObject impactParticle;

    public int damageToGive;

    // Start is called before the first frame update
    void Start(){
        player = FindObjectOfType<PlayerController2D>();
        if(player.transform.localRotation.y < 0) {
          speed = -speed;
        }
    }

    // Update is called once per frame
    void Update(){
        GetComponent<Rigidbody2D>().velocity = new Vector2 (speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Enemy"){
            //Instantiate(enemyDeathEffect, other.transform.position, other.transform.rotation);
            //Destroy(other.gameObject);

            other.GetComponent<EnemyHealthManager>().giveDamage(damageToGive, 0.05f);
            
        }
        Instantiate(impactParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
