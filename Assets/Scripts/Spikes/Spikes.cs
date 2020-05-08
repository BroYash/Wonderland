﻿using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    private Player player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();    
    }
    
     void OnTriggerEnter2D(Collider2D col) // When triggered
    {
        if (col.CompareTag("Player"))
        {
            player.Damage(1);

            //Start coroutine
            StartCoroutine(player.Knockback(0.02f,20, player.transform.position));
        }
    }
}