using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ArenaType {
    Sphere,
    Torus,
    Infinity
}


public static class ArenaExtension {
    public static string ArenaPath(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:   { return "Arenas/Sphere"; }
            case ArenaType.Torus:    { return "Arenas/Torus"; }
            case ArenaType.Infinity: { return "Arenas/Infinity"; }
            default: return "";
        }
    }


    public static int EquipmentCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:   { return 30; }
            case ArenaType.Torus:    { return 50; }
            case ArenaType.Infinity: { return 50; }
            default: return 0;
        }
    }


    public static int StuffCount(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:   { return 5; }
            case ArenaType.Torus:    { return 4; }
            case ArenaType.Infinity: { return 5; }
            default: return 0;
        }
    }


    public static string MinimapPath(this ArenaType type) {
        switch (type) {
            case ArenaType.Sphere:   { return "Minimaps/SphereMinimap"; }
            case ArenaType.Torus:    { return "Minimaps/TorusMinimap"; }
            case ArenaType.Infinity: { return "Minimaps/InfinityMinimap"; }
            default: return "";
        }
    }


    public static Color MinimapColor(this ArenaType type) {
        Color black = new Color(0, 0, 0, 0.3f);
        Color light = new Color(1, 1, 1, 0.03f);

        switch (type) {
            case ArenaType.Sphere:   { return black; }
            case ArenaType.Torus:    { return black; }
            case ArenaType.Infinity: { return light; }
            default: return black;
        }
    }
}
