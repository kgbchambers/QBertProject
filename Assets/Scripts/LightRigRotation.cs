using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRigRotation : MonoBehaviour
{
    public float rotationSpeed;
    private Vector3 currentEulerAngles;

    void FixedUpdate()
    {
        currentEulerAngles += Vector3.forward * Time.fixedDeltaTime * rotationSpeed;
        transform.eulerAngles = new Vector3(45, -45, currentEulerAngles.z);
    }
}
