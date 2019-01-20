using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blatest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetComponent<Renderer>().material.color = new Color(183f, 167f, 144f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
