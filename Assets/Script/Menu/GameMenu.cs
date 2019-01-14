using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{

    public static bool gamePaused = false;
    public GameObject gameMenu;


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))

        {
            if (gamePaused)

            {
                Resume();
            }

            else

            {
                Pause();
            }

        }

    }

    public void Resume()

    {
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
    }

    void Pause()

    {
        gameMenu.SetActive(true);
        Time.timeScale = 0f;
        gamePaused = true;
    }

    public void LoadMenu()

    {
        gameMenu.SetActive(false);
        Time.timeScale = 1f;
        gamePaused = false;
        SceneManager.LoadScene(0);
    }

    public void ExitGame()

    {
        Debug.Log("IT WORKED!");
        Application.Quit();
    }
}
