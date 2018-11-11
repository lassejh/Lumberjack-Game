using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {
    GameObject mainCamera;

    bool carrying;
    GameObject carriedObject;
    private Rigidbody carriedObjectrb;

    //variabler til suspension af objekt
    public float distance = 2f;
    public float multiplier = 40f;
    private Vector3 force;
    private Vector3 trackVelocity;
    private Vector3 lastPos;

    //Den måde objektet bliver roteret på
    public int carryMode = 2;

    //Variabler der holder styr på spillerens rotationsinput
    private Quaternion q;

    private float userRotationX = 0;
    private float userRotationY = 0;
    private float userRotationZ = 0;

    private Quaternion userRotationQ = Quaternion.Euler(0,0,0);
    private int userRotationAxis = 2;

    

	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.L))
        {
            carryMode += 1;
            if (carryMode >= 4)
            {
                carryMode = 0;
            }
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
	}

    void Carry(GameObject o) {
        carriedObjectrb = o.GetComponent<Rigidbody>();

        trackVelocity = (carriedObjectrb.position - lastPos) * 50;
        lastPos = carriedObjectrb.position;

        //o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth);
        

        Vector3 marker = mainCamera.transform.position + mainCamera.transform.forward * distance;
        Vector3 toMarker = o.transform.position - (marker);
        //carriedObjectrb.AddForce(-toMarker.normalized * multiplier );

        force = o.transform.position - toMarker * Time.deltaTime * multiplier;
        carriedObjectrb.MovePosition(force);

        if (carryMode == 0)
        {
            //carriedObjectrb.MoveRotation(transform.rotation * userRotationQ);
            carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, transform.rotation * userRotationQ, 0.2f));
        }
        if (carryMode == 1)
        {
            //carriedObjectrb.MoveRotation(mainCamera.transform.rotation * userRotationQ);
            carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, mainCamera.transform.rotation * userRotationQ, 0.2f));

        }
        else if (carryMode == 2)
        {
            //carriedObjectrb.MoveRotation(q * userRotationQ);
            carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, q * userRotationQ, 0.2f));

        }
        else if (carryMode == 3){
            //carriedObjectrb.MoveRotation(Quaternion.identity * userRotationQ);
            carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, Quaternion.identity * userRotationQ, 0.2f));

        }

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
        }
        distance += Input.GetAxis("Mouse ScrollWheel");
        if (distance<= 0.5f)
        {
            distance = 0.6f;
        }
        if (distance >= 15f)
        {
            distance = 14.9f;
        }

        carriedObjectrb.velocity = Vector3.zero;

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
        userRotationY = 0;
        userRotationZ = 0;
        userRotationQ = Quaternion.Euler(0, 0, 0);
        userRotationAxis = 2;
    }
}
