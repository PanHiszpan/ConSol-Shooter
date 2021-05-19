using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletEnemy : MonoBehaviour
{
    public float bulletSpeedBoost;
    public float speed;
    public Rigidbody2D RB;
    public int damage;
    public float lifetime;
    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            RB.velocity = transform.right * (speed + bulletSpeedBoost); //GameObject.Find("Player").GetComponent<Player>().moveSpeed); //jesli sie porusza to do prędkości pociskow dodajemy prędkośc gracza
        }
        else
        {
            RB.velocity = transform.right * speed;
        }
        Invoke("Destroy", lifetime);
    }

    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Player player = hitinfo.GetComponent<Player>();
        
        if (player != null)
        {
            player.TakeDamage(damage);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);  //Impact animation
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
