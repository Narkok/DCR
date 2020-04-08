using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlumbController: MonoBehaviour
{
    public BlumbType blumbType;

    void Awake() {
        if (blumbType == BlumbType.None) return;
        GameObject blumb = GOManager.Load("Blumbs/Blumb");
        WheelVehicle vehicle = GetComponent<WheelVehicle>();
        blumb.GetComponent<Blumb>().Set(vehicle, blumbType);
        Instantiate(blumb);
    }
}
