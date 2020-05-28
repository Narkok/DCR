using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalPoint: MonoBehaviour {

    private Transform _camera;
    [SerializeField] private Transform _canvas;

    private float maxDistance = 80;
    private float minDistance = 2;
    private float maxScale = 10;
    private float minScale = 1;
    private float delta;

    private float canvasInitialHeight;
    private float canvasMaxHeight = 4;


    private void Awake() {
        _camera = GameObject.FindGameObjectWithTag("MainCamera").transform;

        canvasInitialHeight = _canvas.transform.localPosition.y;
        delta = (maxScale - minScale) / (maxDistance - minDistance);
    }


    void Update() {
        transform.rotation = _camera.rotation;
        float distance = Vector3.Distance(transform.position, _camera.position);
        bool isActive = distance < maxDistance && distance > minDistance;
        _canvas.gameObject.SetActive(isActive);
        if (!isActive) return;
        float scale = (distance - minDistance) * delta + minScale;
        _canvas.localScale = new Vector3(scale, scale, scale);
        float yPos = canvasInitialHeight + canvasMaxHeight * (distance - minDistance) / (maxDistance - minDistance);
        _canvas.localPosition = new Vector3(0, yPos, 0);
    }
}
