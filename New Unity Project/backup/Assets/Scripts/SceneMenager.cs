using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMenager : MonoBehaviour
{
    public bool win = false;
    public bool GamePaused = false;
    private bool showed = false;

    public int howManyLives;
    public static int lives = 3;
    private bool takenlife = false;

    public GameObject pauseScreenUI;
    public GameObject endScreenUI;

    private void Start()
    {
        Time.timeScale = 1f;  //zabezpieczenie przed pausemenu(zatrzymuje czas) + restart = zatrzymany czas po restarcie poziomu
        Debug.Log(lives);
    }

    void Update()
    {
        if (Input.GetButtonDown("Cancel") && !win) //Esc
        {
            PauseMenu();
        }
        if (win)
        {
            EndScreen();
        }
    }

    public void MainMenu()
    {
        StartCoroutine(LoadLevelAsync(0));
    }
    public void NextLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1));
    }
    public void RestartLevel()
    {
        StartCoroutine(LoadLevelAsync(SceneManager.GetActiveScene().buildIndex));
    }

    IEnumerator LoadLevelAsync(int nr)
    {
        AsyncOperation loadLevelAsync = SceneManager.LoadSceneAsync(nr);

        //wait until loaded
        while (!loadLevelAsync.isDone)
        {
            yield return null;
        }
    }

    private void PauseMenu()
    {
        if (GamePaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    public void Resume()
    {
        Cursor.visible = false;
        pauseScreenUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    public void Pause()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        pauseScreenUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    private void EndScreen()
    {

        if (!showed)
        {
            Time.timeScale = 0f;
            Cursor.visible = true;
            endScreenUI.SetActive(true);
            showed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !takenlife)
        {
            takenlife = true;
            takeLife();
        }
    }

    public void takeLife()
    {
        lives--;
        if (lives <= 0)
        {
            SceneManager.LoadScene(3); //laduje scene 3/game over screen
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
