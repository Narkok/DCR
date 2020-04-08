using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArenaType {
    TestSphere
}


public static class ArenaTypeExtension {
    public static string ArenaPath(this ArenaType type) {
        switch (type) {
            case ArenaType.TestSphere: { return "Arenas/Sphere"; }
            default: return "";
        }
    } 
}