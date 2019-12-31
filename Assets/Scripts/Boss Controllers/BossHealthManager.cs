using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthManager : MonoBehaviour
{
    public int maxBossHealth; 

    public static int bossHealth;

    Text text;

    public bool isDead;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        bossHealth = maxBossHealth;

        isDead = false;

        animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {



        if(bossHealth <= 0  && !isDead) {
            bossHealth = 0;
            isDead = true;
            animator.SetBool("isOpen", false);
        }
        else if (!isDead) {
            animator.SetBool("isOpen", true);
        }
        text.text = "" + bossHealth;

        if (maxBossHealth < bossHealth) {
            bossHealth = maxBossHealth;
        }
    }

    public void giveDamage(int damageToGive) {
        GetComponent<AudioSource>().Play();
        bossHealth -= damageToGive;
    }
}
