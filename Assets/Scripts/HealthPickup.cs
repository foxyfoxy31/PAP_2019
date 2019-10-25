using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToGive;

    public GameObject healthPickupParticle;

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
            HealthManager.HurtPlayer(-healthToGive);
            Instantiate(healthPickupParticle, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }    

}
