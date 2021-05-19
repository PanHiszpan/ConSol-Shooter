using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Transform firePointUp;
    public Transform firePoint;
    public Transform firePointDown;
    public Transform firePointCrouching;
    public GameObject bulletPrefab;
    public float shootTime;
    private bool crouching;
    private bool shooting;

    private Animator anim;
    private bool shootsDown = false;
    private bool shootsUp = false;

    // Update is called once per frame
    void Update()
    {
        anim = GetComponent<Animator>();
        crouching = GetComponent<Player>().crouching;

        if (Input.GetButton("Fire1") && !shooting)
        {
            StartCoroutine(Shooting());
        }

        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
        {
            shootsUp = true;
            anim.SetBool("ShootsUp", shootsUp);
        }
        else if(Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow))
        {
            shootsDown = true;
            anim.SetBool("ShootsDown", shootsDown);
        }
        else
        {
            shootsUp = false;
            anim.SetBool("ShootsUp", shootsUp);
            shootsDown = false;
            anim.SetBool("ShootsDown", shootsDown);
        }
    }

    IEnumerator Shooting()
    {
        shooting = true;
        Shoot();
        yield return new WaitForSeconds(shootTime);
        shooting = false;

    }


    void Shoot()
    {
        if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !crouching)  //strzelanie po ukosie w gore
        {
            Instantiate(bulletPrefab, firePointUp.position, firePointUp.rotation);

            
        }

        /*else
        {
            shootsUp = false;
            anim.SetBool("ShootsUp", shootsUp);
        }
        */
        if (!Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow) && !crouching)  //strzelanie prosto
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);  //shooting logic
        }
        if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.UpArrow) && !crouching)  //strzelanie po ukosie w dół
        {
            Instantiate(bulletPrefab, firePointDown.position, firePointDown.rotation);

            shootsDown = true;
            anim.SetBool("ShootsDown", shootsDown);
        }
        /*
        else
        {
            shootsDown = false;
            anim.SetBool("ShootsDown", shootsDown);
        }
        */
        if (crouching)   //strzelanie kucajac
        {
            Instantiate(bulletPrefab, firePointCrouching.position, firePointCrouching.rotation);
        }
    }
}
