using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleController : MonoBehaviour
{
    public BlumbType blumbType;

    void Awake() {
        initBlumb();
    }

    private void initBlumb() {
        if (blumbType == BlumbType.None) return;
        GameObject blumb = Generator.loadPrefab("Blumb");
        WheelVehicle vehicle = GetComponent<WheelVehicle>();
        blumb.GetComponent<Blumb>().Set(vehicle, blumbType);
        Instantiate(blumb);
    }
}


public class Generator {
    public static GameObject loadPrefab(string path) {
        return (GameObject)Resources.Load(path);
    }
}