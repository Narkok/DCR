using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceController: MonoBehaviour {

    [SerializeField] Transform _info;
    [SerializeField] Slider _hpSlider;
    [SerializeField] Text _nicknameLabel;

    [SerializeField] private ParticleSystem[] _boostParticles;

    private WeaponController _weaponController;
    private WheelController _wheelController;
    private BackLights _backLights;
    private Vehicle _vehicle;
    private bool isPlayer;


    void Start() {
        _vehicle = GetComponent<Vehicle>();
        _weaponController = GetComponent<WeaponController>();
        _wheelController = GetComponent<WheelController>();
        _backLights = GetComponentInChildren<BackLights>();
        isPlayer = _vehicle.ControlType.isPlayer();
        _info.gameObject.SetActive(!isPlayer);
    }


    void LateUpdate() {
        /// Каждый фрейм обновлять инфу - дикий костыль
        /// Надо нормально какие-нибудь делегаты организовать
        if (isPlayer) {
            GameCanvasManager.Shared.HP = _vehicle.HP / (float)_vehicle.MaxHP;
            GameCanvasManager.Shared.Nitro = _vehicle.Boost / VehicleExtention.MaxBoost;
            Weapon weapon = _weaponController.SelectedWeapon;
            GameCanvasManager.Shared.WeaponInfo = weapon == null ? "" : (weapon.Type.Name() + ": " + weapon.AmmoCount);
        }
        else {
            _hpSlider.value = _vehicle.HP / (float)_vehicle.MaxHP;
            _nicknameLabel.text = _vehicle.Nickname;
        }

        bool isBoosting = (_wheelController.boosting) && (_vehicle.Boost > 0);
        foreach (ParticleSystem ps in _boostParticles) {
            if (isBoosting) {
                if (!ps.isPlaying) ps.Play();
            } else {
                if (!ps.isStopped) ps.Stop();
            }
        }

        _backLights.isEnabled = backLightsEnabled();
    }


    private bool backLightsEnabled() {
        if (_wheelController.Throttle > 0) return false;
        if (_wheelController.Throttle < 0) return true;
        if (_wheelController.Handbrake) return true;
        return false;
    }
}
