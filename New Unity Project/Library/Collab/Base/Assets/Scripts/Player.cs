using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public HeartSystem heartSystem;

    public float moveSpeed;
    public float jumpHeight;
    public int startHealth = 80;
    private int health;
    //public int damageFromEnemy;

    public Transform ceilingCheck;
    private bool ceiling;
    public CapsuleCollider2D stand;
    public CapsuleCollider2D crouch;
    public bool crouching;

    public Transform groundCheck;                                                                                       //Transform - rotacja, pozycja
    public float groundCheckRadius;
    public LayerMask WhatIsGround;
    private bool OnTheGround;                                                                                           //określenie, czy postać jest na ziemi, czy w powietrzu
    private bool doubleJump;
    private bool facingRight;

    private Animator anim;

    private const float minimumHeldDuration = 0.05f;
    private float pressTime = 0;
    private bool buttonHeld = false;

    //public LayerMask whatIsLadder;
    //private float gravityValue;
    //private bool isClimbing;
    //float distance;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        heartSystem = GetComponent<HeartSystem>();
        facingRight = true;                                                                                             //na poczatku postać skierowana w prawo, więc true na start
        health = startHealth;
        stand.enabled = true;
        crouch.enabled = false;
        //gravityValue = GetComponent<Rigidbody2D>().gravityScale;
    }

    // Update is called once per frame
    void Update()
    {

        OnTheGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, WhatIsGround);                   //zablokowanie możliwości wielokrotnego skoku
        ceiling = Physics2D.OverlapCircle(ceilingCheck.position, groundCheckRadius, WhatIsGround);
        if (Input.GetKeyDown(KeyCode.Space) && OnTheGround)
        {                                                           //GetKey - przytrzymanie, GetKeyDown - wciśnięcie   /poruszanie się
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);                                          //Vector2 - wektor dwuwymiarowy (x,y)
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            pressTime = Time.timeSinceLevelLoad;
            buttonHeld = false;
        }
        else if (Input.GetKeyUp(KeyCode.RightArrow))
        {                                                                   //gracz jedynie wcisnął przycisk
            if (!buttonHeld && facingRight == false)                                                                     //jak klikniesz w prawo, ale postać patrzy w lewo, to obraca
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {                                                                           //gracz przytrzymał przycisk ponad 0,05s
            if (Time.timeSinceLevelLoad - pressTime > minimumHeldDuration)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                buttonHeld = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            pressTime = Time.timeSinceLevelLoad;
            buttonHeld = false;
        }
        else if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            if (!buttonHeld && facingRight == true)                                                                      //jak klikniesz w lewo, ale postać patrzy w prawo, to obraca
            {
                Flip();
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (Time.timeSinceLevelLoad - pressTime > minimumHeldDuration)
            {
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
                buttonHeld = true;
            }
        }
        if (Input.GetButton("Crouch") && OnTheGround)
        {
            crouching = true;
            stand.enabled = false;
            crouch.enabled = true;
        }
        else
        {
            crouching = false;
            stand.enabled = true;
            crouch.enabled = false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////		        //podwójny skok
        if (OnTheGround)
        {
            doubleJump = false;
        }
        anim.SetBool("OnTheGround", OnTheGround);

        if (Input.GetKeyDown(KeyCode.Space) && !OnTheGround && !doubleJump)
        {                                               //GetKey - przytrzymanie, GetKeyDown - wciśnięcie
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, jumpHeight);
            doubleJump = true;
        }
        /////////////////////////////////////////////////////////////////////////////////////////////////
        anim.SetFloat("Speed", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));                                      //ilość liczb między 0 i konkretną liczbą

        // If the input is moving the player right and the player is facing left
        if (GetComponent<Rigidbody2D>().velocity.x > 0 && !facingRight)
        {									            //obrót postaci podczas animacji
            Flip();
        }
        // If the input is moving the player left and the player is facing right
        else if (GetComponent<Rigidbody2D>().velocity.x < 0 && facingRight)
        {
            Flip();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////               //wchodzenie po drabinie
        /*RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            isClimbing = true;
            if (Input.GetKey(KeyCode.UpArrow))
            {
                anim.SetBool("OnTheLadder", isClimbing);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, moveSpeed);
                GetComponent<Rigidbody2D>().gravityScale = 0;
            }    
            if (Input.GetKey(KeyCode.DownArrow) && !OnTheGround)
            {
                anim.SetBool("OnTheLadder", isClimbing);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, -moveSpeed);
                GetComponent<Rigidbody2D>().gravityScale = 0;
            }
            if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("OnTheLadder", false);
                GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                GetComponent<Rigidbody2D>().gravityScale = 0;
            }
        }
        else
        {
            isClimbing = false;
            GetComponent<Rigidbody2D>().gravityScale = gravityValue;
        }*/
        /////////////////////////////////////////////////////////////////////////////////////////////////

        ///\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\/\                  -   KONIEC UPDATE
    }

    private void Flip()                                                                                                 //obraca postać razem ze wszystkim co jest do niej podłączone i odwraca wartość facingRight (wiemy, w którym kierunku "patrzy postać")
    {
        facingRight = !facingRight;
        transform.Rotate(0f, -180f, 0f);
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////                   //kolizja z przeciwnikiem
    /*private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        //Debug.Log(hitinfo.name);  // wyswietla w konsoli z czym się zderzyl
        Enemy enemy = hitinfo.GetComponent<Enemy>();
        if (enemy != null)
        //if (hitinfo.CompareTag("Enemy"))  //zamiast 2 linijek wyżej, szuka zderzenia z obiektem z tagiem Enemy
        {
            TakeDamage(damageFromEnemy);
        }
    }
    */
    /////////////////////////////////////////////////////////////////////////////////////////////////
    public void TakeDamage(int damage)
    {
        health -= damage;
        Debug.Log(health);
        heartSystem.TakeDamage(damage);

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //SceneManager.LoadScene(2);
    }
}
