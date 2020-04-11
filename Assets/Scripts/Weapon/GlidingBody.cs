using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Ammo))]
public class GlidingBody: MonoBehaviour {

    private float _speed;
    private Transform _transform;


    void Awake() {
        _speed = GetComponent<Ammo>().weaponType.Speed();
        _transform = transform;
    }


    void FixedUpdate() {
        Vector3 closestGravityPoint = SceneManager.Shared.Arena.GravityProcessor.Closest(_transform.position);
        float distance = Vector3.Distance(_transform.position, closestGravityPoint);
        float angle = distance * _speed / 400;
        Vector3 curVector = _transform.position - closestGravityPoint;
        Vector3 newVector = (curVector + _transform.forward * distance * Mathf.Tan(angle)).normalized * distance;
        transform.position = newVector + closestGravityPoint;
        transform.forward = transform.forward * Mathf.Cos(angle) - curVector.normalized * Mathf.Sin(angle);
    }
}
