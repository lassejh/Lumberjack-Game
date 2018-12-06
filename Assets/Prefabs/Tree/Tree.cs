using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour {

    public Transform TrunkSpawner;
    public GameObject[] treeTypes;

    void Start () {
        SpawnTree();
	}

    void SpawnTree()
    {
        //transform.position = new Vector3(transform.position.x, transform.position.y + 0.4f, transform.position.z);
        var tree = Instantiate(treeTypes[Random.Range(0, treeTypes.GetLength(0))], TrunkSpawner.position, Quaternion.Euler(-90, 0, 0));
    }
}
