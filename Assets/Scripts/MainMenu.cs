using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private Animator animator;


    private void Start() {
        animator = GetComponent<Animator>();
        Application.targetFrameRate = 60;
        animator.SetBool("isOpen", true);
    }

    public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
