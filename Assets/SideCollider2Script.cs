using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideCollider2Script : MonoBehaviour {

    public GameObject wood;


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider2Triggered = true;
            wood.GetComponent<WoodScript>().touchedObj = other.gameObject;
        }
    }
    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider2Triggered = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider2Triggered = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider2Triggered = true;
            wood.GetComponent<WoodScript>().touchedObj = collision.gameObject;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider2Triggered = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "wooden")
        {
            wood.GetComponent<WoodScript>().sideCollider2Triggered = false;
        }
    }
}
