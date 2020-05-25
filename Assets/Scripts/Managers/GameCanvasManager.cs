using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager: MonoBehaviour {

    private static GameCanvasManager _shared;
    public static GameCanvasManager Shared { get { return _shared; } }

    [SerializeField] Slider HPSlider;
    [SerializeField] Slider NitroSlider;
    [SerializeField] Text WeaponInfoLabel;


    private void Awake() {
        _shared = this;
    }

    public string WeaponInfo { set { WeaponInfoLabel.text = value; } }

    public float HP { set { HPSlider.value = value; } }

    public float Nitro { set { NitroSlider.value = value; } }


}
