using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlidingBody : MonoBehaviour
{
    private Collider _arenaCollider;
    private float _speed;

    void Awake() {
        _arenaCollider = SceneManager.Shared.Arena.GetComponent<Collider>();
        _speed = GetComponent<Ammo>().weaponType.Speed();
    }


    void FixedUpdate() {
    }
}
