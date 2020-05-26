using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WeaponController: MonoBehaviour {

    private bool isPlayer = false;

    private List<WeaponAttachPoint> _attachPoints;
    private List<WeaponAttachPoint> _freeAttachPoints;

    private AttachedWeapon _machineGun;
    public Weapon MachineGun { get { return _machineGun.weapon; } }

    private AttachedWeapon _selectedWeapon;
    public Weapon SelectedWeapon { get { return _selectedWeapon == null ? null : _selectedWeapon.weapon; } }

    private List<AttachedWeapon> _weapons = new List<AttachedWeapon>();
    public List<Weapon> Weapons {  get { return _weapons.Select(w => w.weapon).ToList(); } }

    private int _instanceID;


    private void Start() {
        isPlayer = GetComponent<Vehicle>().ControlType.isPlayer();
        _attachPoints = transform.GetComponentsInChildren<WeaponAttachPoint>().ToList();
        _freeAttachPoints = _attachPoints;
        _weapons.Clear();
        _selectedWeapon = null;
        _instanceID = gameObject.GetInstanceID();
        SetupMG();
    }


    public void ShootFromMachineGun() {
        MachineGun.Shoot();
    }


    public void ShootFromSelectedWeapon() {
        if (_selectedWeapon != null) {
            _selectedWeapon.weapon.Shoot();
            UpgradeWeaponInfo();

            if (_selectedWeapon.weapon.IsEmpty) {
                _freeAttachPoints.Add(_selectedWeapon.attachPoint);
                _weapons.Remove(_selectedWeapon);
                Destroy(_selectedWeapon.weapon.gameObject);
                _selectedWeapon = _weapons.Any() ? _weapons.First() : null;
                UpgradeWeaponInfo();
            }
        }
    }


    public bool SetWeapon(WeaponType weaponType) {
        AttachedWeapon attachedWeapon = Contains(weaponType);
        if (attachedWeapon != null) {
            bool result = attachedWeapon.weapon.AmmoIncrease();
            UpgradeWeaponInfo();
            return result;
        } 
        if (!_freeAttachPoints.Any()) return false;

        WeaponAttachPoint attachPoint = _freeAttachPoints[Random.Range(0, _freeAttachPoints.Count)];
        _freeAttachPoints.Remove(attachPoint);
        Weapon weapon = GOManager.Create(weaponType.WeaponPath(), attachPoint.transform).GetComponent<Weapon>();
        weapon.FirePointRotation = -attachPoint.transform.localRotation.eulerAngles.z;
        weapon.Setup(weaponType, _instanceID);
        attachedWeapon = new AttachedWeapon(weapon, attachPoint);
        _weapons.Add(attachedWeapon);
        if (_selectedWeapon == null) { _selectedWeapon = attachedWeapon; }
        UpgradeWeaponInfo();
        return true;
    }


    private void UpgradeWeaponInfo() {
        if (!isPlayer) return;
        GameCanvasManager.Shared.WeaponInfo = SelectedWeapon == null ? "" : (SelectedWeapon.Type.Name() + ": " + SelectedWeapon.AmmoCount);
    }


    public void NextWeapon() {
        int currentWeaponNum = -1;
        for (int i = 0; i < _weapons.Count; i++) {
            if (_selectedWeapon.weapon.Type == _weapons[i].weapon.Type) {
                currentWeaponNum = i;
                break;
            }
        }
        if (currentWeaponNum == -1) return;
        int nextWeaponNum = (currentWeaponNum + 1) % _weapons.Count;
        _selectedWeapon = _weapons[nextWeaponNum];
        UpgradeWeaponInfo();
    }

    private void SetupMG() {
        WeaponAttachPoint attachPoint = _freeAttachPoints[Random.Range(0, _freeAttachPoints.Count)];
        _freeAttachPoints.Remove(attachPoint);
        Weapon weapon = GOManager.Create(WeaponType.MachineGun.WeaponPath(), attachPoint.transform).GetComponent<Weapon>();
        weapon.FirePointRotation = -attachPoint.transform.localRotation.eulerAngles.z;
        _machineGun = new AttachedWeapon(weapon, attachPoint);
        _machineGun.weapon.Setup(WeaponType.MachineGun, _instanceID);
    }


    private float CalculateGroundDistance(Transform point) {
        Ray ray = new Ray(point.position, -point.up);
        RaycastHit hit;
        Physics.Raycast(ray, out hit, Mathf.Infinity, Arena.Mask);
        return hit.distance;
    }


    private AttachedWeapon Contains(WeaponType weaponType) {
        return _weapons.Find(item => item.weapon.Type == weaponType);
    }


    private class AttachedWeapon {
        public Weapon weapon;
        public WeaponAttachPoint attachPoint;

        public AttachedWeapon(Weapon weapon, WeaponAttachPoint attachPoint) {
            this.weapon = weapon;
            this.attachPoint = attachPoint;
        }
    }
}
