using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForcePoolTest : MonoBehaviour , IPooledObject {

    public float upForce = 20f;
    public float sideForce = .5f;

	public void OnObjectSpawn () {

        float xForce = Random.Range(-sideForce, sideForce);
        float yForce = Random.Range(upForce / 2f, upForce);
        float zForce = Random.Range(-sideForce, sideForce);

        Vector3 force = new Vector3(xForce, yForce, zForce);

        GetComponent<Rigidbody>().velocity = force;

    }

    
	

	void Update () {
		
	}
}
