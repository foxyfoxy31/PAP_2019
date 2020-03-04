using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoBackToStartMenu : MonoBehaviour
{
    public void GoToStartMenu() {
        Debug.Log("Going back!");
        SceneManager.LoadScene("MainMenu");
    }
}
