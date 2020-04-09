using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private List<WeaponAttachPoint> attachPoints;
    private List<WeaponAttachPoint> freeAttachPoints;
    private Weapon machineGun;
    //private Weapon selectedWeapon;
    //private List<Weapon> weapons;
    

    private void Awake() {
        attachPoints = transform.GetComponentsInChildren<WeaponAttachPoint>().ToList();
        freeAttachPoints = attachPoints;
        //weapons = new List<Weapon>();
        //selectedWeapon = null;
        SetupMG();
    }


    void SetupMG() {
        WeaponAttachPoint attachPoint = freeAttachPoints[Random.Range(0, freeAttachPoints.Count)];
        freeAttachPoints.Remove(attachPoint);
        GameObject weapon = GOManager.Create(WeaponType.MachineGun.WeaponPath(), attachPoint.transform);
        machineGun = weapon.GetComponent<Weapon>();
        machineGun.SetType(WeaponType.MachineGun);
    }


    void Update() {
        if (InputManager.isActive(InputManager.LClick)) {
            machineGun.Shoot();
        }
    }


    //void SetWeapon(WeaponType weaponType) {
    //    if (!freeAttachPoints.Any()) return;
    //    WeaponAttachPoint attachPoint = freeAttachPoints[Random.Range(0, freeAttachPoints.Count)];
    //    freeAttachPoints.Remove(attachPoint);
    //    GameObject weapon_GO = GOManager.Create(weaponType.WeaponPath(), attachPoint.transform);
    //    Weapon weapon = weapon_GO.GetComponent<Weapon>();
    //    weapon.SetType(weaponType);
    //    if (selectedWeapon == null) { selectedWeapon = weapon; }
    //}


    void Start() {
    }

}