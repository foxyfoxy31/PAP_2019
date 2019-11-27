using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public AudioSource lettersound;

    public Image avatarFace;

    public GameObject teleportParticle;
    public Animator animator;
    private Queue<string> sentences; 
    private PlayerController2D player;
    
    void Start()
    {
        sentences = new Queue<string>();
        player = FindObjectOfType<PlayerController2D>();
    }

    public void StartDialogue (Dialogue dialogue, bool playerleft, string animation, GameObject talkPoint) {

        if (player.transform.position != talkPoint.transform.position) {
            player.transform.position = talkPoint.transform.position;
            Instantiate(teleportParticle, player.transform.position, player.transform.rotation);
        }        


        if (playerleft) {
            player.transform.eulerAngles = new Vector2(0,180);
        }
        else {
            player.transform.eulerAngles = new Vector2(0,0);
        }


        player.animator.Play(animation);

        player.lockControls = true;

        player.lockPosition = true;
        

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        avatarFace.sprite = dialogue.npcAvatar;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            player.lockControls = false;
            player.lockPosition = false;
            return;
        }

        string  sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        int i = 0;
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            if (i==0) {
                i++;
                lettersound.Play();
            }
            else if (i==2) {
                i=0;
            }
            else i++;
            yield return null;
        }
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        player.enabled = true; 
    }
}
