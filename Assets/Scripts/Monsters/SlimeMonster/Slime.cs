﻿
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private Collider2D coll;
    private Rigidbody2D rb;
    private Animator anim;
    public int curHealth = 100;


    [SerializeField] public LayerMask ground;
    private Player player;


    [SerializeField] public float leftCap;
    [SerializeField] public float rightCap;

    [SerializeField] public float jumpLength;
    [SerializeField] public float jumpHeight;

    public GameObject[] prefab;

    private bool facingRight = true;

    private void Start()
    {

        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

    }


    private void Update()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < .1f)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (coll.IsTouchingLayers(ground) && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
        checkHealth();
    }

    private void Move()
    {
        if (facingRight)
        {
            if (transform.position.x <= rightCap)
            {
                if (transform.localScale.x == -1)
                {
                    transform.localScale = new Vector3(1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingRight = false;

            }


        }
        else
        {
     
            if (transform.position.x >= leftCap)
            {
                if (transform.localScale.x == 1)
                {
                    transform.localScale = new Vector3(-1, 1);
                }
                if (coll.IsTouchingLayers(ground))
                {
                    rb.velocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
            }
            else
            {
                facingRight = true;

            }

        }
    }
    private void checkHealth()
    {
        if (curHealth <= 0)
        {
            if (player.currentExp == player.maxExp)
            {
                player.currentExp = 0;
                player.character_LV += 1;

            }
            else
            {
                player.currentExp += 10;

            }
            int probability;

            probability = Random.RandomRange(0, 5);

            if (probability == 3)
            {
                int getRandPrefab = Random.RandomRange(0, prefab.Length-1);
                Instantiate(prefab[getRandPrefab], new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            }
            AudioManager.instance.PlaySFX(6);
            Destroy(gameObject);

        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player.Damage(2);
            player.gameObject.GetComponent<Animation>().Play("RedFlash_Player");
            player.Knockback(2, 20, player.transform.transform.position);

        }

    }
    public void Damage(int damage)
    {
        curHealth -= damage;
        gameObject.GetComponent<Animation>().Play("RedFlash_Player");
    }
}

