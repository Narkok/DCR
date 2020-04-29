using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StuffType {
    SmallKit,
    LargeKit,
    Nitro
}


public static class StuffTypeExtention {

    public static StuffType RandomWeapon() {
        return List.RandomElement();
    }


    public static List<StuffType> List {
        get {
            return new List<StuffType>() {
                StuffType.LargeKit,
                StuffType.SmallKit,
                StuffType.Nitro
            };
        }
    }

    public static string Path(this StuffType type) {
        switch (type) {
            case StuffType.SmallKit: { return "Stuff/SmallKit"; }
            case StuffType.LargeKit: { return "Stuff/LargeKit"; }
            case StuffType.Nitro:    { return "Stuff/Nitro"; }
            default: return "";
        }
    }
}
