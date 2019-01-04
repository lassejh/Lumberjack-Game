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
    public GameObject panel4; // Chopping
    public GameObject panel5; // Painting

    public GameObject introMessage;

    public int DisplayPhase = 1;

    public bool isDisplayIntroMessage;

    void Start () {
        DisplayPhase = 1;
        isDisplayIntroMessage = true;
    }
	
	void Update () {

        if (isDisplayIntroMessage)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                DisplayPhase = 2;
                isDisplayIntroMessage = false;
            }
        }
        if (isDisplayIntroMessage == false){ 
            if (firstpersoncontroller.GetComponent<PickUpObject>().carrying == false)
            {
                Debug.Log("carrying == false");
                DisplayPhase = 2;
            }
            if (firstpersoncontroller.GetComponent<PickUpObject>().carrying == true)
            {
                Debug.Log("carrying == true");
                DisplayPhase = 3;
            }
        }


        if (DisplayPhase == 1)
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);

        }
        if (DisplayPhase == 2)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(false);
        }

        if (DisplayPhase == 3)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);
            panel4.SetActive(false);
            panel5.SetActive(false);
        }

        if (DisplayPhase == 4)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(true);
            panel5.SetActive(false);
        }
        if (DisplayPhase == 5)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(false);
            panel4.SetActive(false);
            panel5.SetActive(true);
        }


        /*
        if (DisplayPhase == 2)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                DisplayPhase = 3;
            }
        }

        if (DisplayPhase == 3){
            Debug.Log("Chopping phase");
            if (Input.GetButtonUp("Fire1"))
            {

            }
        }
        if (DisplayPhase == 4)
        {
            Debug.Log("Building phase");
            if (Input.GetButtonUp("Fire1"))
            {

            }
        }
        if (DisplayPhase == 5)
        {
            Debug.Log("Inventory phase");
            if (Input.GetButtonUp("Fire1"))
            {

            }
        }
        */
    }
}
