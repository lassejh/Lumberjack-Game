using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodSpawner : MonoBehaviour {

    ObjectPooler objectPooler;

	void Start ()
    {
        objectPooler = ObjectPooler.Instance;
	}
	
	void Update () {
		
	}

    private void FixedUpdate()
    {
        objectPooler.SpawnFromPool("Plank", new Vector3(Random.Range (-10f,10f),Random.Range(0.05f,2f),Random.Range(-10f,10f)), Quaternion.Euler(new Vector3(Random.Range(0f,360f),Random.Range(0f,360f), Random.Range(0f, 360f))));
    }
}
