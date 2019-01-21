using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeTrunk : MonoBehaviour
{
    Rigidbody rb;
    public float health = 3;
    public bool treeHasBeenCut;
    public bool treeCrownTouchingGround;
    public GameObject crownFall;
    public GameObject crownMechanics;
    public Transform spawnPoint;
    public Transform spawnPointReward;
    public GameObject ShowTreeReward;
    private AudioSource source;

    public AudioClip treeTipping;
    public AudioClip treeHitGround;
    public AudioClip treePile;

    void Start()
    {

        rb = GetComponent<Rigidbody>();
        source = GetComponent<AudioSource>();
        crownMechanics.SetActive(false);

        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit, 2f))
        {
            if (hit.collider.tag == "ground")
            {

                transform.position = hit.point;


            }



        }

    }

    public void ChopTrunk(float damage)
    {
        if (!treeHasBeenCut) {

            if (health >= 3)
            {
                GameObject impactGO = Instantiate(crownFall, spawnPoint.position, Quaternion.identity);
                Destroy(impactGO, 6f);
            }

            health -= damage;

            if (health <= 0f)
            {
                Die();
            }
        }
    }

    void Die()
    {
        rb.GetComponent<Rigidbody>().isKinematic = false;
        transform.parent = null;

        treeHasBeenCut = true;
        crownMechanics.SetActive(true);

        source.PlayOneShot(treeTipping);
        Destroy(transform.gameObject, 4f);
    }

    public void TouchingGround()
    {
        GameObject impactGO = Instantiate(crownFall, spawnPoint.position, Quaternion.identity);
        Destroy(impactGO, 6f);

        treeCrownTouchingGround = true;
        StartCoroutine("MakeKinematic");

        source.PlayOneShot(treeHitGround);

        // Give Player wood
    }

    private IEnumerator MakeKinematic()
    {
        yield return new WaitForSeconds(0.5f);
        GameObject impactGO = Instantiate(ShowTreeReward, spawnPointReward.position, Quaternion.identity);
        Destroy(impactGO, 3f);
        source.PlayOneShot(treePile);

        yield return new WaitForSeconds(5);
        rb.GetComponent<Rigidbody>().isKinematic = true;
        crownMechanics.SetActive(false);
    }
}
