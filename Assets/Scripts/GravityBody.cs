using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]

public class GravityBody: MonoBehaviour
{
    GravityAttractor gravityCenter;

    void Awake() {
        gravityCenter = GameObject.FindGameObjectWithTag("GravityCenter").GetComponent<GravityAttractor>();
        GetComponent<Rigidbody>().useGravity = false;
    }

    private void FixedUpdate() {
        gravityCenter.Attract(transform);
    }
}
