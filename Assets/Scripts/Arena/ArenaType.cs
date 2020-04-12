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
}