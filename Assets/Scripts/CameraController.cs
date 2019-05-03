﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public PlayerController2D player;

    public bool isFollowing;

    public float xOffset;

    public float yOffset;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController2D>();

        isFollowing = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFollowing) {
            transform.position = new Vector3(player.transform.position.x + xOffset, player.transform.position.y + yOffset, transform.position.z);
        }
    }
}
