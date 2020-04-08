using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]

public class GravityBody: MonoBehaviour
{
    Transform _transform;
    Rigidbody _rigidbody;

    void Awake() {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void FixedUpdate() {
        SceneManager.Shared.Arena.GravityProcessor.Process(_transform, _rigidbody);
    }
}
