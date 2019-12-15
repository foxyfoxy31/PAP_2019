using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnEnter : MonoBehaviour
{
    private bool dialogueTriggered = false;

    public bool playerleft;

    public string animation;
    private bool CR_running = false;

    public GameObject talkPoint;

    private bool expired;
    private void OnTriggerStay2D(Collider2D other) {
        if (!expired) {
            if (other.gameObject.name == "lucas") {
                if (!CR_running) {
                    StartCoroutine(dialogue());
                }
            }
        }
    }

    IEnumerator dialogue() {
        CR_running = true;
        if(!dialogueTriggered) {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(playerleft, animation, talkPoint);
            dialogueTriggered = true;
            yield return new WaitForSeconds(0.5f);
        }
        else if (Input.GetKeyDown("m") && dialogueTriggered) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            yield return new WaitForSeconds(0.5f);
        }
        else if (FindObjectOfType<PlayerController2D>().lockControls == false) {
            dialogueTriggered = false;
            expired = true;
        }
        CR_running = false;
    }
}
