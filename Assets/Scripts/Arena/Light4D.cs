using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Light4D: MonoBehaviour {

    private void Awake() {
        List<Vector3> points = new List<Vector3>() {
            new Vector3(0.0f, -0.3f, 0.9f),
            new Vector3(0.8f, -0.3f, -0.5f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(-0.8f, -0.3f, -0.5f)
        };

        foreach (Vector3 point in points) {
            GameObject light = GOManager.Create("Arenas/DirectionalLight", transform);
            light.transform.forward = point;
        }
    }
}
