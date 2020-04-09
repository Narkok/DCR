using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlidingBody : MonoBehaviour
{
    private Collider _arenaCollider;
    private float _speed;

    private GameObject aa;

    void Awake() {
        _arenaCollider = SceneManager.Shared.Arena.GetComponent<Collider>();
        _speed = GetComponent<Ammo>().weaponType.Speed();
    }


    void FixedUpdate() {
        if (aa != null) { Destroy(aa); }
        Vector3 closestPoint = _arenaCollider.ClosestPointOnBounds(transform.position);//.ClosestPoint//(transform.position);
        aa = GOManager.Create(WeaponType.MachineGun.AmmoPath());
        aa.transform.position = closestPoint;
    }
}
