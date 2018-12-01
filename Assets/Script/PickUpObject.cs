using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {
    GameObject mainCamera;

    bool carrying = false;
    GameObject carriedObject;
    private Rigidbody carriedObjectrb;

    //variabler til suspension af objekt
    public float distance = 2f;
    public float multiplier = 40f;
    private Vector3 force;
    private Vector3 trackVelocity;
    private Vector3 lastPos;

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

    Vector3 marker;

    ObjectPooler objectPooler;

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
        /*
        if (Input.GetKeyDown(KeyCode.F) && !carrying)
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "wood")
                {
                    Vector3 relativeDistance = hit.point - hit.transform.position;
                    float dist = hit.transform.InverseTransformDirection(relativeDistance).x * 0.25f;
                    Debug.Log(relativeDistance.magnitude);
                    float halfLength = hit.transform.localScale.x * 0.5f;
                    hit.transform.position = hit.transform.position + hit.transform.right * (halfLength - dist);
                    hit.transform.localScale = new Vector3(halfLength + (dist), hit.transform.localScale.y, hit.transform.localScale.z);
                   
                    if (hit.transform.localScale.x < 0)
                    {
                        hit.transform.position -= new Vector3(1000, 0, 0);
                    }
                }
            }
        }
        */
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

        torus.SetActive(true);


        torus.transform.rotation = transform.rotation;
        
        torus.transform.position = carriedObject.transform.position;
        torus.transform.GetChild(0).transform.localRotation = torusRotation;
        

        

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                carriedObject.transform.localScale -= new Vector3(0.01f, 0f, 0f);
            }
            else { carriedObject.transform.localScale -= new Vector3(0.05f + Random.Range(-0.01f, 0.01f), 0f, 0f); }
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

            if (ws.sideCollider2Triggered == true && ws.sideCollider1Triggered == true)
            {

                carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                Destroy(carriedObject.GetComponent<Pickupable>());
                carriedObject.transform.position += -toMarker.normalized * ws.width/ws.multiplier * 0.4f;
                DropObject();
            }   
        }

        if (Input.GetButtonDown("Fire2"))

        {
            WoodScript ws = carriedObject.transform.GetChild(0).GetComponent<WoodScript>();
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
    }


}
