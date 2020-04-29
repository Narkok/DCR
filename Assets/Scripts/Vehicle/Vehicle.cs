using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WheelController))]

public class Vehicle: MonoBehaviour {

    [SerializeField] bool isPlayer = true;
    public bool IsPlayer {
        get { return isPlayer; }
        set { 
            isPlayer = value; 
            _wheelController.IsPlayer = isPlayer;
        }
    }

    private WheelController _wheelController;
    private WeaponController _weaponController;
    private GameObject _blumb;

    public BlumbType BlumbType { get { return _blumbType; } }
    [SerializeField] private BlumbType _blumbType;

    public VehicleType VehicleType { get { return _vehicleType; } }
    [SerializeField] private VehicleType _vehicleType;


    public int HP { get { return hp; } }
    public int MaxHP { get { return _vehicleType.MaxHP(); } }
    [SerializeField] private int hp;

    public float Boost { get { return boost; } }
    private float boost { 
        get { return _wheelController.Boost; } 
        set { _wheelController.Boost = value; }
    }
    

    public void Setup(Data data, Arena.Location location) {
        isPlayer = data.isPlayer;
        _blumbType = data.blumb;
        _vehicleType = data.vehicle;
        transform.position = location.point;
        transform.up = location.normal;
        hp = _vehicleType.MaxHP();
        _weaponController = GetComponent<WeaponController>();
        SetupBlumb();
        SetupWheelController();
    }


    public void Damage(int damage) {
        hp -= damage;
        if (hp <= 0) {
            Destroy(_blumb);
            SceneManager.Shared.Destroy(this);
        }
    }


    private void SetupWheelController() {
        _wheelController = GetComponent<WheelController>();
        _wheelController.IsPlayer = isPlayer;
        _wheelController.maxSpeed = _vehicleType.MaxSpeed();
    }


    private void SetupBlumb() {
        if (_blumbType == BlumbType.None) return;
        _blumb = GOManager.Create("Blumbs/Blumb", SceneManager.Shared.BlumbContainer);
        _blumb.GetComponent<Blumb>().Set(this, _blumbType);
    }


    public bool SetWeapon(WeaponType weaponType) {
        return _weaponController.SetWeapon(weaponType);
    }


    [System.Serializable]
    public struct Data {
        public bool isPlayer;
        public VehicleType vehicle;
        public BlumbType blumb;
    }
}
