using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineDisplay : MonoBehaviour
{
    public GameObject mainBG;
    public GameObject secBG;

    public GameObject display1;
    public GameObject display2;
    public GameObject display3;
    public GameObject display4;
    public GameObject display5;

    private int currentPage = 0;
    // Start is called before the first frame update


    // Update is called once per frame
   public void UpdateDisplay()
    {
        currentPage += 1;
        if (currentPage == 5)
        {
            currentPage = 0;
        }
        switch (currentPage)
        {
            case 0:
                display1.SetActive(true);
                display2.SetActive(false);
                display3.SetActive(false);
                display4.SetActive(false);
                display5.SetActive(false);
                break;

            case 1:
                display1.SetActive(false);
                display2.SetActive(true);
                display3.SetActive(false);
                display4.SetActive(false);
                display5.SetActive(false);
                break;
            case 2:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(true);
                display4.SetActive(false);
                display5.SetActive(false);
                break;
            case 3:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(false);
                display4.SetActive(true);
                display5.SetActive(false);
                break;
            case 4:
                display1.SetActive(false);
                display2.SetActive(false);
                display3.SetActive(false);
                display4.SetActive(false);
                display5.SetActive(true);
                break;

                // and so on
        }
    }
    private void Update()
    {
       // secBG.transform.position = new Vector3(mainBG.transform.position.x, mainBG.transform.position.y + Random.Range(-0.01f, 0.01f), mainBG.transform.position.z);
    }
}
