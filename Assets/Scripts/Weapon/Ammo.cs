using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Ammo))]

public class Ammo: MonoBehaviour {

    [SerializeField] public WeaponType weaponType;


    private void Start() {
        StartCoroutine(SelfDestruction());
    }


    private IEnumerator SelfDestruction() {
        yield return new WaitForSeconds(weaponType.LifeTime());
        Destroy(gameObject);
    }
}
