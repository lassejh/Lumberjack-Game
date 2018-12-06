using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collide : MonoBehaviour {

    public GameObject leaf;

    void OnTriggerEnter(Collider theCollision)
    {
        if (theCollision.gameObject.tag == "ground")
        {
            Debug.Log("Hit Ground");
            transform.parent = null;
        }
    }

    void Leaf(){
        GameObject leafs = Instantiate(leaf, transform.position, Quaternion.identity);
        Destroy(leafs, 10f);
    }
}
