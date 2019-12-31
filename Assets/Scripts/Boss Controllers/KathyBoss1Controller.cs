using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KathyBoss1Controller : MonoBehaviour
{   

    public Animator animator;
    public GameObject orbAttack;
    public GameObject firePoint;

    public float orbTimer;

    private float currentOrbTimer;
    public int animationState;

    private BossHealthManager bossHealthManager;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        bossHealthManager = FindObjectOfType<BossHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bossHealthManager.isDead == true) {
            idle();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 1 && !animator.IsInTransition(0)) {
            animationState = Random.Range(1, 4);
            switch(animationState) {
            case 1:
                idle();
            break;

            case 2:
                swoopAttack();
            break;

            case 3:
                fireOrb();
            break;

            default:
                Debug.Log("Something went wrong! Number OO Range!");
            break;

            }
        }
    }


    void idle () {
        animator.Play("kathy_air");
    }

    void swoopAttack () {
        animator.Play("kathy_swoop");
    }

    void fireOrb () {
        if (currentOrbTimer <= 0f) {
            currentOrbTimer = orbTimer;
            animator.Play("kathy_fire");
            Instantiate(orbAttack, firePoint.transform.position, firePoint.transform.rotation);
        }
        else {
            while (currentOrbTimer > 0f) {
                currentOrbTimer = currentOrbTimer - Time.deltaTime;
            }
        }
    }

}