using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameCanvasManager: MonoBehaviour {

    private static GameCanvasManager _shared;
    public static GameCanvasManager Shared { get { return _shared; } }

    [SerializeField] Slider HPSlider;
    [SerializeField] Slider NitroSlider;


    private void Awake() {
        _shared = this;
    }


    public void SetHP(float value) {
        HPSlider.value = value;
    }


    public void SetNitro(float value) {
        NitroSlider.value = value;
    }
}
