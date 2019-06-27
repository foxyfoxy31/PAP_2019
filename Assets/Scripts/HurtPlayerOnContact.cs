using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour
{

    public int damageToGive;

    private PlayerController2D player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D (Collider2D other) {
        if (other.name == "lucas") {
            var player = other.GetComponent<PlayerController2D>();
            player.knockbackCount = player.knockbackLength;
            HealthManager.HurtPlayer(damageToGive);
            other.GetComponent<AudioSource>().Play();
            player.animator.Play("player_hit");

            if(other.transform.position.x < transform.position.x) {
                player.knockFromRight = true;
            }
            else {
                player.knockFromRight = false;
            }
        }
    }    

}
