using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateGunStuff : MonoBehaviour
{
    public float RotationSpeed = 1f;
    void Update()
    {
        transform.Rotate(0, 0, Time.deltaTime * RotationSpeed);
    }
}
