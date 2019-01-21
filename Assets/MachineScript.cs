using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineScript : MonoBehaviour
{
    public bool hasNotWon = true;
    public GameObject player;
    public Transform[] gnomeSpawnPoints;
    public Transform[] coinSpawnPoints;

    public ObjectPooler objectPooler;

    public bool canSpawnGnome = false;
    public bool canSpawnCoin = false;

    public List<GameObject> gnomeTargets;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (hasNotWon)
        {
            if (canSpawnGnome == true)
            {
                StartCoroutine(WaitAndSpawn());
            }
            if (canSpawnCoin == true)
            {
                StartCoroutine(WaitAndSpawnCoin());
            }
        }
        
    }

    IEnumerator WaitAndSpawn() {
        canSpawnGnome = false;
  
        
        yield return new WaitForSeconds(5f);
        Transform selectedSpawnPoint = gnomeSpawnPoints[Random.Range(0, gnomeSpawnPoints.Length)];
        GameObject p = objectPooler.SpawnFromPool("gnome", selectedSpawnPoint.position + new Vector3(2f, 0, 2f), Quaternion.identity);
        p.transform.GetChild(0).GetComponent<Enemy>().ms = this;
        p.transform.GetChild(0).GetComponent<Enemy>().player = player;
        canSpawnGnome = true;
    }
    IEnumerator WaitAndSpawnCoin()
    {
        canSpawnCoin = false;

        yield return new WaitForSeconds(3f);
        Transform selectedSpawnPoint = coinSpawnPoints[Random.Range(0, coinSpawnPoints.Length)];
        GameObject p = objectPooler.SpawnFromPool("coin", selectedSpawnPoint.position, Quaternion.identity);
        p.GetComponent<Rigidbody>().AddForce(selectedSpawnPoint.forward * 20f);
        gnomeTargets.Add(p);
        canSpawnCoin = true;
    }
}
