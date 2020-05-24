using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehiclePlayerController: MonoBehaviour {

    private WheelController _wheelController;
    
    
    void Awake() {
        _wheelController = GetComponent<WheelController>();
    }


    void Update() {
        _wheelController.Handbrake = InputManager.isActive(InputManager.Break);
            // Accelerate & brake
        _wheelController.Throttle = InputManager.Input(InputManager.Throttle) - InputManager.Input(InputManager.Break);
            // Boost
        _wheelController.boosting = InputManager.isActive(InputManager.Boost);
            // Turn
        _wheelController.Steering = InputManager.Input(InputManager.Turn);
            // Dirft
        _wheelController.Drift = InputManager.isActive(InputManager.Drift) && (_wheelController.Speed > 20) || _wheelController.Handbrake;
            // Jump
        _wheelController.jumping = InputManager.isActive(InputManager.Jump);
    }
}
