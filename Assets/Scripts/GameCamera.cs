using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameCamera: MonoBehaviour {

    /// Настройки
    // private bool keepVertical = false;
    private float mouseSensitivity = 2000;
    private int smoothTime = 15;
    private float smoothDelta = 0.02f;
    private float forwardOffset = 3;
    private float positionResetDelay = 1.7f;
    private float turnRotateSmooth = 0.15f;

    private float currentSmooth = 0f;
    private Coroutine positionResetCoroutine;
    private bool positionResetInProgress = false;

    private Transform _transform;
    private Transform _positionTarget;
    private Transform _lookAtTarget;
    private Transform _cameraTarget;
    private Transform _vehicleTransform;
    private WheelVehicle _vehicle;

    private Vector3 initialLookAtTargetPosition;
    private float localZRotation = 0;
    

    public void Set(Transform followVehicle) {
        _transform = transform;
        _vehicleTransform = followVehicle;
        _vehicle = _vehicleTransform.GetComponent<WheelVehicle>();
        _transform.position = followVehicle.position + followVehicle.up * 50;
        _lookAtTarget = followVehicle.GetComponentInChildren<CameraLookAtTarget>().transform;
        _cameraTarget = _lookAtTarget.parent;
        _positionTarget = followVehicle.GetComponentInChildren<CameraPositionTarget>().transform;
        initialLookAtTargetPosition = _lookAtTarget.localPosition;
        _transform.LookAt(_lookAtTarget);
    }


    void FixedUpdate() {
        /// Плавное приближение к машине
        if (currentSmooth < smoothTime) { 
            currentSmooth += smoothDelta; 
            if (currentSmooth < smoothTime / 5) {
                _transform.LookAt(_lookAtTarget);
                _transform.position = Vector3.Slerp(_transform.position, _positionTarget.position, currentSmooth * Time.fixedDeltaTime);
                return;
            }
        }

        /// Подворачивание камеры при повороте
        float turn = InputManager.Input(InputManager.Turn);
        float speedDelta = (_vehicle.Speed / _vehicle.maxSpeed);
        float camRot = turn * speedDelta * 60;
        _cameraTarget.localRotation = Quaternion.Slerp(_cameraTarget.localRotation, Quaternion.Euler(0, 0, -camRot), turnRotateSmooth * currentSmooth * Time.fixedDeltaTime);

        /// Поворот камеры вокруг машины
        float mouseX = InputManager.Input(InputManager.MouseX) / mouseSensitivity;
        localZRotation = (localZRotation + mouseX) % 360;
        float rotate = (180 - Mathf.Abs(Mathf.Abs(localZRotation) - 180)) / 180;
        
        float deltaZ = forwardOffset * (speedDelta * (1 - rotate));

        _lookAtTarget.Rotate(_lookAtTarget.up, mouseX, Space.World);
        _lookAtTarget.localPosition = initialLookAtTargetPosition + new Vector3(0, 0, deltaZ);

        Quaternion currentRotation = _transform.rotation;
        _transform.LookAt(_lookAtTarget);

        /// Доделать приколюху с сохранением вертикали камеры
        // if (keepVertical) {
        //     float a = Vector3.Angle(_transform.up, _lookAtTarget.right);
        //     float b = Vector3.Angle(_lookAtTarget.right, _transform.right);
        //     float angle = b <= 90 ? (90 - a) : (90 + a);
        //     Debug.Log(angle);
        //     _transform.Rotate(_transform.forward, angle, Space.World);
        // }

        Quaternion destinationRotation = _transform.rotation;

        /// Изменение положения камеры
        _transform.rotation = Quaternion.Slerp(currentRotation, destinationRotation, 5 * currentSmooth * Time.fixedDeltaTime);
        _transform.position = Vector3.Slerp(_transform.position, _positionTarget.position, currentSmooth * Time.fixedDeltaTime);

        /// Сброс поворота камеры
        if (mouseX != 0) {
            if (positionResetCoroutine != null) StopCoroutine(positionResetCoroutine);
            positionResetCoroutine = StartCoroutine(ResetPosition());
            positionResetInProgress = false;
        }
        if (positionResetInProgress) {
            _lookAtTarget.localRotation = Quaternion.Slerp(_lookAtTarget.localRotation, Quaternion.Euler(0, 0, 0), turnRotateSmooth * currentSmooth * Time.fixedDeltaTime);
            positionResetInProgress = _lookAtTarget.localRotation != Quaternion.Euler(0, 0, 0);
        }
    }


    private IEnumerator ResetPosition() {
        yield return new WaitForSeconds(positionResetDelay);
        positionResetInProgress = true;
    }


}
