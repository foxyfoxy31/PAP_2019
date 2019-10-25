using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{
    public int maxPlayerHealth; 

    public static int playerHealth;

    Text text;

    private LevelManager levelManager;

    public bool isDead;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0  && !isDead) {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            isDead = true;
        }

        text.text = "" + playerHealth;

        if (maxPlayerHealth < playerHealth) {
            playerHealth = maxPlayerHealth;
        }
    }

    public static void HurtPlayer (int damageToGive) {
        playerHealth = playerHealth - damageToGive;
    }

    public void FullHealth() {
        playerHealth = maxPlayerHealth;
    }

}
