using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum VehicleType {
    Delorian,
    Shelby,
    Monster
}


public static class VehicleTypeExtention {
    public static float MaxBoost = 8f;

    public static string Path(this VehicleType type) {
        switch (type) {
            case VehicleType.Delorian: { return "Vehicles/Delorian"; }
            case VehicleType.Shelby:   { return "Vehicles/Shelby"; }
            case VehicleType.Monster:  { return "Vehicles/Monster"; }
            default: return "";
        }
    }

    public static int MaxHP(this VehicleType type) {
        switch (type) {
            case VehicleType.Delorian: { return 100; }
            case VehicleType.Shelby:   { return 120; }
            case VehicleType.Monster:  { return 160; }
            default: return 0;
        }
    }

    public static int MaxSpeed(this VehicleType type) {
        switch (type) {
            case VehicleType.Delorian: { return 120; }
            case VehicleType.Shelby:   { return 100; }
            case VehicleType.Monster:  { return 100; }
            default: return 0;
        }
    }
}
