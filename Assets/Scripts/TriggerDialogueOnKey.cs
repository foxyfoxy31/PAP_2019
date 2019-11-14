using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDialogueOnKey : MonoBehaviour
{
    private bool dialogueTriggered = false;

    private void OnTriggerStay2D(Collider2D other) {
        if (other.gameObject.name == "lucas") {
            if(Input.GetKeyDown("m") && !dialogueTriggered) {
                gameObject.GetComponent<DialogueTrigger>().TriggerDialogue();
                dialogueTriggered = true;
            }
            else if (Input.GetKeyUp("m")) {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }
            else if (FindObjectOfType<PlayerController2D>().enabled == true) {
                dialogueTriggered = false;
            }
        }
    }
}
