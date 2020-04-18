using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType {
    MachineGun,
    Cannon
}


public static class WeaponTypeExtension {

    public static WeaponType RandomWeapon() {
        return List.RandomElement();
    }


    public static List<WeaponType> List {
        get {
            return new List<WeaponType>() {
                WeaponType.Cannon
            };
        }
    }


    public static float CoolDownTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 0.4f; }
            case WeaponType.Cannon:     { return 1.0f; }
            default: return 0f;
        }
    }


    public static string WeaponPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Weapons/MachineGun"; }
            case WeaponType.Cannon:     { return "Weapons/Cannon"; }
            default: return "";
        }
    } 


    public static string AmmoPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Ammunition/MGBullet"; }
            case WeaponType.Cannon:     { return "Ammunition/RedMissile"; }
            default: return "";
        }
    }


    public static string EquipmentPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return ""; }
            case WeaponType.Cannon:     { return "Equipment/RedMissile"; }
            default: return "";
        }
    }


    public static float LifeTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 4f; }
            case WeaponType.Cannon:     { return 5f; }
            default: return 0f;
        }
    }


    public static float Speed(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 0.05f; }
            case WeaponType.Cannon:     { return 0.04f; }
            default: return 0f;
        }
    }


    public static int AmmoInitialCount(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return -1; }
            case WeaponType.Cannon:     { return 8; }
            default: return 0;
        }
    }

    
    public static int AmmoMaxCount(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return -1; }
            case WeaponType.Cannon:     { return 20; }
            default: return 0;
        }
    }
}
