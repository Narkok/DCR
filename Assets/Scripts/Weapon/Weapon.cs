using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Weapon: MonoBehaviour {

    private bool isAvalaible = false;

    private WeaponType _type;
    public WeaponType Type { get { return _type; } }

    [SerializeField] private int _ammoCount = 0;
    public bool IsEmpty { get { return _ammoCount == 0; } }


    public void SetType(WeaponType type) {
        _type = type;
        _ammoCount = type.AmmoInitialCount();
        StartCoroutine(CoolDown());
    }


    public void Shoot() {
        if (!isAvalaible) { return; }
        isAvalaible = false;
        GameObject bullet = GOManager.Create(_type.AmmoPath());
        Transform firePoint = GetComponentInChildren<FirePoint>().transform;
        bullet.transform.position = firePoint.position;
        bullet.transform.rotation = firePoint.rotation;
        _ammoCount--;
        StartCoroutine(CoolDown());
    }


    private IEnumerator CoolDown() {
        yield return new WaitForSeconds(_type.CoolDownTime());
        isAvalaible = true;
    }


    public bool AmmoIncrease() {
        if (_ammoCount == _type.AmmoMaxCount()) { return false; }
        _ammoCount = Mathf.Min(_type.AmmoMaxCount(), _ammoCount + _type.AmmoInitialCount());
        return true;
    }
}
