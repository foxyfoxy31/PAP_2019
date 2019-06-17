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

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerHealth <= 0) {
            levelManager.RespawnPlayer();
        }

        text.text = "" + playerHealth;
    }

    public static void HurtPlayer (int damageToGive) {
        playerHealth -= damageToGive;
    }

    public void FullHealth() {
        playerHealth = maxPlayerHealth;
    }

}
