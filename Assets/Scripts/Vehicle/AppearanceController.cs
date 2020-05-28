using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AppearanceController: MonoBehaviour {

    [SerializeField] Transform _info;
    [SerializeField] Slider _hpSlider;
    [SerializeField] Text _nicknameLabel;

    private Vehicle _vehicle;
    private bool isPlayer;


    void Start() {
        _vehicle = GetComponent<Vehicle>();
        isPlayer = _vehicle.ControlType.isPlayer();
        _info.gameObject.SetActive(!isPlayer);
    }


    void Update() {
        if (isPlayer) return;
        _hpSlider.value = _vehicle.HP / (float)_vehicle.MaxHP;
        _nicknameLabel.text = _vehicle.Nickname;
    }
}
