using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    public float startHealth;
    public float healthUp;
    public float healthDown;

    public AudioClip shootingSound;
    private AudioSource audioSource;

    public GameObject lvlMusic;
    public GameObject BossMusic;

    public int attackPattern;
    public bool inRange;
    private bool musicChanged = false;
    


    public Transform firePointP;
    public GameObject P;
    public Transform firePointP2;
    public GameObject P2;
    public Transform firePointU;
    public GameObject U;
    public Transform firePointU2;
    public GameObject U2;
    public Transform firePointDP;
    public GameObject DP;
    public Transform firePointDP2;
    public GameObject DP2;
    public Transform firePointDU;
    public GameObject DU;
    public Transform firePointDU2;
    public GameObject DU2;

    public GameObject pass;
    public Player player;
    public GameObject bulletPrefab;
    public float playerRangeX;
    public float playerRangeY;
    private bool shooting = false;
    public float shootTime;
    public float timeBtwSeries;
    public int bulletsInSeries;
    private int bulletsCounter;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        healthUp = startHealth;
        healthDown = startHealth;
        player = FindObjectOfType<Player>();



    }

    // Update is called once per frame
    void Update()
    {

        Debug.DrawLine(new Vector3(transform.position.x - playerRangeX, transform.position.y + playerRangeY, transform.position.z), new Vector3(transform.position.x, transform.position.y + playerRangeY, transform.position.z));
        Debug.DrawLine(new Vector3(transform.position.x - playerRangeX, transform.position.y - playerRangeY, transform.position.z), new Vector3(transform.position.x, transform.position.y - playerRangeY, transform.position.z));

        checkPlayerInRange();
        if (inRange && !musicChanged)
        {
            lvlMusic.SetActive(false);
            BossMusic.SetActive(true);
            musicChanged = true;
        }

        if (!shooting && inRange)
        {
            StartCoroutine(ShootSeries());
        }

        if (healthUp <= 0 && healthDown <= 0)
        {
            DU2.SetActive(false);
            DP2.SetActive(false);
            pass.GetComponent<Renderer>().enabled = true;
            pass.GetComponent<BoxCollider2D>().enabled = false;
        }
    }

    IEnumerator ShootSeries()
    {
        shooting = true;

        //attackPattern = Random.Range(1, 2 + 1);  //+1 bo innaczej nie losuje 2, zawsze dawaj  Random.Range( (min), (max)+1 )
        if (bulletsCounter >= bulletsInSeries)
        {
            yield return new WaitForSeconds(timeBtwSeries);
            bulletsCounter = 0;
            attackPattern = Random.Range(1, 2 + 1);  //=1 bo innaczej nie losuje 2, zawsze dawaj  Random.Range( (min), (max)+1 )
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
        switch (attackPattern)
        {
             case 1:
                shoot1();
                

                P.SetActive(true);
                P2.SetActive(true);
                DP.SetActive(true);
                DP2.SetActive(true);

                U.SetActive(false);
                U2.SetActive(false);
                DU.SetActive(false);
                DU2.SetActive(false);



                break;
            case 2:
                shoot2();

                P.SetActive(false);
                P2.SetActive(false);
                DP.SetActive(false);
                DP2.SetActive(false);

                U.SetActive(true);
                U2.SetActive(true);
                DU.SetActive(true);
                DU2.SetActive(true);

                break;
        }



    }

    void shoot1()
    {
        if (healthUp > 0 && healthDown <= 0)  //tylko gorna zyje
        {
            //audioSource.PlayOneShot(shootingSound, 1f);
            audioSource.PlayOneShot(shootingSound, 1f);
            Instantiate(bulletPrefab, firePointP.position, firePointP.rotation);
            Instantiate(bulletPrefab, firePointP2.position, firePointP2.rotation);

        }
        else if (healthUp <=0 && healthDown > 0)  //tylko dolna zyje
        {
            audioSource.PlayOneShot(shootingSound, 1f);
            Instantiate(bulletPrefab, firePointDP.position, firePointDP.rotation);
            Instantiate(bulletPrefab, firePointDP2.position, firePointDP2.rotation);
        }
        else if (healthUp > 0 && startHealth > 0) //zyja obie
        {
            audioSource.PlayOneShot(shootingSound, 1f);
            Instantiate(bulletPrefab, firePointP.position, firePointP.rotation);
            Instantiate(bulletPrefab, firePointP2.position, firePointP2.rotation);

            Instantiate(bulletPrefab, firePointDP.position, firePointDP.rotation);
            Instantiate(bulletPrefab, firePointDP2.position, firePointDP2.rotation);

        }
    }

    void shoot2()
    {
        if (healthUp > 0 && healthDown <= 0)
        {
             audioSource.PlayOneShot(shootingSound, 1f);
            Instantiate(bulletPrefab, firePointU.position, firePointU.rotation);
            Instantiate(bulletPrefab, firePointU2.position, firePointU2.rotation);
        }
        else if (healthUp <= 0 && healthDown > 0)  //tylko dolna zyje
        {
            audioSource.PlayOneShot(shootingSound, 1f);
            Instantiate(bulletPrefab, firePointDU.position, firePointDU.rotation);
            Instantiate(bulletPrefab, firePointDU2.position, firePointDU2.rotation);
        }
        else if (healthUp > 0 && startHealth > 0) //zyja obie
        {
            audioSource.PlayOneShot(shootingSound, 1f);
            Instantiate(bulletPrefab, firePointU.position, firePointU.rotation);
            Instantiate(bulletPrefab, firePointU2.position, firePointU2.rotation);

            Instantiate(bulletPrefab, firePointDU.position, firePointDU.rotation);
            Instantiate(bulletPrefab, firePointDU2.position, firePointDU2.rotation);

        }
    }

    private void checkPlayerInRange()
    {
        if (player.transform.position.x < transform.position.x && player.transform.position.x > transform.position.x - playerRangeX && player.transform.position.y > transform.position.y - playerRangeY && player.transform.position.y < transform.position.y + playerRangeY)
        {
            inRange = true; ;
        }
        else
        {
            inRange = false;
        }
    }


}
