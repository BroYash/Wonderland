﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellFollow : MonoBehaviour
{
    /*
    private Rigidbody2D spellRB;
    [SerializeField] private float speed;
    private Transform target;

    // Start is called before the first frame update
    void Start()
    {
        spellRB = GetComponent<Rigidbody2D>();

        //JUST FINDING TARGET
        target = GameObject.Find("QutieMonster").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;
        spellRB.velocity = direction.normalized * speed;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }
    */
}