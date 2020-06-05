using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MinimapManager: MonoBehaviour {
    
    private Image minimapImage;


    public void Setup() {
        minimapImage = GetComponent<Image>();
        minimapImage.sprite = Resources.Load<Sprite>(SceneManager.Shared.arenaType.MinimapPath());

        foreach (Vehicle vehicle in SceneManager.Shared.Vehicles) {
            VehiclePointer pointer = GOManager
                .Create("Minimaps/VehiclePointer", this.transform)
                .GetComponent<VehiclePointer>();
            
            pointer.minimapScale = minimapImage.rectTransform.rect.height / SceneManager.Shared.Arena.Size;
            vehicle.GetComponent<AppearanceController>().Set(pointer);
        }
    }
}
