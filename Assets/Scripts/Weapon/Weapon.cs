using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon : MonoBehaviour
{
    private bool isAvalaible = true;
    public WeaponType type;

    public int AmmoCount = 0;



    private void Awake() {
    }


    public void SetType(WeaponType type) {
        this.type = type;
        AmmoCount = type.AmmoInitialCount();
    }


    public void Shoot() {
        if (!isAvalaible) { return; }
        isAvalaible = false;
        GameObject bullet = GOManager.Create(type.AmmoPath());
        Transform firePoint = GetComponentInChildren<FirePoint>().transform;
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        AmmoCount--;
        StartCoroutine(CoolDown());
    }


    private IEnumerator CoolDown() {
        yield return new WaitForSeconds(type.CoolDownTime());
        isAvalaible = true;
    }
}
