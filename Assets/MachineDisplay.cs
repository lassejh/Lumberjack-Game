using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MachineDisplay : MonoBehaviour
{
    public Animator anim;
    public Text counter;
    public PickUpObject player;
    public GameObject mainBG;
    public GameObject secBG;

    public GameObject gun;
    public GameObject gunTable;

    public GameObject display1;
    public GameObject display2;
    public GameObject display3;
    public GameObject display4;
    public GameObject display5;
    public GameObject display6;
    public GameObject display7;

    public bool hasWon = false;

    public int currentPage = 0;
    // Start is called before the first frame update


    // Update is called once per frame
   public void UpdateDisplay()
    {
        
        currentPage += 1;
        
        if (currentPage >= 5)
        {
            
            currentPage = 0;
        }
        if (hasWon)
        {
            switch (currentPage)
            {
                case 0:
                    
                    display1.SetActive(false);
                    display2.SetActive(false);
                    display3.SetActive(false);
                    display4.SetActive(false);
                    display5.SetActive(false);
                    display6.SetActive(true);
                    display7.SetActive(false);
                    break;

                case 1:
                    display1.SetActive(false);
                    display2.SetActive(false);
                    display3.SetActive(false);
                    display4.SetActive(false);
                    display5.SetActive(false);
                    display6.SetActive(false);
                    display7.SetActive(true);
                    gun.SetActive(false);
                    
                    break;
                case 2:
                    anim.SetTrigger("start");
                    transform.gameObject.SetActive(false);
                    break;

                    
            }
        }
        else
        {
            switch (currentPage)
            {
                case 0:
                    display1.SetActive(true);
                    display2.SetActive(false);
                    display3.SetActive(false);
                    display4.SetActive(false);
                    display5.SetActive(false);
                    display6.SetActive(false);
                    display7.SetActive(false);
                    break;

                case 1:
                    display1.SetActive(false);
                    display2.SetActive(true);
                    display3.SetActive(false);
                    display4.SetActive(false);
                    display5.SetActive(false);
                    display6.SetActive(false);
                    display7.SetActive(false);
                    break;
                case 2:
                    display1.SetActive(false);
                    display2.SetActive(false);
                    display3.SetActive(true);
                    display4.SetActive(false);
                    display5.SetActive(false);
                    display6.SetActive(false);
                    display7.SetActive(false);
                    break;
                case 3:
                    display1.SetActive(false);
                    display2.SetActive(false);
                    display3.SetActive(false);
                    display4.SetActive(true);
                    display5.SetActive(false);
                    display6.SetActive(false);
                    display7.SetActive(false);
                    break;
                case 4:
                    display1.SetActive(false);
                    display2.SetActive(false);
                    display3.SetActive(false);
                    display4.SetActive(false);
                    display5.SetActive(true);
                    display6.SetActive(false);
                    display7.SetActive(false);
                    gun.SetActive(true);
                    gunTable.SetActive(false);
                    player.timeStarted = true;
                    counter.gameObject.SetActive(true);
                    
                    break;

                    // and so on
            }
        }
        
    }
    
}
