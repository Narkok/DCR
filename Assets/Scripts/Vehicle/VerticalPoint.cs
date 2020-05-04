using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPoint: MonoBehaviour {

    private Transform _camera;
    [SerializeField] private Transform _hpPoint;

    private float maxDistance = 80;
    private float minDistance = 2;
    private float maxScale = 10;
    private float minScale = 1;
    

    private void Awake() {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }


    void Update() {
        transform.rotation = _camera.rotation;
        float distance = Vector3.Distance(transform.position, _camera.position);
        bool isActive = distance < maxDistance && distance > minDistance;
        _hpPoint.gameObject.SetActive(isActive);
        if (!isActive) return;
        float delta = (maxScale - minScale) / (maxDistance - minDistance);
        float scale = (distance - minDistance) * delta + minScale;
        _hpPoint.localScale = new Vector3(scale, scale, scale);
    }
}
