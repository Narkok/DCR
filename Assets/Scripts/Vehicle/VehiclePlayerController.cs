using UnityEngine;

public class VehiclePlayerController: MonoBehaviour {

    private WheelController _wheelController;
    private WeaponController _weaponController;
    private PlayerInput _input;


    private void Awake() {
        _wheelController = GetComponent<WheelController>();
        _weaponController = GetComponent<WeaponController>();
        _input = new PlayerInput();
    }


    private void OnEnable()
    {
        _input.Enable();
        _input.Player.NextWeapon.performed += ctx => NextWeapon();
    }


    private void OnDisable()
    {
        _input.Player.NextWeapon.performed -= ctx => NextWeapon();
        _input.Disable();
    }


    private void NextWeapon()
    {
        _weaponController.NextWeapon();
    }


    private void Update() {
        /// Handbrake
        _wheelController.Handbrake = _input.Player.Break.ReadValue<float>() > 0.5;

        /// Accelerate & brake
        _wheelController.Throttle = _input.Player.Acceleration.ReadValue<float>() - _input.Player.Back.ReadValue<float>();

        /// Boost
        _wheelController.boosting = _input.Player.Boost.ReadValue<float>() > 0.5;

        /// Turn
        _wheelController.Steering = _input.Player.Turn.ReadValue<float>();

        /// Dirft
        _wheelController.Drift = false;

        /// Jump
        _wheelController.jumping = _input.Player.Jump.ReadValue<float>() > 0.5;

        /// Shoot
        if (_input.Player.LightShoot.ReadValue<float>() > 0.5) _weaponController.ShootFromMachineGun();
        if (_input.Player.HeavyShoot.ReadValue<float>() > 0.5) _weaponController.ShootFromSelectedWeapon();
    }
}
