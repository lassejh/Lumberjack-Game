using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletScript : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem ps;

    

    private void OnCollisionEnter(Collision collision)
    {
        

        if (collision.gameObject.tag == "enemy")
        {
            /*
            //Enemy[] p = hit.collider.GetComponentsInChildren<Enemy>();
            Enemy[] p = collision.gameObject.GetComponentsInParent<Enemy>();
            foreach (Enemy item in p)
            {
                if (item != null)
                {
                    p[0].Damage(10);
                    
                    break;
                }
            }*/
            Quaternion slopeRotation = Quaternion.FromToRotation(Vector3.forward, collision.contacts[0].normal);
            ParticleSystem clone = Instantiate(ps, collision.contacts[0].point, slopeRotation);
            //clone.transform.parent = collision.transform;
            Enemy enemy = collision.transform.root.GetChild(0).GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.Damage(10);
            }
            
        }
        
        Rigidbody rb = collision.rigidbody;
        if (rb != null)
        {
            rb.AddForceAtPosition(transform.forward * 5f, collision.contacts[0].point, ForceMode.Impulse);
        }
        Destroy(gameObject, 0f);
    }
}
