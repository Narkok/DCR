using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow: MonoBehaviour {
    
    [SerializeField] private float smoothTime = 12f;
    [SerializeField] private Transform positionTarget;
    [SerializeField] private Transform lookAtTarget;

    private Transform _transform;


    void Awake() {
        _transform = transform;
        _transform.position = positionTarget.position;
        _transform.LookAt(lookAtTarget);
    }


    void FixedUpdate() {
        _transform.position = Vector3.Lerp(_transform.position, positionTarget.position, smoothTime * Time.fixedDeltaTime);
        _transform.LookAt(lookAtTarget);
    }
}