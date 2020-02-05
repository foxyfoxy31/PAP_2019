using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnKey : MonoBehaviour
{
    private bool dialogueTriggered = false;

    public bool playerleft;

    public string animation;
    private bool CR_running = false;

    public GameObject talkPoint;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name == "lucas") {
            if (!CR_running) {
                StartCoroutine(dialogueKey());
            }
        }
    }

    IEnumerator dialogueKey() {
        CR_running = true;
        if(Input.GetKey("m") && !dialogueTriggered) {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(playerleft, animation, talkPoint);
            dialogueTriggered = true;
            yield return new WaitForSeconds(0.7f);
        }
        else if (Input.GetKey("m") && dialogueTriggered) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            yield return new WaitForSeconds(0.7f);
        }
        else if (FindObjectOfType<PlayerController2D>().lockControls == false) {
            dialogueTriggered = false;
        }
        CR_running = false;
    }
}
