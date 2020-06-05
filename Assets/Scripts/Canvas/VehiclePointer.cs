using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VehiclePointer: MonoBehaviour {

    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Image _image;

    public float minimapScale;
    

    void Awake() {
        _image = GetComponent<Image>();
        _rectTransform = GetComponent<RectTransform>();
    }


    public void Set(VehicleType vehicleType) {
        _image.color = vehicleType.PointerColor();
    }


    public void UpdatePosition(Transform vehicle) {
        float x = vehicle.position.x * minimapScale;
        float y = vehicle.position.z * minimapScale;
        _rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
