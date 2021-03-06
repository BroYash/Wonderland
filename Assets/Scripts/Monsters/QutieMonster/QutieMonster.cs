﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QutieMonster : MonoBehaviour
{
    //Integers
    public int curHealth = 100;
    public int maxHealth = 100;
    int nextMove;

    //Prefab for items
    public GameObject[] prefab;

    //Define
    private Rigidbody2D rigid;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private CapsuleCollider2D capsuleCollider;

    //Player reference
    private Player player;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        rigid = GetComponent<Rigidbody2D>();
        Invoke("Think", 5);
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        curHealth = maxHealth;
    }
    void Update()
    {
        if (curHealth <= 0)
        {
            int probability;

            probability = Random.Range(0, 5);

            if (probability == 3)
            {
                int getRandPrefab = Random.Range(0, prefab.Length-1);
                Instantiate(prefab[getRandPrefab], new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            }
            Destroy(gameObject);
            AudioManager.instance.PlaySFX(6);
            
        }
    }

    //Automatically, executed by itself per second 50~60times
    //Physics bases stuffs in the FixedUpdate()
    private void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Detect of the platform
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.5f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));

        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("PlatForm"));

        //Detect the monster is almost in front of the collased part from the ground
        if (rayHit.collider == null)
        {
            Debug.Log("Oh there is wall!!");
             Turn();
        }
    }

    //Recursive Function
    private void Think()
    {
        //Sets next active
        nextMove = Random.Range(-1, 2);

        //Sprite animation
        animator.SetInteger("WalkSpeed", nextMove);

        //Flip sprite
        if (nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }

        ////Recursive functionality
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);

    }

    //Turn
    private void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke();
        Invoke("Think", 2);
    }

    //If the Qutie Monster getDamaged
    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("RedFlash_Player");
    }

    //When the player get damaged
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            player.Damage(2);
             StartCoroutine(player.Knockback(0.02f, 20, player.transform.position));
        }
    }
}
