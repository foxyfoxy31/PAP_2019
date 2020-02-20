using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SubmitMenu : MonoBehaviour
{
    
    public InputField playerNameField;
    public TextMeshProUGUI timerText;

    public TimeStorage timeStorage;

    public float levelTimer;
    public Button submitButton;


    private void Start() {
        timeStorage = FindObjectOfType<TimeStorage>();
        levelTimer = timeStorage.getLevelTimer();
        string minutes = ((int) levelTimer / 60).ToString();
        string seconds = (levelTimer % 60).ToString("f0");

        timerText.text = minutes + " minutes and " + seconds + " seconds";
    }

    public void CallRegister() {
        StartCoroutine(Register());
    }


    IEnumerator Register () {
        WWWForm form = new WWWForm();
        form.AddField("playerName", playerNameField.text);
        form.AddField("levelTime", levelTimer.ToString());
        WWW www = new WWW("http://localhost/sqlconnect/register.php", form);
        yield return www;
        if (www.text == "0") {
            Debug.Log("Player succesfully registered in the leaderboard.");
        }
        else {
            Debug.Log("Player registration failed. Error #" + www.text);
        }
    }

    public void VerifyInputs() {
        submitButton.interactable = (playerNameField.text.Length >= 3);
    }


}
