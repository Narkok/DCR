using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum WeaponType {
    MachineGun,
    Cannon,
    ProtoGun
}


public enum AmmoType {
    Crawling,
    Following,
    Straight
}


public static class WeaponExtension {

    public static WeaponType RandomWeapon() {
        return List.RandomElement();
    }


    public static List<WeaponType> List {
        get {
            return new List<WeaponType>() {
                WeaponType.Cannon,
                WeaponType.ProtoGun
            };
        }
    }


    public static float CoolDownTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 0.4f; }
            case WeaponType.Cannon:     { return 1.0f; }
            case WeaponType.ProtoGun:   { return 1.5f; }
            default: return 0f;
        }
    }


    public static string WeaponPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Weapons/MachineGun"; }
            case WeaponType.Cannon:     { return "Weapons/Cannon"; }
            case WeaponType.ProtoGun:   { return "Weapons/ProtoGun"; }
            default: return "";
        }
    } 


    public static string AmmoPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Ammunition/MGBullet"; }
            case WeaponType.Cannon:     { return "Ammunition/RedMissile"; }
            case WeaponType.ProtoGun:   { return "Ammunition/RedMissile"; }
            default: return "";
        }
    }


    public static AmmoType AmmoType(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return global::AmmoType.Crawling; }
            case WeaponType.Cannon:     { return global::AmmoType.Crawling; }
            case WeaponType.ProtoGun:   { return global::AmmoType.Crawling; }
            default: return global::AmmoType.Crawling;
        }
    }


    public static string EquipmentPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return ""; }
            case WeaponType.Cannon:     { return "Equipment/RedMissile"; }
            case WeaponType.ProtoGun:   { return "Equipment/ProtoGun"; }
            default: return "";
        }
    }


    public static bool IsCrawling(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return true; }
            case WeaponType.Cannon:     { return true; }
            case WeaponType.ProtoGun:   { return true; }
            default: return true;
        }
    }


    public static float LifeTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 4f; }
            case WeaponType.Cannon:     { return 5f; }
            case WeaponType.ProtoGun:   { return 15f; }
            default: return 0f;
        }
    }


    public static float Speed(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 70f; }
            case WeaponType.Cannon:     { return 60f; }
            case WeaponType.ProtoGun:   { return 50f; }
            default: return 0f;
        }
    }


    public static int AmmoInitialCount(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return -1; }
            case WeaponType.Cannon:     { return 8; }
            case WeaponType.ProtoGun:   { return 5; }
            default: return 0;
        }
    }

    
    public static int AmmoMaxCount(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return -1; }
            case WeaponType.Cannon:     { return 20; }
            case WeaponType.ProtoGun:   { return 14; }
            default: return 0;
        }
    }

    public static int Damage(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 2; }
            case WeaponType.Cannon:     { return 10; }
            case WeaponType.ProtoGun:   { return 16; }
            default: return 0;
        }
    }


    public static string ExplosionPath(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return "Explosions/Sparks"; }
            case WeaponType.Cannon:     { return "Explosions/Sparks"; }
            case WeaponType.ProtoGun:   { return "Explosions/Sparks"; }
            default: return "";
        }
    } 


    public static float ExplosionTime(this WeaponType type) {
        switch (type) {
            case WeaponType.MachineGun: { return 2; }
            case WeaponType.Cannon:     { return 2; }
            case WeaponType.ProtoGun:   { return 2; }
            default: return 0;
        }
    } 
}
