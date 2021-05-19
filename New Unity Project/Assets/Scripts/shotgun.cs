using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shotgun : MonoBehaviour
{
    public AudioClip shootingSound;
    private AudioSource audioSource;

    public Transform firePointUp;
    public Transform firePoint;
    public Transform firePointDown;
    public GameObject bulletPrefab;
    private bool shooting = false;
    public float shootTime;
    public float timeBtwSeries;
    public int bulletsInSeries;
    private int bulletsCounter;
    public float playerRangeX;
    public float playerRangeY;
    public Player player;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = FindObjectOfType<Player>();
        bulletsCounter = 0;
    }


    // Update is called once per frame
    void Update()
    {
        // wyswietlanie zasiegu
        if (transform.rotation.y > 0)  //kiedy postac patrzy w prawo
        {
            Debug.DrawLine(new Vector3(transform.position.x - playerRangeX, transform.position.y + playerRangeY, transform.position.z), new Vector3(transform.position.x, transform.position.y + playerRangeY, transform.position.z));
            Debug.DrawLine(new Vector3(transform.position.x - playerRangeX, transform.position.y - playerRangeY, transform.position.z), new Vector3(transform.position.x, transform.position.y - playerRangeY, transform.position.z));
        }
        if (transform.rotation.y <= 0) // i w lewo
        {
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y + playerRangeY, transform.position.z), new Vector3(transform.position.x + playerRangeX, transform.position.y + playerRangeY, transform.position.z));
            Debug.DrawLine(new Vector3(transform.position.x, transform.position.y - playerRangeY, transform.position.z), new Vector3(transform.position.x + playerRangeX, transform.position.y - playerRangeY, transform.position.z));
        }

        // jesli enemy patrzy w prawo, gracz jest po prawej stronie, gracz jest w zasięgu i gracz jest na tej samej wysokości
        if (transform.rotation.y <= 0 && player.transform.position.x > transform.position.x && player.transform.position.x < transform.position.x + playerRangeX && player.transform.position.y > transform.position.y - playerRangeY && player.transform.position.y < transform.position.y + playerRangeY)
        {
            GetComponent<Enemy>().moving = false;
            if (!shooting)
            {
                StartCoroutine(ShootSeries());
            }
        }
        // jesli enemy patrzy w lewo, gracz jest po lewej stronie enemy i gracz jest w zasięgu i gracz jest (mniej więcej) na tej samej wysokości
        else if (transform.rotation.y > 0 && player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRangeX && player.transform.position.y > transform.position.y - playerRangeY && player.transform.position.y < transform.position.y + playerRangeY)
        {
            GetComponent<Enemy>().moving = false;
            if (!shooting)
            {
                StartCoroutine(ShootSeries());
            }

        }
        //jesli nie ma gracza w zasiegu po lewej ani po prawej
        else
        {
            GetComponent<Enemy>().moving = true;
        }

    }
    IEnumerator ShootSeries()
    {
        shooting = true;
        /*
        if (bulletsCounter > bulletsInSeries)
        {
            yield return new WaitForSeconds(2f);
            bulletsCounter = 0;
        }
        else
        {

            if (startShootTime < 0)  //spawner pocisków
            {
                Debug.Log(bulletsCounter);
                bulletsCounter++;
                Shoot();
                startShootTime = shootTime;
                
            }
            else
            {
                startShootTime -= Time.deltaTime;
            }
        }
        */

        if (bulletsCounter >= bulletsInSeries)
        {
            yield return new WaitForSeconds(timeBtwSeries);
            bulletsCounter = 0;
        }
        else
        {

            Shoot();
            bulletsCounter++;
            yield return new WaitForSeconds(shootTime);

        }

        shooting = false;
    }

    void Shoot()
    {
        audioSource.PlayOneShot(shootingSound, 1f);
        audioSource.PlayOneShot(shootingSound, 1f);
        audioSource.PlayOneShot(shootingSound, 1f);
        Instantiate(bulletPrefab, firePointUp.position, firePointUp.rotation);  //shooting logic
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Instantiate(bulletPrefab, firePointDown.position, firePointDown.rotation);
    }
}