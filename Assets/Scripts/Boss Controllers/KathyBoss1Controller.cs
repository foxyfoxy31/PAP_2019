using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KathyBoss1Controller : BossController
{   
    public GameObject orbAttack;
    public GameObject firePoint;

    public float orbTimer;

    public string animation;

    public GameObject talkPoint;

    private float currentOrbTimer;

    private DialogueTrigger dialogueTrigger;

    private bool triggeredDialogue = false;

    private DialogueManager dialogueManager;


    public override void Start()
    {
        animator = GetComponent<Animator>();
        bossHealthManager = FindObjectOfType<BossHealthManager>();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if (active) {

            if (triggeredDialogue) {
                
                bossHealthManager.enabled = true;
                if (bossHealthManager.isDead == true) {
                    idle();
                    active = false;
                    SceneManager.LoadScene("DatabaseMenu");
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
        }
        else {
            idle();
            bossHealthManager.enabled = false;
        }
    }

    public override void Trigger() {
        active = true;
        StartCoroutine("dialogue");
        idle();
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

    IEnumerator dialogue() {
        dialogueTrigger.TriggerDialogue(false, animation, talkPoint);
        yield return new WaitForSeconds(0.5f);
        while (dialogueManager.canPlayerMove() == false) {
            if (Input.GetKeyDown("m")) {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
                yield return new WaitForSeconds(0.5f);
            }
            yield return 0;
        }
        triggeredDialogue = true;
    }



}