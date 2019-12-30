using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSongOnTrigger : MonoBehaviour
{
    public ChangeSong songChanger;

    private bool expired = false;


    void Start()
    {
        songChanger = GetComponent<ChangeSong>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!expired) {
            if (other.gameObject.name == "lucas") {
               songChanger.Change();
               expired = true;
            }
        }
    }
}
