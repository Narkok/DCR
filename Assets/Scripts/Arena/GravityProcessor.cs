using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityProcessor: MonoBehaviour {

    [SerializeField] public float gravity = 9.8f;

    private Transform _transform;


    private void Awake() {
        _transform = transform;
    }


    public void Process(Transform transform, Rigidbody rigidbody){
        Vector3 targetDir = (transform.position - _transform.position).normalized;
        rigidbody.AddForce(targetDir * gravity * rigidbody.mass);
    }
}
