using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Stuff: MonoBehaviour {

    [SerializeField] private StuffType _type;
    public StuffType Type { get { return _type; } }


    private void OnTriggerEnter(Collider other) {
        Vehicle vehicle = other.GetComponent<Vehicle>();
        if (vehicle == null) return; 
        if (!vehicle.SetStuff(_type)) return;
        SceneManager.Shared.Destroy(this);
    }


    public void Setup(Arena.Location location, StuffType type) {
        _type = type;
        transform.position = location.point;
        transform.up = location.normal;
    }
}
