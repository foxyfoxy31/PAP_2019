using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyFinishedParticle : MonoBehaviour
{
    private ParticleSystem myPS;
    // Start is called before the first frame update
    void Start()
    {
        myPS = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myPS.isPlaying) {
            return;
        }
        Destroy (gameObject);
    }

    void OnBecameInvisible ()
    {
        Destroy(gameObject);
    }

}
