using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeRandomSizeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float rnd = Random.Range(0f, .5f);
        transform.localScale += new Vector3(rnd, rnd, rnd);
        Destroy(this);
    }

    // Update is called once per frame
    
}
