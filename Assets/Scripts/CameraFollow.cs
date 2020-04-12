using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraFollow: MonoBehaviour {
    
    [SerializeField] public float smoothTime = 12f;

    private Transform _transform;
    private Transform _positionTarget;
    private Transform _lookAtTarget;

    private float updatingSmooth = 0f;


    public void Set(Transform followVehicle) {
        _transform = transform;
        _lookAtTarget = followVehicle.Find("CameraLookAtTarget");
        _positionTarget = followVehicle.Find("CameraPositionTarget");
    }


    void FixedUpdate() {
        _transform.position = Vector3.Lerp(_transform.position, _positionTarget.position, updatingSmooth * Time.fixedDeltaTime);
        _transform.LookAt(_lookAtTarget);
        if (updatingSmooth < smoothTime) { updatingSmooth += 0.02f; }
    }
}
