using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeChopRay : MonoBehaviour {

    public Camera fpsCam;
    public GameObject impactEffect;
    public float range = 100f;
    public float damage = 1f;
    public float impactForce = 30f;
    //public float fireRate = .24f;


    void Start () {
		
	}
	
	void Update () {
        // Press interact Btn
        if(Input.GetButton("Fire1")){
            RaycastHit hit;
            if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
            {
                Debug.Log(hit.transform.name);

                TreeTrunk target = hit.transform.GetComponent<TreeTrunk>();

                if (target != null)
                {
                    target.ChopTrunk(damage);

                    if (hit.rigidbody != null)
                    {
                        hit.rigidbody.AddForce(-hit.normal * impactForce);
                    }
                    GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                    Destroy(impactGO, 2f);
                }



                //TrunkDevide trunkDevide = hit.transform.GetComponent<TrunkDevide>();

                //if (trunkDevide != null)
                //{
                //    trunkDevide.DevideTrunk(lengthX);
                //    //GameObject placeToCut = Instantiate(impactCut, hit.point, Quaternion.LookRotation(hit.normal));

                //    //trunkDevide.DevideTrunk(lengthX);

                //}


            }
        }
	}
}
