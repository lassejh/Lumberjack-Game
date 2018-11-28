using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider1Script : MonoBehaviour {

    public GameObject wood;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider1Triggered = true;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider1Triggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider1Triggered = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider1Triggered = true;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider1Triggered = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider1Triggered = false;
        }
    }
}
