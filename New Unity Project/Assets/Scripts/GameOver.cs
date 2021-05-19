using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    private Save save;

    private void Start()
    {
        Cursor.visible = true;
        save = GameObject.FindGameObjectWithTag("save").GetComponent<Save>();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);
        save.setLife(80);
        save.setMoney(0);
        save.setLives(3);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
