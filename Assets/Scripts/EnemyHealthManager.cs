using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    public int enemyHealth;

    public Rigidbody2D rb2d;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth<=0) {
            Instantiate(deathEffect, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }

    public void giveDamage(int damageToGive, float stunTime) {
        GetComponent<AudioSource>().Play();
        enemyHealth -= damageToGive;
        StartCoroutine(stunEnemy(stunTime));
    }


    IEnumerator stunEnemy (float stunTime) {
        rb2d.constraints = RigidbodyConstraints2D.FreezeAll;
        yield return new WaitForSeconds(stunTime);
        rb2d.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
}
