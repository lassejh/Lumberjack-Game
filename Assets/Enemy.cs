using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject woodRay;
    public GameObject moveObject;
    public Transform target;
    public int maxHp = 5;
    public float maxSpeed = 0.1f;
    private int hp;
    private bool isDead = false;
    private Vector3 lastPos;
    public GameObject axe;
    private bool hasHit = false;
    private AudioSource audioS;
    public AudioClip hitsWood;
    public AudioClip hitsFlesh;

    private bool hasJumped;




    Animator anim;

    Rigidbody rbMove;

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
        Destroy(col);
        Destroy(rbMove);
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


    }
    void HitWood(GameObject target) {
        rbMove.AddForce(new Vector3(0, 2f, 0), ForceMode.Impulse);
        StartCoroutine(WaitAndReenable());
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
                            ws.Damage(30f);
                        }
                        i++;
                    }


                }



            }

            Vector3 movedDistance = rbMove.position - lastPos;
            
            anim.SetFloat("Speed", movedDistance.magnitude * 30f);
            
            if (movedDistance.magnitude * 30 < 0.2f && !hasJumped)
            {
                hasJumped = true;
                StartCoroutine(WaitJump());
                anim.SetTrigger("Jump");
                rbMove.AddForce(Vector3.up * 100f + (transform.forward * -10f) );
            }
            lastPos = rbMove.position;

            if (rbMove.velocity.magnitude > maxSpeed)
            {
                rbMove.velocity = rbMove.velocity.normalized * maxSpeed;
            }


            if (!isDead)
            {

                Vector3 direction = target.position - transform.position;
                if (direction.magnitude > 1.5f)
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

        yield return new WaitForSeconds(Random.Range(2f,4f));
        hasJumped = false;
        

    }
}
