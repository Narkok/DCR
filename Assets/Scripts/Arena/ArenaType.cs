using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArenaType {
    Sphere,
    Torus
}


public static class ArenaTypeExtension {
    public static string ArenaPath(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere: { return "Arenas/Sphere"; }
            case ArenaType.Torus: { return "Arenas/Torus"; }
            default: return "";
        }
    }

    public static int WeaponsCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere: { return 30; }
            case ArenaType.Torus: { return 50; }
            default: return 0;
        }
    }

    public static int AidKitCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere: { return 2; }
            case ArenaType.Torus: { return 3; }
            default: return 0;
        }
    }

    public static int NitroCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere: { return 2; }
            case ArenaType.Torus: { return 3; }
            default: return 0;
        }
    }
}
