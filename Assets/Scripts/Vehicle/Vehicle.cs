using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(WheelController))]

public class Vehicle: MonoBehaviour {

    [SerializeField] ControlType controlType = ControlType.AI;
    public ControlType ControlType {
        get { return controlType; }
        set { controlType = value; }
    }

    private WheelController _wheelController;
    private WeaponController _weaponController;
    private GameObject _blumb;

    public BlumbType BlumbType { get { return _blumbType; } }
    [SerializeField] private BlumbType _blumbType;

    public string Nickname { get { return _nickname; } }
    [SerializeField] private string _nickname;

    public VehicleType VehicleType { get { return _vehicleType; } }
    [SerializeField] private VehicleType _vehicleType;


    public int HP { 
        get { return hp; }
        set { hp = value; } 
    }
    public int MaxHP { get { return _vehicleType.MaxHP(); } }
    private int hp;

    public float Boost { get { return boost; } }
    private float boost { 
        get { return _wheelController.Boost; } 
        set { _wheelController.Boost = value; }
    }
    

    public void Setup(Data data, Arena.Location location) {
        _blumbType = data.blumbType;
        _vehicleType = data.vehicleType;
        _nickname = data.nickname;
        transform.position = location.point;
        transform.up = location.normal;
        hp = _vehicleType.MaxHP();
        _weaponController = GetComponent<WeaponController>();
        SetupControllType(data.controlType);
        SetupBlumb();
        SetupWheelController();
    }


    public void Damage(int damage) {
        HP -= damage;
        if (HP <= 0) {
            Destroy(_blumb);
            SceneManager.Shared.Destroy(this);
        }
    }


    private void SetupWheelController() {
        _wheelController = GetComponent<WheelController>();
        _wheelController.ControlType = ControlType;
        _wheelController.maxSpeed = _vehicleType.MaxSpeed();
    }


    private void SetupControllType(ControlType controlType) {
        ControlType = controlType;
        
        if (controlType.isAI()) {
            gameObject.AddComponent(typeof(VehicleAIController));
            return;
        }

        if (controlType.isPlayer()) {
            gameObject.AddComponent(typeof(VehiclePlayerController));
            return;
        }
    }


    private void SetupBlumb() {
        if (_blumbType == BlumbType.None) return;
        _blumb = GOManager.Create("Blumbs/Blumb", SceneManager.Shared.BlumbContainer);
        _blumb.GetComponent<Blumb>().Set(this, _blumbType);
    }


    public bool SetWeapon(WeaponType weaponType) {
        return _weaponController.SetWeapon(weaponType);
    }


    public bool SetStuff(StuffType stuffType) {
        switch (stuffType) {
            case StuffType.SmallKit: { 
                if (HP == MaxHP) return false;
                HP = Mathf.Clamp(hp + StuffExtention.SmallKitHPSize, 0, MaxHP);
                return true;
            }

            case StuffType.LargeKit: { 
                if (HP == MaxHP) return false;
                HP = Mathf.Clamp(hp + StuffExtention.LargeKitHPSize, 0, MaxHP);
                return true;
            }

            case StuffType.Nitro:    {
                if (boost == VehicleExtention.MaxBoost) return false;
                boost = Mathf.Clamp(boost + StuffExtention.NitroSize, 0, VehicleExtention.MaxBoost);
                return true;
            }
        }
        return true;
    }


    [System.Serializable]
    public struct Data {
        public ControlType controlType;
        public VehicleType vehicleType;
        public BlumbType blumbType;
        public string nickname;
    }
}
