using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlumbController : MonoBehaviour
{
    public BlumbType blumbType;

    void Awake() {
        initBlumb();
    }

    private void initBlumb() {
        if (blumbType == BlumbType.None) return;
        GameObject blumb = GOManager.Load("Blumb");
        WheelVehicle vehicle = GetComponent<WheelVehicle>();
        blumb.GetComponent<Blumb>().Set(vehicle, blumbType);
        Instantiate(blumb);
    }
}
