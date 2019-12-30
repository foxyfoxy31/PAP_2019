using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSong : MonoBehaviour
{

    public MusicManager musicManager;

    public AudioClip song;

    public float pitch = 1;
    // Start is called before the first frame update
    void Start()
    {
        musicManager = FindObjectOfType<MusicManager>();
    }

    public void Change() {
        musicManager.stop();
        musicManager.setSong(song);
        musicManager.setPitch(pitch);
        musicManager.play();
    }

}
