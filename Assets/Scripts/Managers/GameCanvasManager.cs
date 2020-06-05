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
    [SerializeField] MinimapManager Minimap;


    private void Awake() {
        _shared = this;
    }

    /// Количество HP
    public float HP { set { HPSlider.value = value; } }

    /// Количество нитро
    public float Nitro { set { NitroSlider.value = value; } }

    /// Текст информации о текущем оружии
    public string WeaponInfo { set { WeaponInfoLabel.text = value; } }

    /// Вызывается после настройки SceneManager, поэтому все законно
    public void Setup() {
        Minimap.Setup();
    }
}
