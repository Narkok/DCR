using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class WeaponController: MonoBehaviour {

    private List<WeaponAttachPoint> _attachPoints;
    private List<WeaponAttachPoint> _freeAttachPoints;

    private AttachedWeapon _machineGun;
    private AttachedWeapon _selectedWeapon;
    private List<AttachedWeapon> _weapons = new List<AttachedWeapon>();


    private void Start() {
        _attachPoints = transform.GetComponentsInChildren<WeaponAttachPoint>().ToList();
        _freeAttachPoints = _attachPoints;
        _weapons.Clear();
        _selectedWeapon = null;
        SetupMG();
    }


    private void Update() {
        if (InputManager.isActive(InputManager.LClick))
            _machineGun.weapon.Shoot();

        if (InputManager.isActive(InputManager.RClick)) {
            if (_selectedWeapon != null) {
                _selectedWeapon.weapon.Shoot();

                if (_selectedWeapon.weapon.IsEmpty) {
                    _freeAttachPoints.Add(_selectedWeapon.attachPoint);
                    _weapons.Remove(_selectedWeapon);
                    Destroy(_selectedWeapon.weapon.gameObject);
                    _selectedWeapon = _weapons.Any() ? _weapons.First() : null;
                }
            }
        }
    }


    public bool SetWeapon(WeaponType weaponType) {
        AttachedWeapon attachedWeapon = Contains(weaponType);
        if (attachedWeapon != null) return attachedWeapon.weapon.AmmoIncrease();
        if (!_freeAttachPoints.Any()) return false;

        WeaponAttachPoint attachPoint = _freeAttachPoints[Random.Range(0, _freeAttachPoints.Count)];
        _freeAttachPoints.Remove(attachPoint);
        Weapon weapon = GOManager.Create(weaponType.WeaponPath(), attachPoint.transform).GetComponent<Weapon>();
        weapon.FirePointRotation = -attachPoint.transform.localRotation.eulerAngles.z;
        FirePoint firePoint = weapon.GetComponentInChildren<FirePoint>();
        weapon.GroundDistance = CalculateGroundDistance(firePoint.transform);
        weapon.SetType(weaponType);
        attachedWeapon = new AttachedWeapon(weapon, attachPoint);
        _weapons.Add(attachedWeapon);
        if (_selectedWeapon == null) { _selectedWeapon = attachedWeapon; }
        return true;
    }


    private void SetupMG() {
        WeaponAttachPoint attachPoint = _freeAttachPoints[Random.Range(0, _freeAttachPoints.Count)];
        _freeAttachPoints.Remove(attachPoint);
        Weapon weapon = GOManager.Create(WeaponType.MachineGun.WeaponPath(), attachPoint.transform).GetComponent<Weapon>();
        weapon.FirePointRotation = -attachPoint.transform.localRotation.eulerAngles.z;
        FirePoint firePoint = weapon.GetComponentInChildren<FirePoint>();
        weapon.GroundDistance = CalculateGroundDistance(firePoint.transform);
        _machineGun = new AttachedWeapon(weapon, attachPoint);
        _machineGun.weapon.SetType(WeaponType.MachineGun);
    }


    private float CalculateGroundDistance(Transform point) {
        Ray ray = new Ray(point.position, -point.up);
        RaycastHit hit;
        LayerMask mask = LayerMask.GetMask(Arena.Name);
        Physics.Raycast(ray, out hit, Mathf.Infinity, mask);
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
