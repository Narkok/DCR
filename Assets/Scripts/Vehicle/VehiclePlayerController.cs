using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePlayerController: MonoBehaviour {

    private WheelController _wheelController;
    private WeaponController _weaponController;
    
    
    void Awake() {
        _wheelController = GetComponent<WheelController>();
        _weaponController = GetComponent<WeaponController>();
    }


    void Update() {
        /// Handbrake
        _wheelController.Handbrake = InputManager.isActive(InputManager.Break);

        /// Accelerate & brake
        _wheelController.Throttle = InputManager.Input(InputManager.Throttle) - InputManager.Input(InputManager.Break);

        /// Boost
        _wheelController.boosting = InputManager.isActive(InputManager.Boost);

        /// Turn
        _wheelController.Steering = InputManager.Input(InputManager.Turn);

        /// Dirft
        _wheelController.Drift = InputManager.isActive(InputManager.Drift) && (_wheelController.Speed > 20) || _wheelController.Handbrake;

        /// Jump
        _wheelController.jumping = InputManager.isActive(InputManager.Jump);

        /// Shoot
        if (InputManager.isActive(InputManager.LClick)) _weaponController.ShootFromMachineGun();
        if (InputManager.isActive(InputManager.RClick)) _weaponController.ShootFromSelectedWeapon();
        if (UnityEngine.Input.GetKeyDown(KeyCode.N)) _weaponController.NextWeapon();
    }
}
