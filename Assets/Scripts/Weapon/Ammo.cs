using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ammo: MonoBehaviour {

    [SerializeField] public WeaponType weaponType;
    private TrailRenderer _trail;
    private float _trailInitialTime;


    private void Awake() {
        StartCoroutine(SelfDestruction());
        _trail = GetComponentInChildren<TrailRenderer>();
        _trailInitialTime = _trail.time;
        _trail.time = 0;
    }


    void Update() {
        _trail.time = Mathf.Min(_trail.time + 0.2f * Time.deltaTime, _trailInitialTime);
    }


    private IEnumerator SelfDestruction() {
        yield return new WaitForSeconds(weaponType.LifeTime());
        Destroy(gameObject);
    }
}
