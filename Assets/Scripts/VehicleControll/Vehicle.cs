using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WheelVehicle))]

public class Vehicle: MonoBehaviour {

    [SerializeField] bool isPlayer = true;
    public bool IsPlayer {
        get { return isPlayer; }
        set { isPlayer = value; }
    }

    [SerializeField] public BlumbType blumbType;

    private WheelVehicle _wheelController;
    private WeaponController _weaponController;


    public void Setup(Data data, Arena.Location location) {
        isPlayer = data.isPlayer;
        blumbType = data.blumb;
        transform.position = location.point;
        transform.up = location.normal;
        _weaponController = GetComponent<WeaponController>();
        SetupBlumb();
        SetupWheelController();
    }


    private void SetupWheelController() {
        _wheelController = GetComponent<WheelVehicle>();
        _wheelController.IsPlayer = isPlayer;
    }


    private void SetupBlumb() {
        if (blumbType == BlumbType.None) return;
        GOManager.Create("Blumbs/Blumb")
            .GetComponent<Blumb>()
            .Set(this, blumbType);
    }


    public bool SetWeapon(WeaponType weaponType) {
        return _weaponController.SetWeapon(weaponType);
    }


    [System.Serializable]
    public struct Data {
        public bool isPlayer;
        public VehicleType vehicle;
        public BlumbType blumb;
    }
}



public enum VehicleType {
    Delorian,
    Shelby
}


public static class VehicleTypeExtention {
    public static string Path(this VehicleType type) {
        switch (type) {
            case VehicleType.Delorian: { return "Vehicles/Delorian"; }
            case VehicleType.Shelby: { return "Vehicles/Shelby"; }
            default: return "";
        }
    }
}
