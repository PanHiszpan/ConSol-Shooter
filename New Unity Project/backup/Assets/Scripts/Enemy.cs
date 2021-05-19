using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startHealth;
    private float health;
    public float moveSpeed;
    private bool facingRight;
    public bool moving;

    public Transform wallCheck;                                               
    public float wallCheckRadius;
    public LayerMask WhatIsWall;
    private bool hittingTheWall;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    private bool OnTheGround;

    public Image healthBar;

    public GameObject drop;
    public GameObject drop2;

    public void Start()
    {
        health = startHealth;
        moving = true;
    }

    //public GameObject deathEffect;  //animacja smierci do zrobienia
    private void Update()
    {

        hittingTheWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, WhatIsWall);
        OnTheGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);
        if (hittingTheWall || !OnTheGround)
        {
            Flip();
            moveSpeed = -moveSpeed;
        }
        if (moving)
        {
            GetComponent<Animator>().enabled = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            
            GetComponent<Animator>().enabled = false;
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        
    }

    private void Flip()  //obraca postać razem ze wszystkim co jest do niej podłączone i odwraca wartość facingRight (wiemy, w którym kierunku "patrzy" postać)
    {
        facingRight = !facingRight;
        transform.Rotate(0f, -180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        healthBar.fillAmount = health / startHealth;

        if(health <= 0)
        {
            Die();
        }
    }
    
    void Die()
    {
        float randomValue = Random.Range(1, 100);
        if(randomValue <= 50)
        {
            Instantiate(drop, transform.position, drop.transform.rotation);
        }
        else if(randomValue > 50 && randomValue <= 75)
        {
            Instantiate(drop2, transform.position, drop2.transform.rotation);
        }
        //Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);  
    }
}
