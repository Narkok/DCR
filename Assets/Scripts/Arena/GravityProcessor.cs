using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent (typeof (MeshCollider))]

public class GravityProcessor: MonoBehaviour {

    [SerializeField] public float gravity = 9.8f;
    private List<Vector3> _vertices;


    private void Awake() {
        _vertices = new HashSet<Vector3>(GetComponent<MeshCollider>().sharedMesh.vertices)
            .Select(v => transform.TransformPoint(v))
            .ToList();
    }


    public void Process(Transform transform, Rigidbody rigidbody){
        Vector3 position = transform.position;
        Vector3 targetDir = (position - _vertices.Closest(position)).normalized;
        rigidbody.AddForce(targetDir * gravity * rigidbody.mass);
    }
}
