using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetColliderSize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        UpdateCollider();
	}
	
	// Update is called once per frame
	public void UpdateCollider () {
        BoxCollider boxCol = transform.GetComponent<BoxCollider>();

        WoodScript woodScript = transform.GetChild(0).GetComponent<WoodScript>();

        boxCol.size = new Vector3(woodScript.length / woodScript.multiplier, woodScript.width / woodScript.multiplier * 1.5f, woodScript.height / woodScript.multiplier * 1.5f);
    }
}
