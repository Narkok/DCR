using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment: MonoBehaviour {

    [SerializeField] private WeaponType _type;
    public WeaponType Type { get { return _type; } }


    private void OnTriggerEnter(Collider other) {
        Vehicle vehicle = other.GetComponent<Vehicle>();
        if (vehicle == null) return; 
        if (!vehicle.SetWeapon(_type)) return;
        Destroy(gameObject);
    }


    public void Setup(Arena.Location location, WeaponType type) {
        _type = type;
        transform.position = location.point;
        transform.up = location.normal;
    }
}
