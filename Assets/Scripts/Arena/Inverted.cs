using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshCollider))]
//[ExecuteInEditMode]
public class Inverted: MonoBehaviour {
    
    void Awake() {
        Mesh mesh = GetComponent<MeshFilter>().mesh;

        Vector3[] normals = mesh.normals;
        for(int i = 0; i < normals.Length; i++) {
            normals[i] = -normals[i];
        }
        mesh.normals = normals;

        int[] triangles = mesh.triangles;
        for (int i = 0; i < triangles.Length; i+=3) {
            int t = triangles[i];
            triangles[i] = triangles[i + 2];
            triangles[i + 2] = t;
        }           

        mesh.triangles = triangles;
        GetComponent<MeshCollider>().sharedMesh = mesh;
    }
}
