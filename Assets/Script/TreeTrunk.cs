using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    Rigidbody rb;
    public float health = 3f;
    public bool treeHasBeenCut;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void ChopTrunk(float damage)
    {
        health -= damage;
        if (health <= 0f)
        {
            Die();
        }
    }

    void Die()
    {
        rb.GetComponent<Rigidbody>().isKinematic = false;
        //transform.parent = null;

        treeHasBeenCut = true;

        // Give Player wood reward

    }
}
