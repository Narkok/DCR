using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


[RequireComponent(typeof(MeshCollider))]
public class GravityProcessor: MonoBehaviour {

    [SerializeField] public float gravity = 9.8f;
    [SerializeField] private float filterRadius = 2;

    private List<Vector3> _vertices = new List<Vector3>();

    public Vector3 RangomPoint { get { return _vertices.RandomElement(); } }


    private void Awake() {
        prepareVertices();
    }


    private void prepareVertices() {
        _vertices.Clear();

        List<Vector3> vertices = new HashSet<Vector3>(GetComponent<MeshCollider>().sharedMesh.vertices)
            .Select(v => transform.TransformPoint(v))
            .ToList();

        foreach (Vector3 point in vertices) { 
            if (!_vertices.Exists(v => Vector3.Distance(v, point) < filterRadius)) {
                _vertices.Add(point);
            }
        }

        foreach (Vector3 point in _vertices) { 
            DebugManager.AddPoint(point);
        }
    }


    public Vector3 Closest(Vector3 point) {
        return _vertices.Closest(point);
    }


    public Vector3 Normal(Vector3 point) {
        Vector3 closest = Closest(point);
        Vector3 result = (point - closest).normalized;
        return result;
    }


    public void Process(Transform transform, Rigidbody rigidbody) {
        if ((transform == null) || (rigidbody == null)) return;
        Vector3 position = transform.position;
        Vector3 targetDir = (position - _vertices.Closest(position)).normalized;
        rigidbody.AddForce(targetDir * gravity * rigidbody.mass);
    }
}
