using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameCamera: MonoBehaviour {

    /// Цель слежения
    [SerializeField] 
    private CameraTarget _target;
    public CameraTarget Target {
        get {
            return _target;
        }
        set {
            _target = value;
            _smoothTime = 0;
        }
    }

    private Transform _transform;
    private float _smoothTime = 0;
    private float _deltaSmooth = 0.05f;


    private void Awake() {
        _transform = transform;
    }


    private void FixedUpdate() {
        if (_target == null) return;
        if (_target.Target == null) return;

        _smoothTime = Mathf.Clamp(_smoothTime + _deltaSmooth, 0, Target.SmoothTime);

        float smooth = _smoothTime * Time.fixedDeltaTime;
        _transform.position = Vector3.Slerp(_transform.position, _target.Target.position, smooth);
        _transform.rotation = Quaternion.Slerp(_transform.rotation,  _target.Target.rotation, smooth);
    }
}
