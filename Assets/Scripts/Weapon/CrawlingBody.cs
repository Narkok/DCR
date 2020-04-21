using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SphereCollider))]
public class CrawlingBody : MonoBehaviour {

    [SerializeField] public float speed = 30f;

    private Vector3 _normal;
    private Transform _transform;
    private Rigidbody _rigidbody;
    private SphereCollider _collider;


    private void Awake(){
        _transform = transform;
        _collider = GetComponent<SphereCollider>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.freezeRotation = true; 
        _rigidbody.useGravity = false;
        _normal = transform.up;
    }


    public void Setup(float distance, Vector3 position, Quaternion rotation, float speed) {
        _collider.radius = distance - 0.1f;
        _transform.position = position;
        _transform.rotation = rotation;
        _normal = transform.up;
        this.speed = speed;
    }


    private void FixedUpdate(){
        _rigidbody.AddForce(-_normal);
        LayerMask mask = LayerMask.GetMask(Arena.Name);
        Ray ray = new Ray(_transform.position, -_normal);
        RaycastHit hit;
        Vector3 surfaceNormal = Physics.Raycast(ray, out hit, Mathf.Infinity, mask) ? hit.normal : Vector3.up;
        _normal = Vector3.Lerp(_normal, surfaceNormal, 10 * Time.fixedDeltaTime);
        Vector3 myForward = Vector3.Cross(_transform.right, _normal);
        Quaternion targetRot = Quaternion.LookRotation(myForward, _normal);
        _transform.rotation = targetRot;
        _transform.Translate(0, 0, speed * Time.fixedDeltaTime);
    }
}
