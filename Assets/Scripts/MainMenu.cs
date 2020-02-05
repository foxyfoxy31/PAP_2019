using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Start() {
        Application.targetFrameRate = 60;
    }

    public void PlayGame() {
        SceneManager.LoadScene("Level1");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
