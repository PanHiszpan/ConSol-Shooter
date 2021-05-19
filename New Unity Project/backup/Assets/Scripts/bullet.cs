using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Shop shop;
    public float bulletSpeedBoost;
    public float speed;
    public Rigidbody2D rb;
    public int damage;
    public float lifetime;
    //public GameObject impactEffect;

    // Start is called before the first frame update
    void Start()
    {
        shop = FindObjectOfType<Shop>().GetComponent<Shop>();
        damage = shop.DMG;
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = transform.right * (speed + bulletSpeedBoost); //GameObject.Find("Player").GetComponent<Player>().moveSpeed); //jesli sie porusza to do prędkości pociskow dodajemy prędkośc gracza
        }
        else
        {
            rb.velocity = transform.right * speed; 
        }
        Invoke("Destroy", lifetime);
    }

    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Enemy enemy = hitinfo.GetComponent<Enemy>();
        if(enemy != null)
        {
            enemy.TakeDamage(damage);
        }
        //Instantiate(impactEffect, transform.position, transform.rotation);  //Impact animation
        Destroy(gameObject);
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

}
