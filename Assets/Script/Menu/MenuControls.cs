using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour {

    public GameObject menuSlider;
    public GameObject anyWhereButton;
    public GameObject anyWhereText;
    public AudioSource menuMusic;
    public GameObject loadingScreen;
    

    public void SlideMenu()

    {
        menuSlider.GetComponent<Animation>().Play("MenuSlider");
        anyWhereButton.SetActive(false);
        anyWhereText.SetActive(false);
    }

    public void PlayGame()

    {

        menuMusic.Stop();
        loadingScreen.SetActive(true);
        SceneManager.LoadScene(1);
    }

    public void ExitGame()

    {
        Application.Quit();
    }

}
