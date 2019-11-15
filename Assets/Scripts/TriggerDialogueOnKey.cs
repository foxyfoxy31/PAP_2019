using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnKey : MonoBehaviour
{
    private bool dialogueTriggered = false;

    public bool playerleft;

    public string animation;
    private bool CR_running = false;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name == "lucas") {
            if (!CR_running) {
                StartCoroutine(dialogueKey());
            }
        }
    }

    IEnumerator dialogueKey() {
        CR_running = true;
        if(Input.GetKeyDown("m") && !dialogueTriggered) {
            gameObject.GetComponent<DialogueTrigger>().TriggerDialogue(playerleft, animation);
            dialogueTriggered = true;
            yield return new WaitForSeconds(0.5f);
        }
        else if (Input.GetKeyDown("m") && dialogueTriggered) {
            FindObjectOfType<DialogueManager>().DisplayNextSentence();
            yield return new WaitForSeconds(0.5f);
        }
        else if (FindObjectOfType<PlayerController2D>().enabled == true) {
            dialogueTriggered = false;
        }
        CR_running = false;
    }
}
