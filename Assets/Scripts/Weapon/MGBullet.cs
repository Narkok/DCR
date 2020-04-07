using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGBullet : MonoBehaviour
{
    public const WeaponType weaponType = WeaponType.MachineGun;

    private void Start() {
        StartCoroutine(SelfDestruction());
    }

    private IEnumerator SelfDestruction() {
        yield return new WaitForSeconds(weaponType.LifeTime());
        Destroy(gameObject);
    }
}
