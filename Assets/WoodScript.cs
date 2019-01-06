using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour {

    private Vector2 rndom;
    float rnd;

    public float length;
    public int width;
    public int height;

    public float multiplier = 1f;

    public GameObject endCollider1;
    public GameObject endCollider2;
    public GameObject sideCollider1;
    public GameObject sideCollider2;

    public bool endColliderTriggered = false;

    public bool sideCollider1Triggered = false;
    public bool sideCollider2Triggered = false;

    public GameObject arrowObject;
    public GameObject arrowObject2;

    public Material woodMat;

    public GameObject touchedObj;

    void Start () {
        rnd = Random.Range(0.9f, 1f);
        rndom = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        UpdateWood();
        

    }

	public void UpdateWood () {

        this.transform.localScale = new Vector3(length / multiplier, width / multiplier, height / multiplier);

        this.transform.localPosition = new Vector3(0, 0, 0);

        endCollider1.transform.rotation = transform.rotation;
        endCollider2.transform.rotation = transform.rotation;
        this.transform.GetComponent<Renderer>().material = woodMat;
        this.transform.GetComponent<Renderer>().material.mainTextureOffset = rndom;
        this.transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(4,0.4f);
        this.transform.GetComponent<Renderer>().material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0,0));
        this.transform.GetComponent<Renderer>().material.SetTextureScale("_DetailAlbedoMap", new Vector2(1, 1f));
        
        this.transform.GetComponent<Renderer>().material.color = new Color(rnd,rnd,rnd);
        endCollider1.transform.localScale = new Vector3(0.05f, width / multiplier, height / multiplier);
        endCollider2.transform.localScale = new Vector3(0.05f, width / multiplier, height / multiplier);

        endCollider1.transform.localPosition = new Vector3(
            transform.localPosition.x - transform.localScale.x * 0.5f - 0.05f,
            0f,
            0f);

        endCollider2.transform.localPosition = new Vector3(
            transform.localPosition.x + transform.localScale.x * 0.5f + 0.05f,
            0f,
            0f);

        arrowObject2.transform.localPosition = endCollider1.transform.localPosition;
        arrowObject.transform.localPosition = endCollider2.transform.localPosition;

        sideCollider1.transform.rotation = transform.rotation;
        sideCollider1.transform.localScale = new Vector3(length / multiplier / 2, width / multiplier + 0.02f, height / multiplier + 0.02f);
        sideCollider1.transform.localPosition = new Vector3(- transform.localScale.x * 0.25f, 0f, 0f);
        


        sideCollider2.transform.rotation = transform.rotation;
        sideCollider2.transform.localScale = new Vector3(length / multiplier / 2, width / multiplier + 0.02f, height / multiplier + 0.02f);
        sideCollider2.transform.localPosition = new Vector3( transform.localScale.x * 0.25f, 0f, 0f);
        
    }
}
