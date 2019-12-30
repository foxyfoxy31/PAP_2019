using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{

    public AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void setSong (AudioClip song) {
        audioSource.clip = song;
    }

    public void setPitch (float pitch) {
        audioSource.pitch = pitch;
    }

    public void play () {
        audioSource.Play();
    }

    public void stop () {
        audioSource.Stop();
    }

    public void pause () {
        audioSource.Pause();
    }
}
