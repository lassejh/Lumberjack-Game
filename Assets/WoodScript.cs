using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodScript : MonoBehaviour {
    public bool customColor = false;
    public float maxHealth = 100f;
    private float hp;
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
        if (hp < 100f)
        {
            hp = 100f;
        }
        rnd = Random.Range(0f, 0.1f);
        rndom = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));

        UpdateWood();
        

    }

	public void UpdateWood () {
        if (!customColor)
        {
            this.transform.localScale = new Vector3(length / multiplier, width / multiplier, height / multiplier);

            this.transform.localPosition = new Vector3(0, 0, 0);
        }
        

        if (endCollider1!= null && endCollider2 != null)
        {
            endCollider1.transform.rotation = transform.rotation;
            endCollider2.transform.rotation = transform.rotation;

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
        }



        if (sideCollider1 !=null)
        {
            sideCollider1.transform.rotation = transform.rotation;
            sideCollider1.transform.localScale = new Vector3(length / multiplier / 2, width / multiplier + 0.02f, height / multiplier + 0.02f);
            sideCollider1.transform.localPosition = new Vector3(-transform.localScale.x * 0.25f, 0f, 0f);
        }



        if (sideCollider2 != null)
        {
            sideCollider2.transform.rotation = transform.rotation;
            sideCollider2.transform.localScale = new Vector3(length / multiplier / 2, width / multiplier + 0.02f, height / multiplier + 0.02f);
            sideCollider2.transform.localPosition = new Vector3(transform.localScale.x * 0.25f, 0f, 0f);
        }
        this.transform.GetComponent<Renderer>().material = woodMat;
        this.transform.GetComponent<Renderer>().material.mainTextureOffset = rndom;
        this.transform.GetComponent<Renderer>().material.mainTextureScale = new Vector2(4, 0.4f);
        this.transform.GetComponent<Renderer>().material.SetTextureOffset("_DetailAlbedoMap", new Vector2(0, 0));
        this.transform.GetComponent<Renderer>().material.SetTextureScale("_DetailAlbedoMap", new Vector2(1, 1f));
        if (customColor == true)
        {
            this.transform.GetComponent<Renderer>().material.color = new Color(0.57f + rnd, 0.57f + rnd, 0.57f + rnd);
        }
        else
        {
            this.transform.GetComponent<Renderer>().material.color -= new Color(rnd, rnd, rnd);

        }
    }

    public void Damage(float dmg)
    {
        hp -= dmg;
        Debug.Log("took damage" + transform.parent.gameObject);
        if (hp <= 0f)
        {
            transform.position = new Vector3(0, -100f, 0);
            //Destroy(transform.gameObject); 
        }
    }
    public void Repair(float hpoints)
    {
        hp += hpoints;
        if (hp > maxHealth)
        { hp = maxHealth; }
    }

    public void DisableColliders()
    {
        sideCollider1.SetActive(false);
        sideCollider2.SetActive(false);
        endCollider1.SetActive(false);
        endCollider2.SetActive(false);
    }

}
