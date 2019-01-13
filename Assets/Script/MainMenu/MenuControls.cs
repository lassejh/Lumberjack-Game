using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControls : MonoBehaviour
{

    public GameObject menuSlider;
    public GameObject anyWhereButton;
    public GameObject anyWhereText;
    

    public void SlideMenu()

    {
        menuSlider.GetComponent<Animation>().Play("MenuSlider");
        anyWhereButton.SetActive(false);
        anyWhereText.SetActive(false);
    }

}
