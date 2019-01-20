using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeCrown : MonoBehaviour
{
    readonly bool hasHitGround;

    void OnTriggerEnter(Collider theCollision)
    {
        if (theCollision.gameObject.tag == "ground" && !hasHitGround)
        {
            GetComponentInParent<TreeTrunk>().TouchingGround();
        }
    }
}
