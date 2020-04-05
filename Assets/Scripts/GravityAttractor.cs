using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAttractor : MonoBehaviour
{
    public float gravity = 9.8f;

    public void Attract(Transform body){
        Rigidbody targetRB = body.GetComponent<Rigidbody>();
        float mass = targetRB.mass;
        Vector3 targetDir = (body.position - transform.position).normalized;
        targetRB.AddForce(targetDir * gravity * mass);
    }
}
