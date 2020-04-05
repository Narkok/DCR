using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    
    [SerializeField] private float smoothTime = 12f;
    [SerializeField] private Transform positionTarget;
    [SerializeField] private Transform lookAtTarget;

    void Awake() {
        transform.position = positionTarget.transform.position;
        transform.LookAt(lookAtTarget);
    }

    void FixedUpdate() {
        transform.position = Vector3.Lerp(transform.position, positionTarget.transform.position, smoothTime * Time.fixedDeltaTime);
        transform.LookAt(lookAtTarget);
    }
}