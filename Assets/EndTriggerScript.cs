using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTriggerScript : MonoBehaviour {

    public GameObject woodObject;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ground")
        {
            woodObject.GetComponent<WoodScript>().endColliderTriggered = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "ground")
        {
            woodObject.GetComponent<WoodScript>().endColliderTriggered = false;
        }
    }
}
