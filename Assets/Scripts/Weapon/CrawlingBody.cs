using UnityEngine;
using System.Collections.Generic;


public class CrawlingBody : MonoBehaviour {

    private float _speed = 30f;
    private float _groundDistance = 1.1f;

    private Vector3 _normal;
    private Transform _transform;


    private void Awake(){
        _transform = transform;
        _normal = transform.up;
    }


    public void Setup(Vector3 position, Quaternion rotation, float speed) {
        _transform.position = position;
        _transform.rotation = rotation;
        _normal = transform.up;
        _speed = speed;
    }


    private void FixedUpdate(){
        Ray ray = new Ray(_transform.position, -_normal);
        RaycastHit hit;
        Vector3 surfaceNormal = Physics.Raycast(ray, out hit, Mathf.Infinity, Arena.Mask) ? hit.normal : Vector3.up;
        _normal = Vector3.Lerp(_normal, surfaceNormal, 10 * Time.fixedDeltaTime);
        /// Максимально альтернативная замена Rigidbody и Collider
        float deltaDistance = _groundDistance - hit.distance;
        _transform.position += _normal * (deltaDistance > 0 ? deltaDistance : -0.8f * Time.fixedDeltaTime);
        Vector3 myForward = Vector3.Cross(_transform.right, _normal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, _normal);
        _transform.rotation = targetRot;
        _transform.Translate(0, 0, _speed * Time.fixedDeltaTime);
    }
}
