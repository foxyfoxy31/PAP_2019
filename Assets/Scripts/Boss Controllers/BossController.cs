using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossController : MonoBehaviour
{
    public Animator animator;

    public int animationState;

    public BossHealthManager bossHealthManager;

    public bool active = false;

    // Start is called before the first frame update
    public virtual void Start()
    {
        animator = GetComponent<Animator>();
        bossHealthManager = FindObjectOfType<BossHealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Trigger() {
        active = true;
    }
}
