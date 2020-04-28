using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Ammo: MonoBehaviour {

    [SerializeField] public WeaponType weaponType;
    private TrailRenderer _trail;
    private float _trailInitialTime;

    /// Штука, чтобы не ранить самого себя
    public int parentID = 0;


    private void Awake() {
        StartCoroutine(ClearParent());
        StartCoroutine(SelfDestruction());
        _trail = GetComponentInChildren<TrailRenderer>();
        _trailInitialTime = _trail.time;
        _trail.time = 0;
    }


    private void Update() {
        _trail.time = Mathf.Min(_trail.time + 0.2f * Time.deltaTime, _trailInitialTime);
    }


    private void OnTriggerEnter(Collider other) {
        if (parentID == other.gameObject.GetInstanceID()) return;
        Vehicle vehicle = other.GetComponent<Vehicle>();
        if (vehicle == null) return; 
        vehicle.Damage(weaponType.Damage());

        vehicle.GetComponent<Rigidbody>()
            .AddForce((transform.forward - transform.up / 2) * 8000, ForceMode.Impulse);

        GOManager.Create(weaponType.ExplosionPath(), SceneManager.Shared.ExplosionContainer)
            .GetComponent<Explosion>()
            .Setup(transform.position, weaponType.ExplosionTime());

        Destroy(gameObject);
    }
    

    private IEnumerator ClearParent() {
        yield return new WaitForSeconds(0.2f);
        parentID = 0;
    }


    private IEnumerator SelfDestruction() {
        yield return new WaitForSeconds(weaponType.LifeTime());
        Destroy(gameObject);
    }
}
