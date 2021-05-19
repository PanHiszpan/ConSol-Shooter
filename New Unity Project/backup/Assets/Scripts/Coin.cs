using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float rotationSpeed;
    //public int moneyPerCoin = 25;
    //private Player playerObject;

    // Start is called before the first frame update
    void Start()
    {
        //playerObject = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }

    /*void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {            
            playerObject.amountOfMoney = playerObject.amountOfMoney + moneyPerCoin;
            Destroy(this.gameObject);
        }
    }*/
}
