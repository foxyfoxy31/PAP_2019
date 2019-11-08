using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int enemyHealth;

    public EnemyPatrol enemyP;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        enemyP = GetComponent<EnemyPatrol>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth<=0) {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void giveDamage(int damageToGive) {
        GetComponent<AudioSource>().Play();
        enemyHealth -= damageToGive;
    }
}
