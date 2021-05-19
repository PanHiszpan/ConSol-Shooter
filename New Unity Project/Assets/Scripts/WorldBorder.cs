using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldBorder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D hitinfo)
    {
        Player player = hitinfo.GetComponent<Player>();
        if (player != null)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}

