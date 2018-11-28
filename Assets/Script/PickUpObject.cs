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
    public int carryMode = 0;

    //Variabler der holder styr på spillerens rotationsinput
    private Quaternion q;

    private float userRotationX = 0;
    private float userRotationY = 270;
    private float userRotationZ = 90;

    private Quaternion userRotationQ = Quaternion.Euler(0,0,0);
    private int userRotationAxis = 0;
    public Material capMaterial;

    Vector3 marker;

    ObjectPooler objectPooler;
	void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
        objectPooler = ObjectPooler.Instance;
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "wood")
                {
                   
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1) && carrying == false)
        {
            GameObject p = objectPooler.SpawnFromPool("pillar", mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
            carrying = true;
            carriedObject = p.gameObject;
            p.gameObject.GetComponent<Rigidbody>().useGravity = false;
            p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            p.gameObject.layer = 10;
            Vector3 dist = p.transform.position - mainCamera.transform.position;
            distance = dist.magnitude;
            q = p.transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && !carrying)
        {
            GameObject p = objectPooler.SpawnFromPool("medium", mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
            carrying = true;
            carriedObject = p.gameObject;
            p.gameObject.GetComponent<Rigidbody>().useGravity = false;
            p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            p.gameObject.layer = 10;
            Vector3 dist = p.transform.position - mainCamera.transform.position;
            distance = dist.magnitude;
            q = p.transform.rotation;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && !carrying)
        {
            GameObject p = objectPooler.SpawnFromPool("plank", mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
            carrying = true;
            carriedObject = p.gameObject;
            p.gameObject.GetComponent<Rigidbody>().useGravity = false;
            p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
            p.gameObject.layer = 10;
            Vector3 dist = p.transform.position - mainCamera.transform.position;
            distance = dist.magnitude;
            q = p.transform.rotation;
        }
        /*else if (Input.GetKeyDown(KeyCode.L))
        {
            carryMode += 1;
            if (carryMode >= 4)
            {
                carryMode = 0;
            }
        }*/


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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            carriedObject.transform.localScale -= new Vector3(0.05f, 0f, 0f);
        }

        carriedObjectrb = o.GetComponent<Rigidbody>();


        trackVelocity = (carriedObjectrb.position - lastPos) * 50;
        lastPos = carriedObjectrb.position;

        //o.transform.position = Vector3.Lerp (o.transform.position, mainCamera.transform.position + mainCamera.transform.forward * distance, Time.deltaTime * smooth); /*ANDEN MÅDE AT FLYTTE OBJEKTET*/


        marker = mainCamera.transform.position + mainCamera.transform.forward * distance;
        Vector3 toMarker = o.transform.position - (marker);

        //carriedObjectrb.AddForce(-toMarker.normalized * multiplier ); /*ANDEN MÅDE AT FLYTTE OBJEKTET*/

        force = o.transform.position - toMarker * Time.deltaTime * multiplier * 0.5f;
        carriedObjectrb.MovePosition(force);
        /*
        switch (carryMode) 
        {
            case 1:
                carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, transform.rotation * userRotationQ, 0.2f));
                break;
            case 2:
                     carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, mainCamera.transform.rotation * userRotationQ, 0.2f)); 
                break;
            case 3:
                carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, q * userRotationQ, 0.2f));
                break;
            case 4:
                carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, Quaternion.identity * userRotationQ, 0.2f));
                break;

            default:
                
                break;
        }
        */

        carriedObjectrb.MoveRotation(Quaternion.Slerp(carriedObject.transform.rotation, transform.rotation * userRotationQ , 0.1f));
        
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

        if (Input.GetButtonDown("Fire1"))
        {
            WoodScript ws = carriedObject.transform.GetChild(0).GetComponent<WoodScript>();
            Debug.Log(ws.sideCollider1Triggered);
            Debug.Log(ws.sideCollider2Triggered);
            if (ws.sideCollider2Triggered == true && ws.sideCollider1Triggered == true)
            {
                Debug.Log("Fired");
                carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(carriedObject.GetComponent<Pickupable>());
                carriedObject.transform.position += mainCamera.transform.forward * 0.01f;
                DropObject();
            }
            else if (ws.endColliderTriggered == true)
            {
                float storedHeight = carriedObject.transform.position.y;
                carriedObject.transform.position -= carriedObject.transform.right;
                if (carriedObject.transform.position.y > storedHeight)
                {
                    carriedObject.transform.position += carriedObject.transform.right * 2;
                }
                carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(carriedObject.GetComponent<Pickupable>());
                DropObject();
            }
            

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
        userRotationY = 270;
        userRotationZ = 90;
        userRotationQ = Quaternion.Euler(0, 0, 0);
        userRotationAxis = 0;
    }

    void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawLine(transform.position, transform.position + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + transform.up * 0.5f, transform.position + transform.up * 0.5f + transform.forward * 5.0f);
        Gizmos.DrawLine(transform.position + -transform.up * 0.5f, transform.position + -transform.up * 0.5f + transform.forward * 5.0f);

        Gizmos.DrawLine(transform.position, transform.position + transform.up * 0.5f);
        Gizmos.DrawLine(transform.position, transform.position + -transform.up * 0.5f);

    }
}
