using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunDisplay : MonoBehaviour
{
    public GameObject firstpersoncontroller;

    public GameObject panel1; // Intro message
    public GameObject panel2; // Carrying false
    public GameObject panel3; // Carrying true

    //public GameObject introMessage;

    public int DisplayPhase = 1;
    public bool isDisplayIntroMessage = true;

    private void Start()
    {
        panel1.SetActive(true);
        panel2.SetActive(false);
        panel3.SetActive(false);
    }

    public void UpdateDisplay () {

        
        if (firstpersoncontroller.GetComponent<PickUpObject>().carrying == true)
        {

            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);
        }
        else
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);
        }
    }
}
