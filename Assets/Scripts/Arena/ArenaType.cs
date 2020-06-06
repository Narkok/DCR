using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArenaType {
    Sphere,
    Torus,
    Eternal
}


public static class ArenaExtension {
    public static string ArenaPath(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:  { return "Arenas/Sphere"; }
            case ArenaType.Torus:   { return "Arenas/Torus"; }
            case ArenaType.Eternal: { return "Arenas/Eternal"; }
            default: return "";
        }
    }


    public static int EquipmentCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:  { return 30; }
            case ArenaType.Torus:   { return 50; }
            case ArenaType.Eternal: { return 50; }
            default: return 0;
        }
    }


    public static int StuffCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:  { return 5; }
            case ArenaType.Torus:   { return 4; }
            case ArenaType.Eternal: { return 5; }
            default: return 0;
        }
    }


    public static string MinimapPath(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere: { return "Minimaps/SphereMinimap"; }
            case ArenaType.Torus: { return "Minimaps/TorusMinimap"; }
            case ArenaType.Eternal: { return "Minimaps/EternalMinimap"; }
            default: return "";
        }
    }
}
