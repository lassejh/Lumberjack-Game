using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpObject : MonoBehaviour {

    public GameObject laserPrefab;
    public Transform bulletSpawnTarget;
    GameObject mainCamera;

    public GameObject groundObject;

    public bool carrying = false;
    GameObject carriedObject;
    private Rigidbody carriedObjectrb;

    //variabler til suspension af objekt
    public float distance = 5f;
    public float multiplier = 80f;
    private Vector3 force;
    private Vector3 trackVelocity;
    private Vector3 lastPos;

    public GameObject gun; // Display Panel text
    public GameObject gunHoloDisplay; // Holo background

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

    AudioSource audiosource;

    public AudioClip[] woodClips;

    public AudioClip chopWoodClip;

    public AudioClip whooshClip;

    public AudioClip groundClip;

    public AudioClip blasterClip;

    public AudioClip blasterDisplayONClip;
    public AudioClip blasterDisplayOFFClip;

    public GameObject impactEffect;
    public float range = 100f;
    public float damage = 1f;
    public float impactForce = 30f;



    void Start () {
        mainCamera = GameObject.FindWithTag("MainCamera");
        objectPooler = ObjectPooler.Instance;
        carrying = false;
        torusRotation = Quaternion.Euler(90f, 0f, 0f);
        audiosource = GetComponent<AudioSource>();
}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            RaycastHit hit;

            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit))
            {
                if (hit.transform.tag == "wood")
                {
                    hit.transform.position += Vector3.down * 100f;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (!carrying)
            {
                SpawnWoodObject("pillar");
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (!carrying)
            
            {
                SpawnWoodObject("medium");

            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            if (!carrying)

            {
                SpawnWoodObject("plank");
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            gun.SetActive(!gun.active);
            gunHoloDisplay.SetActive(!gunHoloDisplay.active);

            if (gunHoloDisplay.active) {
                audiosource.clip = blasterDisplayONClip;
                audiosource.Play(0);
            }
            if (!gunHoloDisplay.active)
            {
                audiosource.clip = blasterDisplayOFFClip;
                audiosource.Play(0);
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

            if (Input.GetButtonDown("Fire1"))
            {
                if (triggeredWelcomeScreen == false)
                {
                    triggeredWelcomeScreen = true;

                    gun.GetComponent<GunDisplay>().UpdateDisplay();
                }
                else 
                {
                    audiosource.clip = blasterClip;
                    audiosource.Play(0);

                    int x = Screen.width / 2;
                    int y = Screen.height / 2;


                    GameObject clone = Instantiate(laserPrefab, bulletSpawnTarget.position, bulletSpawnTarget.rotation) as GameObject;

                    clone.GetComponent<Rigidbody>().AddForce(clone.transform.forward * 1200f);
                }
                /*
                int layerMask = 1 << 11;
                layerMask = ~layerMask;
                int x = Screen.width / 2;
                int y = Screen.height / 2;
                Ray ray = mainCamera.GetComponent<Camera>().ScreenPointToRay(new Vector3(x, y));
                RaycastHit hit;
                if (Physics.Raycast(ray,out hit, 100f,layerMask))
                {
                    if (hit.collider.tag == "enemy")
                    {

                        //Enemy[] p = hit.collider.GetComponentsInChildren<Enemy>();
                        Enemy[] p = hit.collider.GetComponentsInParent<Enemy>();
                        foreach (Enemy item in p)
                        {
                            if (item != null)
                            {
                                p[0].Damage(10);
                                Rigidbody rb = hit.rigidbody;
                                rb.AddForceAtPosition(mainCamera.transform.forward * 10f, hit.point, ForceMode.Impulse);
                                break;
                            }
                        }
                    }
                }
                */

                // Testing start
                RaycastHit hit;
                if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, range))
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
                }
                // Testing end
            }
        }
        
    }

    void SpawnWoodObject(string type)
    {
        audiosource.clip = whooshClip;
        audiosource.Play(0);
        torusRotation = Quaternion.Euler(90f, 0f, 0f);
        GameObject p = objectPooler.SpawnFromPool(type, mainCamera.transform.position + mainCamera.transform.forward * distance, mainCamera.transform.rotation);
        carrying = true;
        carriedObject = p.gameObject;
        p.gameObject.GetComponent<Rigidbody>().useGravity = false;
        p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
        p.gameObject.layer = 10;
        
        Vector3 dist = p.transform.position - mainCamera.transform.position;
        distance = dist.magnitude;
        q = p.transform.rotation;
        gun.GetComponent<GunDisplay>().UpdateDisplay();
    }


    void Carry(GameObject o) {

        ws = carriedObject.transform.GetChild(0).GetComponent<WoodScript>();

        if (ws != null)
        {
            if ( ws.sideCollider2Triggered && ws.sideCollider1Triggered) { torusMaterial.color = Color.green; }
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
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    ws.length -= 2f;
                    ws.UpdateWood();
                    ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                }
                else
                {
                    ws.length -= 5f;
                    ws.UpdateWood();
                    float rnd = Random.Range(-0.01f, 0.01f);
                    ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                }
            }


            if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 10f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(0.1f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 18f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.2f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 26f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.3f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha4) &&  Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 32f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.4f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha5) && Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 40f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.5f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha6) &&  Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 48f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.6f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha7) && Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 56f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.7f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha8) &&  Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 64f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.8f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
            if (Input.GetKeyDown(KeyCode.Alpha9) &&  Input.GetKey(KeyCode.LeftShift))
            {
                ws.length = 74f;
                ws.UpdateWood();
                ws.GetComponentInParent<SetColliderSize>().UpdateCollider();
                //            carriedObject.transform.localScale = new Vector3(carriedObject.transform.localScale.x * 0.9f, 1f, 1f);
                audiosource.clip = chopWoodClip;
                audiosource.Play(0);
            }
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
            audiosource.clip = whooshClip;
            audiosource.Play(0);
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
        if (ws != null && ws.sideCollider2Triggered || ws != null && ws.sideCollider1Triggered)
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

        if (ws != null)
        {
            if (Input.GetButtonDown("Fire1"))
            {
            

                if (ws.sideCollider2Triggered == true && ws.sideCollider1Triggered == true)
                {
                    carriedObject.transform.parent = ws.touchedObj.transform.parent;
                    carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                    //Destroy(carriedObject.GetComponent<Pickupable>());
                    carriedObject.transform.position += -toMarker.normalized * ws.width/ws.multiplier * 0.4f;
                    Vector3 actualDistance = carriedObject.transform.position - mainCamera.transform.position;
                
                
                    distance = actualDistance.magnitude-0.4F;
                    audiosource.clip = woodClips[Random.Range(0, woodClips.Length - 1)];
                    audiosource.Play(0);
                    DropObject();

                }   
            }

            if (Input.GetButtonDown("Fire2"))

            {
            

                if (ws.endColliderTriggered == true)
                {
                    audiosource.clip = groundClip;
                    audiosource.Play(0);
                    float storedHeight = carriedObject.transform.position.y;
                    carriedObject.transform.position -= carriedObject.transform.right/2;
                    if (carriedObject.transform.position.y > storedHeight)
                    {
                        carriedObject.transform.position += carriedObject.transform.right ;
                    }
                    carriedObject.transform.GetComponent<Rigidbody>().isKinematic = true;
                    // Destroy(carriedObject.GetComponent<Pickupable>());
                    carriedObject.transform.parent = groundObject.transform;
                    DropObject();
                }
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
                    audiosource.clip = whooshClip;
                    audiosource.Play(0);
                    carrying = true;
                    carriedObject = p.gameObject;
                    p.transform.parent = null;
                    p.gameObject.GetComponent<Rigidbody>().isKinematic = false;
                    p.gameObject.GetComponent<Rigidbody>().useGravity = false;
                    p.gameObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                    p.gameObject.layer = 10;
                    Transform[] allChildren = p.GetComponentsInChildren<Transform>();
                    foreach (Transform child in allChildren)
                    {
                        child.gameObject.layer = 10;
                    }
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
        Transform[] allChildren = carriedObject.GetComponentsInChildren<Transform>();
        foreach (Transform child in allChildren)
        {
            child.gameObject.layer = 0;
        }

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
