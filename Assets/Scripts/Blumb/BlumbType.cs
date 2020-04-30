using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum BlumbType {
    None,
    Skull,
    Luci,
    Creeper,
    Cupcake
}


public static class BlumbTypeExtention {
    public static string Path(this BlumbType type) {
        switch (type) {
            case BlumbType.Skull: { return "Blumbs/Skull"; }
            case BlumbType.Luci: { return "Blumbs/Luci"; }
            case BlumbType.Creeper: { return "Blumbs/Creeper"; }
            case BlumbType.Cupcake: { return "Blumbs/Cupcake"; }
            default: return "";
        }
    }
}
