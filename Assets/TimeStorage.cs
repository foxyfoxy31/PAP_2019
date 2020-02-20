using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStorage : MonoBehaviour
{
    static public float levelTimer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLevelTimer(float value) {
        levelTimer = value;
    }

    public float getLevelTimer() {
        return levelTimer;
    }
}
