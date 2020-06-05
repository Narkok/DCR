using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MinimapManager: MonoBehaviour {
    
    private Image minimapImage;

    public void Setup() {
        minimapImage = GetComponent<Image>();
        minimapImage.sprite = Resources.Load<Sprite>(SceneManager.Shared.arenaType.MinimapPath());
    }
    

    void Update() {
        
    }
}

