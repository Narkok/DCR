using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleAIController: MonoBehaviour {

    private WheelController _wheelController;
    private WeaponController _weaponController;
    

    void Awake() {
        _wheelController = GetComponent<WheelController>();
        _weaponController = GetComponent<WeaponController>();
        StartCoroutine(Turn());
        StartCoroutine(MachineGunShot());
        StartCoroutine(SelectedWeaponShot());
    }


    private float steering = 1;


    void Update() {
        _wheelController.Throttle = 1;
        _wheelController.Steering = steering;
    }


    private IEnumerator Turn() {
        yield return new WaitForSeconds(Random.Range(1, 4));
        steering = Random.Range(-1f, 1f);
        StartCoroutine(Turn());
    }


    private IEnumerator MachineGunShot() {
        yield return new WaitForSeconds(Random.Range(1, 4));
        _weaponController.ShootFromMachineGun();
        StartCoroutine(MachineGunShot());
    }


    private IEnumerator SelectedWeaponShot() {
        yield return new WaitForSeconds(Random.Range(1, 4));
        _weaponController.ShootFromSelectedWeapon();
        StartCoroutine(SelectedWeaponShot());
    }
}
