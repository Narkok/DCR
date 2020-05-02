using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackLights: MonoBehaviour {

    private Color enabledColor = new Color(0.9f, 0, 0, 1);
    private Color disabledColor = new Color(0.2f, 0, 0, 1);

    private Material _material;
    private Renderer _renderer;


    private void Awake() {
        _renderer = GetComponent<Renderer>();
        _material = new Material(GetComponent<Renderer>().material);
        _renderer.material = _material;
    }


    public bool isEnabled {
        set {
            _material.SetColor("_EmissionColor", value ? enabledColor : disabledColor);
        }
    }
}
