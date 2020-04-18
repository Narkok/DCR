using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arena: MonoBehaviour {

    [SerializeField] private GravityProcessor gravityProcessor;
    public GravityProcessor GravityProcessor { get { return gravityProcessor; } }

    public Location RandomLocation { get { return GenerateLocation(); } }

    private Mesh _mesh;


    private void Awake() {
        _mesh = GetComponent<MeshFilter>().sharedMesh;
    }


    private Location GenerateLocation() {
        Vector3 surfacePoint = transform.TransformPoint(_mesh.vertices.RandomElement());
        Vector3 closestGPPoint = gravityProcessor.Closest(surfacePoint);
        Vector3 normal = (closestGPPoint - surfacePoint).normalized;
        Vector3 point = surfacePoint;
        return new Location(point, normal);
    }


    public struct Location {
        public Vector3 point;
        public Vector3 normal;

        public Location(Vector3 point, Vector3 normal) {
            this.point = point;
            this.normal = normal;
        }
    }
}
