﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {
    GameObject mainCamera;

    public bool carrying = false;
    GameObject carriedObject;
    private Rigidbody carriedObjectrb;

    //variabler til suspension af objekt
    public float distance = 5f;
    public float multiplier = 80f;
    private Vector3 force;
    private Vector3 trackVelocity;
    private Vector3 lastPos;

    public GameObject gun;

    //Variabler der holder styr på spillerens rotationsinput
    private Quaternion q;

    private float userRotationX = 0;
    private float userRotationY = 270;
    private float userRotationZ = 90;

    private Quaternion userRotationQ = Quaternion.Euler(0,0,0);
    private Quaternion torusRotation = Quaternion.Euler(90f, 0f, 0f);

    private int userRotationAxis = 0;
    public Material capMaterial;

    public GameObject torus;

    WoodScript ws;

    Vector3 marker;

    ObjectPooler objectPooler;

    public Material torusMaterial;

    bool triggeredWelcomeScreen = false;

	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
        objectPooler = ObjectPooler.Instance;
        carrying = false;
        torusRotation = Quaternion.Euler(90f, 0f, 0f);
}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "wood")
                {
                    hit.transform.position += Vector3.down * 1000f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!carrying)
            {
                torusRotation = Quaternion.Euler(90f, 0f, 0f);
                GameObject p = objectPooler.SpawnFromPool("pillar", mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
                carrying = true;
                carriedObject = p.gameObject;
                p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                p.gameObject.layer = 10;
                Vector3 dist = p.transform.position - mainCamera.transform.position;
                distance = dist.magnitude;
                q = p.transform.rotation;
                gun.GetComponent<GunDisplay>().UpdateDisplay();
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!carrying)
            
            {
                torusRotation = Quaternion.Euler(90f, 0f, 0f);
                GameObject p = objectPooler.SpawnFromPool("medium", mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
                carrying = true;
                carriedObject = p.gameObject;
                p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                p.gameObject.layer = 10;
                Vector3 dist = p.transform.position - mainCamera.transform.position;
                distance = dist.magnitude;
                q = p.transform.rotation;            
                gun.GetComponent<GunDisplay>().UpdateDisplay();

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!carrying)
            {
                torusRotation = Quaternion.Euler(90f, 0f, 0f);
                GameObject p = objectPooler.SpawnFromPool("plank", mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
                carrying = true;
                carriedObject = p.gameObject;
                p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                p.gameObject.layer = 10;
                Vector3 dist = p.transform.position - mainCamera.transform.position;
                distance = dist.magnitude;
                q = p.transform.rotation;
                gun.GetComponent<GunDisplay>().UpdateDisplay();
            }       
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            gun.SetActive(!gun.active);
        }


        if (carrying)
        {
            Carry(carriedObject);
            CheckDrop();
        }
        else
        {
            PickUp();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (triggeredWelcomeScreen == false)
            {
                triggeredWelcomeScreen = true;

                gun.GetComponent<GunDisplay>().UpdateDisplay();
            }
        }
        if (Input.GetButtonDown("Fire2"))
        {
            if (triggeredWelcomeScreen == false)
            {
                triggeredWelcomeScreen = true;

                gun.GetComponent<GunDisplay>().UpdateDisplay();
            }
        }
        }

    void Carry(GameObject o) {

        ws = carriedObject.transform.GetChild(0).GetComponent<WoodScript>();
        if (ws.sideCollider2Triggered && ws.sideCollider1Triggered) { torusMaterial.color = Color.green; }
        else { torusMaterial.color = Color.blue; }

        torus.SetActive(true);


        torus.transform.rotation = transform.rotation;
        
        torus.transform.position = carriedObject.transform.position;
        torus.transform.GetChild(0).transform.localRotation = torusRotation;

        if (ws.endColliderTriggered == true)
        {
            if (ws.arrowObject.transform.position.y > ws.arrowObject2.transform.position.y)
            {
                ws.arrowObject2.SetActive(true);
            }
            else
            {
                ws.arrowObject.SetActive(true);
            }
        }
        else {
            ws.arrowObject.SetActive(false);
            ws.arrowObject2.SetActive(false);
        }




        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                carriedObject.transform.localScale -= new Vector3(0.01f, 0f, 0f);
                carriedObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(carriedObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale.x-0.04f, carriedObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale.y);
            }
            else
            {
                float rnd = Random.Range(-0.01f, 0.01f);
                carriedObject.transform.localScale -= new Vector3(0.05f + rnd, 0f, 0f);
                carriedObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale = new Vector2(carriedObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale.x - 0.2f + rnd, carriedObject.transform.GetChild(0).GetComponent<Renderer>().material.mainTextureScale.y);
            }
        }


        if (Input.GetKeyDown(KeyCode.Alpha1) && carriedObject.transform.localScale.x > 0.1f && Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(0.1f, 1f, 1f);


        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.2f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.3f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) &&  Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.4f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.5f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6) &&  Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.6f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7) && Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.7f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8) &&  Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.8f, 1f, 1f);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9) &&  Input.GetKey(KeyCode.LeftShift))
        {

            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.9f, 1f, 1f);
        }


        carriedObjectrb = o.GetComponent<Rigidbody>();


        trackVelocity = (carriedObjectrb.position - lastPos) * 50;
        lastPos = carriedObjectrb.position;

        marker = mainCamera.transform.position + mainCamera.transform.forward * distance;
        Vector3 toMarker = o.transform.position - (marker);

        force = o.transform.position - toMarker * Time.deltaTime * multiplier * 0.5f;
        carriedObjectrb.MovePosition(force);

        carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, transform.rotation * userRotationQ , 0.15f));
        

        // Rotation of carried object
        if (Input.GetKeyDown(KeyCode.R))
        {
            
            if (userRotationAxis == 0)
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    userRotationX += 5;
                }
                else
                {
                    userRotationX += 45;
                }

                if (userRotationX >= 360)
                {
                    userRotationX = userRotationX - 360;
                }
            }
            else if (userRotationAxis == 1)
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    userRotationY += 5;
                }
                else
                {
                    userRotationY += 45;
                }

                if (userRotationY >= 360)
                {
                    userRotationY = userRotationY - 360;
                }
            }
            else if (userRotationAxis == 2)
            {
                
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    userRotationZ += 5;
                }
                else
                {
                    userRotationZ += 45;
                }
                if (userRotationZ >= 360)
                {
                    userRotationZ = userRotationZ - 360;
                }
                
            }

            

        }
        userRotationQ = Quaternion.Euler(userRotationX, userRotationY, userRotationZ);



        if (Input.GetKeyDown(KeyCode.T))
        {
            userRotationAxis += 1;
            if (userRotationAxis >= 3)
            {
                userRotationAxis = 0;

            }
             if (userRotationAxis == 0)
             {
                 torusRotation = Quaternion.Euler(90f, 0f, 0f);
             }
             else if (userRotationAxis == 1)
             {
                 torusRotation = Quaternion.Euler(0f, 0f, 0f);
             }
             else if (userRotationAxis == 2)
             {
                 torusRotation = Quaternion.Euler(0f, 0f, 90f);
             }
        }
        if (ws.sideCollider2Triggered ||ws.sideCollider1Triggered)
        {
            Vector3 actualDistance = carriedObject.transform.position - mainCamera.transform.position;
            if (distance > actualDistance.magnitude + 0.3f)
            { distance = actualDistance.magnitude + 0.2f; }
            if (distance < actualDistance.magnitude - 0.3f)
            { distance = actualDistance.magnitude - 0.02f; }
        }
        
        distance += Input.GetAxis("Mouse ScrollWheel");

        if (distance<= 0.5f)
        {
            distance = 0.6f;
        }
        if (distance >= 10f)
        {
            distance = 9.9f;
        }
        

        if (Input.GetButtonDown("Fire1"))
        {
            

            if (ws.sideCollider2Triggered == true && ws.sideCollider1Triggered == true)
            {

                carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(carriedObject.GetComponent<Pickupable>());
                carriedObject.transform.position += -toMarker.normalized * ws.width/ws.multiplier * 0.4f;
                DropObject();
                Vector3 actualDistance = carriedObject.transform.position - mainCamera.transform.position;
                distance = actualDistance.magnitude-0.4F;
               

            }   
        }

        if (Input.GetButtonDown("Fire2"))

        {
            

            if (ws.endColliderTriggered == true)
            {
                float storedHeight = carriedObject.transform.position.y;
                carriedObject.transform.position -= carriedObject.transform.right/2;
                if (carriedObject.transform.position.y > storedHeight)
                {
                    carriedObject.transform.position += carriedObject.transform.right ;
                }
                carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(carriedObject.GetComponent<Pickupable>());
                DropObject();
            }
        }

        if (carriedObjectrb != null)
        {
            carriedObjectrb.velocity = Vector3.zero;
        }
        

    }

    void PickUp()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            int x = Screen.width / 2;
            int y = Screen.height / 2;

            Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
                Pickupable p = hit.collider.GetComponent<Pickupable>();
                if (p != null) {
                    carrying = true;
                    carriedObject = p.gameObject;
                    p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                    p.gameObject.layer = 10;
                    Vector3 dist = p.transform.position - mainCamera.transform.position;
                    distance = dist.magnitude;
                    q = p.transform.rotation;
                    torusRotation = Quaternion.EulerAngles(90f, 0f, 0f);

                    gun.GetComponent<GunDisplay>().isDisplayIntroMessage = false;

                    gun.GetComponent<GunDisplay>().UpdateDisplay();


                }
            }
        }
    }

    void CheckDrop()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            DropObject();
        }
    }

    void DropObject() {
        carrying = false;
        carriedObjectrb.velocity = trackVelocity;
        carriedObject.GetComponent<Rigidbody>().useGravity = true;
        carriedObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;
        carriedObject.gameObject.layer = 0;
        carriedObject = null;
        carriedObjectrb = null;
        userRotationX = 0;
        userRotationY = 270;
        userRotationZ = 90;
        userRotationQ = Quaternion.Euler(0, 0, 0);
        userRotationAxis = 0;
        torus.SetActive(false);
        
        ws.arrowObject.SetActive(false);
        ws.arrowObject2.SetActive(false);
        ws = null;

        gun.GetComponent<GunDisplay>().UpdateDisplay();

    }


}
