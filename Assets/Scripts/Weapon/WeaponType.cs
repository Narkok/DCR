using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType {
    MachineGun
}


public static class WeaponTypeExtension {
    public static float CoolDownTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 0.5f; }
            default: return 0f;
        }
    }


    public static string WeaponPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Weapons/MachineGun"; }
            default: return "";
        }
    } 


    public static string AmmoPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Ammunition/MGBullet"; }
            default: return "";
        }
    }


    public static float LifeTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 4f; }
            default: return 0f;
        }
    }


    public static float Speed(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 0.05f; }
            default: return 0f;
        }
    }


    public static int AmmoInitialCount(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return -1; }
            default: return 0;
        }
    }
}