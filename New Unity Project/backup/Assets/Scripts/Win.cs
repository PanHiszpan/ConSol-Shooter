using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Win : MonoBehaviour
{
    private SceneMenager sceneMenager;

    void Start()
    {
        //getting refference to SceneMenager
        sceneMenager = GameObject.FindGameObjectWithTag("SceneMenager").GetComponent<SceneMenager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            sceneMenager.win = true;
        }
    }
}
