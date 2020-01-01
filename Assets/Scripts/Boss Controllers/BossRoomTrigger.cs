using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRoomTrigger : MonoBehaviour
{
    [SerializeField]
    private BossController bossController;
    private bool expired = false;


    void Start()
    {
        bossController = FindObjectOfType<BossController>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!expired) {
            if (other.gameObject.name == "lucas") {
               bossController.Trigger();
               expired = true;
            }
        }
    }


}
