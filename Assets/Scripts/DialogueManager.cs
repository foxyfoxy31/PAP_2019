using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public AudioSource letterSound;

    public Image avatarFace;

    public GameObject teleportParticle;
    public Animator animator;
    private Queue<string> sentences;
    private Queue<string> names;
    private Queue<Sprite> avatars;
    private Queue<AudioSource> letterSounds; 
    private PlayerController2D player;
    
    void Start()
    {
        sentences = new Queue<string>();
        names = new Queue<string>();
        avatars = new Queue<Sprite>();
        letterSounds = new Queue<AudioSource>();
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

        sentences.Clear();

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence);
        }

        foreach (Sprite avatar in dialogue.npcAvatar) {
            avatars.Enqueue(avatar);
        }

        foreach (string npcName in dialogue.name) {
            names.Enqueue(npcName);
        }

        foreach (AudioSource ltrSnd in dialogue.letterSound) {
            letterSounds.Enqueue(ltrSnd);
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
        Sprite avatar = avatars.Dequeue();
        string npcName = names.Dequeue();
        AudioSource ltrSnd = letterSounds.Dequeue();
        StopAllCoroutines();
        avatarFace.sprite = avatar;
        nameText.text = npcName;
        if (letterSound != null) {
            letterSound = ltrSnd;
        }
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence) {
        dialogueText.text = "";
        int i = 0;
        foreach (char letter in sentence.ToCharArray()) {
            dialogueText.text += letter;
            if (i==0) {
                i++;
                letterSound.Play();
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

    public bool canPlayerMove() {
        if (player.lockControls == false || player.lockPosition == false) {
            return true;
        }
        else {
            return false;
        }
    }

}
