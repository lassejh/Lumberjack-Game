using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public GameObject woodRay;
    public GameObject woodRay1;
    public GameObject woodRay2;
    public GameObject moveObject;
    public Transform target;
    public int maxHp = 5;
    public float maxSpeed = 0.1f;
    private int hp;
    private bool isDead = false;
    private Vector3 lastPos;
    public GameObject axe;
    public GameObject axePrefab;
    private bool hasHit = false;
    private AudioSource audioS;
    public AudioClip hitsWood;
    public AudioClip hitsFlesh;

    private bool hasJumped;

    public MachineScript ms;


    Animator anim;

    Rigidbody rbMove;


    private bool canRetarget = true;
    private bool canAttack = true;

    void SetKinematic(bool newValue)
    {
        Rigidbody[] bodies = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rb in bodies)
        {
            rb.isKinematic = newValue;
        }
    }
    void Start()
    {
        target = ms.transform;
        anim = GetComponent<Animator>();
        audioS = GetComponent<AudioSource>();
        rbMove = moveObject.GetComponent<Rigidbody>();
        SetKinematic(true);
        hp = maxHp;
    }
    public void Damage(int dmg)
    {
        if (hp <= 0) return;
        hp -= dmg;
        if (hp <= 0) Die();
    }
    public void Die()
    {
        SetKinematic(false);
        GetComponent<Animator>().enabled = false;
        CapsuleCollider col = rbMove.GetComponent<CapsuleCollider>();
        col.enabled = false;
        
        //Destroy(rbMove);
        isDead = true;
        axe.GetComponent<Rigidbody>().isKinematic = false;
        axe.transform.parent = null;
        axe.GetComponent<Rigidbody>().AddTorque(new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)));
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            isDead = !isDead;
        }

        if (canRetarget)
        {
            StartCoroutine(WaitAndReTarget());
        }
        Vector3 distanceToPlayerV = player.transform.position - transform.position;
        if (distanceToPlayerV.magnitude < 2.5f && canAttack && !isDead)
        {
            
            StartCoroutine(WaitAndThrow());
        }
    }
    void HitWood(GameObject target) {
        
        StartCoroutine(WaitAndReenable());
        StartCoroutine(WaitJump());
        hasHit = true;
        anim.SetInteger("nAttack", Random.Range(0, 2));
        anim.SetTrigger("Attack");
        this.audioS.clip = hitsWood;
        this.audioS.Play();
    }
    void FixedUpdate()
    {
        
        if (rbMove != null)
        {
            
            int layerMask = 1 << 14;
            RaycastHit hit;
            if (Physics.Raycast(woodRay.transform.position, rbMove.transform.forward, out hit, 0.3f,layerMask))
            {
                if (hit.collider.tag == "wooden" && hasHit == false)
                {

                    HitWood(hit.collider.gameObject);
                    Collider[] hitColliders = Physics.OverlapSphere(woodRay.transform.position, 0.3f, layerMask);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        WoodScript ws = hitColliders[i].GetComponent<WoodScript>();
                        if (ws != null && ws.tag == "wooden")
                        {
                            ws.Damage(10f);
                        }
                        i++;
                    }


                }



            }
            else if (Physics.Raycast(woodRay1.transform.position, woodRay1.transform.forward, out hit, 0.3f, layerMask))
            {
                if (hit.collider.tag == "wooden" && hasHit == false)
                {

                    HitWood(hit.collider.gameObject);
                    Collider[] hitColliders = Physics.OverlapSphere(woodRay.transform.position, 0.3f, layerMask);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        WoodScript ws = hitColliders[i].GetComponent<WoodScript>();
                        if (ws != null && ws.tag == "wooden")
                        {
                            ws.Damage(10f);
                        }
                        i++;
                    }


                }



            }
            else if (Physics.Raycast(woodRay2.transform.position, woodRay2.transform.forward, out hit, 0.3f, layerMask))
            {
                if (hit.collider.tag == "wooden" && hasHit == false)
                {

                    HitWood(hit.collider.gameObject);
                    Collider[] hitColliders = Physics.OverlapSphere(woodRay.transform.position, 0.3f, layerMask);
                    int i = 0;
                    while (i < hitColliders.Length)
                    {
                        WoodScript ws = hitColliders[i].GetComponent<WoodScript>();
                        if (ws != null && ws.tag == "wooden")
                        {
                            ws.Damage(10f);
                        }
                        i++;
                    }


                }



            }
            
            Vector3 movedDistance = rbMove.position - lastPos;
            
            anim.SetFloat("Speed", movedDistance.magnitude * 30f);
            
            if (movedDistance.magnitude * 30 < 0.2f && !hasJumped)
            {
                
            }
            lastPos = rbMove.position;

            if (rbMove.velocity.magnitude > maxSpeed)
            {
                rbMove.velocity = rbMove.velocity.normalized * maxSpeed;
            }


            if (!isDead)
            {

                Vector3 direction = target.position - transform.position;
                if (direction.magnitude > 0f)
                {
                    direction = direction.normalized;

                    rbMove.velocity = new Vector3(direction.x * maxSpeed * 0.2f, rbMove.velocity.y, direction.z * maxSpeed * 0.2f);
                }

                //Vector3 pos = target.position - transform.position * Time.deltaTime * 80f;
                Vector3 newPos = Vector3.Lerp(rbMove.position, target.position, Time.deltaTime * 1.5f);

                //rbMove.MovePosition(newPos);

                Quaternion newQ = Quaternion.LookRotation(target.position - transform.position);
                newQ = Quaternion.Euler(0, newQ.eulerAngles.y, 0);
                rbMove.MoveRotation(newQ);

            }
        }
        
    }

    IEnumerator WaitAndReenable()
    {
        
        yield return new WaitForSeconds(Random.Range(1.8f, 2.2f));
        hasHit = false;
        rbMove.AddForce(new Vector3(0, -2f, 0), ForceMode.Impulse);

    }
    IEnumerator WaitJump()
    {
        hasJumped = true;
        yield return new WaitForSeconds(Random.Range(2f,4f));
        hasJumped = false;        
        anim.SetTrigger("Jump");
        rbMove.AddForce(Vector3.up * 100f + (transform.forward * -10f));

    }

    IEnumerator WaitAndReTarget() {
        canRetarget = false;
        yield return new WaitForSeconds(5f);

        GameObject newtarget = ms.gnomeTargets[Random.Range(0, ms.gnomeTargets.Count-1)];
        if (newtarget != null)
        {
            target = newtarget.transform;
        }
        canRetarget = true;
    }
    IEnumerator WaitAndThrow()
    {
        canAttack = false;
        anim.SetTrigger("Throw");
        
        rbMove.velocity = Vector3.zero;
        target = player.transform;
        
        
        GameObject clone = Instantiate(axePrefab, woodRay.transform.position, transform.rotation) as GameObject;
        clone.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        clone.GetComponent<Rigidbody>().isKinematic = false;
        clone.GetComponent<Rigidbody>().AddForce(player.transform.GetChild(0).position - transform.position * 20f);
        
        yield return new WaitForSeconds(5f);

       
        canAttack = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "coin")
        {
            Destroy(other.gameObject);
        }
    }

}
