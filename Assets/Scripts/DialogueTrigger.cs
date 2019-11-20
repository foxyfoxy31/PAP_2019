using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public void TriggerDialogue (bool playerleft, string animation, GameObject talkPoint) {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue, playerleft, animation, talkPoint);
    }
}
