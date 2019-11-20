using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;
    private Queue<string> sentences; 
    private PlayerController2D player;
    void Start()
    {
        sentences = new Queue<string>();
        player = FindObjectOfType<PlayerController2D>();
    }

    public void StartDialogue (Dialogue dialogue, bool playerleft, string animation, GameObject talkPoint) {

        player.rb2d.velocity = new Vector2 (0, 0);

        player.animator.Play("player_run");

        while (player.transform.position.x != talkPoint.transform.position.x) {
        if (player.transform.position.x < talkPoint.transform.position.x) {
            transform.eulerAngles = new Vector2(0,0);
        }
        else {
            transform.eulerAngles = new Vector2(0,180);
        }

        player.transform.position = Vector2.MoveTowards(player.transform.position, talkPoint.transform.position, (0.3f*Time.deltaTime));

        }

        if (playerleft) {
            player.transform.eulerAngles = new Vector2(0,180);
        }
        else {
            player.transform.eulerAngles = new Vector2(0,0);
        }

        player.animator.Play(animation);

        player.rb2d.velocity = new Vector2 (0, 0);

        player.lockControls = true;
        

        animator.SetBool("isOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence () {
        if (sentences.Count == 0) {
            EndDialogue();
            return;
        }

        string  sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue() {
        animator.SetBool("isOpen", false);
        player.enabled = true; 
    }
}
