using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour {

    public float health = 3f;
    Rigidbody rb;

    public bool treeHasBeenCut;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Update () {
        if(treeHasBeenCut)
        {
            // When Player hits the log it turns into wood.

        }
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
        transform.parent = null;

        treeHasBeenCut = true;
    }
}
